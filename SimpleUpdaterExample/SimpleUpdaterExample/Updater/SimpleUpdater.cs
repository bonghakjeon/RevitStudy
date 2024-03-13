using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using SimpleUpdaterExample.Common.LogManager;
using SimpleUpdaterExample.Common.ParamsManager;
using SimpleUpdaterExample.Models.UpdaterBase.Updater;

namespace SimpleUpdaterExample.Updater
{
    // TODO : Revit AddIn 개발 소스를 비쥬얼스튜디오 2022 버전 - .net Core 8.0을 사용하려면 Revit 2025 버전 부터 사용이 가능하므로 현 시점에서 해당 소스는 .net FrameWork 4.8에서만 구동시킬 수 있다. (2024.03.11 jbh)

    /// <summary>
    /// RevitBox 업데이터 
    /// </summary>
    public class SimpleUpdater : IUpdater
    {
        #region 프로퍼티 

        /// <summary>
        /// 업데이터 아이디 생성시 필요한 GUID 문자열 프로퍼티
        /// </summary>
        private const string Id = "d42d28af-d2cd-4f07-8873-e7cfb61903d8";

        /// <summary>
        /// 업데이터 아이디
        /// </summary>
        // private UpdaterId Updater_Id { get; }
        private UpdaterId Updater_Id { get; set; }

        /// <summary>
        /// 카테고리 필터 (벽)
        /// </summary>
        private ElementCategoryFilter WallCategoryFilter { get; set; }

        /// <summary>
        /// 카테고리 필터 (파이프 - OST_PipeCurves)
        /// </summary>
        private ElementCategoryFilter PipeCurvesCategoryFilter { get; set; }


        /// <summary>
        /// 카테고리 필터 (파이프 피팅류 - OST_PipeFitting)
        /// </summary>
        private ElementCategoryFilter PipeFittingCategoryFilter { get; set; }

        /// <summary>
        /// BuiltInParameter 매개변수 이름 
        /// </summary>
        private string BuiltInParamName { get; set; }

        /// <summary>
        /// BuiltInParameter 매개변수에 입력할 값 (“현재 날짜 시간 조합 문자” )
        /// </summary>
        private string CurrentDateTime { get; set; }

        /// <summary>
        /// 매개변수 값 입력 완료 여부 
        /// </summary>
        private bool IsCompleted { get; set; } 


        #endregion 프로퍼티 

        #region 생성자 

        public SimpleUpdater(Document pDoc, AddInId pAddInId)
        {
            Guid guId = new Guid(Id);
            Updater_Id = new UpdaterId(pAddInId, guId);

            InitSetting(pDoc, Updater_Id);   // 업데이터 초기 셋팅
        }

        #endregion 생성자 

        #region InitSetting

        private void InitSetting(Document pDoc, UpdaterId pUpdater_Id)
        {
            var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

            try
            {
                IsCompleted = false; // 매개변수 값 입력 완료 여부 false 초기화

                // 1. 객체 "벽"(BuiltInCategory.OST_Walls)만 필터링 처리 
                WallCategoryFilter        = new ElementCategoryFilter(BuiltInCategory.OST_Walls);

                // 2. new ElementCategoryFilter(BuiltInCategory.OST_PipeCurves);  // 객체 "배관" 만 필터링 처리 
                PipeCurvesCategoryFilter  = new ElementCategoryFilter(BuiltInCategory.OST_PipeCurves);

                // 3. new ElementCategoryFilter(BuiltInCategory.OST_PipeFitting); // 객체 "배관 부속류" 만 필터링 처리 
                PipeFittingCategoryFilter = new ElementCategoryFilter(BuiltInCategory.OST_PipeFitting);

                RegisterUpdater(pDoc, Updater_Id);                         // 업데이터 등록 

                UpdaterRegistry.RemoveAllTriggers(Updater_Id);             // 지정된 UpdaterId를 사용하여 업데이터와 연결된 모든 트리거를 제거. 업데이터 등록을 취소하지 않음.
                
                RegisterTriggers(Updater_Id, WallCategoryFilter);          // 객체 "벽" Triggers 등록 
                RegisterTriggers(Updater_Id, PipeCurvesCategoryFilter);    // 객체 "배관" Triggers 등록
                RegisterTriggers(Updater_Id, PipeFittingCategoryFilter);   // 객체 "배관 부속류" Triggers 등록

                // TODO : 아래 주석친 테스트 코드 필요시 참고 (2024.03.13 jbh)
                //var builtInCategories = Enum.GetValues(typeof(BuiltInCategory));

                //List<string> builtInCategoryNameList = builtInCategories.OfType<BuiltInCategory>()
                //                                               .Where(builtInCategory
                //                                                      => builtInCategory == BuiltInCategory.OST_PipeCurves
                //                                                      && builtInCategory == BuiltInCategory.OST_PipeFitting)
                //                                               .Select(builtInCategory => LabelUtils.GetLabelFor(builtInCategory))
                //                                               .ToList();


                //var builtInCategoryList = ParamsManager.builtInCategoryNameList(builtInCategories);
            }
            catch (Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                TaskDialog.Show(UpdaterHelper.ErrorTitle, ex.Message);
            }
        }

        #endregion InitSetting

        #region 기본 메소드 

        // TODO : 콜백 함수 Execute 구현 (2024.03.11 jbh)
        // 콜백(CallBack) 함수란? 시스템이 사용자가 요청한 처리를 하다가 특정 이벤트를 발생시켜 해당 이벤트를 처리해달라고 역으로 전달해 오는 함수
        // 참고 URL   - https://nephrolepis.tistory.com/12
        // 참고 2 URL - https://todaycode.tistory.com/24
        /// <summary>
        /// 콜백 함수 Execute
        /// </summary>
        public void Execute(UpdaterData data)
        {
            var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

            try
            {
                Log.Information(Logger.GetMethodPath(currentMethod) + "SimpleUpdater Execute 시작");

                // 매개변수 값 입력 완료 여부 확인 
                if (true == IsCompleted) 
                {
                    IsCompleted = false;   // 매개변수 값 입력 완료 여부 false 다시 초기화
                    return;                // 콜백함수 Execute 종료 처리 
                }
                

                var revitDoc = data.GetDocument();                                                                        // UpdaterData 클래스 객체 data와 연관된 Document 개체 반환

                var addElementIds            = data.GetAddedElementIds();                                                 // 활성화된 Revit 문서에서 새로 추가된 객체 집합 반환 
                List<Element> addElements    = addElementIds.Select(addId => revitDoc.GetElement(addId)).ToList();        // 새로 추가된 객체 리스트 
                List<string> addElementNames = addElementIds.Select(addId => revitDoc.GetElement(addId).Name).ToList();   // 새로 추가된 객체 집합에서 객체 이름만 추출 


                var modElementIds            = data.GetModifiedElementIds();                                              // 활성화된 Revit 문서에서 수정(편집)된 객체 집합 반환 
                List<Element> modElements    = modElementIds.Select(addId => revitDoc.GetElement(addId)).ToList();        // 수정된 객체 리스트 
                List<string> modElementNames = modElementIds.Select(modId => revitDoc.GetElement(modId).Name).ToList();   // 수정된 객체 집합에서 객체 이름만 추출

                BuiltInParamName = ParamsManager.GetBuiltInParameterName(BuiltInParameter.ALL_MODEL_INSTANCE_COMMENTS);   // BuiltInParameter 매개변수 이름 가져오기

                CurrentDateTime  = DateTime.Now.ToString();   // "해설" 매개변수에 입력할 값 ("현재 날짜 시간 조합 문자")

                if (addElementIds.Count >= (int)EnumExistElements.EXIST
                    && addElementNames.Count >= (int)EnumExistElements.EXIST)  // 새로 추가된 객체 집합이 존재하고 객체 이름 또한 존재하는 경우 
                {
                    // ParamsManager 클래스 static 메서드 "SetParametersValue" 호출
                    // 신규 추가 객체 집합에 속하는 BuiltInParameter“해설”매개변수에 입력되는 값으로“현재 날짜 시간 조합 문자”입력
                    // static 메서드 "SetParametersValue" 기능
                    // 1.“해설”매개변수 추출하기 
                    // 2.“해설”매개변수에 값“현재 날짜 시간 조합 문자”입력하기 
                    // 3.“해설”매개변수에 입력 완료된 값“현재 날짜 시간 조합 문자”메시지 출력하기 
                    IsCompleted = ParamsManager.SetParametersValue(addElements, BuiltInParamName, CurrentDateTime);

                    // 신규 추가 완료된 객체 집합의 이름 목록 메세지 출력 
                    if (true == IsCompleted) TaskDialog.Show("테스트 Simple Updater", "신규 업데이트 완료\r\n객체 명 - " + string.Join<string>(", ", addElementNames) + $"\r\n매개변수 이름 : {BuiltInParamName}\r\n매개변수 입력된 값 : {CurrentDateTime}");

                    // 신규 업데이트 실패한 경우 
                    else throw new Exception("신규 업데이트 실패!!\r\n담당자에게 문의 하시기 바랍니다.");
                }

                if (modElementIds.Count >= (int)EnumExistElements.EXIST
                    && modElementNames.Count >= (int)EnumExistElements.EXIST)   // 수정된 객체 집합이 존재하고 객체 이름 또한 존재하는 경우 
                {

                    // 수정된 객체 집합에 속하는 BuiltInParameter“해설”매개변수에 입력되는 값으로“현재 날짜 시간 조합 문자”입력
                    IsCompleted = ParamsManager.SetParametersValue(modElements, BuiltInParamName, CurrentDateTime);

                    // 수정 업데이트 완료된 객체 집합의 이름 목록 메세지 출력 
                    if (true == IsCompleted) TaskDialog.Show("테스트 Simple Updater", "수정 업데이트 완료\r\n객체 명 - " + string.Join<string>(", ", modElementNames) + $"\r\n매개변수 이름 : {BuiltInParamName}\r\n매개변수 입력된 값 : {CurrentDateTime}");
                    // 수정 업데이트 실패한 경우 
                    else throw new Exception("수정 업데이트 실패!!\r\n담당자에게 문의 하시기 바랍니다.");
                }

                Log.Information(Logger.GetMethodPath(currentMethod) + "SimpleUpdater Execute 완료");
            }
            catch (Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                TaskDialog.Show(UpdaterHelper.ErrorTitle, ex.Message);
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
            return "SimpleUpdater";
        }

        #endregion 기본 메소드 

        #region RegisterUpdater

        /// <summary>
        /// 업데이터 등록 
        /// </summary>
        private void RegisterUpdater(Document pDoc, UpdaterId pUpdaterId)
        {
            var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

            try
            {
                Log.Information(Logger.GetMethodPath(currentMethod) + "업데이터 등록 시작");

                if (UpdaterRegistry.IsUpdaterRegistered(pUpdaterId, pDoc))  // Revit 문서에 해당 ID를 가진 업데이터가 등록된 경우 
                {
                    UpdaterRegistry.RemoveAllTriggers(pUpdaterId);          // 지정된 ID를 가진 업데이터와 연결된 모든 트리거 제거 
                    UpdaterRegistry.UnregisterUpdater(pUpdaterId, pDoc);    // 지정된 ID를 가진 업데이터와 연결된 업데이터 프로그램 등록 취소 (해당 트리거 포함 레지스트리에서 완전 제거 처리)
                }

                UpdaterRegistry.RegisterUpdater(this, pDoc);

                TaskDialog.Show("Simple Updater Example", "테스트 업데이터 등록 완료");   

                Log.Information(Logger.GetMethodPath(currentMethod) + "업데이터 등록 완료");
            }
            catch (Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                TaskDialog.Show(UpdaterHelper.ErrorTitle, ex.Message);
            }
        }

        #endregion RegisterUpdater

        #region RegisterTriggers

        /// <summary>
        /// Triggers 등록 
        /// </summary>
        private void RegisterTriggers(UpdaterId pUpdaterId, ElementCategoryFilter pElementCategoryFilter)
        {
            var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

            try
            {
                Log.Information(Logger.GetMethodPath(currentMethod) + "Triggers 등록 시작");

                BuiltInCategory builtInCategory = (BuiltInCategory)pElementCategoryFilter.CategoryId.Value;

                string builtInCategoryName = LabelUtils.GetLabelFor(builtInCategory); 

                // 해당 업데이터 아이디가 존재하고, 업데이터가 등록되어 있는 경우 
                if (pUpdaterId is not null
                    && UpdaterRegistry.IsUpdaterRegistered(pUpdaterId))
                {
                    var changeTypeAny = Element.GetChangeTypeAny();                                       // 객체가 수정 방식으로 업데이터될 때 업데이터를 트리거하려면 해당 변경 유형 사용
                    UpdaterRegistry.AddTrigger(pUpdaterId, pElementCategoryFilter, changeTypeAny);        // 지정된 UpdaterId와 연결된 모든 문서에 대해 지정된 요소 필터(pElementCategoryFilter) 및 changeTypeAny을 이용해서 수정 트리거 추가

                    var changeTypeAddition = Element.GetChangeTypeElementAddition();                      // 객체가 새로 추가된 방식으로 업데이터될 때 업데이터를 트리거하려면 해당 변경 유형 사용
                    UpdaterRegistry.AddTrigger(pUpdaterId, pElementCategoryFilter, changeTypeAddition);   // 지정된 UpdaterId와 연결된 모든 문서에 대해 지정된 요소 필터(pElementCategoryFilter) 및 changeTypeAny을 이용해서 새로 추가 트리거 추가

                    TaskDialog.Show("Simple Updater Example", $"테스트 {builtInCategoryName} Triggers 등록 완료");  
                    Log.Information(Logger.GetMethodPath(currentMethod) + $"테스트 {builtInCategoryName} Triggers 등록 완료");
                }
                else 
                {
                    TaskDialog.Show("Simple Updater Example", $"테스트 {builtInCategoryName} Triggers 등록 실패!\r\n담당자에게 문의하세요.");   
                    Log.Information(Logger.GetMethodPath(currentMethod) + $"테스트 {builtInCategoryName} Triggers 등록 실패!");
                }
            }
            catch (Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                TaskDialog.Show(UpdaterHelper.ErrorTitle, ex.Message);
            }
        }

        #endregion RegisterTriggers

        #region Sample

        #endregion Sample
    }
}
