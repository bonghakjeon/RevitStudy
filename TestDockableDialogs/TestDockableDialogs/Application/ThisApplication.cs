using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI;
using TestDockableDialogs.TopLevelCommands;
using TestDockableDialogs.Utility;

namespace TestDockableDialogs.Application
{
    /// <summary>
    /// Implements the Revit add-in interface IExternalApplication
    /// Revit 외부 입력 애드인 프로그램(TestDockableDialogs)에 필요한 기능(인터페이스) 구현한 클래스 
    /// </summary>
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class ThisApplication : IExternalApplication
    {
        public ThisApplication()
        {

        }

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }

        /// <summary>
        /// Dockable Window 등록 1
        /// Register a dockable Window
        /// 메서드 파라미터 UIApplication application
        /// </summary>
        public void RegisterDockableWindow(UIApplication application, Guid mainPageGuid)
        {
            // DockablePaneId 클래스 객체 Globals.sm_UserDockablePaneId 생성하여
            // Revit 응용 프로그램에 새로 추가하고자 하는 Dockable Window 식별자(Guid) 생성
            Globals.sm_UserDockablePaneId = new DockablePaneId(mainPageGuid);
            // 메서드 RegisterDockablePane 호출하여 Revit 응용 프로그램 화면에서 새로운 Dockable Window 등록
            application.RegisterDockablePane(Globals.sm_UserDockablePaneId, Globals.ApplicationName, ThisApplication.thisApp.GetMainWindow() as IDockablePaneProvider);
        }

        /// <summary>
        /// Dockable Window 등록 2
        /// Register a dockable Window
        /// 메서드 파라미터 UIControlledApplication application
        /// </summary>
        public void RegisterDockableWindow(UIControlledApplication application, Guid mainPageGuid)
        {
            Globals.sm_UserDockablePaneId = new DockablePaneId(mainPageGuid);
            application.RegisterDockablePane(Globals.sm_UserDockablePaneId, Globals.ApplicationName, ThisApplication.thisApp.GetMainWindow() as IDockablePaneProvider);
        }

        /// <summary>
        /// Revit 시작 (OnStartup)
        /// Add UI for registering, showing, and hiding dockable panes.
        /// Dockable Window 등록 및 화면 출력(showing dockable panes.) 또는 숨기기(hiding dockable panes.) 처리
        /// </summary>
        public Result OnStartup(UIControlledApplication application)
        {
            // 1 단계 : ThisApplication 클래스 객체 thisApp에 Revit 외부 입력 애드인 프로그램 객체 자신(this) 할당 
            thisApp = this;

            // 2 단계 : Revit 외부 입력 애드인 프로그램 실행시
            //          필요한 기본적인 기능(인터페이스)을 관리하는 APIUtility 클래스 객체 m_APIUtility 생성
            m_APIUtility = new APIUtility();

            // 3 단계 : 리본 탭 생성
            application.CreateRibbonTab(Globals.DiagnosticsTabName);

            // 4 단계 : 3 단계에서 생성한 리본 탭에 속하는 리본 패널 생성 
            RibbonPanel panel = application.CreateRibbonPanel(Globals.DiagnosticsTabName, Globals.DiagnosticsPanelName);

            // 5 단계 : 리본 패널에 구분자(Separator) 추가 
            panel.AddSeparator();

            // 6 단계 : 리본 패널에 버튼(Register, Show, Hide) 추가 
            // 리본 패널에 버튼 "Register" 추가
            PushButtonData pushButtonRegisterPageData = new PushButtonData(Globals.RegisterPage, Globals.RegisterPage,
            FileUtility.GetAssemblyFullName(), typeof(ExternalCommandRegisterPage).FullName);
            pushButtonRegisterPageData.LargeImage = new BitmapImage(new Uri(FileUtility.GetApplicationResourcesPath() + "Register.png"));
            PushButton pushButtonRegisterPage = panel.AddItem(pushButtonRegisterPageData) as PushButton;

            // 버튼 "Register" 클릭시 명령 실행되는 Command 클래스 위치 지정 
            pushButtonRegisterPage.AvailabilityClassName = typeof(ExternalCommandRegisterPage).FullName;

            // 리본 패널에 버튼 "Show" 추가
            PushButtonData pushButtonShowPageData = new PushButtonData(Globals.ShowPage, Globals.ShowPage, FileUtility.GetAssemblyFullName(), typeof(ExternalCommandShowPage).FullName);
            pushButtonShowPageData.LargeImage = new BitmapImage(new Uri(FileUtility.GetApplicationResourcesPath() + "Show.png"));
            PushButton pushButtonShowPage = panel.AddItem(pushButtonShowPageData) as PushButton;

            // 버튼 "Show" 클릭시 명령 실행되는 Command 클래스 위치 지정 
            pushButtonShowPage.AvailabilityClassName = typeof(ExternalCommandShowPage).FullName;

            // 리본 패널에 버튼 "Hide" 추가
            PushButtonData pushButtonHidePageData = new PushButtonData(Globals.HidePage, Globals.HidePage, FileUtility.GetAssemblyFullName(), typeof(ExternalCommandHidePage).FullName);
            pushButtonHidePageData.LargeImage = new BitmapImage(new Uri(FileUtility.GetApplicationResourcesPath() + "Hide.png"));
            PushButton pushButtonHidePage = panel.AddItem(pushButtonHidePageData) as PushButton;

            // 버튼 "Hide" 클릭시 명령 실행되는 Command 클래스 위치 지정 
            pushButtonHidePage.AvailabilityClassName = typeof(ExternalCommandHidePage).FullName;

            return Result.Succeeded;
        }

        /// <summary>
        /// Revit 응용 프로그램 화면에 Docking할 새로운 WPF Window Page 생성  
        /// Create the new WPF Page that Revit will dock.
        /// </summary>
        public void CreateWindow()
        {
            m_mainPage = new MainPage();
        }

        /// <summary>
        /// Dockable Window 보여주거나(Show) 숨기는(Hide) 기능 실헹
        /// </summary>
        public void SetWindowVisibility(UIApplication application, bool state)
        {
            // DockablePaneId 클래스 객체 sm_UserDockablePaneId(Dockable Window 객체 식별하는 Guid) 값에 매핑되는 DockablePane 객체를 가져오기
            DockablePane pane = application.GetDockablePane(Globals.sm_UserDockablePaneId);
            
            // DockablePane 클래스 객체 pane 존재하는 경우 
            if(pane is not null)
            {
                if(state) pane.Show(); // Dockable Window 보여주기
                else pane.Hide();      //  Dockable Window 숨기기
            }
        }

        /// <summary>
        /// Dockable Window 객체 프로퍼티 (m_mainPage) 존재 여부 유효성 검사 
        /// </summary>
        public bool IsMainWindowAvailable()
        {
            if(m_mainPage is null) return false;

            bool isAvailable = true;

            try 
            { 
                bool isVisible = m_mainPage.IsVisible; 
            }
            catch(Exception ex)
            {
                isAvailable = false;
            }
            return isAvailable;

        }

        /// <summary>
        /// Dockable Window 객체 프로퍼티 (m_mainPage) 가져오기 
        /// </summary>
        public MainPage GetMainWindow()
        {
            if(false == IsMainWindowAvailable()) throw new InvalidOperationException("Main window not constructed.");
            return m_mainPage;
        }

        /// <summary>
        /// Revit 외부 입력 애드인 프로그램(프로젝트 파일 "TestDockableDialogs") 실행시 
        /// 필요한 기본적인 기능(인터페이스)을 관리하는 APIUtility 클래스 객체 m_APIUtility 가져오기 
        /// </summary>
        public APIUtility GetDockableAPIUtility() { return m_APIUtility; }


        /// <summary>
        /// 읽기 전용 프로퍼티 MainPageDockablePaneId
        /// Revit 응용 프로그램에 새로 추가하고자 하는 Dockable Window 식별자(Guid) 가져오기  
        /// </summary>
        public DockablePaneId MainPageDockablePaneId
        {
            get { return Globals.sm_UserDockablePaneId; }
        }

        #region Data

        /// <summary>
        /// Dockable Window(MainPage.xaml) 객체 
        /// </summary>
        MainPage m_mainPage;

        /// <summary>
        /// Revit 외부 입력 애드인 프로그램 객체 자신을 의미하는 ThisApplication 클래스 객체 thisApp 
        /// </summary>
        internal static ThisApplication thisApp = null;

        /// <summary>
        /// Revit 외부 입력 애드인 프로그램(프로젝트 파일 "TestDockableDialogs") 실행시 
        /// 필요한 기본적인 기능(인터페이스)을 관리하는 APIUtility 클래스 객체 m_APIUtility 
        /// </summary>
        private APIUtility m_APIUtility;

        #endregion Data

        #region Sample

        #endregion Sample
    }
}
