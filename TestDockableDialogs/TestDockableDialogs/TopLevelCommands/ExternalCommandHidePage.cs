using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

using TestDockableDialogs.Application;

namespace TestDockableDialogs.TopLevelCommands
{
    // TODO : TransactionAttribute - TransactionMode.Manual 설정 구현 (2024.08.09 jbh)
    // 참고 URL - https://manzoo.tistory.com/107
    [Transaction(TransactionMode.Manual)]       // Transaction 사용이 필요한 경우 (Document 내에서 수정/삭제/추가 등의 작업이 필요한 경우)
    [Regeneration(RegenerationOption.Manual)]
    /// <summary>
    /// Dockable Window 숨기기(Hide) Command
    /// </summary>
    ----- public class ExternalCommandHidePage : IExternalCommand, IExternalCommandAvailability
    {
        /// <summary>
        /// Command - 숨기기(Hide) 실행 
        /// </summary>
        public virtual Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            try
            {
                // Revit 외부 입력 애드인 프로그램 실행시 
                // 필요한 기본적인 기능(인터페이스)을 관리하는 APIUtility 클래스 객체 m_APIUtility 가져와서 
                // APIUtility 클래스 인스턴스 메서드 Initialize 호출 -> UIApplication 클래스 객체 m_uiApplication 초기화 처리 
                ThisApplication.thisApp.GetDockableAPIUtility().Initialize(commandData.Application);

                // ThisApplication 인스턴스 메서드 "SetWindowVisibility" 호출 -> Dockable Window 숨기기 처리 (Hide)
                ThisApplication.thisApp.SetWindowVisibility(commandData.Application, false);
            }
            catch(Exception ex)
            {
                TaskDialog.Show("Dockable Dialogs", "Dialog not registered.");
            }
            return Result.Succeeded;
        }

        /// <summary>
        /// 숨기기(Hide) Command 실행 가능/불가능 여부 
        /// Dockable Window는 Revit 응용 프로그램상에 열려 있지 않을 때만 표시(Show)
        /// Dockable Window는 Revit 응용 프로그램상에 열려 있지 않을 때만 등록 가능(Register)
        /// Onlys show the dockable dialog when a document is not open, 
        /// as Dockable dialogs should only be registered when no documents are open.
        /// </summary>
        public bool IsCommandAvailable(UIApplication applicationData, CategorySet selectedCategories)
        {
            // Revit 응용 프로그램상에 Dockable Window가 등록되지 않은 경우 
            if(applicationData.ActiveUIDocument is null) return false;  // Dockable Window 숨기기(Hide) Command 실행 불가 

            // Revit 응용 프로그램상에 Dockable Window가 등록된 경우 
            else return true;   // Dockable Window 숨기기(Hide) Command 실행 가능
        }
    }
}
