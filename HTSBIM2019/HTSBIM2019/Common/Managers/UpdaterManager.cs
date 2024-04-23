using Serilog;
using System;
using System.Reflection;
using System.Collections.Generic;

using HTSBIM2019.Common.LogBase;
using HTSBIM2019.Models.HTSBase.MEPUpdater;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace HTSBIM2019.Common.Managers
{
    public class UpdaterManager
    {
        #region RegisterUpdater

        // TODO : static 메서드 "RegisterUpdater" 필요시 추가 수정 및 사용 예정 (2024.03.22 jbh)
        /// <summary>
        /// Revit MEP 업데이터 등록
        /// </summary>
        // public static void RegisterUpdater(IUpdater pUpdaterForm, Document rvDoc, UpdaterId rvUpdaterId)
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
        //     catch(Exception ex)
        //     {
        //         Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
        //         throw;   // 오류 발생시 상위 호출자 예외처리 전달
        //     }
        // }

        #endregion RegisterUpdater

        #region RegisterTriggers

        /// <summary>
        /// Revit MEP Triggers 등록
        /// </summary>
        //public static void RegisterTriggers(UpdaterId rvUpdaterId, Document rvDoc, ElementCategoryFilter rvElementCategoryFilter)
        //public static void RegisterTriggers(UpdaterId rvUpdaterId, ElementCategoryFilter rvElementCategoryFilter, string rvBuiltInCategoryName)
        // public static void RegisterTriggers(UpdaterId rvUpdaterId, ElementCategoryFilter rvElementCategoryFilter, List<CategoryInfoView> rvCategoryInfoList)
        public static void RegisterTriggers(UpdaterId rvUpdaterId, List<CategoryInfoView> rvCategoryInfoList)
        {
            var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

            try
            {
                // TODO : 카테고리 필터 객체(rvElementCategoryFilter)의 BuiltInCategory 가져오기 (2024.04.01 jbh)
                // 참고 URL - https://chat.openai.com/c/32ce8d83-d39a-48d7-af9a-44c408b64fe0
                // BuiltInCategory builtInCategory = (BuiltInCategory)rvElementCategoryFilter.CategoryId.IntegerValue;

                // 해당 업데이터 아이디가 존재하고, 업데이터가 등록되어 있는 경우 
                // if (rvUpdaterId is not null
                //     && UpdaterRegistry.IsUpdaterRegistered(rvUpdaterId))
                // 해당 업데이터 아이디가 존재하는 경우 
                if(rvUpdaterId is not null)
                {
                    foreach(CategoryInfoView categoryInfo in rvCategoryInfoList)
                    {
                        Log.Information(Logger.GetMethodPath(currentMethod) + $"테스트 {categoryInfo.CategoryName} Triggers 등록 시작");
                
                        ElementCategoryFilter categoryFilter = new ElementCategoryFilter(categoryInfo.Category);
                
                        // TODO : Revit API 메서드 "Element.GetChangeTypeGeometry()" 사용해서 객체 위치만 변경 되었을 때 실행되는 업데이터 트리거 추가 구현 (2024.03.27 jbh)
                        // 참고 URL - https://www.revitapidocs.com/2018/45751c5b-6d10-657a-a017-04219d1a5ac8.htm
                        ChangeType changeTypeGeometry = Element.GetChangeTypeGeometry();                         // 객체가 수정 방식(객체 위치변경만 해당 / 객체 자체의 속성값 변경은 해당되지 않음.)으로 업데이터 트리거 추가 하려면 해당 변경 유형 사용
                        UpdaterRegistry.AddTrigger(rvUpdaterId, categoryFilter, changeTypeGeometry);      // 지정된 rvUpdaterId와 연결된 모든 문서에 대해 지정된 요소 필터(rvElementCategoryFilter) 및 changeTypeAny을 이용해서 수정 트리거 추가
                
                        // TODO : Revit API 메서드 "Element.GetChangeTypeAny()" 사용해서 객체 위치 + 속성값 변경 되었을 때 실행되는 업데이터 트리거 추가 구현 (2024.03.27 jbh)
                        // ChangeType changeTypeAny = Element.GetChangeTypeAny();                                // 객체가 수정 방식(객체 위치 변경 및 객체 자체의 속성값 변경 모두 포함)으로 업데이터 트리거 추가 하려면 해당 변경 유형 사용
                        // UpdaterRegistry.AddTrigger(rvUpdaterId, rvElementCategoryFilter, changeTypeAny);        // 지정된 rvUpdaterId와 연결된 모든 문서에 대해 지정된 요소 필터(rvElementCategoryFilter) 및 changeTypeAny을 이용해서 수정 트리거 추가
                
                        ChangeType changeTypeAddition = Element.GetChangeTypeElementAddition();                  // 객체가 새로 추가된 방식으로 업데이터 트리거 추가 하려면 해당 변경 유형 사용
                        UpdaterRegistry.AddTrigger(rvUpdaterId, categoryFilter, changeTypeAddition);      // 지정된 rvUpdaterId와 연결된 모든 문서에 대해 지정된 요소 필터(rvElementCategoryFilter) 및 changeTypeAddition을 이용해서 새로 추가 트리거 추가
                
                        Log.Information(Logger.GetMethodPath(currentMethod) + $"테스트 {categoryInfo.CategoryName} Triggers 등록 완료");
                        TaskDialog.Show("테스트 MEP Updater", $"테스트 {categoryInfo.CategoryName} Triggers 등록 완료");
                    }
                }
                
                // 해당 업데이터 아이디가 존재하지 않거나 업데이터가 등록되어 있지 않은 경우 
                else throw new Exception($"테스트 MEP 업데이터 등록 실패!\r\n담당자에게 문의하세요.");
            }
            catch(Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                throw;   // 오류 발생시 상위 호출자 예외처리 전달
            }
        }

        #endregion RegisterTriggers

        #region RemoveSetting

        // TODO : static 메서드 "RemoveSetting" 필요시 추가 수정 및 사용 예정 (2024.03.22 jbh)
        /// <summary>
        /// Revit MEP 업데이터 + Triggers 해제 
        /// </summary>
        // public static void RemoveSetting(Document rvDoc, UpdaterId rvUpdaterId)
        // {
        //     var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록
        // 
        //     try
        //     {
        //         if(UpdaterRegistry.IsUpdaterRegistered(rvUpdaterId, rvDoc))   // Revit 문서(rvDoc)에 해당 rvUpdaterId를 가진 업데이터가 등록된 경우 
        //         {
        //             Log.Information(Logger.GetMethodPath(currentMethod) + " Revit MEP 업데이터 + Triggers 해제 시작");
        // 
        //             UpdaterRegistry.RemoveAllTriggers(rvUpdaterId);            // 지정된 rvUpdaterId를 가진 업데이터와 연결된 모든 트리거 제거. 업데이터 등록을 취소하지 않음.
        //             UpdaterRegistry.UnregisterUpdater(rvUpdaterId, rvDoc);     // Revit 문서(rvDoc)에 지정된 rvUpdaterId를 가진 업데이터와 연결된 업데이터 프로그램 등록 취소 (해당 트리거 포함 레지스트리에서 완전 제거 처리)
        // 
        //             Log.Information(Logger.GetMethodPath(currentMethod) + " Revit MEP 업데이터 + Triggers 해제 완료");
        //         }
        //     }
        //     catch(Exception ex)
        //     {
        //         Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
        //         throw;   // 오류 발생시 상위 호출자 예외처리 전달
        //     }
        // }

        #endregion RemoveSetting

        #region Sample

        #endregion Sample
    }
}
