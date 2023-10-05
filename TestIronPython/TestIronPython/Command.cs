using Stylet;
using StyletIoC;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

using TestIronPython.Common.LogManager;
using TestIronPython.ViewModels;
using TestIronPython.ViewModels.Pages;
using TestIronPython.Service;
using TestIronPython.Views.Pages;
//using TestIronPython.Views.Pages;
//using IronPython.Hosting;

// TODO : Nuget 패키지 "IronPython"에 파이썬 전용 패키지 "tkinter, random, os 등등..." 사용할 수 있도록 구현(2023.09.27 jbh)
// 참고 URL - https://hybridego.net/m/489#google_vignette

// TODO : Nuget 패키지 "IronPython" 설치된 C# 소스파일 안에서 파이썬 tkinter GUI 소스파일 실행할 수 있도록 구현 (2023.09.27 jbh)
// 참고 URL - https://vmpo.tistory.com/55

// TODO : 파이썬 tkinter 패키지 설치 (설치 명령어 - pip install tk) (2023.09.27 jbh)
// 참고 URL - https://ngio.co.kr/11034

namespace TestIronPython
{
    // TODO : 오류 코드 "CS0060" 오류 메시지 "일관성 없는 액세스 가능성: 'Bootstrapper<MainVM>' 기본 클래스가 'StyletBootstrapper' 클래스보다 액세스하기 어렵습니다."
    //        해당 오류 해결하기 위해 MainVM.cs 소스파일에 들어가서 MainVM 클래스 접근 제한자 public 추가 (2023.10.4 jbh)
    // 참고 URL - https://nsstbg.tistory.com/63

    // Revit API 로 개발 시 사용하는 기본 단위는 FEET이다.
    // 이 기본 단위 FEET를 mm 단위로 변경을 시켜주거나 
    // mm 단위를 FEET로 변경할 필요가 있다.
    // 이렇게 단위를 변경하지 않으면 원치 않는 사이즈가 출력되어 오류가 발생한다.
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class Command : IExternalCommand
    {
        #region 프로퍼티 

        /// <summary>
        /// Start
        /// </summary>
        public const string start = "Start";

        #endregion 프로퍼티 

        /// <summary>
        /// 명령 클래스(Command) 안의 실행 메서드 "Execute"
        /// </summary>
        /// <param name="commandData"></param>
        /// <param name="message"></param>
        /// <param name="elements"></param>
        /// <returns></returns>
        public Result Execute(ExternalCommandData commandData,
          ref string message, ElementSet elements)
        {
            //var engine = Python.CreateEngine();
            //var scope  = engine.CreateScope();

            try
            {
                // 메서드 "Execute" 실행시 실행되는 코드 
                UIApplication uiapp = commandData.Application;
                Document doc = uiapp.ActiveUIDocument.Document;

                //Autodesk.Revit.ApplicationServices.Application app = commandData.Application.Application;
                //Document doc = commandData.Application.ActiveUIDocument.Document;
                // 현재 문서(doc)에서 어떤 클릭을 하거나 Dynamo의 Selection (해당 문서에서 어떤걸 가져 오는 것 ) 같은 기능들이 모여있는 객체 "uidoc"
                UIDocument uidoc = commandData.Application.ActiveUIDocument;

                using (Transaction transaction = new Transaction(doc))
                {
                    // transaction.Start(start); 부터 transaction.Commit(); 까지가 연산처리를 하는 하나의 작업단위이다.
                    transaction.Start(start);  // 해당 "TestIronPython" 프로젝트에서 연산처리(객체 생성, 정보 변경 및 삭제 등등... ) 시작

                    MessageBox.Show("C# WPF 테스트");

                    // TODO : Revit 2024 SDK - SDKSamples.sln 솔루션 파일 -> 프로젝트 파일 "DockableDialogs" -> 소스 파일 ExternalCommandRegisterPage.cs 참고해서 
                    //        테스트 화면 "ShellV.xaml" 출력하도록 로직 구현 (2023.10.4 jbh)
                    //ShellV shellV = new ShellV();
                    //shellV.ShowDialog();

                    // ShellVM shellVM = new ShellVM();

                    // TODO : Revit 2024 SDK - SDKSamples.sln 솔루션 파일 -> 프로젝트 파일 "DockableDialogs" -> 소스 파일 ExternalCommandRegisterPage.cs 참고해서 
                    //        테스트 화면 "ShellV.xaml" 출력하도록 로직 구현 (2023.10.4 jbh)
                    ShellV shellV = new ShellV();
                    shellV.ShowDialog();



                    // DialogVM.DataContext = shellVM;



                    // StyletBootstrapper styletBootstrapper = new StyletBootstrapper();

                    // this.Container.Get<ShellVM>().ToShowData(); // ShellV 화면 출력 

                    //MessageBox.Show("IronPython 테스트");

                    //string path = @"D:\bhjeon\RevitStudy\TestIronPython\TestIronPython\bin\Debug\PythonGUI.py";

                    //var source = engine.CreateScriptSourceFromFile(path);
                    //source.Execute(scope);

                    transaction.Commit();      // 해당 "TestIronPython" 프로젝트에서 연산처리(객체 생성, 정보 변경 및 삭제 등등... )된 결과 커밋
                }

                return Result.Succeeded;
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
                return Result.Failed;
            }   
        }
    }

    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class ParameterCommand : IExternalCommand
    {
        #region 프로퍼티 

        /// <summary>
        /// Start
        /// </summary>
        public const string start = "Start";

        #endregion 프로퍼티 

        /// <summary>
        /// 명령 클래스(ParameterCommand) 안의 실행 메서드 "Execute"
        /// </summary>
        /// <param name="commandData"></param>
        /// <param name="message"></param>
        /// <param name="elements"></param>
        /// <returns></returns>
        public Result Execute(ExternalCommandData commandData,
          ref string message, ElementSet elements)
        {
            //var engine = Python.CreateEngine();
            //var scope  = engine.CreateScope();

            try
            {
                // 메서드 "Execute" 실행시 실행되는 코드 
                UIApplication uiapp = commandData.Application;
                Document doc = uiapp.ActiveUIDocument.Document;

                //Autodesk.Revit.ApplicationServices.Application app = commandData.Application.Application;
                //Document doc = commandData.Application.ActiveUIDocument.Document;
                // 현재 문서(doc)에서 어떤 클릭을 하거나 Dynamo의 Selection (해당 문서에서 어떤걸 가져 오는 것 ) 같은 기능들이 모여있는 객체 "uidoc"
                UIDocument uidoc = commandData.Application.ActiveUIDocument;

                using (Transaction transaction = new Transaction(doc))
                {
                    // transaction.Start(start); 부터 transaction.Commit(); 까지가 연산처리를 하는 하나의 작업단위이다.
                    transaction.Start(start);  // 해당 "TestIronPython" 프로젝트에서 연산처리(객체 생성, 정보 변경 및 삭제 등등... ) 시작

                    MessageBox.Show("C# WPF 매개변수 생성 테스트");


                    // TODO : Revit 2024 SDK - SDKSamples.sln 솔루션 파일 -> 프로젝트 파일 "DockableDialogs" -> 소스 파일 ExternalCommandRegisterPage.cs 참고해서 
                    //        테스트 화면 "ShellV.xaml" 출력하도록 로직 구현 (2023.10.4 jbh)
                    // ShellV shellV = new ShellV();
                    // shellV.ShowDialog();

                    transaction.Commit();      // 해당 "TestIronPython" 프로젝트에서 연산처리(객체 생성, 정보 변경 및 삭제 등등... )된 결과 커밋
                }

                return Result.Succeeded;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return Result.Failed;
            }
        }
    }

}
