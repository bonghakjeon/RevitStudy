using Serilog;

using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;

using HTSBIM2019.Settings;
using HTSBIM2019.Common.HTSBase;
using HTSBIM2019.Common.LogBase;
using HTSBIM2019.Common.Managers;
using HTSBIM2019.Common.RequestBase;
using HTSBIM2019.Models.HTSBase.MEPUpdater;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.ApplicationServices;

using DevExpress.XtraEditors;

namespace HTSBIM2019.UI.Test.MEPUpdater
{
    /// <summary>
    /// 1. MEP 사용 기록 관리 - Form
    /// </summary>
    public partial class TestMEPUpdaterForm : XtraForm// , IUpdater
    {
        #region 프로퍼티

        // TODO : Revit 응용프로그램 객체 프로퍼티 "RevitUIApp" 필요시 사용 예정 (2024.04.05 jbh)
        /// <summary>
        /// Revit 응용프로그램 객체
        /// </summary>
        // private UIApplication RevitUIApp { get; set; }


        // TODO : 폼 화면(CopyParams.cs) 출력시 기존처럼 .ShowDialog(Modal - 부모 창 제어 X)하는 방식이 아니라
        //        .Show(Modaless - 부모 창 제어 O)으로 하는 방식으로 수정해야 하면
        //        부모창 제어가 가능하므로 사용자가 Revit 문서가 1개가 아니라 여러 개를 열어서 작업할 수 있으므로
        //        아래처럼 프로퍼티 "RevitDoc"로 구현하면 안 되고,
        //        Command.cs에서 Revit 문서 여러 개를 인자로 한 번에 받도록 구현 해야함. (2024.03.14 jbh) 
        // Modal VS Modaless 차이
        // 참고 URL   - https://blog.naver.com/PostView.naver?blogId=wlsdml1103&logNo=220512538948
        // 참고 2 URL - https://greensul.tistory.com/37
        /// <summary>
        /// Revit 문서 
        /// </summary>
        private Document RevitDoc { get; set; }

        /// <summary>
        /// Revit 문서 안에 포함된 객체(Element)를 검색 및 필터링
        /// 참고 URL - https://www.revitapidocs.com/2024/263cf06b-98be-6f91-c4da-fb47d01688f3.htm
        /// </summary>
        private FilteredElementCollector Collector { get; set; }

        /// <summary>
        /// Geometry Element Options
        /// </summary>
        private Options GeometryOpt { get; set; }

        /// <summary>
        /// Revit 애플리케이션 언어 타입(영문, 한글 등등...)
        /// </summary>
        private LanguageType RevitLanguageType { get; set; }

        /// <summary>
        /// 객체 타입이 Geometry 유형 객체(GeometryElement)에 속하는 카테고리 정보 리스트
        /// </summary>
        private List<CategoryInfoView> GeometryCategoryInfoList { get; set; } = new List<CategoryInfoView>();

        /// <summary>
        /// 객체 타입이 Geometry 유형 객체(GeometryElement)에 속하는 카테고리 정보 객체
        /// </summary>
        public CategoryInfoView CategoryInfo { get; set; }

        /// <summary>
        /// Modaless 폼(.Show()) 형식에 의해 발생하는 외부 요청 핸들러 프로퍼티 
        /// </summary>
        private MEPUpdaterRequestHandler RequestHandler { get; set; }

        /// <summary>
        /// 외부 이벤트 프로퍼티
        /// </summary>
        private ExternalEvent ExEvent { get; set; }

        #endregion 프로퍼티

        #region 생성자

        //public MEPUpdaterForm(ExternalEvent rvExEvent, MEPUpdaterRequestHandler pHandler, UIApplication rvUIApp, AddInId rvAddInId)
        public TestMEPUpdaterForm(ExternalEvent rvExEvent, MEPUpdaterRequestHandler pHandler, UIApplication rvUIApp, LanguageType rvLanguageType)
        {
            InitializeComponent();

            // InitSetting(rvExEvent, pHandler, rvUIApp, rvAddInId);   // 업데이터 초기 셋팅
            InitSetting(rvExEvent, pHandler, rvUIApp, rvLanguageType);   // 업데이터 초기 셋팅
        }

        #endregion 생성자

        #region InitSetting

        /// <summary>
        /// 업데이터 초기 셋팅
        /// </summary>
        //private void InitSetting(ExternalEvent rvExEvent, MEPUpdaterRequestHandler pHandler, UIApplication rvUIApp, AddInId rvAddInId)
        private void InitSetting(ExternalEvent rvExEvent, MEPUpdaterRequestHandler pHandler, UIApplication rvUIApp, LanguageType rvLanguageType)
        {
            var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

            try
            {
                Log.Information(Logger.GetMethodPath(currentMethod) + "업데이터 초기 셋팅 시작");

                // TODO : 화면 상단 바 버튼 "최소화", "최대화" 비활성화 구현 (2024.03.14 jbh)
                // 참고 URL - https://zelits.tistory.com/690
                // this.MinimizeBox = false;                            // 버튼 "최소화" 비활성화
                // this.MaximizeBox = false;                            // 버튼 "최대화" 비활성화

                // Behind Code "MEPUpdater.cs"에서 폼 화면(MEPUpdater) Width, Height 고정 
                // 참고 URL - https://kdsoft-zeros.tistory.com/3
                // this.FormBorderStyle = FormBorderStyle.FixedSingle;

                // TextBox (또는 TextEdit) 컨트롤 "textParamName" 높이 조절하기 위해 속성 Properties.AutoHeight = false; 설정 
                // 주의사항 - Properties.AutoHeight = true; 설정시 TextEdit 높이 조절 불가 
                // 참고 URL - https://supportcenter.devexpress.com/ticket/details/q278069/sizing-the-text-area-of-a-textedit-control
                // this.textParamName.Properties.AutoHeight = false;

                // 1. 외부 이벤트 프로퍼티 할당 
                ExEvent = rvExEvent;

                // 2. 외부 요청 핸들러 프로퍼티 할당 
                RequestHandler = pHandler;

                // TODO : 아래 주석친 프로퍼티 "RevitUIApp" 필요시 사용 예정 (2024.04.05 jbh)
                // 3. Revit 응용프로그램 객체 프로퍼티에 전달 받은 Revit 객체(rvUIApp) 할당
                // RevitUIApp = rvUIApp;

                // 4. Revit 문서 프로퍼티 "RevitDoc" 할당 
                RevitDoc = rvUIApp.ActiveUIDocument.Document;    // 활성화된 Revit 문서

                

                // 5. Revit 문서 안에 포함된 객체(Element)를 검색 및 필터링
                Collector = new FilteredElementCollector(RevitDoc).WhereElementIsNotElementType();

                // 6. Revit 문서 프로퍼티(RevitDoc) 안에 포함된 객체(Element) 목록을 리스트 프로퍼티 "ElementList" 에 할당 
                // TODO : ElementList 초기화(Clear) 필요시 사용 예정 (2024.03.25 jbh)
                // ElementList.Clear();  
                // ElementList = Collector.ToList();


                // 7. GeometryOpt Element Options 프로퍼티 "GeometryOpt" 할당
                // 참고 URL - https://thebuildingcoder.typepad.com/blog/2016/04/stable-reference-string-magic-voodoo.html
                GeometryOpt = rvUIApp.Application.Create.NewGeometryOptions();
                GeometryOpt.DetailLevel = ViewDetailLevel.Fine;
                GeometryOpt.ComputeReferences = true;              // 각 Geometry 객체에 대해 GeometryObject.Reference 속성을 활성화하도록 Revit을 설정

                // 8. Revit 애플리케이션 언어 타입(영문, 한글 등등...) 할당 
                RevitLanguageType = rvLanguageType;

                // CategoryDataCreate(Collector, GeometryOpt);     // ComboBox 컨트롤(comboBoxCategory)에 데이터 생성 및 출력 
                CategoryDataCreate(RevitDoc);                      // ComboBox 컨트롤(comboBoxCategory)에 데이터 생성 및 출력 

                // 9. GUID 생성 
                // Guid guId = new Guid(HTSHelper.GId);

                // 10. 업데이터 아이디(Updater_Id) 객체 생성 
                // AppSetting.Default.UpdaterBase.MEPUpdater.Updater_Id = new UpdaterId(rvAddInId, guId);

                Log.Information(Logger.GetMethodPath(currentMethod) + "업데이터 초기 셋팅 완료");
            }
            catch(Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                TaskDialog.Show(HTSHelper.ErrorTitle, ex.Message);
            }
            finally
            {
                // TODO : finally문 안에 코드 필요시 구현 예정 (2024.03.14 jbh)
            }
        }

        #endregion InitSetting

        #region CategoryDataCreate

        /// <summary>
        /// ComboBox 컨트롤(comboBoxCategory)에 데이터 생성 및 출력 
        /// </summary>
        // private void CategoryDataCreate(FilteredElementCollector rvCollector, Options rvGeometryOpt)
        private void CategoryDataCreate(Document rvDoc)
        {
            var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

            try
            {
                List<CategoryInfoView> categoryInfoList = CategoryManager.GetCategoryInfoList(rvDoc);

                // TODO : ComboBox(comboBoxCategory)에 바인딩할 리스트에 데이터 할당 구현 (2024.03.26 jbh)
                GeometryCategoryInfoList.Clear();   // 카테고리 정보 리스트 초기화
                // CategoryInfoList = CategoryManager.GetCategoryInfoList(rvCollector, rvGeometryOpt);
                GeometryCategoryInfoList = categoryInfoList.Where(categoryInfo => categoryInfo.CategoryName.Equals(HTSHelper.배관)
                                                                               || categoryInfo.CategoryName.Equals(HTSHelper.배관단열재)
                                                                               || categoryInfo.CategoryName.Equals(HTSHelper.배관부속류)
                                                                               || categoryInfo.CategoryName.Equals(HTSHelper.배관밸브류))
                                                           .ToList();


                this.comboBoxCategory.DataSource    = GeometryCategoryInfoList;
                this.comboBoxCategory.ValueMember   = HTSHelper.Category;
                this.comboBoxCategory.DisplayMember = HTSHelper.CategoryName;

                // TODO : ComboBox(comboBoxCategory)안에 있는 텍스트를 수정 못 하도록 "ComboBoxStyle.DropDownList"로 설정 (2024.03.26 jbh)
                // 참고 URL - https://milkoon1.tistory.com/73
                this.comboBoxCategory.DropDownStyle = ComboBoxStyle.DropDownList;
                this.comboBoxCategory.SelectedIndex = (int)EnumCategoryInfo.OST_PIPE_CURVES;

                this.comboBoxCategory.Refresh();   // 변경 사항 반영 하도록 comboBoxCategory 컨트롤 Refresh
            }
            catch(Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                throw;   // 오류 발생시 상위 호출자 예외처리 전달
            }
        }

        #endregion CategoryDataCreate

        #region TestMEPUpdater_FormClosed

        /// <summary>
        /// Revit 업데이터 종료 이벤트 
        /// </summary>
        private void TestMEPUpdater_FormClosed(object sender, FormClosedEventArgs e)
        {
            var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

            try
            {
                Log.Information(Logger.GetMethodPath(currentMethod) + "업데이터 화면 종료 시작");

                // Revit Updater Modaless 폼(MEPUpdater) 화면 닫기 전에 외부 이벤트 프로퍼티 "ExEvent" 리소스 해제 
                ExEvent.Dispose();
                ExEvent = null;             // 외부 이벤트 null로 초기화
                RequestHandler = null;      // 외부 요청 핸들러 null로 초기화

                // base.OnFormClosed(e);    // 폼화면 닫기 (해당 메서드 base.OnFormClosed(e); 호출시 이벤트 메서드 MEPUpdater_FormClosed 두번 실행되서 오류 발생하므로 주석처리 진행)

                Log.Information(Logger.GetMethodPath(currentMethod) + "업데이터 화면 종료 완료");
            }
            catch(Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
            }
        }

        #endregion TestMEPUpdater_FormClosed

        #region MakeRequest

        /// <summary>
        /// 요청(Request) 생성
        /// </summary>
        private void MakeRequest(EnumMEPUpdaterRequestId pRequest)
        {
            RequestHandler.Request.Make(pRequest);
            ExEvent.Raise();
            // DozeOff(); // 주의사항 - MEPUpdater 폼 객체에 속한 모든 컨트롤 비활성화 메서드 호출시 화면의 버튼 기능("ON", "OFF")을 사용할 수 없으므로 해당 메서드는 호출 안 함.
        }

        #endregion MakeRequest

        #region DozeOff

        /// <summary>
        /// MEPUpdater 폼 객체에 속한 모든 컨트롤 비활성화
        /// </summary>
        private void DozeOff()
        {
            EnableCommands(false);
        }

        #endregion DozeOff

        #region WakeUp

        /// <summary>
        /// MEPUpdater 폼 객체에 속한 모든 컨트롤 활성화
        /// </summary>
        public void WakeUp()
        {
            EnableCommands(true);
        }

        #endregion WakeUp

        #region EnableCommands

        /// <summary>
        /// 폼 객체에 속한 모든 컨트롤 활성화 / 비활성화
        /// </summary>
        private void EnableCommands(bool pStatus)
        {
            foreach(System.Windows.Forms.Control ctrl in this.Controls)
            {
                ctrl.Enabled = pStatus;
            }

            // TODO : 아래 if절 로직 필요시 사용 예정 (2024.03.22 jbh)
            // if(false == pStatus)
            // {
            //     this.btnON.Enabled  = true;
            //     this.btnOFF.Enabled = true;
            // }
        }

        #endregion EnableCommands

        #region btnTest_Click

        /// <summary>
        /// 테스트 기능 이벤트 메서드
        /// </summary>
        private void btnTest_Click(object sender, EventArgs e)
        {
            var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

            try
            {
                TaskDialog.Show(HTSHelper.NoticeTitle, "테스트 진행 중...");

                // string dllParentDirPath = DirectoryManager.GetDllParentDirectoryPath(HTSHelper.AssemblyFilePath);

                // var paramList = ParamsManager.GetMEPUpdaterParameterList(dllParentDirPath);

                // var testCategoryList = CategoryManager.GetCategoryInfoList(RevitDoc); 

                // 카테고리 정보 RequestHandler.cs 소스파일로 넘겨서 업데이터 + Triggers 등록 및 해제 구현하기 
                // var categoryInfo = this.comboBoxCategory.SelectedItem as CategoryInfoView;

                // if(categoryInfo is not null) CategoryInfo = new CategoryInfoView(categoryInfo.CategoryName, categoryInfo.Category);
                // else throw new Exception("카테고리 정보 존재하지 않습니다.\r\n담당자에게 문의 하시기 바랍니다.");
                //CategoryInfo = this.comboBoxCategory.SelectedItem as CategoryInfoView; 

                // BuiltInCategory testCategory = CategoryManager.GetBuiltInCategory("배관");

                //BuiltInCategory testCategory = CategoryInfoList.Where(x => x.CategoryName.Equals("배관"))
                //                                               .Select(x => x.Category)
                //                                               .FirstOrDefault();


                // BuiltInCategory test2Category = CategoryManager.GetBuiltInCategory("OST_PipeFitting");

            }
            catch(Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                TaskDialog.Show(HTSHelper.ErrorTitle, ex.Message);
            }
            finally
            {
                // TODO : finally문 안에 코드 필요시 구현 예정 (2024.03.14 jbh)
            }
        }
        #endregion btnTest_Click

        #region btnON_Click

        /// <summary>
        /// 업데이터 + Triggers 등록 
        /// </summary>
        private void btnON_Click(object sender, EventArgs e)
        {
            var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

            try
            {
                TaskDialog.Show("Revit MEPUpdater", "업데이터 + Triggers 등록 테스트 진행 중...");

                // 카테고리 정보 RequestHandler.cs 소스파일로 넘겨서 업데이터 + Triggers 등록 및 해제 구현하기 
                var categoryInfo = this.comboBoxCategory.SelectedItem as CategoryInfoView;
                // CategoryInfo = new CategoryInfoView(categoryInfo.CategoryName, categoryInfo.Category);

                if(categoryInfo is not null)
                {
                    AppSetting.Default.UpdaterBase.MEPUpdater.CategoryInfo = new CategoryInfoView(categoryInfo.CategoryName, categoryInfo.Category);

                    // List<Updater_Parameters> updaterParamList = AppSetting.Default.UpdaterBase.MEPUpdater.UpdaterParamList;

                    MakeRequest(EnumMEPUpdaterRequestId.REGISTER);
                }
                else throw new Exception("카테고리 정보 존재하지 않습니다.\r\n담당자에게 문의 하시기 바랍니다.");
            }
            catch(Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                TaskDialog.Show(HTSHelper.ErrorTitle, ex.Message);
            }
            finally
            {
                // TODO : finally문 안에 코드 필요시 구현 예정 (2024.03.14 jbh)
            }
        }

        #endregion btnON_Click

        #region Sample

        #endregion Sample
    }
}