using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

using TestDockableDialogs.Application;
using TestDockableDialogs.Utility;

namespace TestDockableDialogs.TopLevelCommands
{
    // TODO : TransactionAttribute - TransactionMode.Manual 설정 구현 (2024.08.09 jbh)
    // 참고 URL - https://manzoo.tistory.com/107
    [Transaction(TransactionMode.Manual)]       // Transaction 사용이 필요한 경우 (Document 내에서 수정/삭제/추가 등의 작업이 필요한 경우)
    [Regeneration(RegenerationOption.Manual)]
    /// <summary>
    /// Dockable Window 등록(Register) Command
    /// </summary>
    public class ExternalCommandRegisterPage : IExternalCommand, IExternalCommandAvailability
    {
        /// <summary>
        /// Command - 등록(Register) 실행 
        /// </summary>
        public virtual Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            try
            {
                // Revit 외부 입력 애드인 프로그램 실행시 
                // 필요한 기본적인 기능(인터페이스)을 관리하는 APIUtility 클래스 객체 m_APIUtility 가져와서 
                // APIUtility 클래스 인스턴스 메서드 Initialize 호출 -> UIApplication 클래스 객체 m_uiApplication 초기화 처리 
                ThisApplication.thisApp.GetDockableAPIUtility().Initialize(commandData.Application);


                // ThisApplication 클래스 인스턴스 메서드 CreateWindow 호출하여 
                // Revit 응용 프로그램 화면에 Docking할 새로운 WPF Window Page 생성  
                ThisApplication.thisApp.CreateWindow();

                DockingSetupDialog dlg = new DockingSetupDialog();   // DockingSetupDialog Window 객체 dlg 생성 
                Nullable<bool> dlgResult = dlg.ShowDialog();         // Modal 형식으로 DockingSetupDialog 화면 출력 
                if(false == dlgResult) return Result.Succeeded;      // DialogResult 값이 false 이면 Result.Succeeded 리턴

                // ThisApplication 클래스 인스턴스 메서드 GetMainWindow 호출 -> Dockable Window 객체 프로퍼티 (MainPage m_mainPage) 가져오기 
                // Dockable Window 객체 프로퍼티 (MainPage m_mainPage) 인스턴스 메서드 SetInitialDockingParameters 호출 
                // -> Dockable Window에 필요한 초기값 설정하기 
                ThisApplication.thisApp.GetMainWindow().SetInitialDockingParameters(dlg.FloatLeft, dlg.FloatRight, dlg.FloatTop, dlg.FloatBottom, dlg.DockPosition, dlg.TargetGuid);

                // ThisApplication 클래스 인스턴스 메서드 RegisterDockableWindow 호출 -> Dockable Window 등록
                ThisApplication.thisApp.RegisterDockableWindow(commandData.Application, dlg.MainPageGuid);
            }
            catch(Exception ex)
            {
                TaskDialog.Show(Globals.ApplicationName, ex.Message);
            }
            return Result.Succeeded;
        }

        /// <summary>
        /// 등록(Register) Command 실행 가능/불가능 여부 
        /// Dockable Window는 Revit 응용 프로그램상에 등록되어 있을 때만 사용 가능 
        /// Dockable Window는 Revit 응용 프로그램상에 등록되어 있을 때만 표시(Show) 가능
        /// Onlys show the dialog when a document is open, as Dockable dialogs are only available
        /// when a document is open.
        /// </summary>
        public bool IsCommandAvailable(UIApplication applicationData, CategorySet selectedCategories)
        {
            // Revit 응용 프로그램상에 Dockable Window가 등록되지 않은 경우 
            if(applicationData.ActiveUIDocument is null) return true;   // Dockable Window 등록(Register) Command 실행 가능 

            // Revit 응용 프로그램상에 Dockable Window가 등록된 경우 
            else return false;   // Dockable Window 등록(Register) Command 실행 불가
        }
    }
}
