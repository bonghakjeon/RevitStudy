using System;
using Stylet;
using StyletIoC;
using TestIronPython.ViewModels;
using TestIronPython.Common.LogManager;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter.Analytics;
using TestIronPython.ViewModels.Pages;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Threading;

// TODO : 오류 코드 "XLS0414" 오류 메시지 "'TestIronPython.App' 형식을 찾을 수 없습니다. 어셈블리 참조가 있는지, 참조된 모든 어셈블리가 빌드되었는지 확인하세요."
//        해당 오류를 해결하기 위해 C# 클래스 라이브러리 프로젝트 파일 "TestIronPython" 안에 존재하는 WPF 프로젝트 파일 실행시 시작점이 되는 소스파일 "App.xaml" 삭제 (2023.10.4 jbh) 
//        WPF 프로젝트 파일 실행시 시작점이 되는 소스파일 "App.xaml"을 삭제 하는 이유는
//        C# 클래스 라이브러리 프로젝트 파일 "TestIronPython"에 Nuget 패키지 "Stylet"(또는 "Stylet.Start")가 설치될 때 자동으로 추가되는 소스파일이기 때문이고,
//        이 "App.xaml" 소스파일은 WPF 애플리케이션 프로젝트 파일에서만 사용할 수 있다.
// 참고 URL - https://yeko90.tistory.com/entry/Appxaml-%EA%B8%B0%EB%B3%B8-%EA%B5%AC%EC%A1%B0


namespace TestIronPython
{
    // TODO : 오류 코드 "CS0060" 오류 메시지 "일관성 없는 액세스 가능성: 'Bootstrapper<MainVM>' 기본 클래스가 'StyletBootstrapper' 클래스보다 액세스하기 어렵습니다."
    //        해당 오류 해결하기 위해 MainVM.cs 소스파일에 들어가서 MainVM 클래스 접근 제한자 public 추가 (2023.10.4 jbh)
    // 참고 URL - https://nsstbg.tistory.com/63

    public class StyletBootstrapper : Bootstrapper<MainVM>
    {
        /// <summary>
        /// 1. 시작 시
        /// </summary>
        protected override void OnStart()
        {
            // This is called just after the application is started, but before the IoC container is set up.
            // Set up things like logging, etc
            Stylet.Logging.LogManager.LoggerFactory = name => new TestLogger(name);     // LogManager 구성   참고 URL - https://github.com/canton7/Stylet/wiki/Logging
            Stylet.Logging.LogManager.Enabled = true;                                   // LogManager 활성화 참고 URL - https://github.com/canton7/Stylet/wiki/Logging

            // TODO : StyletBootstrapper.cs 이벤트 메서드 OnStart 추후 로직 보완 예정 (2023.10.4 jbh)
            base.OnStart();
#if DEBUG
            bool isDebug = true;
#else
            isDebug = false;
#endif
            if (!isDebug)
            {
                // <Guid("1FDFD875-A485-425E-89BE-7C480B57AA36")>
                AppCenter.Start("148B9F39-8E9A-4340-A6E1-278B3EB38A5B", typeof(Analytics), typeof(Crashes));
            }
        }

        /// <summary>
        /// 2. 컨테이너 초기화
        /// </summary>
        /// <param name="builder"></param>
        protected override void ConfigureIoC(IStyletIoCBuilder builder)
        {
            // Configure the IoC container in here
            base.ConfigureIoC(builder);

            // TODO: 새로 추가할 싱글톤 객체가 존재할시 아래처럼 싱글톤 객체로 설정하기 (2023.10.4 jbh)
            // 싱글톤 참고 URL - https://morit.tistory.com/5
            // 싱글톤 참고 2 URL - https://github.com/canton7/Stylet/wiki/StyletIoC-Configuration
            // 싱글톤 객체 설정 또 다른 예시 - builder.Bind<IConfigurationManager>().ToFactory(container => new ConfigurationManager()).InSingletonScope();
            builder.Bind<IWindowManager>().To<WindowManager>().InSingletonScope();
            builder.Bind<IEventAggregator>().To<EventAggregator>().InSingletonScope();

            // TODO : ShellViewModel, TestViewModel 말고도 "셀프 바인딩"할 뷰모델 찾아서 아래처럼 구현하기 (2023.10.4 jbh)
            // 셀프 바인딩 - StyletIoC에게 CobimExplorer.sln 솔루션이 MainVM, ShellViewModel, TestViewModel을 요청할 때마다 모든 종속 항목이 이미 채워진 ShellViewModel, TestViewModel을 제공 해주는 기능.
            // 참고 URL - https://github.com/canton7/Stylet/wiki/StyletIoC-Configuration
            // 셀프 바인딩 예시 1 - builder.Bind<ShellViewModel>().ToSelf();
            // 셀프 바인딩 예시 2 - builder.Bind<ShellViewModel>().To<ShellViewModel>();
            builder.Bind<MainVM>().ToSelf();
            // builder.Bind<ShellVM>().ToSelf();

            // TODO: 추후 필요시 입력 받은 키 값을 전달 (Clear, Back space, 숫자 키 등등.... ) 받는 인터페이스 IPageBase 멤버  메서드 "OnReceivePosKeyUp" 구현 예정 (2023.10.4 jbh)
            // builder.Bind<IPageBase>().ToAllImplementations();
        }

        /// <summary>
        /// 3. 컨테이너 초기화 후 설정
        /// <see cref="ConfigureIoC(IStyletIoCBuilder)"/> 이후
        /// </summary>
        protected override void Configure()
        {
            // Perform any other configuration before the application starts
            // This is called after Stylet has created the IoC container, so this.Container exists, but before the
            // Root ViewModel is launched.
            // Configure your services, etc, in here
            // 참고 URL - https://github.com/canton7/Stylet/wiki/The-ViewManager
            base.Configure();
            var vm = this.Container.Get<ViewManager>();
            vm.ViewNameSuffix = "V";
            vm.ViewModelNameSuffix = "VM";
            vm.NamespaceTransformations = new Dictionary<string, string>()
            {
                ["TestIronPython.ViewModels"] = "TestIronPython.Views"
            };
        }

        /// <summary>
        /// 4. 실행 후
        /// </summary>
        protected override void OnLaunch()
        {
            // This is called just after the root ViewModel has been launched
            // Something like a version check that displays a dialog might be launched from here
            base.OnLaunch();
        }

        /// <summary>
        /// 5. 종료 시
        /// </summary>
        /// <param name="e"></param>
        protected override void OnExit(ExitEventArgs e)
        {
            // Called on Application.Exit
            //SystemCurrentInfo.Instance.Dispose();
            base.OnExit(e);
        }

        /// <summary>
        /// 6. 예외
        /// </summary>
        /// <param name="e"></param>
        protected override void OnUnhandledException(DispatcherUnhandledExceptionEventArgs e)
        {
            // Called on Application.DispatcherUnhandledException
            base.OnUnhandledException(e);

            // 인터넷 연결 되어있을 때에 Exception 수집 
            Crashes.TrackError(e.Exception);
#if DEBUG
            var isRelease = false;
#else
            isRelease = true;
#endif
            if (isRelease)
            {
                e.Handled = true;   //  프로그램이 죽지 않고 진행 가능 하도록 
            }
        }
    }
}
