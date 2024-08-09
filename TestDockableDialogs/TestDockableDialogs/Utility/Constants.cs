using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDockableDialogs.Utility
{
    // TODO : 추후 클래스 파일 명칭 (기존) Constants.cs -> (변경) CobimHelper.cs
    //        클래스 명칭 (기존) Globals -> (변경) CobimHelper.cs

    /// <summary>
    /// 프로젝트 파일 "TestDockableDialogs"에서 전역으로 사용할 수 있는 
    /// const string + DockablePaneId 객체 모음 
    /// </summary>
    class Globals
    {

        public const string ApplicationName = "DockableDialogs";
        public const string DiagnosticsTabName = "DockableDialogs";
        public const string DiagnosticsPanelName = "DockableDialogs Panel";

        public const string RegisterPage = "Register Page";
        public const string ShowPage = "Show Page";
        public const string HidePage = "Hide Page";

        // Dockable Window 객체 식별하는 Guid 객체 생성
        public static DockablePaneId sm_UserDockablePaneId = new DockablePaneId(new Guid("{3BAFCF52-AC5C-4CF8-B1CB-D0B1D0E90237}"));

    }
}
