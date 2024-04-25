using Serilog;
using System;
using System.Reflection;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

using RevitUpdater.Common.LogBase;
using RevitUpdater.Common.UpdaterBase;
using RevitUpdater.Common.Managers;

using RevitUpdater.UI.MEPUpdater;

namespace RevitUpdater.Common.RequestBase
{
    public class TestRequestHandler : IExternalEventHandler
    {
        #region 프로퍼티

        #endregion 프로퍼티

        #region Execute

        public void Execute(UIApplication app)
        {
            throw new NotImplementedException();
        }

        #endregion Execute

        #region GetName

        public string GetName()
        {
            throw new NotImplementedException();
        }

        #endregion GetName
    }

    /// <summary>
    /// Modaless 폼(.Show()) 형식에 의해 발생하는 외부 요청 핸들러
    /// </summary>
    public class MEPUpdaterRequestHandler : IExternalEventHandler
    {
        #region 프로퍼티 

        /// <summary>
        /// Modaless 폼(.Show()) 최근 요청 값에 엑세스 하기위한 프로퍼티
        /// </summary>
        public MEPUpdaterRequest Request { get { return _Request; } }
        private MEPUpdaterRequest _Request = new MEPUpdaterRequest();

        // TODO : 폼 화면(MEPUpdater.cs) 출력시 .ShowDialog(Modal - 부모 창 제어 X)하는 방식이 아니라
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
        /// 업데이터 아이디
        /// </summary>
        // private UpdaterId Updater_Id { get; }
        private UpdaterId Updater_Id { get; set; }

        /// <summary>
        /// 카테고리(객체) 필터 (카테고리(객체) 대상 - 배관, 배관 부속류, 단열재, 배관 밸브류)
        /// </summary>
        private ElementCategoryFilter MEPCategoryFilter { get; set; }

        // TODO : 아래 주석친 프로퍼티 카테고리 필터 (배관 - OST_PipeCurves) 필요시 참고 (2024.03.27 jbh)
        /// <summary>
        /// 카테고리 필터 (배관 - OST_PipeCurves)
        /// </summary>
        // private ElementCategoryFilter PipeCurvesCategoryFilter { get; set; }


        // TODO : 아래 주석친 프로퍼티 카테고리 필터 (배관 부속류 - OST_PipeFitting) 필요시 참고 (2024.03.27 jbh)
        /// <summary>
        /// 카테고리 필터 (배관 부속류 - OST_PipeFitting)
        /// </summary>
        // private ElementCategoryFilter PipeFittingCategoryFilter { get; set; }

        #endregion 프로퍼티

        #region 생성자

        public MEPUpdaterRequestHandler()
        {

        }

        #endregion 생성자

        #region GetName

        /// <summary>
        /// 해당 외부 이벤트 핸들러(MEPUpdaterRequestHandler)(이벤트 메서드) 이름으로 식별 
        /// </summary>
        public string GetName()
        {
            return UpdaterHelper.MEPUpdaterFormName;
        }

        #endregion GetName

        #region Execute

        /// <summary>
        /// Modaless 폼(.Show()) 형식에 의해 발생하는 외부 이벤트 메서드(외부 이벤트 핸들러 - MEPUpdaterRequestHandler) 
        /// </summary>
        /// <param name="app"></param>
        public void Execute(UIApplication rvUIApp)
        {
            bool isUpdaterRegistered = false;                         // Revit MEP 업데이터 + Triggers 등록 여부                  
            var currentMethod      = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록 

            try
            {
                Log.Information(Logger.GetMethodPath(currentMethod) + "메서드 Execute 시작");

                // 1. Revit 문서 프로퍼티 "RevitDoc" 할당 
                RevitDoc = rvUIApp.ActiveUIDocument.Document;    // 활성화된 Revit 문서 

                // 2. RevitBox 업데이터 Command 아이디 값 프로퍼티 "Updater_Id" 할당 
                AddInId addInId = rvUIApp.ActiveAddInId;         // RevitBox 업데이터 Command 아이디
                Guid guId  = new Guid(UpdaterHelper.GId);
                Updater_Id = new UpdaterId(addInId, guId);

                // 해당 Transaction이 끝날 때까지는 화면 상에서는 다른 기능을 실행할 수 있고 다른 기능의 화면도 출력되지만
                // 다른 기능을 실행해서 데이터를 변경할 수 없다.(다른 작업이나 Command 명령이 끼어들 수 없다.)
                using(Transaction transaction = new Transaction(RevitDoc))
                {
                    Log.Information(Logger.GetMethodPath(currentMethod) + "Request 작업 시작");

                    // transaction.Start(AABIMHelper.Start); 부터 transaction.Commit(); 까지가 연산처리를 하는 하나의 작업단위이다.
                    transaction.Start(UpdaterHelper.Start);  // 해당 "AABIM2024" 프로젝트에서 연산처리(객체 생성, 정보 변경 및 삭제 등등... ) 시작


                    // 3. 인터페이스 "IUpdater" 상속 받는 폼 객체 "MEPUpdater" 찾기 
                    MEPUpdaterForm mepUpdaterForm = (MEPUpdaterForm)FormManager.GetForm(typeof(IUpdater), typeof(MEPUpdaterForm));

                    // Revit MEP 업데이터 + Triggers가 등록되어 있는 경우 true 리턴 / Revit MEP 업데이터 + Triggers가 이미 해제되어 있는 경우 false 리턴 
                    isUpdaterRegistered = UpdaterRegistry.IsUpdaterRegistered(Updater_Id, RevitDoc);   // Revit 문서(rvDoc)에 해당 pUpdaterId를 가진 업데이터가 등록된 경우 

                    // TODO : 부모 폼(Revit)의 쓰레드를 자식 폼(MEPUpdater)이 제어할 수 있도록 구현 예정 (2024.03.15 jbh)
                    EnumMEPUpdaterRequestId requestIdValue = Request.Take();

                    switch(requestIdValue)
                    {
                        case EnumMEPUpdaterRequestId.NONE:   // 요청이 없는 경우 -> 즉시 종료
                            return;

                        case EnumMEPUpdaterRequestId.REGISTER:
                            if(true == isUpdaterRegistered) RemoveMEP(RevitDoc, Updater_Id);
                            RegisterMEP(mepUpdaterForm, RevitDoc, Updater_Id);
                            break;

                        case EnumMEPUpdaterRequestId.REMOVE:
                            if(true == isUpdaterRegistered) RemoveMEP(RevitDoc, Updater_Id);
                            // Revit MEP 업데이터 + Triggers가 이미 해제되어 있는 경우 
                            else TaskDialog.Show("테스트 MEP Updater", "MEP 업데이터 + Triggers 이미 해제 완료되었습니다.");
                            break;

                        default:
                            TaskDialog.Show(UpdaterHelper.NoticeTitle, $"요청 아이디{requestIdValue}이/가 존재하지 않습니다.\r\n담당자에게 문의하시기 바랍니다.");
                            break;
                    }
                    transaction.Commit();    // 해당 "AABIM2024" 프로젝트에서 연산처리(객체 생성, 정보 변경 및 삭제 등등... )된 결과 커밋

                    Log.Information(Logger.GetMethodPath(currentMethod) + "Request 작업 완료");
                }   // 여기서 Dispose (리소스 해제) 처리 

                Log.Information(Logger.GetMethodPath(currentMethod) + "메서드 Execute 완료");
            }
            catch(Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                TaskDialog.Show(UpdaterHelper.ErrorTitle, ex.Message);
            }
            finally
            {
                CmdMEPUpdater.WakeFormUp();
            }
        }

        #endregion Execute

        #region RemoveSetting

        /// <summary>
        /// Revit MEP 업데이터 + Triggers 해제 
        /// </summary>
        //private void RemoveMEP(MEPUpdater pMEPUpdaterForm, Document rvDoc, UpdaterId pUpdaterId)
        private void RemoveMEP(Document rvDoc, UpdaterId pUpdaterId)
        {
            var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

            try
            {
                // TODO : static 메서드 "UpdaterManager.RemoveSetting" 필요시 추가 수정 및 사용 예정 (2024.03.22 jbh)
                // UpdaterManager.RemoveSetting(rvDoc, pUpdaterId);   // Revit MEP 업데이터 + Triggers 해제 

                Log.Information(Logger.GetMethodPath(currentMethod) + "Revit MEP 업데이터 + Triggers 해제 시작");

                UpdaterRegistry.RemoveAllTriggers(pUpdaterId);            // 지정된 pUpdaterId를 가진 업데이터와 연결된 모든 트리거 제거. 업데이터 등록을 취소하지 않음.
                UpdaterRegistry.UnregisterUpdater(pUpdaterId, rvDoc);     // Revit 문서(rvDoc)에 지정된 pUpdaterId를 가진 업데이터와 연결된 업데이터 프로그램 등록 취소 (해당 트리거 포함 레지스트리에서 완전 제거 처리)

                Log.Information(Logger.GetMethodPath(currentMethod) + "Revit MEP 업데이터 + Triggers 해제 완료");
                TaskDialog.Show("테스트 MEP Updater", "MEP 업데이터 + Triggers 해제 완료");
            }
            catch(Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                throw;   // 오류 발생시 상위 호출자 예외처리 전달 throw 
            }
        }

        #endregion RemoveSetting

        #region RegisterSetting

        /// <summary>
        /// Revit MEP 업데이터 + Triggers 등록 
        /// </summary>
        private void RegisterMEP(MEPUpdaterForm pMEPUpdaterForm, Document rvDoc, UpdaterId pUpdaterId)
        {
            var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록
        
            try
            {
                Log.Information(Logger.GetMethodPath(currentMethod) + "Revit MEP 업데이터 + Triggers 등록 셋팅 시작");

                // 해당 메서드 "UpdaterRegistry.RegisterUpdater" 호출시 파라미터로 넘길 인자로 넘길 때, 
                // 인터페이스 IUpdater를 상속 받는 MEPUpdater 클래스 객체를 넘기도록 구현하기 

                // 1. Revit MEP 업데이터 등록 
                // TODO : static 메서드 "UpdaterManager.RegisterUpdater" 필요시 추가 수정 및 사용 예정 (2024.03.22 jbh)
                // UpdaterManager.RegisterUpdater(pMEPUpdaterForm, rvDoc, pUpdaterId);
                Log.Information(Logger.GetMethodPath(currentMethod) + "업데이터 등록 시작");

                UpdaterRegistry.RegisterUpdater(pMEPUpdaterForm, rvDoc);   // 인터페이스 IUpdater를 상속받는 폼 객체에 업데이터 등록

                Log.Information(Logger.GetMethodPath(currentMethod) + "업데이터 등록 완료");
                TaskDialog.Show("테스트 MEP Updater", "MEP 업데이터 등록 완료");

                BuiltInCategory updaterBuiltInCategory = pMEPUpdaterForm.CategoryInfo.Category;

                // 2. 카테고리 ComboBox 컨트롤(comboBoxCategory)에서 선택한
                // 카테고리 객체 (BupdaterBuiltInCategory)만 필터링 처리 및 MEP Triggers 등록 
                MEPCategoryFilter = new ElementCategoryFilter(updaterBuiltInCategory);
                UpdaterManager.RegisterTriggers(pUpdaterId, MEPCategoryFilter);

                // TODO : 아래 주석친 테스트 코드 필요시 참고 (2024.03.27 jbh)
                // 객체 "배관"(BuiltInCategory.OST_PipeCurves)만 필터링 처리 및 MEP Triggers 등록 
                // PipeCurvesCategoryFilter  = new ElementCategoryFilter(BuiltInCategory.OST_PipeCurves);
                // UpdaterManager.RegisterTriggers(pUpdaterId, PipeCurvesCategoryFilter);

                // 객체 "배관 부속류"(BuiltInCategory.OST_PipeFitting)만 필터링 처리 및 MEP Triggers 등록 
                //PipeFittingCategoryFilter = new ElementCategoryFilter(BuiltInCategory.OST_PipeFitting);
                //UpdaterManager.RegisterTriggers(pUpdaterId, PipeFittingCategoryFilter);   

                Log.Information(Logger.GetMethodPath(currentMethod) + "Revit MEP 업데이터 + Triggers 등록 셋팅 완료");
            }
            catch(Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                throw;   // 오류 발생시 상위 호출자 예외처리 전달 throw 
            }
        }

        #endregion RegisterSetting
    }
}
