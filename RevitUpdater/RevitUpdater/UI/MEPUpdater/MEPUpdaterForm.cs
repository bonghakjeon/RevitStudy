using Serilog;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

using RevitUpdater.Common.LogBase;
using RevitUpdater.Common.Managers;
using RevitUpdater.Common.RequestBase;
using RevitUpdater.Common.UpdaterBase;
using RevitUpdater.Models.UpdaterBase.MEPUpdater;

using DevExpress.XtraEditors;

namespace RevitUpdater.UI.MEPUpdater
{
    public partial class MEPUpdaterForm : XtraForm, IUpdater
    {
        #region 프로퍼티

        // TODO : 필요시 프로퍼티 "dtSearchLookUpEdit" 사용 예정 (2024.03.26 jbh)
        /// <summary>
        /// 컨트롤 "searchLookUpEditCategory"에 바인딩 할 데이터 테이블 프로퍼티
        /// </summary>
        // private DataTable dtSearchLookUpEdit { get; set; } = new DataTable();

        /// <summary>
        /// Revit 응용프로그램 객체
        /// </summary>
        private UIApplication RevitUIApp { get; set; }


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

        // TODO : Revit 문서 안에 포함된 객체(Element) 리스트 프로퍼티 "ElementList" 필요시 사용 예정 (2024.03.26 jbh)
        /// <summary>
        /// Revit 문서 안에 포함된 객체(Element) 리스트 
        /// </summary>
        // private List<Element> ElementList { get; set; } = new List<Element>();

        /// <summary>
        /// Geometry Element Options
        /// </summary>
        private Options GeometryOpt { get; set; }

        /// <summary>
        /// 객체 타입이 Geometry 유형 객체(GeometryElement)에 속하는 카테고리 정보 리스트
        /// </summary>
        private List<CategoryInfoView> CategoryInfoList { get; set; } = new List<CategoryInfoView>();

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

        /// <summary>
        /// 업데이터 아이디 생성시 필요한 GUID 문자열 프로퍼티
        /// </summary>
        // private const string GId = "d42d28af-d2cd-4f07-8873-e7cfb61903d8";

        /// <summary>
        /// 업데이터 아이디
        /// </summary>
        // private UpdaterId Updater_Id { get; }
        private UpdaterId Updater_Id { get; set; }

        /// <summary>
        /// 카테고리 필터 (배관 - OST_PipeCurves)
        /// </summary>
        private ElementCategoryFilter PipeCurvesCategoryFilter { get; set; }


        /// <summary>
        /// 카테고리 필터 (배관 부속류 - OST_PipeFitting)
        /// </summary>
        private ElementCategoryFilter PipeFittingCategoryFilter { get; set; }


        /// <summary>
        /// 매개변수 값 입력 완료 여부 
        /// </summary>
        private bool IsCompleted { get; set; }

        #endregion 프로퍼티

        #region 생성자

        public MEPUpdaterForm(ExternalEvent rvExEvent, MEPUpdaterRequestHandler pHandler, UIApplication rvUIApp, AddInId rvAddInId)
        {
            InitializeComponent();

            InitSetting(rvExEvent, pHandler, rvUIApp, rvAddInId);   // 업데이터 초기 셋팅
        }

        #endregion 생성자

        #region InitSetting

        /// <summary>
        /// 업데이터 초기 셋팅
        /// </summary>
        private void InitSetting(ExternalEvent rvExEvent, MEPUpdaterRequestHandler pHandler, UIApplication rvUIApp, AddInId rvAddInId)
        {
            var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

            try
            {
                Log.Information(Logger.GetMethodPath(currentMethod) + "업데이터 초기 셋팅 시작");

                // TODO : 화면 상단 바 버튼 "최소화", "최대화" 비활성화 구현 (2024.03.14 jbh)
                // 참고 URL - https://zelits.tistory.com/690
                this.MinimizeBox = false;                            // 버튼 "최소화" 비활성화
                this.MaximizeBox = false;                            // 버튼 "최대화" 비활성화

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

                // 3. Revit 응용프로그램 객체 프로퍼티에 전달 받은 Revit 객체(rvUIApp) 할당
                RevitUIApp  = rvUIApp;

                // 4. Revit 문서 프로퍼티 "RevitDoc" 할당 
                RevitDoc    = rvUIApp.ActiveUIDocument.Document;    // 활성화된 Revit 문서 

                // 5. Revit 문서 안에 포함된 객체(Element)를 검색 및 필터링
                Collector   = new FilteredElementCollector(RevitDoc).WhereElementIsNotElementType();

                // 6. Revit 문서 프로퍼티(RevitDoc) 안에 포함된 객체(Element) 목록을 리스트 프로퍼티 "ElementList" 에 할당 
                // TODO : ElementList 초기화(Clear) 필요시 사용 예정 (2024.03.25 jbh)
                // ElementList.Clear();  
                // ElementList = Collector.ToList();


                // 7. GeometryOpt Element Options 프로퍼티 "GeometryOpt" 할당
                // 참고 URL - https://thebuildingcoder.typepad.com/blog/2016/04/stable-reference-string-magic-voodoo.html
                GeometryOpt = rvUIApp.Application.Create.NewGeometryOptions();
                GeometryOpt.DetailLevel       = ViewDetailLevel.Fine;
                GeometryOpt.ComputeReferences = true;           // 각 Geometry 객체에 대해 GeometryObject.Reference 속성을 활성화하도록 Revit을 설정


                CategoryDataCreate(Collector, GeometryOpt);   // LookUpEdit 컨트롤(searchLookUpEditCategory)에 데이터 생성 및 출력 

                // 8. GUID 생성 
                Guid guId   = new Guid(UpdaterHelper.GId);

                // 9. 업데이터 아이디(Updater_Id) 객체 생성 
                Updater_Id  = new UpdaterId(rvAddInId, guId);

                // 10. 매개변수 값 입력 완료 여부 false 초기화
                IsCompleted = false;

                // 11. 객체 "배관"(BuiltInCategory.OST_PipeCurves)만 필터링 처리 
                PipeCurvesCategoryFilter  = new ElementCategoryFilter(BuiltInCategory.OST_PipeCurves);

                // 12. 객체 "배관 부속류"(BuiltInCategory.OST_PipeFitting)만 필터링 처리 
                PipeFittingCategoryFilter = new ElementCategoryFilter(BuiltInCategory.OST_PipeFitting);

                Log.Information(Logger.GetMethodPath(currentMethod) + "업데이터 초기 셋팅 완료");
            }
            catch(Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                TaskDialog.Show(UpdaterHelper.ErrorTitle, ex.Message);
            }
            finally
            {
                // TODO : finally문 안에 코드 필요시 구현 예정 (2024.03.14 jbh)
            }
        }

        #endregion InitSetting

        #region CategoryDataCreate

        /// <summary>
        /// ComboBox와 비슷한 SearchLookUpEdit 컨트롤(searchLookUpEditCategory)에 데이터 생성 및 출력 
        /// </summary>
        //private void CategoryDataCreate(FilteredElementCollector rvCollector, List<Element> rvElementList, Options rvGeometryOpt)
        private void CategoryDataCreate(FilteredElementCollector rvCollector, Options rvGeometryOpt)
        {
            var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

            try
            {
                // TODO : ComboBox(comboBoxCategory)에 리스트 데이터 바인딩 구현 (2024.03.26 jbh)
                CategoryInfoList.Clear();   // 카테고리 정보 리스트 초기화
                CategoryInfoList = CategoryManager.GetCategoryInfoList(rvCollector, rvGeometryOpt);

                this.comboBoxCategory.DataSource    = CategoryInfoList;
                this.comboBoxCategory.ValueMember   = "category";
                this.comboBoxCategory.DisplayMember = "categoryName";

                // TODO : ComboBox(comboBoxCategory)안에 있는 텍스트를 수정 못 하도록 "ComboBoxStyle.DropDownList"로 설정 (2024.03.26 jbh)
                // 참고 URL - https://milkoon1.tistory.com/73
                this.comboBoxCategory.DropDownStyle = ComboBoxStyle.DropDownList;
                this.comboBoxCategory.SelectedIndex = -1;

                this.comboBoxCategory.Refresh();   // 변경 사항 반영 하도록 comboBoxCategory 컨트롤 Refresh
            }
            catch(Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                throw;   // 오류 발생시 상위 호출자 예외처리 전달
            }
        }

        #endregion CategoryDataCreate

        #region GetCategoryData

        /// <summary>
        /// SearchLookUpEdit(콤보박스 + 데이터 검색 기능)에 Geometry 유형 객체(GeometryElement)에 속하는 카테고리 데이터(DataTable) 가져오기 
        /// </summary>
        //private DataTable GetCategoryData(FilteredElementCollector rvCollector, Options rvGeometryOpt)
        //{
        //    var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

        //    try
        //    {
        //        // 1 단계 : 객체 타입이 Geometry 유형 객체(GeometryElement)에 속하는 카테고리 정보 리스트로 가져오기
        //        //CategoryInfoList.Clear();
        //        //CategoryInfoList = CategoryManager.GetCategoryInfoList(rvCollector, rvGeometryOpt);

        //        // 2 단계 : DataTable 클래스 프로퍼티 "dtSearchLookUpEdit"에 컬럼 구현 
        //        //dtSearchLookUpEdit.Columns.Add(UpdaterHelper.CategoryName);
        //        //dtSearchLookUpEdit.Columns.Add(UpdaterHelper.CategoryType);

        //        // 3 단계 : DataTable 클래스 프로퍼티 "dtSearchLookUpEdit"에 Caption 구현 
        //        // dtSearchLookUpEdit.Columns[UpdaterHelper.CategoryName].Caption = UpdaterHelper.CategoryNameCaption;   // 필드명 "카테고리 이름" 변경
        //        // dtSearchLookUpEdit.Columns[UpdaterHelper.CategoryType].Caption = UpdaterHelper.CategoryTypeCaption;   // 필드명 "카테고리 타입" 변경

        //        // 4 단계 : DataTable 클래스 객체 dtSearchLookUpEdit의 DataRow에 리스트 객체 "categoryInfoList"에 저장된(Geometry 유형 객체에 포함된) 카테고리 정보 데이터 추가 
        //        // foreach(CategoryInfoView categoryInfo in CategoryInfoList)
        //        //     dtSearchLookUpEdit.Rows.Add(categoryInfo.categoryName, categoryInfo.category);

        //        // return dtSearchLookUpEdit;
        //    }
        //    catch(Exception ex)
        //    {
        //        Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
        //        throw;   // 오류 발생시 상위 호출자 예외처리 전달
        //    }
        //}

        #endregion GetCategoryData

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
                ExEvent = null;   // 외부 이벤트 null로 초기화
                RequestHandler = null;   // 외부 요청 핸들러 null로 초기화

                // base.OnFormClosed(e);    // 폼화면 닫기 (해당 메서드 base.OnFormClosed(e); 호출시 이벤트 메서드 MEPUpdater_FormClosed 두번 실행되서 오류 발생하므로 주석처리 진행)

                Log.Information(Logger.GetMethodPath(currentMethod) + "업데이터 화면 종료 완료");
            }
            catch(Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
            }
        }

        #endregion MEPUpdater_FormClosed

        #region 기본 메소드

        // TODO : 콜백 함수 Execute 구현 (2024.03.11 jbh)
        // 콜백(CallBack) 함수란? 시스템이 사용자가 요청한 처리를 하다가 특정 이벤트를 발생시켜 해당 이벤트를 처리해달라고 역으로 전달해 오는 함수
        // 참고 URL   - https://nephrolepis.tistory.com/12
        // 참고 2 URL - https://todaycode.tistory.com/24

        /// <summary>
        /// 콜백 함수 Execute
        /// </summary>
        public void Execute(UpdaterData rvData)
        {
            string targetParamName = string.Empty;   // 값을 할당할 매개변수 이름 
            string currentDateTime = string.Empty;   // 매개변수에 입력할 값(“현재 날짜 시간 조합 문자” )

            var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

            try
            {
                Log.Information(Logger.GetMethodPath(currentMethod) + "MEPUpdater Execute 시작");

                // 매개변수 값 입력 완료 여부 확인 
                if(true == IsCompleted)
                {
                    IsCompleted = false;   // 매개변수 값 입력 완료 여부 false 다시 초기화
                    return;                // 콜백함수 Execute 종료 처리 (종료 처리 안 하면 콜백 함수 Execute가 무한으로 실행됨.)
                }


                RevitDoc = rvData.GetDocument();   // UpdaterData 클래스 객체 rvData와 연관된 Document 개체 반환

                // Revit 문서 안에 포함된 객체(Element)를 다시 검색 및 필터링
                Collector = new FilteredElementCollector(RevitDoc).WhereElementIsNotElementType();

                CategoryDataCreate(Collector, GeometryOpt);   // LookUpEdit 컨트롤(searchLookUpEditCategory)에 데이터 다시 생성 및 출력 

                var addElementIds = rvData.GetAddedElementIds();      // 활성화된 Revit 문서에서 새로 추가된 객체 아이디 리스트(addElementIds) 구하기 
                List<Element> addElements    = addElementIds.Select(addElementId => RevitDoc.GetElement(addElementId)).ToList();        // 새로 추가된 객체 리스트 
                List<string> addElementNames = addElementIds.Select(addElementId => RevitDoc.GetElement(addElementId).Name).ToList();   // 새로 추가된 객체 집합에서 객체 이름만 추출 


                var modElementIds = rvData.GetModifiedElementIds();   // 활성화된 Revit 문서에서 수정(편집)된 객체 아이디 리스트(modElementIds) 구하기 
                List<Element> modElements    = modElementIds.Select(modElementId => RevitDoc.GetElement(modElementId)).ToList();        // 수정된 객체 리스트 
                List<string> modElementNames = modElementIds.Select(modElementId => RevitDoc.GetElement(modElementId).Name).ToList();   // 수정된 객체 집합에서 객체 이름만 추출

                targetParamName = this.textParamName.Text;   // MEPUpdater 폼 화면의 TextBox에서 입력 받은 문자열을 string 클래스 객체 targetParamName에 할당하기 
                // builtInParamName = ParamsManager.GetBuiltInParameterName(BuiltInParameter.ALL_MODEL_INSTANCE_COMMENTS);   // 테스트용 - BuiltInParameter 매개변수 이름 "해설" 가져오기

                currentDateTime = DateTime.Now.ToString();   // "targetParamName"에 저장된 문자열과 동일한 이름의 매개변수에 입력할 값 ("현재 날짜 시간 조합 문자") 문자열 변환 후 할당

                if(addElementIds.Count >= (int)EnumExistElements.EXIST
                    && addElementNames.Count >= (int)EnumExistElements.EXIST)   // 새로 추가된 객체 아이디 리스트(addElementIds)와 객체 이름 리스트(addElementNames)에 모두 값이 존재하는 경우 
                {
                    // ParamsManager 클래스 static 메서드 "SetParametersValue" 호출
                    // 신규 추가 객체 리스트(addElements)에 속하는"targetParamName"과 동일한 이름의 매개변수에 입력되는 값으로“현재 날짜 시간 조합 문자”입력
                    // static 메서드 "SetParametersValue" 기능
                    // 1."targetParamName"과 동일한 이름의 매개변수 추출하기 
                    // 2."targetParamName"과 동일한 이름의 매개변수에 값“현재 날짜 시간 조합 문자”입력하기 
                    // 3."targetParamName"과 동일한 이름의 매개변수에 입력 완료된 값“현재 날짜 시간 조합 문자”메시지 출력하기 
                    IsCompleted = ParamsManager.SetParametersValue(addElements, targetParamName, currentDateTime);

                    // 신규 추가 완료된 객체 이름 리스트(addElementNames) 메세지 출력 
                    if(true == IsCompleted) TaskDialog.Show("테스트 MEP Updater", "신규 업데이트 완료\r\n객체 명 - " + string.Join<string>(", ", addElementNames) + $"\r\n매개변수 이름 : {targetParamName}\r\n매개변수 입력된 값 : {currentDateTime}");

                    // 신규 업데이트 실패한 경우 
                    else throw new Exception("신규 업데이트 실패!!\r\n담당자에게 문의 하시기 바랍니다.");
                }

                if(modElementIds.Count >= (int)EnumExistElements.EXIST
                    && modElementNames.Count >= (int)EnumExistElements.EXIST)   // 수정된 객체 아이디 리스트(modElementIds)와 객체 이름 리스트(modElementNames)에 모두 값이 존재하는 경우 
                {
                    // 수정된 객체 리스트(modElements)에 속하는 "targetParamName"과 동일한 이름의 매개변수에 입력되는 값으로“현재 날짜 시간 조합 문자”입력
                    IsCompleted = ParamsManager.SetParametersValue(modElements, targetParamName, currentDateTime);

                    // 수정 업데이트 완료된 객체 이름 리스트(modElementNames) 메세지 출력 
                    if(true == IsCompleted) TaskDialog.Show("테스트 MEP Updater", "수정 업데이트 완료\r\n객체 명 - " + string.Join<string>(", ", modElementNames) + $"\r\n매개변수 이름 : {targetParamName}\r\n매개변수 입력된 값 : {currentDateTime}");
                    // 수정 업데이트 실패한 경우 
                    else throw new Exception("수정 업데이트 실패!!\r\n담당자에게 문의 하시기 바랍니다.");
                }

                Log.Information(Logger.GetMethodPath(currentMethod) + "MEPUpdater Execute 종료");
            }
            catch(Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                TaskDialog.Show(UpdaterHelper.ErrorTitle, ex.Message);
            }
            finally
            {
                // TODO : finally문 안에 코드 필요시 구현 예정 (2024.03.14 jbh)
            }
        }

        /// <summary>
        /// 부가정보 가져오기
        /// </summary>
        public string GetAdditionalInformation()
        {
            return "NA";
        }

        /// <summary>
        /// 업데이터 우선순위 변경하기
        /// </summary>
        public ChangePriority GetChangePriority()
        {
            return ChangePriority.MEPFixtures;
        }

        /// <summary>
        /// 업데이터 아이디 가져오기
        /// </summary>
        public UpdaterId GetUpdaterId()
        {
            return Updater_Id;
        }

        /// <summary>
        /// 업데이터 이름 가져오기 
        /// </summary>
        public string GetUpdaterName()
        {
            return UpdaterHelper.MEPUpdaterFormName;
        }

        #endregion 기본 메소드

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
                TaskDialog.Show(UpdaterHelper.NoticeTitle, "테스트 진행 중...");

                // 카테고리 정보 RequestHandler.cs 소스파일로 넘겨서 업데이터 + Triggers 등록 및 해제 구현하기 
                var categoryInfo = this.comboBoxCategory.SelectedItem as CategoryInfoView;

                CategoryInfo = new CategoryInfoView(categoryInfo.CategoryName, categoryInfo.Category);
                //CategoryInfo = this.comboBoxCategory.SelectedItem as CategoryInfoView; 

                BuiltInCategory testCategory = CategoryManager.GetBuiltInCategory("배관");

                //BuiltInCategory testCategory = CategoryInfoList.Where(x => x.CategoryName.Equals("배관"))
                //                                               .Select(x => x.Category)
                //                                               .FirstOrDefault();


                BuiltInCategory test2Category = CategoryManager.GetBuiltInCategory("OST_PipeFitting");

            }
            catch(Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                TaskDialog.Show(UpdaterHelper.ErrorTitle, ex.Message);
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
                TaskDialog.Show("Revit MEPUpdater", "업데이터 + Triggers 등록 구현 예정...");

                // 카테고리 정보 RequestHandler.cs 소스파일로 넘겨서 업데이터 + Triggers 등록 및 해제 구현하기 
                var categoryInfo = this.comboBoxCategory.SelectedItem as CategoryInfoView;
                CategoryInfo = new CategoryInfoView(categoryInfo.CategoryName, categoryInfo.Category);

                MakeRequest(EnumMEPUpdaterRequestId.REGISTER);

                // TODO : Command.cs -> RevitBox 업데이터 Command 클래스 "CmdMEPUpdater" -> 콜백함수 "Execute"가 실행되어
                //        MEPUpdater 클래스 폼 화면을 출력할 때 메서드 ".ShowDialog()"가 아닌 ".Show()"로 호출 후
                //        MEPUpdater 폼 화면이 출력 되고 -> 버튼 "ON" 클릭시 이벤트 메서드 "btnON_Click"가 실행되면
                //        아래 주석친 using(Transaction transaction = new Transaction(RevitDoc)) 코드 실행시 
                //        오류 메시지 출력 "Starting a transaction from an external application running outside of API context is not allowed."
                //        해당 오류 원인 파악 예정 (2024.03.15 jbh)
                //        
                //        오류 메시지 출력 원인
                //        메서드 ".Show()" 호출해서 MEPUpdater 클래스 폼 화면을 출력할 때에는
                //        부모 폼(Revit)과 자식 폼(MEPUpdater)이 서로 독립적이고 쓰레드 또한 개별로 갖고 있어서
                //        자식 폼(MEPUpdater)이 부모 폼(Revit)의 쓰레드를 별도로 제어할 수 없기 때문이다.
                //        하여 해당 오류 메시지가 출력 하지 않기 위해서는 클래스 파일 "RequestHandler.cs" 생성해서
                //        클래스 "MEPUpdaterRequestHandler" 생성 및 인터페이스 "IExternalEventHandler" 상속 받은 후 Execute 메소드 구현 해야함. (2024.03.15 jbh)
                // 참고 자료 - 프로젝트 파일 "RevitBox2023" -> 소스 파일 "RequestHandler.cs" 참고하기

                // 참고 URL   - https://nomadcoder.tistory.com/entry/Revit-%ED%95%B4%EA%B2%B0%EB%B0%A9%EB%B2%95Starting-a-transaction-from-an-external-application-running-outside-of-API-context-is-not-allowed
                // 참고 2 URL - https://stackoverflow.com/questions/31490990/starting-a-transaction-from-an-external-application-running-outside-of-api-conte
                // 참고 3 URL - https://thebuildingcoder.typepad.com/blog/2015/12/external-event-and-10-year-forum-anniversary.html

                // 해당 Transaction이 끝날 때까지는 화면 상에서는 다른 기능을 실행할 수 있고 다른 기능의 화면도 출력되지만
                // 다른 기능을 실행해서 데이터를 변경할 수 없다.(다른 작업이나 Command 명령이 끼어들 수 없다.)
                // 해당 Transaction 기능은 부포 폼(Revit)의 쓰레드를 자식 폼(MEPUpdater)이 제어하는 과정이다.
                // using(Transaction transaction = new Transaction(RevitDoc))
                // {
                //     // transaction.Start(AABIMHelper.Start); 부터 transaction.Commit(); 까지가 연산처리를 하는 하나의 작업단위이다.
                //     transaction.Start(UpdaterHelper.Start);   // 연산처리(객체 생성, 정보 변경 및 삭제 등등... ) 시작
                // 
                //     Log.Information(Logger.GetMethodPath(currentMethod) + "업데이터 + Triggers 등록 시작");
                // 
                //     Log.Information(Logger.GetMethodPath(currentMethod) + "업데이터 + Triggers 등록 완료");
                // 
                //     transaction.Commit();   // 연산처리(객체 생성, 정보 변경 및 삭제 등등... )된 결과 커밋
                // }   // 여기서 Dispose (리소스 해제) 처리 

            }
            catch(Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                TaskDialog.Show(UpdaterHelper.ErrorTitle, ex.Message);
            }
            finally
            {
                // TODO : finally문 안에 코드 필요시 구현 예정 (2024.03.14 jbh)
            }
        }

        #endregion btnON_Click

        #region btnOFF_Click

        /// <summary>
        /// 업데이터 + Triggers 해제 
        /// </summary>
        private void btnOFF_Click(object sender, EventArgs e)
        {
            var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

            try
            {
                TaskDialog.Show("Revit MEPUpdater", "업데이터 + Triggers 해제 구현 예정...");

                MakeRequest(EnumMEPUpdaterRequestId.REMOVE);

                // TODO : Command.cs -> RevitBox 업데이터 Command 클래스 "CmdMEPUpdater" -> 콜백함수 "Execute"가 실행되어
                //        MEPUpdater 클래스 폼 화면을 출력할 때 메서드 ".ShowDialog()"가 아닌 ".Show()"로 호출 후
                //        MEPUpdater 폼 화면이 출력 되고 -> 버튼 "OFF" 클릭시 이벤트 메서드 "btnOFF_Click"가 실행되면
                //        아래 주석친 using(Transaction transaction = new Transaction(RevitDoc)) 코드 실행시 
                //        오류 메시지 출력 "Starting a transaction from an external application running outside of API context is not allowed."
                //        해당 오류 원인 파악 예정 (2024.03.15 jbh)
                //        
                //        오류 메시지 출력 원인
                //        메서드 ".Show()" 호출해서 MEPUpdater 클래스 폼 화면을 출력할 때에는
                //        부모 폼(Revit)과 자식 폼(MEPUpdater)이 서로 독립적이고 쓰레드 또한 개별로 갖고 있어서
                //        자식 폼(MEPUpdater)이 부모 폼(Revit)의 쓰레드를 별도로 제어할 수 없기 때문이다.
                //        하여 해당 오류 메시지가 출력 하지 않기 위해서는 클래스 파일 "RequestHandler.cs" 생성해서
                //        클래스 "MEPUpdaterRequestHandler" 생성 및 인터페이스 "IExternalEventHandler" 상속 받은 후 Execute 메소드 구현 해야함. (2024.03.15 jbh)
                // 참고 자료 - 프로젝트 파일 "RevitBox2023" -> 소스 파일 "RequestHandler.cs" 참고하기

                // 참고 URL   - https://nomadcoder.tistory.com/entry/Revit-%ED%95%B4%EA%B2%B0%EB%B0%A9%EB%B2%95Starting-a-transaction-from-an-external-application-running-outside-of-API-context-is-not-allowed
                // 참고 2 URL - https://stackoverflow.com/questions/31490990/starting-a-transaction-from-an-external-application-running-outside-of-api-conte
                // 참고 3 URL - https://thebuildingcoder.typepad.com/blog/2015/12/external-event-and-10-year-forum-anniversary.html

                // 해당 Transaction이 끝날 때까지는 화면 상에서는 다른 기능을 실행할 수 있고 다른 기능의 화면도 출력되지만
                // 다른 기능을 실행해서 데이터를 변경할 수 없다.(다른 작업이나 Command 명령이 끼어들 수 없다.)
                // 해당 Transaction 기능은 부포 폼(Revit)의 쓰레드를 자식 폼(MEPUpdater)이 제어하는 과정이다.
                // using(Transaction transaction = new Transaction(RevitDoc))
                // {
                //     // transaction.Start(AABIMHelper.Start); 부터 transaction.Commit(); 까지가 연산처리를 하는 하나의 작업단위이다.
                //     transaction.Start(UpdaterHelper.Start);   // 연산처리(객체 생성, 정보 변경 및 삭제 등등... ) 시작
                // 
                //     Log.Information(Logger.GetMethodPath(currentMethod) + "업데이터 + Triggers 해제 시작");
                // 
                // 
                //     Log.Information(Logger.GetMethodPath(currentMethod) + "업데이터 + Triggers 해제 완료");
                // 
                //     transaction.Commit();   // 연산처리(객체 생성, 정보 변경 및 삭제 등등... )된 결과 커밋
                // }   // 여기서 Dispose (리소스 해제) 처리 
            }
            catch(Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                TaskDialog.Show(UpdaterHelper.ErrorTitle, ex.Message);
            }
            finally
            {
                // TODO : finally문 안에 코드 필요시 구현 예정 (2024.03.14 jbh)
            }
        }

        #endregion btnOFF_Click
    }
}