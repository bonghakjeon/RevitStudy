using Serilog;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

using RevitUpdater.Common.LogManager;
using RevitUpdater.Common.ParamsManager;
using RevitUpdater.Common.UpdaterBase;
using RevitUpdater.Models.UpdaterBase.MEPUpdater;

namespace RevitUpdater.Test
{
    // TODO : Revit AddIn 개발 소스를 비쥬얼스튜디오 2022 .net Core 버전(8.0)을 사용하려면 Revit 2025 버전 부터 사용이 가능하므로 현 시점에서 해당 소스는 .net FrameWork 4.8에서만 구동시킬 수 있다. (2024.03.11 jbh)

    /// <summary>
    /// RevitBox 업데이터 
    /// </summary>
    public class TestMEPUpdater : IUpdater
    {
        #region 프로퍼티 

        /// <summary>
        /// 업데이터 아이디 생성시 필요한 GUID 문자열 프로퍼티
        /// </summary>
        private const string GId = "d42d28af-d2cd-4f07-8873-e7cfb61903d8";

        /// <summary>
        /// 업데이터 아이디
        /// </summary>
        // private UpdaterId Updater_Id { get; }
        private UpdaterId Updater_Id { get; set; }

        /// <summary>
        /// 카테고리 필터 (벽 - OST_Walls)
        /// </summary>
        private ElementCategoryFilter WallCategoryFilter { get; set; }

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

        public TestMEPUpdater(Document rvDoc, AddInId rvAddInId)
        {
            Guid guId = new Guid(GId);
            Updater_Id = new UpdaterId(rvAddInId, guId);

            InitSetting(rvDoc, Updater_Id);   // 업데이터 초기 셋팅
        }

        #endregion 생성자 

        #region InitSetting

        /// <summary>
        /// 업데이터 초기 셋팅
        /// </summary>
        private void InitSetting(Document rvDoc, UpdaterId pUpdaterId)
        {
            var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

            try
            {
                Log.Information(Logger.GetMethodPath(currentMethod) + "업데이터 초기 셋팅 시작");

                // 1. 매개변수 값 입력 완료 여부 false 초기화
                IsCompleted = false;

                // 2. 객체 "벽"(BuiltInCategory.OST_Walls)만 필터링 처리 
                WallCategoryFilter = new ElementCategoryFilter(BuiltInCategory.OST_Walls);

                // 3. 객체 "배관"(BuiltInCategory.OST_PipeCurves)만 필터링 처리 
                PipeCurvesCategoryFilter = new ElementCategoryFilter(BuiltInCategory.OST_PipeCurves);

                // 4. 객체 "배관 부속류"(BuiltInCategory.OST_PipeFitting)만 필터링 처리 
                PipeFittingCategoryFilter = new ElementCategoryFilter(BuiltInCategory.OST_PipeFitting);

                RegisterUpdater(rvDoc, pUpdaterId);                         // 업데이터 등록 

                RegisterTriggers(pUpdaterId, WallCategoryFilter);          // 객체 "벽" Triggers 등록 
                RegisterTriggers(pUpdaterId, PipeCurvesCategoryFilter);    // 객체 "배관" Triggers 등록
                RegisterTriggers(pUpdaterId, PipeFittingCategoryFilter);   // 객체 "배관 부속류" Triggers 등록

                Log.Information(Logger.GetMethodPath(currentMethod) + "업데이터 초기 셋팅 완료");

                // TODO : 아래 주석친 테스트 코드 필요시 참고 (2024.03.13 jbh)
                //var builtInCategories = Enum.GetValues(typeof(BuiltInCategory));

                //List<string> builtInCategoryNameList = builtInCategories.OfType<BuiltInCategory>()
                //                                                        .Where(builtInCategory
                //                                                               => builtInCategory == BuiltInCategory.OST_PipeCurves
                //                                                               && builtInCategory == BuiltInCategory.OST_PipeFitting)
                //                                                        .Select(builtInCategory => LabelUtils.GetLabelFor(builtInCategory))
                //                                                        .ToList();


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
        public void Execute(UpdaterData pData)
        {
            string builtInParamName = string.Empty;   // BuiltInParameter 매개변수 이름 
            string currentDateTime = string.Empty;   // BuiltInParameter 매개변수에 입력할 값(“현재 날짜 시간 조합 문자” )

            var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

            try
            {
                Log.Information(Logger.GetMethodPath(currentMethod) + "TestMEPUpdater Execute 시작");

                // 매개변수 값 입력 완료 여부 확인 
                if (true == IsCompleted)
                {
                    IsCompleted = false;   // 매개변수 값 입력 완료 여부 false 다시 초기화
                    return;                // 콜백함수 Execute 종료 처리 (종료 처리 안 하면 콜백 함수 Execute가 무한으로 실행됨.)
                }


                var revitDoc = pData.GetDocument();   // UpdaterData 클래스 객체 pData와 연관된 Document 개체 반환

                var addElementIds = pData.GetAddedElementIds();      // 활성화된 Revit 문서에서 새로 추가된 객체 아이디 리스트(addElementIds) 구하기 
                List<Element> addElements = addElementIds.Select(addElementId => revitDoc.GetElement(addElementId)).ToList();        // 새로 추가된 객체 리스트 
                List<string> addElementNames = addElementIds.Select(addElementId => revitDoc.GetElement(addElementId).Name).ToList();   // 새로 추가된 객체 집합에서 객체 이름만 추출 


                var modElementIds = pData.GetModifiedElementIds();   // 활성화된 Revit 문서에서 수정(편집)된 객체 아이디 리스트(modElementIds) 구하기 
                List<Element> modElements = modElementIds.Select(modElementId => revitDoc.GetElement(modElementId)).ToList();        // 수정된 객체 리스트 
                List<string> modElementNames = modElementIds.Select(modElementId => revitDoc.GetElement(modElementId).Name).ToList();   // 수정된 객체 집합에서 객체 이름만 추출

                builtInParamName = ParamsManager.GetBuiltInParameterName(BuiltInParameter.ALL_MODEL_INSTANCE_COMMENTS);   // BuiltInParameter 매개변수 이름 "해설" 가져오기

                currentDateTime = DateTime.Now.ToString();   // "해설" 매개변수에 입력할 값 ("현재 날짜 시간 조합 문자") 문자열 변환 후 할당

                if (addElementIds.Count >= (int)EnumExistElements.EXIST
                    && addElementNames.Count >= (int)EnumExistElements.EXIST)   // 새로 추가된 객체 아이디 리스트(addElementIds)와 객체 이름 리스트(addElementNames)에 모두 값이 존재하는 경우 
                {
                    // ParamsManager 클래스 static 메서드 "SetParametersValue" 호출
                    // 신규 추가 객체 리스트(addElements)에 속하는 BuiltInParameter“해설”매개변수에 입력되는 값으로“현재 날짜 시간 조합 문자”입력
                    // static 메서드 "SetParametersValue" 기능
                    // 1.“해설”매개변수 추출하기 
                    // 2.“해설”매개변수에 값“현재 날짜 시간 조합 문자”입력하기 
                    // 3.“해설”매개변수에 입력 완료된 값“현재 날짜 시간 조합 문자”메시지 출력하기 
                    IsCompleted = ParamsManager.SetParametersValue(addElements, builtInParamName, currentDateTime);

                    // 신규 추가 완료된 객체 이름 리스트(addElementNames) 메세지 출력 
                    if (true == IsCompleted) TaskDialog.Show("테스트 Simple Updater", "신규 업데이트 완료\r\n객체 명 - " + string.Join<string>(", ", addElementNames) + $"\r\n매개변수 이름 : {builtInParamName}\r\n매개변수 입력된 값 : {currentDateTime}");

                    // 신규 업데이트 실패한 경우 
                    else throw new Exception("신규 업데이트 실패!!\r\n담당자에게 문의 하시기 바랍니다.");
                }

                if (modElementIds.Count >= (int)EnumExistElements.EXIST
                    && modElementNames.Count >= (int)EnumExistElements.EXIST)   // 수정된 객체 아이디 리스트(modElementIds)와 객체 이름 리스트(modElementNames)에 모두 값이 존재하는 경우 
                {

                    // 수정된 객체 리스트(modElements)에 속하는 BuiltInParameter“해설”매개변수에 입력되는 값으로“현재 날짜 시간 조합 문자”입력
                    IsCompleted = ParamsManager.SetParametersValue(modElements, builtInParamName, currentDateTime);

                    // 수정 업데이트 완료된 객체 이름 리스트(modElementNames) 메세지 출력 
                    if (true == IsCompleted) TaskDialog.Show("테스트 Simple Updater", "수정 업데이트 완료\r\n객체 명 - " + string.Join<string>(", ", modElementNames) + $"\r\n매개변수 이름 : {builtInParamName}\r\n매개변수 입력된 값 : {currentDateTime}");
                    // 수정 업데이트 실패한 경우 
                    else throw new Exception("수정 업데이트 실패!!\r\n담당자에게 문의 하시기 바랍니다.");
                }

                Log.Information(Logger.GetMethodPath(currentMethod) + "TestMEPUpdater Execute 완료");
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
            return "TestMEPUpdater";
        }

        #endregion 기본 메소드 

        #region RegisterUpdater

        /// <summary>
        /// 업데이터 등록 
        /// </summary>
        private void RegisterUpdater(Document rvDoc, UpdaterId pUpdaterId)
        {
            var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

            try
            {
                Log.Information(Logger.GetMethodPath(currentMethod) + "업데이터 등록 시작");

                if (UpdaterRegistry.IsUpdaterRegistered(pUpdaterId, rvDoc))   // Revit 문서(rvDoc)에 해당 pUpdaterId를 가진 업데이터가 등록된 경우 
                {
                    UpdaterRegistry.RemoveAllTriggers(pUpdaterId);           // 지정된 pUpdaterId를 가진 업데이터와 연결된 모든 트리거 제거. 업데이터 등록을 취소하지 않음.
                    UpdaterRegistry.UnregisterUpdater(pUpdaterId, rvDoc);     // Revit 문서(rvDoc)에 지정된 pUpdaterId를 가진 업데이터와 연결된 업데이터 프로그램 등록 취소 (해당 트리거 포함 레지스트리에서 완전 제거 처리)
                }

                UpdaterRegistry.RegisterUpdater(this, rvDoc);   // 업데이터 등록

                Log.Information(Logger.GetMethodPath(currentMethod) + "업데이터 등록 완료");

                TaskDialog.Show("테스트 Simple Updater", "테스트 업데이터 등록 완료");
            }
            catch (Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                throw;   // 오류 발생시 상위 호출자 예외처리 전달
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

                string builtInCategoryName = LabelUtils.GetLabelFor(builtInCategory);   // BuiltInCategory 이름 가져오기 

                // 해당 업데이터 아이디가 존재하고, 업데이터가 등록되어 있는 경우 
                if (pUpdaterId is not null
                    && UpdaterRegistry.IsUpdaterRegistered(pUpdaterId))
                {
                    var changeTypeAny = Element.GetChangeTypeAny();                                       // 객체가 수정 방식으로 업데이터 트리거 추가 하려면 해당 변경 유형 사용
                    UpdaterRegistry.AddTrigger(pUpdaterId, pElementCategoryFilter, changeTypeAny);        // 지정된 pUpdaterId와 연결된 모든 문서에 대해 지정된 요소 필터(pElementCategoryFilter) 및 changeTypeAny을 이용해서 수정 트리거 추가

                    var changeTypeAddition = Element.GetChangeTypeElementAddition();                      // 객체가 새로 추가된 방식으로 업데이터 트리거 추가 하려면 해당 변경 유형 사용
                    UpdaterRegistry.AddTrigger(pUpdaterId, pElementCategoryFilter, changeTypeAddition);   // 지정된 pUpdaterId와 연결된 모든 문서에 대해 지정된 요소 필터(pElementCategoryFilter) 및 changeTypeAddition을 이용해서 새로 추가 트리거 추가

                    Log.Information(Logger.GetMethodPath(currentMethod) + $"테스트 {builtInCategoryName} Triggers 등록 완료");
                    TaskDialog.Show("테스트 Simple Updater", $"테스트 {builtInCategoryName} Triggers 등록 완료");
                }

                // 해당 업데이터 아이디가 존재하지 않거나 업데이터가 등록되어 있지 않은 경우 
                else throw new Exception($"테스트 {builtInCategoryName} Triggers 등록 실패!\r\n담당자에게 문의하세요.");
            }
            catch (Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                throw;   // 오류 발생시 상위 호출자 예외처리 전달
            }
        }

        #endregion RegisterTriggers

        #region Sample

        #endregion Sample
    }
}
