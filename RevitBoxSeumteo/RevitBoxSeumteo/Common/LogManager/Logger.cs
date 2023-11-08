using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace RevitBoxSeumteo.Common.LogManager
{
    public class Logger
    {
        #region 프로퍼티 

        /// <summary>
        /// 년도별 버전 이름 길이 enum 구조체 AssemblyVersion
        /// </summary>
        public enum AssemblyVersion : int
        {
            VersionLen = 4  // 버전 이름 길이 (2019, 2020, 2021, 2023, 2024 ..)
        }

        // TODO : 로그 타입 유형 문자열 (const string) 객체 다른 유형 필요시 추가 예정 (2023.10.11 jbh)
        public const string debugMessage = "디버그 - ";

        public const string errorMessage = "오류 - ";

        public const string warningMessage = "경고 - ";

        // TODO : 로그파일 생성 경로 프로퍼티 "root", "LogsPath" (RevitBox2024 버전 설치 경로 하위 폴더에 생성) 필요시 사용 예정 (2023.10.16 jbh)
        //public static string root = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);

        //public static string LogsPath = root + @"C:\Program Files\ImagineBuilder\RevitBox 2024\Logs\";


        // TODO : 프로젝트 파일(어셈블리 버전 정보) 이름 가져오기 
        // 참고할 자료 URL 주소 - https://chashtag.tistory.com/29
        public static string projectFileName = Assembly.GetExecutingAssembly().GetName().Name;

        // C# 문자열 보간 참고 URL 주소 - https://gaeunhan.tistory.com/61
        // string template_name = root + $@"\ImagineBuilder\RevitBox {verName}\RevitBox 템플릿\철골 공장 템플릿_{verName}.rte";

        /// <summary>
        /// 로그 파일 생성 파일 경로 
        /// </summary>
        // public static string createLogPath = $"D:\\bhjeon\\RevitStudy\\RevitBoxSeumteo\\RevitBoxSeumteo\\bin\\x64\\Debug\\" + $"Logs\\{projectFileName}\\{projectFileName}_.log";
        public static string createLogPath = $"Logs\\{projectFileName}\\{projectFileName}_.log";

        #endregion 프로퍼티 

        #region ConfigureLogger

        /// <summary>
        /// Serilog 로그 초기 설정 
        /// </summary>
        public static void ConfigureLogger()
        {
            // AssemblyName assemblyName = Assembly.GetExecutingAssembly().GetName();

            try
            {
                // TODO : Stylet LogManager 클래스 사용 안하고 Serilog 이용해서 로그 기록 및 로그 파일 생성 (2023.10.10 jbh)
                // 참고 URL - https://m.blog.naver.com/wolfre/221713399852
                // 참고 2 URL - https://afsdzvcx123.tistory.com/entry/C-%EB%AC%B8%EB%B2%95-C-Serilog-%EC%82%AC%EC%9A%A9%ED%95%98%EC%97%AC-%EB%A1%9C%EA%B7%B8-%EB%82%A8%EA%B8%B0%EA%B8%B0
                // 참고 3 URL - https://blog.naver.com/PostView.naver?blogId=wolfre&logNo=221713399852&parentCategoryNo=&categoryNo=26&viewDate=&isShowPopularPosts=true&from=search
                // 참고 4 URL - https://www.reddit.com/r/dotnet/comments/qhwa4i/serilog_remove_files_older_than_30_days/
                // 참고 5 URL - https://github.com/serilog/serilog-sinks-file#rolling-policies

                // TODO : 에러 로그를 메일로 보내는 로직(.WriteTo.Email) 필요시 추후 구현 예정 (2023.10.10 jbh)
                // 참고 URL - https://github.com/serilog/serilog-sinks-email
                var log = new LoggerConfiguration()
                    .MinimumLevel.Verbose()   // 로그 남기는 기준 ex) Verbose 이상 다 남겨야 함.
                                              // Verbose(0) => Debug => Information => Warning => Error => Fatal(5)
                    .WriteTo.Console()        // 콘솔 파일에도 로그 출력
                    .WriteTo.Debug()
                    // 로그 파일 이름을 날짜 형식(예)"CobimExplorer_20231016.log" 으로 로그 파일 생성
                    .WriteTo.File(
                    // $"Logs\\{Assembly.GetEntryAssembly().GetName().Name}\\{Assembly.GetEntryAssembly().GetName().Name}_{DateTime.Now.ToString("yyyyMMdd")}.log",
                    path: createLogPath,
                    Serilog.Events.LogEventLevel.Verbose,
                    // TODO : 로그 파일에 날짜를 수동으로 지정할 필요 없이 rollingInterval: RollingInterval.Day, 사용해서 로그파일 이름 언더바(_)옆에 로그 생성된 날짜가 추가되도록 구현
                    //        (이렇게 구현해야 아래 프로퍼티 "retainedFileCountLimit" 사용해서 날짜가 지난 로그파일 삭제 처리 할 수 있음.) (2023.10.16 jbh) 
                    // 참고 URL - https://stackoverflow.com/questions/32108148/serilog-rollingfile
                    rollingInterval: RollingInterval.Day,
                    rollOnFileSizeLimit: true,   // 로그 파일 최대 사이즈 1GB  null 옵션 가능
                                                 // TODO : 테스트 용 로그 파일 갯수 1개(1일치 로그) 설정(1일 이상 지난 오래된 로그는 삭제 처리)
                                                 //        1일 지난 로그 파일 정상적으로 삭제 처리시 아래 주석 처리된 소스코드 "retainedFileCountLimit: 62,"로 다시 구현해야 함. (2023.10.11 jbh)
                                                 // 참고 URL - https://stackoverflow.com/questions/44577336/how-do-i-automatically-tail-delete-older-logs-using-serilog-in-a-net-wpf-appl
                    retainedFileCountLimit: 1,
                    // retainedFileCountLimit: 62,  // 로그 파일 갯수 62개(2달치 로그) 설정(2달 지난 오래된 로그는 삭제 처리) - 기본 31개 설정 가능, null 옵션 가능
                    encoding: Encoding.UTF8
                    )
                    .CreateLogger();
                //LogManager.LoggerFactory = name => new SerilogStyletAdapter(log, name);
                //LogManager.Enabled = false;   // Stylet LogManager 비활성화 (사용 안함.)
                Log.Logger = log;             // Serilog를 CobimExplorer에서 사용할 수 있도록 설정 (전역 로그)

                // 로그 레벨 기록 예시
                //Log.Information($"Info name = {System.Reflection.Assembly.GetEntryAssembly().GetName().Name}");
                //Log.Debug($"Debug name = {System.Reflection.Assembly.GetEntryAssembly().GetName().Name}");
                //Log.Verbose($"Verbose name = {System.Reflection.Assembly.GetEntryAssembly().GetName().Name}");
                //Log.Warning($"Warning name = {System.Reflection.Assembly.GetEntryAssembly().GetName().Name}");
                //Log.Error($"Error name = {System.Reflection.Assembly.GetEntryAssembly().GetName().Name}");
                //Log.Fatal($"Fatal name = {System.Reflection.Assembly.GetEntryAssembly().GetName().Name}");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion ConfigureLogger

        #region GetMethodPath

        /// <summary>
        /// 로그 실행한 Namespace 및 메서드(비동기 메서드 포함) 경로 추출
        /// </summary>
        /// <param name="currentMethod"></param>
        /// <returns></returns>
        public static string GetMethodPath(MethodBase currentMethod)
        {
            var fullMethodPath = string.Empty;

            // TODO : 로그 기록시 현재 실행 중인 메서드 위치 기록하기 (2023.10.10 jbh)
            // 참고 URL - https://slaner.tistory.com/73
            // 참고 2 URL - https://stackoverflow.com/questions/4132810/how-can-i-get-a-method-name-with-the-namespace-class-name
            // 참고 3 URL - https://stackoverflow.com/questions/44153/can-you-use-reflection-to-find-the-name-of-the-currently-executing-method
            var currentLoggerMethod = MethodBase.GetCurrentMethod();

            try
            {
                // TODO : 로그 실행한 Namespace 및 메서드(비동기 메서드 포함) 경로 추출하여 문자열로 변환 (2023.10.11 jbh)
                // 참고 URL - https://stackoverflow.com/questions/2968352/using-system-reflection-to-get-a-methods-full-name
                // var generatedType = currentMethod.DeclaringType;

                // TODO : "generatedType", "originalType" Null Exception 발생한 원인 파악하기 (2023.10.20 jbh)
                // 참고 URL - https://stackoverflow.com/questions/22809141/why-does-resharper-show-a-system-nullreferenceexception-warning-in-this-case
                // var originalType = generatedType.DeclaringType;
                // var foundMethod = originalType.GetMethods(
                //       BindingFlags.Instance | BindingFlags.Static
                //     | BindingFlags.Public | BindingFlags.NonPublic
                //     | BindingFlags.DeclaredOnly)
                //     .Single(m => m.GetCustomAttribute<AsyncStateMachineAttribute>()?.StateMachineType == generatedType);
                //string test =  foundMethod.DeclaringType.Name + "." + foundMethod.Name;
                // string test = foundMethod.DeclaringType.FullName + "." + foundMethod.Name;

                // fullMethodPath = "[" + currentMethod.ReflectedType.ReflectedType.FullName.Replace("+", " | ").Replace("d__77", "") + "] : ";
                // fullMethodPath = "[" + foundMethod.DeclaringType.FullName + " | " + foundMethod.Name + "] : ";

                // TODO : 테스트 코드 - 강제로 오류 발생하도록 Exception 생성 하도록 구현 (필요시 사용) (2023.10.19 jbh)
                // 참고 URL - https://morm.tistory.com/187
                // 참고 2 URL - https://learn.microsoft.com/ko-kr/dotnet/csharp/fundamentals/exceptions/creating-and-throwing-exceptions
                // throw new Exception();

                fullMethodPath = "[" + currentMethod.DeclaringType.FullName + " | " + currentMethod.Name + "] : ";
                
                return fullMethodPath;
            }
            catch (Exception ex)
            {
                fullMethodPath = "[" + currentLoggerMethod.DeclaringType.FullName + " | " + currentLoggerMethod.Name + "] : ";
                // var loggerPath = MethodBase.GetCurrentMethod().DeclaringType.FullName;
                Log.Error(fullMethodPath + Logger.errorMessage + ex.Message);
                throw;

            }
        }

        #endregion GetMethodPath
    }
}
