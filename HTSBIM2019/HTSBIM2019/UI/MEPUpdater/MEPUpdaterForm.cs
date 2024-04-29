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

namespace HTSBIM2019.UI.MEPUpdater
{
    /// <summary>
    /// 1. MEP 사용 기록 관리 - Form
    /// </summary>
    public partial class MEPUpdaterForm : XtraForm// , IUpdater
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
        /// Revit 문서 내에 내장된 BuiltInCategory 정보 리스트
        /// </summary>
        private List<Category> CategoryList { get; set; } = new List<Category>();

        /// <summary>
        /// 객체 타입이 Geometry 유형 객체(GeometryElement)에 속하는 카테고리 정보 리스트
        /// </summary>
        // private List<CategoryInfoView> GeometryCategoryInfoList { get; set; } = new List<CategoryInfoView>();

        /// <summary>
        /// 객체 타입이 Geometry 유형 객체(GeometryElement)에 속하는 배관 카테고리 정보 리스트
        /// </summary>
        private List<CategoryInfoView> PipeCategoryInfoList { get; set; } = new List<CategoryInfoView>();

        /// <summary>
        /// 객체 타입이 Geometry 유형 객체(GeometryElement)에 속하는 전기/제어 카테고리 정보 리스트
        /// </summary>
        private List<CategoryInfoView> ElectricCategoryInfoList { get; set; } = new List<CategoryInfoView>();

        /// <summary>
        /// 객체 타입이 Geometry 유형 객체(GeometryElement)에 속하는 카테고리 정보 객체
        /// </summary>
        public CategoryInfoView CategoryInfo { get; set; }

        /// <summary>
        /// 객체 타입이 Geometry 유형 객체(GeometryElement)에 속하는 카테고리 정보 리스트
        /// </summary>
        public List<CategoryInfoView> CategoryInfoList { get; set; } = new List<CategoryInfoView>();

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
        public MEPUpdaterForm(ExternalEvent rvExEvent, MEPUpdaterRequestHandler pHandler, UIApplication rvUIApp)
        {
            InitializeComponent();

            // InitSetting(rvExEvent, pHandler, rvUIApp, rvAddInId);   // 업데이터 초기 셋팅
            InitSetting(rvExEvent, pHandler, rvUIApp);   // 업데이터 초기 셋팅
        }

        #endregion 생성자

        #region InitSetting

        /// <summary>
        /// 업데이터 초기 셋팅
        /// </summary>
        //private void InitSetting(ExternalEvent rvExEvent, MEPUpdaterRequestHandler pHandler, UIApplication rvUIApp, AddInId rvAddInId)
        private void InitSetting(ExternalEvent rvExEvent, MEPUpdaterRequestHandler pHandler, UIApplication rvUIApp)
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

                // 8. Revit 문서 내에 내장된 BuiltInCategory 정보 리스트로 가져오기 
                CategoryList = RevitDoc.Settings.Categories.OfType<Category>().ToList();


                // CategoryDataCreate(Collector, GeometryOpt);     // TreeView 컨트롤(comboBoxCategory)에 데이터 생성 및 출력 
                CategoryDataCreate(RevitDoc);                      // TreeView 컨트롤(comboBoxCategory)에 데이터 생성 및 출력 

                // 9. GUID 생성 
                // Guid guId = new Guid(HTSHelper.GId);

                // 10. 업데이터 아이디(Updater_Id) 객체 생성 
                // AppSetting.Default.UpdaterBase.MEPUpdater.Updater_Id = new UpdaterId(rvAddInId, guId);

                Log.Information(Logger.GetMethodPath(currentMethod) + "업데이터 초기 셋팅 완료");
            }
            catch (Exception ex)
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

        // TODO : TreeView 컨트롤 (treeViewCategory) 체크박스 구현 및 체크박스 전체 선택 및 전체 해제 기능 구현 (2024.04.26 jbh)
        // 참고 URL - https://afsdzvcx123.tistory.com/entry/C-%EC%9C%88%ED%8F%BC-TreeView-%EC%BB%A8%ED%8A%B8%EB%A1%A4-CheckBox-%EB%A1%9C-%EB%B3%80%EA%B2%BD-%EB%B0%8F-%EC%B2%B4%ED%81%AC-%EC%9D%B4%EB%B2%A4%ED%8A%B8-%EC%84%A0%EC%96%B8%ED%95%98%EA%B8%B0

        // TODO : C# Winform(윈폼) 트리뷰 체크박스 더블클릭(체크 + 언체크) 체크 오류 버그 방지 해결책 구현 (2024.04.26 jbh)
        // 참고 URL - https://mintaku.tistory.com/33

        // TODO : TreeView 컨트롤 (treeViewCategory) 체크박스 구현 및 체크박스 전체 선택 및 전체 해제 기능 구현 (2024.04.26 jbh)
        // 참고 URL - https://stackoverflow.com/questions/44927977/un-check-behavior-in-c-sharp-treeview

        // TODO : TreeView 컨트롤(treeViewCategory)에 카테고리 이름 데이터를 Node로 바인딩 할 수 있도록 구현 (2024.04.19 jbh)
        // 참고 URL - https://www.csharpstudy.com/WinForms/WinForms-treeview.aspx
        // 유튜브 참고 URL - https://youtu.be/PNIZDLFPmXE?si=TXd8f6fvEWwQV08k

        /// <summary>
        /// TreeView 컨트롤(treeViewCategory)에 데이터 생성 및 출력
        /// </summary>
        private void CategoryDataCreate(Document rvDoc)
        {
            var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

            try
            {
                Log.Information(Logger.GetMethodPath(currentMethod) + "카테고리 데이터 생성 시작");

                List<CategoryInfoView> categoryInfoList = CategoryManager.GetCategoryInfoList(rvDoc);

                // "일반 모델" - "OST_GenericModel", "기계 장비" - "OST_MechanicalEquipment"
                //var testList = categoryInfoList.Where(categoryInfo => categoryInfo.CategoryName.Equals("일반모델")
                //                                                   || categoryInfo.CategoryName.Equals("기계장비"))
                //                               .ToList();

                // "케이블 트레이" - OST_CableTray, "케이블 트레이 부속류" - OST_CableTrayFitting, "덕트" - OST_DuctCurves, "덕트 부속" - OST_DuctFitting,
                // "덕트 액세서리" - OST_DuctAccessory, "배관 밸브류" - OST_PipeAccessory, "일반 모델" - OST_GenericModel, "전기 설비" - OST_ElectricalFixtures,
                // "전기 시설물" - OST_ElectricalEquipment, "전선관" - OST_Conduit, "전선관 부속류" - OST_ConduitFitting, "전화 장치" - OST_TelephoneDevices
                var testList = categoryInfoList.Where(categoryInfo => categoryInfo.CategoryName.Equals("케이블 트레이")
                                                                   || categoryInfo.CategoryName.Equals("케이블 트레이 부속류")
                                                                   || categoryInfo.CategoryName.Equals("덕트")
                                                                   || categoryInfo.CategoryName.Equals("덕트 부속")
                                                                   || categoryInfo.CategoryName.Equals("덕트 액세서리")
                                                                   // || categoryInfo.CategoryName.Equals("배관 밸브류")
                                                                   // || categoryInfo.CategoryName.Equals("일반 모델")
                                                                   || categoryInfo.CategoryName.Equals("전기 설비")
                                                                   || categoryInfo.CategoryName.Equals("전기 시설물")
                                                                   || categoryInfo.CategoryName.Equals("전선관")
                                                                   || categoryInfo.CategoryName.Equals("전선관 부속류")
                                                                   || categoryInfo.CategoryName.Equals("전화 장치"))
                                               .ToList();


                PipeCategoryInfoList.Clear();   // 배관 카테고리 정보 리스트 초기화

                // TODO : Revit 한글, 영문판 모두에서 카테고리 리스트를 가져올 수 있도록 필요한 카테고리 리스트를 Where 조건절로 추출할 때, 
                //        categoryInfo.CategoryName이 아닌 categoryInfo.Category로
                //        "배관 - OST_PipeCurves", "배관 단열재 - OST_PipeInsulations", "배관 부속류 - OST_PipeAccessory", "배관 밸브류 - OST_PipeFitting"
                //        "기계 장비" - "OST_MechanicalEquipment", "일반 모델" - "OST_GenericModel" 추출하기 (2024.04.23 jbh)
                PipeCategoryInfoList = categoryInfoList.Where(categoryInfo => categoryInfo.Category.Equals(BuiltInCategory.OST_PipeCurves)
                                                                           || categoryInfo.Category.Equals(BuiltInCategory.OST_PipeInsulations)
                                                                           || categoryInfo.Category.Equals(BuiltInCategory.OST_PipeFitting)
                                                                           || categoryInfo.Category.Equals(BuiltInCategory.OST_PipeAccessory)
                                                                           || categoryInfo.Category.Equals(BuiltInCategory.OST_MechanicalEquipment)
                                                                           || categoryInfo.Category.Equals(BuiltInCategory.OST_GenericModel))
                                                           // .OrderByDescending(categoryInfo => categoryInfo.CategoryName)
                                                           // .OrderByDescending(categoryInfo => categoryInfo.Category)
                                                       .OrderBy(categoryInfo => categoryInfo.CategoryName)
                                                       .ToList();

                ElectricCategoryInfoList.Clear();   // 전기/제어 카테고리 정보 리스트 초기화

                ElectricCategoryInfoList = categoryInfoList.Where(categoryInfo => categoryInfo.Category.Equals(BuiltInCategory.OST_CableTray)
                                                                               || categoryInfo.Category.Equals(BuiltInCategory.OST_CableTrayFitting)
                                                                               || categoryInfo.Category.Equals(BuiltInCategory.OST_DuctCurves)
                                                                               || categoryInfo.Category.Equals(BuiltInCategory.OST_DuctFitting)
                                                                               || categoryInfo.Category.Equals(BuiltInCategory.OST_DuctAccessory)
                                                                               || categoryInfo.Category.Equals(BuiltInCategory.OST_ElectricalFixtures)
                                                                               || categoryInfo.Category.Equals(BuiltInCategory.OST_ElectricalEquipment)
                                                                               || categoryInfo.Category.Equals(BuiltInCategory.OST_Conduit)
                                                                               || categoryInfo.Category.Equals(BuiltInCategory.OST_ConduitFitting)
                                                                               || categoryInfo.Category.Equals(BuiltInCategory.OST_TelephoneDevices))
                                                            .OrderBy(categoryInfo => categoryInfo.CategoryName)
                                                            .ToList();


                // 상위 카테고리 ("배관", "전기/제어") 추가
                this.treeViewCategory.Nodes.Add(HTSHelper.배관);
                this.treeViewCategory.Nodes.Add(HTSHelper.전기제어);

                // 상위 카테고리("배관")에 속하는 하위 카테고리 추가 
                PipeCategoryInfoList.ForEach(categoryInfo => { 
                                                this.treeViewCategory.Nodes[(int)EnumMainCategoryInfo.PIPE].Nodes.Add(categoryInfo.CategoryName);
                                    });

                // 상위 카테고리("전기/제어")에 속하는 하위 카테고리 추가 
                ElectricCategoryInfoList.ForEach(categoryInfo => {
                                                    this.treeViewCategory.Nodes[(int)EnumMainCategoryInfo.ELECTRIC_CONTROL].Nodes.Add(categoryInfo.CategoryName);
                                        });

                this.treeViewCategory.CheckBoxes = true;   // TreeView 컨트롤(treeViewCategory)에 속한 모든 노드들을 체크박스로 변경 

                Log.Information(Logger.GetMethodPath(currentMethod) + "카테고리 데이터 생성 완료");

                // TODO : 아래 테스트 코드 필요시 참고 (2024.04.25 jbh)
                // TODO : TreeView (treeViewCategory)에 바인딩할 리스트에 데이터 할당 구현 (2024.03.26 jbh)
                //GeometryCategoryInfoList.Clear();   // 카테고리 정보 리스트 초기화
                // CategoryInfoList = CategoryManager.GetCategoryInfoList(rvCollector, rvGeometryOpt);
                //GeometryCategoryInfoList = categoryInfoList.Where(categoryInfo => categoryInfo.CategoryName.Equals(HTSHelper.배관)
                //                                                               || categoryInfo.CategoryName.Equals(HTSHelper.배관단열재)
                //                                                               || categoryInfo.CategoryName.Equals(HTSHelper.배관부속류)
                //                                                               || categoryInfo.CategoryName.Equals(HTSHelper.배관밸브류))
                //                                           .ToList();

                // TODO : Revit 한글, 영문판 모두에서 카테고리 리스트를 가져올 수 있도록 필요한 카테고리 리스트를 Where 조건절로 추출할 때, 
                //        categoryInfo.CategoryName이 아닌 categoryInfo.Category로
                //        "배관 - OST_PipeCurves", "배관 단열재 - OST_PipeInsulations", "배관 부속류 - OST_PipeAccessory", "배관 밸브류 - OST_PipeFitting"
                //        "기계 장비" - "OST_MechanicalEquipment", "일반 모델" - "OST_GenericModel" 추출하기 (2024.04.23 jbh)
                // GeometryCategoryInfoList = categoryInfoList.Where(categoryInfo => categoryInfo.Category.Equals(BuiltInCategory.OST_PipeCurves)
                //                                                                || categoryInfo.Category.Equals(BuiltInCategory.OST_PipeInsulations)
                //                                                                || categoryInfo.Category.Equals(BuiltInCategory.OST_PipeFitting)
                //                                                                || categoryInfo.Category.Equals(BuiltInCategory.OST_PipeAccessory)
                //                                                                || categoryInfo.Category.Equals(BuiltInCategory.OST_MechanicalEquipment)
                //                                                                || categoryInfo.Category.Equals(BuiltInCategory.OST_GenericModel))
                //                                            // .OrderByDescending(categoryInfo => categoryInfo.CategoryName)
                //                                            // .OrderByDescending(categoryInfo => categoryInfo.Category)
                //                                            .OrderBy(categoryInfo => categoryInfo.CategoryName)
                //                                            .ToList();


                // GeometryCategoryInfoList.ForEach(CategoryInfo => { 
                //                             this.treeViewCategory.Nodes.Add(CategoryInfo.CategoryName);
                //                         });
            }
            catch (Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                throw;   // 오류 발생시 상위 호출자 예외처리 전달
            }
        }

        /// <summary>
        /// ComboBox 컨트롤(comboBoxCategory)에 데이터 생성 및 출력 
        /// </summary>
        // private void CategoryDataCreate(FilteredElementCollector rvCollector, Options rvGeometryOpt)
        //private void CategoryDataCreate(Document rvDoc)
        //{
        //    var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

        //    try
        //    {
        //        List<CategoryInfoView> categoryInfoList = CategoryManager.GetCategoryInfoList(rvDoc);

        //        // TODO : ComboBox(comboBoxCategory)에 바인딩할 리스트에 데이터 할당 구현 (2024.03.26 jbh)
        //        GeometryCategoryInfoList.Clear();   // 카테고리 정보 리스트 초기화
        //        // CategoryInfoList = CategoryManager.GetCategoryInfoList(rvCollector, rvGeometryOpt);
        //        GeometryCategoryInfoList = categoryInfoList.Where(categoryInfo => categoryInfo.CategoryName.Equals(HTSHelper.배관)
        //                                                                       || categoryInfo.CategoryName.Equals(HTSHelper.배관단열재)
        //                                                                       || categoryInfo.CategoryName.Equals(HTSHelper.배관부속류)
        //                                                                       || categoryInfo.CategoryName.Equals(HTSHelper.배관밸브류))
        //                                                   .ToList();


        //        this.comboBoxCategory.DataSource    = GeometryCategoryInfoList;
        //        this.comboBoxCategory.ValueMember   = HTSHelper.Category;
        //        this.comboBoxCategory.DisplayMember = HTSHelper.CategoryName;

        //        // TODO : ComboBox(comboBoxCategory)안에 있는 텍스트를 수정 못 하도록 "ComboBoxStyle.DropDownList"로 설정 (2024.03.26 jbh)
        //        // 참고 URL - https://milkoon1.tistory.com/73
        //        this.comboBoxCategory.DropDownStyle = ComboBoxStyle.DropDownList;
        //        this.comboBoxCategory.SelectedIndex = (int)EnumCategoryInfo.OST_PIPE_CURVES;

        //        this.comboBoxCategory.Refresh();   // 변경 사항 반영 하도록 comboBoxCategory 컨트롤 Refresh
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
        //        throw;   // 오류 발생시 상위 호출자 예외처리 전달
        //    }
        //}

        #endregion CategoryDataCreate

        #region MEPUpdater_FormClosed

        /// <summary>
        /// Revit 업데이터 종료 이벤트 
        /// </summary>
        private void MEPUpdater_FormClosed(object sender, FormClosedEventArgs e)
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
            catch (Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
            }
        }

        #endregion MEPUpdater_FormClosed

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
            var currentMethod = MethodBase.GetCurrentMethod();              // 로그 기록시 현재 실행 중인 메서드 위치 기록

            try
            {
                TaskDialog.Show(HTSHelper.NoticeTitle, "테스트 진행 중...");

                // var testCheckedList = testList.Where(treeNode => true == treeNode.Checked).ToList();
                // var testCheckedList = this.treeViewCategory.Nodes.OfType<TreeNode>()
                //                                                  .Where(treeNode => true == treeNode.Checked)
                //                                                  .ToList();

                // TODO : Linq 확장 메서드 "SelectMany" 사용해서 2차원 카테고리 리스트에서 실제로 체크(선택)한 카테고리만 모아서 1차원 리스트로 변환하기 (2024.04.25 jbh)
                // 참고 URL - https://chat.openai.com/c/d68dec9b-494a-43cf-8925-081d23d11c93
                var testCheckedList = this.treeViewCategory.Nodes.OfType<TreeNode>()
                                                                 .SelectMany(mainCategory => mainCategory.Nodes.OfType<TreeNode>())
                                                                 // .Select(mainCategory => mainCategory.Nodes.OfType<TreeNode>())
                                                                 .Where(subCategory => subCategory.Checked)   // 상위 카테고리 하위의 자식 카테고리 중 체크한 카테고리만 추출
                                                                 .ToList();

                // 카테고리 체크한 경우 
                if (testCheckedList.Count >= (int)EnumCheckedCategoryInfo.EXIST)
                {
                    //AppSetting.Default.UpdaterBase.MEPUpdater.CategoryInfoList.Clear();
                    // AppSetting.Default.UpdaterBase.MEPUpdater.CategoryInfoList = new List<CategoryInfoView>();

                    CategoryInfoList.Clear();

                    foreach (TreeNode checkedNode in testCheckedList)
                    {
                        string chkCategoryName = checkedNode.Text;   // TreeView 체크박스에서 체크한 카테고리 이름 

                        // BuiltInCategory chkBuiltInCategory = CategoryManager.GetCategory(RevitDoc, chkCategoryName);    // TreeView 체크박스에서 체크한 카테고리(BuiltInCategory)
                        BuiltInCategory chkBuiltInCategory = CategoryManager.GetCategory(CategoryList, chkCategoryName);    // TreeView 체크박스에서 체크한 카테고리(BuiltInCategory)

                        CategoryInfoView categoryInfo = new CategoryInfoView(chkCategoryName, chkBuiltInCategory);   // CategoryInfoView 클래스 객체 categoryInfo 생성

                        //AppSetting.Default.UpdaterBase.MEPUpdater.CategoryInfoList.Add(categoryInfo);  // 소스파일 "MEPUpdater.cs"에 존재하는  -> CategoryInfoView 클래스 리스트 객체 CategoryInfoList 데이터 추가 
                        CategoryInfoList.Add(categoryInfo);   // CategoryInfoView 클래스 리스트 객체 CategoryInfoList 데이터 추가
                    }

                    MakeRequest(EnumMEPUpdaterRequestId.REGISTER);
                }
                else TaskDialog.Show(HTSHelper.NoticeTitle, "카테고리 체크 하시기 바랍니다.");



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
            catch (Exception ex)
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
                // TaskDialog.Show("Revit MEPUpdater", "업데이터 + Triggers 등록 테스트 진행 중...");

                // 업데이터 + Triggers 등록 대기처리 화면 출력
                AppSetting.Default.UpdaterBase.UpdaterLoadingForm.ShowLoadingForm();

                // 체크한 카테고리 목록 리스트로 변환
                // var checkedCategoryList = this.treeViewCategory.Nodes.OfType<TreeNode>()
                //                                                      .Where(treeNode => true == treeNode.Checked)
                //                                                      .ToList();

                // TODO : Linq 확장 메서드 "SelectMany" 사용해서 2차원 카테고리 리스트에서 실제로 체크(선택)한 카테고리만 모아서 1차원 리스트로 변환하기 (2024.04.25 jbh)
                // 참고 URL - https://chat.openai.com/c/d68dec9b-494a-43cf-8925-081d23d11c93
                // var testList = this.treeViewCategory.Nodes.OfType<TreeNode>()
                //                                           .SelectMany(mainCategory => mainCategory.Nodes.OfType<TreeNode>())
                //                                           // .Select(mainCategory => mainCategory.Nodes.OfType<TreeNode>())
                //                                           .Where(subCategory => subCategory.Checked)
                //                                           .ToList();

                // TODO : Linq 확장 메서드 "SelectMany" 사용해서 2차원 카테고리 리스트에서 실제로 체크(선택)한 카테고리만 모아서 1차원 리스트로 변환하기 (2024.04.25 jbh)
                // 참고 URL - https://chat.openai.com/c/d68dec9b-494a-43cf-8925-081d23d11c93
                var checkedCategoryList = this.treeViewCategory.Nodes.OfType<TreeNode>()
                                                                     .SelectMany(mainCategory => mainCategory.Nodes.OfType<TreeNode>())
                                                                     // .Select(mainCategory => mainCategory.Nodes.OfType<TreeNode>())
                                                                     .Where(subCategory => subCategory.Checked)   // 상위 카테고리 하위의 자식 카테고리 중 체크한 카테고리만 추출
                                                                     .ToList();

                // 카테고리 체크한 경우 
                if (checkedCategoryList.Count >= (int)EnumCheckedCategoryInfo.EXIST)
                {
                    // AppSetting.Default.UpdaterBase.MEPUpdater.CategoryInfoList.Clear();
                    // AppSetting.Default.UpdaterBase.MEPUpdater.CategoryInfoList = new List<CategoryInfoView>();
                    CategoryInfoList.Clear();

                    foreach (TreeNode checkedNode in checkedCategoryList)
                    {
                        string chkCategoryName = checkedNode.Text;   // TreeView 체크박스에서 체크한 카테고리 이름 

                        // BuiltInCategory chkBuiltInCategory = CategoryManager.GetCategory(RevitDoc, chkCategoryName);    // TreeView 체크박스에서 체크한 카테고리(BuiltInCategory)
                        BuiltInCategory chkBuiltInCategory = CategoryManager.GetCategory(CategoryList, chkCategoryName);    // TreeView 체크박스에서 체크한 카테고리(BuiltInCategory)

                        CategoryInfoView categoryInfo = new CategoryInfoView(chkCategoryName, chkBuiltInCategory);   // CategoryInfoView 클래스 객체 categoryInfo 생성

                        // AppSetting.Default.UpdaterBase.MEPUpdater.CategoryInfoList.Add(categoryInfo);  // 소스파일 "MEPUpdater.cs"에 존재하는  -> CategoryInfoView 클래스 리스트 객체 CategoryInfoList 데이터 추가 
                        CategoryInfoList.Add(categoryInfo);   // CategoryInfoView 클래스 리스트 객체 CategoryInfoList 데이터 추가 
                    }

                    // List<Updater_Parameters> updaterParamList = AppSetting.Default.UpdaterBase.MEPUpdater.UpdaterParamList;

                    //string language = RevitDoc.GetUnits().GetFormatOptions(UnitType.UT_Length).DisplayUnits.ToString();

                    MakeRequest(EnumMEPUpdaterRequestId.REGISTER);
                }
                else TaskDialog.Show(HTSHelper.NoticeTitle, "카테고리 체크 하시기 바랍니다.");

                // 카테고리 정보 RequestHandler.cs 소스파일로 넘겨서 업데이터 + Triggers 등록 및 해제 구현하기 
                // var categoryInfo = this.comboBoxCategory.SelectedItem as CategoryInfoView;
                // CategoryInfo = new CategoryInfoView(categoryInfo.CategoryName, categoryInfo.Category);

                //if(categoryInfo is not null)
                //{
                //    AppSetting.Default.UpdaterBase.MEPUpdater.CategoryInfo = new CategoryInfoView(categoryInfo.CategoryName, categoryInfo.Category);

                //    List<Updater_Parameters> updaterParamList = AppSetting.Default.UpdaterBase.MEPUpdater.UpdaterParamList;

                //    // TODO : 매개변수 4가지("객체 생성 날짜", "최종 수정 날짜", "객체 생성자", "최종 수정자") 생성 로직 추가하기 (2024.04.02 jbh)
                //    // 주의사항 - 생성한 매개변수에 매핑된 데이터 값을 사용자가 화면에서 수정하지 못하도록 설정 구현 
                //    // 프로퍼티 "UserModifiable" 설명
                //    // 사용자가 이 매개변수의 값을 수정할 수 있는지 여부를 나타냅니다.
                //    // 참고 URL - https://www.revitapidocs.com/2018/c0343d88-ea6f-f718-2828-7970c15e4a9e.htm
                //    CategoryManager.CreateCategorySet(RevitDoc, updaterParamList);

                //    MakeRequest(EnumMEPUpdaterRequestId.REGISTER);
                //}
                //else TaskDialog.Show(HTSHelper.NoticeTitle, "카테고리 체크 하시기 바랍니다.");
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

        #region treeViewCategory_AfterCheck

        // TODO : TreeView 컨트롤("treeViewCategory")에 속하는 상위 카테고리 ("배관", "전기/제어") 클릭시
        //        체크박스 전체 선택 또는 전체 해제 기능 메서드 "treeViewCategory_AfterCheck", "CheckAllChildNodes" 구현 (2024.04.25 jbh)
        // 참고 URL - https://mintaku.tistory.com/34
        // 참고 2 URL - https://hvyair.tistory.com/50
        // 참고 3 URL - https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.treeview.aftercheck?redirectedfrom=MSDN&view=windowsdesktop-6.0

        /// <summary>
        /// 상위 카테고리("배관", "전기/제어") 클릭시 체크박스 전체 선택 또는 전체 해제 
        /// </summary>
        private void treeViewCategory_AfterCheck(object sender, TreeViewEventArgs e)
        {
            var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

            try
            {
                // The code only executes if the user caused the checked state to change.
                // 사용자가 카테고리 체크박스를 체크(선택)하여 상태가 변한 경우 
                if(e.Action != TreeViewAction.Unknown)
                {
                    // 체크박스를 체크(선택)한 카테고리가 존재하는 경우 (상위, 하위 카테고리 포함)
                    // if(e.Node.Nodes.Count > 0)
                    if (e.Node.Nodes.Count >= (int)EnumCheckedCategoryInfo.EXIST) 
                    {
                        /* Calls the CheckAllChildNodes method, passing in the current 
                        Checked value of the TreeNode whose checked state changed. */
                        this.CheckAllChildNodes(e.Node, e.Node.Checked);   // 메서드 "CheckAllChildNodes" 호출 
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                TaskDialog.Show(HTSHelper.ErrorTitle, ex.Message);
            }
            finally
            {
                // TODO : finally문 안에 코드 필요시 구현 예정 (2024.03.14 jbh)
            }
        }

        #endregion treeViewCategory_AfterCheck

        #region CheckAllChildNodes

        // TODO : TreeView 컨트롤("treeViewCategory")에 속하는 상위 카테고리 ("배관", "전기/제어") 클릭시 전체 선택 또는 전체 해제 기능 메서드 "treeViewCategory_AfterCheck", "CheckAllChildNodes" 구현 (2024.04.25 jbh)
        // 참고 URL - https://mintaku.tistory.com/34
        // 참고 2 URL - https://hvyair.tistory.com/50
        // 참고 3 URL - https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.treeview.aftercheck?redirectedfrom=MSDN&view=windowsdesktop-6.0

        /// <summary>
        /// 상위 카테고리("배관", "전기/제어") 하위에 존재하는 모든 카테고리 재귀적으로 체크박스 선택 또는 해제 
        /// </summary>
        private void CheckAllChildNodes(TreeNode pTreeNode, bool pNodeChecked)
        {
            var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

            try
            {
                foreach(TreeNode node in pTreeNode.Nodes)
                {
                    node.Checked = pNodeChecked;

                    // 체크박스를 체크(선택)한 카테고리가 존재하는 경우 (상위, 하위 카테고리 포함)
                    // if (node.Nodes.Count > 0)
                    if (node.Nodes.Count >= (int)EnumCheckedCategoryInfo.EXIST) 
                    {
                        // If the current node has child nodes, call the CheckAllChildsNodes method recursively.
                        // 현재 체크(선택)한 카테고리 안에 하위 카테고리가 존재하는 경우 메서드 "CheckAllChildNodes" 재귀 호출 
                        this.CheckAllChildNodes(node, pNodeChecked);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                throw;   // 오류 발생시 상위 호출자 예외처리 전달
            }
        }

        #endregion CheckAllChildNodes

        #region Sample

        #endregion Sample
    }
}