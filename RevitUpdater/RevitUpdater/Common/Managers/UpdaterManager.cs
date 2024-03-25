using Serilog;
using System;
using System.Reflection;

using RevitUpdater.Common.LogBase;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace RevitUpdater.Common.Managers
{
    public class UpdaterManager
    {
        // TODO : static 메서드 "RegisterUpdater" 필요시 추가 수정 및 사용 예정 (2024.03.22 jbh)
        /// <summary>
        /// Revit MEP 업데이터 등록
        /// </summary>
        // public static void RegisterUpdater(IUpdater pUpdaterForm, Document rvDoc, UpdaterId pUpdaterId)
        // {
        //     var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록
        // 
        //     try
        //     {
        //         Log.Information(Logger.GetMethodPath(currentMethod) + "업데이터 등록 시작");
        // 
        // 
        //         UpdaterRegistry.RegisterUpdater(pUpdaterForm, rvDoc);   // 인터페이스 IUpdater를 상속받는 폼 객체에 업데이터 등록
        // 
        //         Log.Information(Logger.GetMethodPath(currentMethod) + "업데이터 등록 완료");
        // 
        //         TaskDialog.Show("테스트 MEP Updater", "테스트 업데이터 등록 완료");
        //     }
        //     catch (Exception ex)
        //     {
        //         Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
        //         throw;   // 오류 발생시 상위 호출자 예외처리 전달
        //     }
        // }

        /// <summary>
        /// Revit MEP Triggers 등록
        /// </summary>
        public static void RegisterTriggers(UpdaterId pUpdaterId, ElementCategoryFilter pElementCategoryFilter)
        {
            var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

            try
            {
                BuiltInCategory builtInCategory = (BuiltInCategory)pElementCategoryFilter.CategoryId.Value;

                string builtInCategoryName = LabelUtils.GetLabelFor(builtInCategory);   // BuiltInCategory 이름 가져오기 

                // 해당 업데이터 아이디가 존재하고, 업데이터가 등록되어 있는 경우 
                if (pUpdaterId is not null
                    && UpdaterRegistry.IsUpdaterRegistered(pUpdaterId))
                {
                    Log.Information(Logger.GetMethodPath(currentMethod) + $"테스트 {builtInCategoryName} Triggers 등록 시작");

                    var changeTypeAny = Element.GetChangeTypeAny();                                       // 객체가 수정 방식으로 업데이터 트리거 추가 하려면 해당 변경 유형 사용
                    UpdaterRegistry.AddTrigger(pUpdaterId, pElementCategoryFilter, changeTypeAny);        // 지정된 pUpdaterId와 연결된 모든 문서에 대해 지정된 요소 필터(pElementCategoryFilter) 및 changeTypeAny을 이용해서 수정 트리거 추가

                    var changeTypeAddition = Element.GetChangeTypeElementAddition();                      // 객체가 새로 추가된 방식으로 업데이터 트리거 추가 하려면 해당 변경 유형 사용
                    UpdaterRegistry.AddTrigger(pUpdaterId, pElementCategoryFilter, changeTypeAddition);   // 지정된 pUpdaterId와 연결된 모든 문서에 대해 지정된 요소 필터(pElementCategoryFilter) 및 changeTypeAddition을 이용해서 새로 추가 트리거 추가

                    Log.Information(Logger.GetMethodPath(currentMethod) + $"테스트 {builtInCategoryName} Triggers 등록 완료");
                    TaskDialog.Show("테스트 MEP Updater", $"테스트 {builtInCategoryName} Triggers 등록 완료");
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

        // TODO : static 메서드 "RemoveSetting" 필요시 추가 수정 및 사용 예정 (2024.03.22 jbh)
        /// <summary>
        /// Revit MEP 업데이터 + Triggers 해제 
        /// </summary>
        // public static void RemoveSetting(Document rvDoc, UpdaterId pUpdaterId)
        // {
        //     var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록
        // 
        //     try
        //     {
        //         if (UpdaterRegistry.IsUpdaterRegistered(pUpdaterId, rvDoc))   // Revit 문서(rvDoc)에 해당 pUpdaterId를 가진 업데이터가 등록된 경우 
        //         {
        //             Log.Information(Logger.GetMethodPath(currentMethod) + " Revit MEP 업데이터 + Triggers 해제 시작");
        // 
        //             UpdaterRegistry.RemoveAllTriggers(pUpdaterId);            // 지정된 pUpdaterId를 가진 업데이터와 연결된 모든 트리거 제거. 업데이터 등록을 취소하지 않음.
        //             UpdaterRegistry.UnregisterUpdater(pUpdaterId, rvDoc);     // Revit 문서(rvDoc)에 지정된 pUpdaterId를 가진 업데이터와 연결된 업데이터 프로그램 등록 취소 (해당 트리거 포함 레지스트리에서 완전 제거 처리)
        // 
        //             Log.Information(Logger.GetMethodPath(currentMethod) + " Revit MEP 업데이터 + Triggers 해제 완료");
        //         }
        //     }
        //     catch (Exception ex)
        //     {
        //         Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
        //         throw;   // 오류 발생시 상위 호출자 예외처리 전달
        //     }
        // }
    }
}
