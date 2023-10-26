using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI;

using System;
using System.Windows;
using System.Diagnostics;
using System.Threading;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;
// using System.ComponentModel;
// using System.Collections.Generic;

using RevitBoxSeumteo.Common.RibbonBase;
using RevitBoxSeumteo.Common.LogManager;
using RevitBoxSeumteo.Views.SplashScreen;
using RevitBoxSeumteo.ViewModels.SplashScreen;

// Revit 리본 만들기 
// 참고 URL - https://blog.naver.com/sojunbeer/221756535707

// Revit Ribbon API 문서 
// 참고 URL - https://help.autodesk.com/view/RVT/2024/ENU/?guid=Revit_API_Revit_API_Developers_Guide_Introduction_Add_In_Integration_External_Application_html
// 참고 2 URL - https://help.autodesk.com/view/RVT/2024/ENU/?guid=Revit_API_Revit_API_Developers_Guide_Introduction_Add_In_Integration_Ribbon_Panels_and_Controls_html

namespace RevitBoxSeumteo
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class Application : IExternalApplication   // Revit이 실행될 때, Ribbon(메뉴, 탭, 버튼)이 등록되어야 하기 때문에 인터페이스 "IExternalApplication"를 상속 받아야 함.
    {
        #region 프로퍼티 

        #endregion 프로퍼티 

        #region 기본 메소드 

        /// <summary>
        /// Revit 시작 (OnStartup)
        /// </summary>
        /// <param name="application"></param>
        /// <returns></returns>
        public Result OnStartup(UIControlledApplication application)
        {
            try
            {
                Ribbon.CreateRibbonControl(application);   // 리본 메뉴 등록

                // TODO : 추후 Logger.cs -> static 메서드 "ConfigureLogger" 구현 예정(2023.10.16 jbh)
                Logger.ConfigureLogger();

                return Result.Succeeded;
            }
            catch(Exception e)
            {
                // TODO : 오류 메시지 로그 기록으로 남길 수 있도록 LogManager.cs 추후 구현 예정 (2023.10.6 jbh)
                MessageBox.Show(e.Message);
                return Result.Failed;
            }
        }

        /// <summary>
        /// Revit 종료 (OnShutdown)
        /// </summary>
        /// <param name="application"></param>
        /// <returns></returns>
        public Result OnShutdown(UIControlledApplication application)
        {
            // TODO : 에러 처리 필요시 메서드 "OnShutdown" 몸체 안에 try - catch문으로 구현 예정 (2023.10.6 jbh)
            return Result.Succeeded;
        }

        #endregion 기본 메소드 
    }

    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class SplashScreenCommand : IExternalApplication
    {
        #region 프로퍼티 

        private const int MINIMUM_SPLASH_TIME = 10000; // Miliseconds  

        #endregion 프로퍼티 

        #region 기본 메소드 

        public Result OnStartup(UIControlledApplication application)
        {
            // TODO : SplashScreen 화면(SplashScreenBoard.xaml) 실행 되도록 구현 (2023.10.25 jbh)
            // 참고 URL - https://www.c-sharpcorner.com/UploadFile/07c1e7/create-splash-screen-in-wpf/
            // 참고 2 URL - https://insurang.tistory.com/entry/WPF-C-SplashScreen-%EC%8A%A4%ED%94%8C%EB%9E%98%EC%89%AC-%EC%8A%A4%ED%94%8C%EB%9E%98%EC%8B%9C-%EC%8A%A4%ED%94%8C%EB%9E%98%EC%89%AC%EC%8A%A4%ED%81%AC%EB%A6%B0-%EC%8A%A4%ED%94%8C%EB%9E%98%EC%8B%9C%EC%8A%A4%ED%81%AC%EB%A6%B0-%EB%A1%9C%EB%94%A9%EC%A4%91-Loading
            // 참고 3 URL - https://blog.naver.com/goldrushing/221353980465
            // 참고 4 URL - https://blog.naver.com/goldrushing/130185507840

            // TODO : Revit 2024 SDK - SDKSamples.sln 솔루션 파일 -> 프로젝트 파일 "DockableDialogs" -> 소스 파일 ExternalCommandRegisterPage.cs 참고해서 
            //        SplashScreen 화면 "SplashScreenBoard.xaml" 출력 하도록 로직 구현 (2023.10.25 jbh)
            // 1 단계 - SplashScreenBoard.xaml 화면 출력 
            SplashScreenBoardV splashV = new SplashScreenBoardV();
            splashV.Show();

            // TODO : Stopwatch 클래스 객체 timer 생성해서 시간 체크 및 SplashScreen 화면 "SplashScreenBoard.xaml" 출력 및 화면 출력 시간(MINIMUM_SPLASH_TIME) 설정 (2023.10.25 jbh)
            // 참고 URL - https://inyongs.tistory.com/15
            // 2 단계 - 타이머 시작 Start a stop watch  
            Stopwatch timer = new Stopwatch();
            timer.Start();
            
            Thread.Sleep(MINIMUM_SPLASH_TIME);  // 시간 "MINIMUM_SPLASH_TIME" 동안 화면 출력되도록 Thread.Sleep 처리

            // 스톱워치가 작동 중이면
            if (timer.IsRunning)
            {
                timer.Stop();   // 스톱워치 끝
                splashV.Close();
            }

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            // TODO : 에러 처리 필요시 메서드 "OnShutdown" 몸체 안에 try - catch문으로 구현 예정 (2023.10.25 jbh)
            return Result.Succeeded;
        }

        

        #endregion 기본 메소드

    }
}
