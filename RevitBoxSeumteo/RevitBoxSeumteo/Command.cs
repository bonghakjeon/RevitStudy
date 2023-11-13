using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Reflection;
using System.Threading.Tasks;

using RevitBoxSeumteo.Views.Windows;
using RevitBoxSeumteo.Common.CommandBase;
using RevitBoxSeumteo.Common.LogManager;
using System.Windows.Controls;
using RevitBoxSeumteo.ViewModels.Windows;

namespace RevitBoxSeumteo
{
    // Revit API 로 개발 시 사용하는 기본 단위는 FEET이다.
    // 이 기본 단위 FEET를 mm 단위로 변경을 시켜주거나 
    // mm 단위를 FEET로 변경할 필요가 있다.
    // 이렇게 단위를 변경하지 않으면 원치 않는 사이즈가 출력되어 오류가 발생한다.
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class Command : IExternalCommand
    {
        #region 프로퍼티 

        #endregion 프로퍼티 

        #region 기본 메소드 

        /// <summary>
        /// 명령 클래스(Command) 안의 실행 메서드 "Execute"
        /// </summary>
        /// <param name="commandData"></param>
        /// <param name="message"></param>
        /// <param name="elements"></param>
        /// <returns></returns>
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            // TODO : 로그 기록시 현재 실행 중인 메서드 위치 기록하기 (2023.10.10 jbh)
            // 참고 URL - https://slaner.tistory.com/73
            // 참고 2 URL - https://stackoverflow.com/questions/4132810/how-can-i-get-a-method-name-with-the-namespace-class-name
            // 참고 3 URL - https://stackoverflow.com/questions/44153/can-you-use-reflection-to-find-the-name-of-the-currently-executing-method
            var currentMethod = MethodBase.GetCurrentMethod();

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
                    transaction.Start(CommandHelper.start);  // 해당 "RevitBoxSeumteo" 프로젝트에서 연산처리(객체 생성, 정보 변경 및 삭제 등등... ) 시작

                    MessageBox.Show("C# WPF 테스트");

                    // TODO : Revit 2024 SDK - SDKSamples.sln 솔루션 파일 -> 프로젝트 파일 "DockableDialogs" -> 소스 파일 ExternalCommandRegisterPage.cs 참고해서 
                    //        테스트 화면 "AISParamsBoardV.xaml" 출력 하도록 로직 구현 (2023.10.6 jbh)
                    AISParamsBoardV AISParamsBoardV = new AISParamsBoardV();
                    // AISParamsBoardV.ShowDialog();
                    AISParamsBoardV.Show();

                    Log.Information(Logger.GetMethodPath(currentMethod) + "세움터 매개변수 관리 화면 종료");

                    transaction.Commit();   // 해당 "RevitBoxSeumteo" 프로젝트에서 연산처리(객체 생성, 정보 변경 및 삭제 등등... )된 결과 커밋
                }

                return Result.Succeeded;
            }
            catch (Exception ex)
            {
                // TODO : 오류 메시지 로그 기록으로 남길 수 있도록 LogManager.cs 추후 구현 예정 (2023.10.6 jbh)
                MessageBox.Show(ex.Message);
                return Result.Failed;
            }
            finally
            {
                // TODO : finally문 안에 코드 필요시 구현 예정 (2023.10.6 jbh)
            }
        }

        #endregion 기본 메소드 
    }

    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class ParameterCommand : IExternalCommand
    {
        #region 프로퍼티 


        #endregion 프로퍼티 

        #region 기본 메소드 

        /// <summary>
        /// 명령 클래스(ParameterCommand) 안의 실행 메서드 "Execute"
        /// </summary>
        /// <param name="commandData"></param>
        /// <param name="message"></param>
        /// <param name="elements"></param>
        /// <returns></returns>
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            // TODO : 로그 기록시 현재 실행 중인 메서드 위치 기록하기 (2023.10.30 jbh)
            var currentMethod = MethodBase.GetCurrentMethod();

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
                    transaction.Start(CommandHelper.start);  // 해당 "RevitBoxSeumteo" 프로젝트에서 연산처리(객체 생성, 정보 변경 및 삭제 등등... ) 시작

                    MessageBox.Show("C# WPF 매개변수 생성 테스트");


                    // TODO : 버튼 "테스트 AIS 매개변수 생성" 클릭시 ProgressBar가 화면에 출력되면서 매개변수가 생성 처리될 수 있도록
                    //        화면 AISParamsCreateBoardV.xaml 화면 및 뷰모델(AISParamsCreateBoardVM.cs) 구현하기 (2023.10.27 jbh)
                    // 참고 URL - http://ojc.asia/bbs/board.php?bo_table=WPF&wr_id=40
                    AISParamsCreateBoardV AISParamsCreateBoardV = new AISParamsCreateBoardV();
                    // AISParamsCreateBoardV.ShowDialog();
                    AISParamsCreateBoardV.Show();

                    // AISParamsCreateBoardVM AISParamsCreateBoardVM = new AISParamsCreateBoardVM();

                    // UserControl userControl = new UserControl { Content = AISParamsCreateBoardV, DataContext = };

                    // TODO : 아래 Window 객체 생성 코드 사용시 오류 메시지 출력 "WPF 지정한 요소가 이미 다른 요소의 논리자식입니다. 먼저 이 연결을 끊으십시오. -"되서 사용 안함(2023.11.07 jbh)
                    // 참고 URL - https://noteofdeveloper.tistory.com/83
                    // TODO : Window 객체 "window" 생성 -> 화면 뷰 "AISParamsCreateBoardV" 출력 및 뷰모델 "AISParamsCreateBoardVM" 연동 하도록 구현 (2023.11.07 jbh)
                    // 참고 URL - https://www.c-sharpcorner.com/article/use-of-user-controls-in-wpf/
                    // 참고 2 URL - https://yeko90.tistory.com/entry/c-wpf-DataContext%EB%9E%80-%EC%99%9C
                    // Window paramsCreateWindow = new Window { Content = AISParamsCreateBoardV, DataContext = AISParamsCreateBoardVM };

                    Log.Information(Logger.GetMethodPath(currentMethod) + "세움터 매개변수 생성 화면 종료");

                    transaction.Commit();      // 해당 "RevitBoxSeumteo" 프로젝트에서 연산처리(객체 생성, 정보 변경 및 삭제 등등... )된 결과 커밋
                }

                return Result.Succeeded;
            }
            catch (Exception ex)
            {
                // TODO : 오류 메시지 로그 기록으로 남길 수 있도록 LogManager.cs 추후 구현 예정 (2023.10.6 jbh)
                MessageBox.Show(ex.Message);
                return Result.Failed;
            }
            finally
            {
                // TODO : finally문 안에 코드 필요시 구현 예정 (2023.10.6 jbh)
            }
        }

        #endregion 기본 메소드

    }


}
