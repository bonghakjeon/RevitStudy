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

namespace SimpleUpdaterExample.Common.UpdaterBase
{
    // TODO : Revit AddIn 개발 소스를 비쥬얼스튜디오 2022 버전 - .net Core 8.0을 사용하려면 Revit 2025 버전 부터 사용이 가능하므로 현 시점에서 해당 소스는 .net FrameWork 4.8에서만 구동시킬 수 있다. (2024.03.11 jbh)

    /// <summary>
    /// RevitBox 업데이트 
    /// </summary>
    public class SimpleUpdater : IUpdater
    {
        #region 프로퍼티 

        /// <summary>
        /// 업데이트 아이디 생성시 필요한 GUID 문자열 프로퍼티
        /// </summary>
        private const string Id = "d42d28af-d2cd-4f07-8873-e7cfb61903d8";

        /// <summary>
        /// 업데이트 아이디
        /// </summary>
        // private UpdaterId Updater_Id { get; }
        private UpdaterId Updater_Id { get; set; }


        /// <summary>
        /// 카테고리 필터
        /// </summary>
        private ElementCategoryFilter ElementCategoryFilter { get; set; }


        #endregion 프로퍼티 

        #region 생성자 

        public SimpleUpdater(Document pDoc, AddInId pAddInId)
        {
            Guid guId = new Guid(Id);
            Updater_Id = new UpdaterId(pAddInId, guId);
            ElementCategoryFilter = new ElementCategoryFilter(BuiltInCategory.OST_Walls);

            RegisterUpdater(pDoc, Updater_Id);                          // 업데이트 등록 
            RegisterTriggers(Updater_Id, ElementCategoryFilter);        // Triggers 등록 
        }

        #endregion 생성자 

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

                var doc = data.GetDocument();                            // UpdaterData 클래스 객체 data와 연관된 Document 개체 반환

                var ids = data.GetModifiedElementIds();                  // 활성화된 Revit 문서에서 수정된 객체 집합 반환 

                var names = ids.Select(id => doc.GetElement(id).Name);   // 수정된 객체 집합에서 객체 이름만 추출

                TaskDialog.Show("Simple Updater", string.Join<string>(",", names));    // 수정된 객체 집합의 이름 목록 메세지 출력 

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
        /// 업데이트 등록 
        /// </summary>
        private void RegisterUpdater(Document pDoc, UpdaterId pUpdaterId)
        {
            var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

            try
            {
                Log.Information(Logger.GetMethodPath(currentMethod) + "업데이트 등록 시작");

                if (UpdaterRegistry.IsUpdaterRegistered(pUpdaterId, pDoc))  // Revit 문서에 해당 ID를 가진 업데이터가 등록된 경우 
                {
                    UpdaterRegistry.RemoveAllTriggers(pUpdaterId);          // 지정된 ID를 가진 업데이터와 연결된 모든 트리거 제거 
                    UpdaterRegistry.UnregisterUpdater(pUpdaterId, pDoc);    // 지정된 ID를 가진 업데이터와 연결된 업데이트 프로그램 등록 취소 (해당 트리거 포함 레지스트리에서 완전 제거 처리)
                }

                UpdaterRegistry.RegisterUpdater(this, pDoc);

                Log.Information(Logger.GetMethodPath(currentMethod) + "업데이트 등록 완료");
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

                // 해당 업데이터 아이디가 존재하고, 업데이터가 등록되어 있는 경우 
                if (pUpdaterId is not null
                    && UpdaterRegistry.IsUpdaterRegistered(pUpdaterId))
                {
                    var changeType = Element.GetChangeTypeAny();     // 객체가 어떤 방식으로든 업데이트될 때 업데이터를 트리거하려면 해당 변경 유형 사용

                    UpdaterRegistry.RemoveAllTriggers(pUpdaterId);   // 지정된 UpdaterId를 사용하여 업데이터와 연결된 모든 트리거를 제거. 업데이터 등록을 취소하지 않음.
                    UpdaterRegistry.AddTrigger(pUpdaterId, pElementCategoryFilter, changeType);   // 지정된 UpdaterId와 연결된 모든 문서에 대해 지정된 요소 필터(pElementCategoryFilter) 및 ChangeType을 이용해서 트리거 추가
                }

                Log.Information(Logger.GetMethodPath(currentMethod) + "Triggers 등록 완료");
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
