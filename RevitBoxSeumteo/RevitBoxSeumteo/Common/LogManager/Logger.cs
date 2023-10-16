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

        // TODO : 로그 타입 유형 문자열 (const string) 객체 다른 유형 필요시 추가 예정 (2023.10.11 jbh)
        public const string debugMessage = "디버그 - ";

        public const string errorMessage = "오류 - ";

        public const string warningMessage = "경고 - ";

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
                // TODO : 에러 로그를 메일로 보내는 로직(.WriteTo.Email) 필요시 추후 구현 예정 (2023.10.10 jbh)
                // TODO : .WriteTo.File() 메서드 실행시 var log에서 null 값이 들어가서 발생하는 Null Exception 오류 원인 파악하기(2023.10.12 jbh)
                // 참고 URL - https://github.com/serilog/serilog-sinks-email
                var log = new LoggerConfiguration()
                    .MinimumLevel.Verbose()   // 로그 남기는 기준 ex) Verbose 이상 다 남겨야 함.
                                              // Verbose(0) => Debug => Information => Warning => Error => Fatal(5)
                    .WriteTo.Console()        // 콘솔 파일에도 로그 출력
                    .WriteTo.Debug()
                     // 날짜 형식으로 로그 남김
                    .WriteTo.File(
                    $"Logs\\{Assembly.GetEntryAssembly().GetName().Name}\\{Assembly.GetEntryAssembly().GetName().Name}_{DateTime.Now.ToString("yyyyMMdd")}.log",
                    Serilog.Events.LogEventLevel.Verbose,
                    // rollingInterval: RollingInterval.Day, // 로그 파일 생성시 로그파일 이름에 날짜가 중복되서 추가되므로 해당 "rollingInterval: RollingInterval.Day" 내용 주석 처리
                    rollOnFileSizeLimit: true,   // 로그 파일 최대 사이즈 1GB  null 옵션 가능
                                                 // TODO : 테스트 용 로그 파일 갯수 1개(1일치 로그) 설정(1일 이상 지난 오래된 로그는 삭제 처리)
                                                 //        1일 지난 로그 파일 정상적으로 삭제 처리시 아래 주석 처리된 소스코드 "retainedFileCountLimit: 62,"로 다시 구현해야 함. (2023.10.11 jbh)
                                                 // 참고 URL - https://stackoverflow.com/questions/44577336/how-do-i-automatically-tail-delete-older-logs-using-serilog-in-a-net-wpf-appl
                                                 // retainedFileCountLimit: 1,
                    retainedFileCountLimit: 62,  // 로그 파일 갯수 62개(2달치 로그) 설정(2달 지난 오래된 로그는 삭제 처리) - 기본 31개 설정 가능, null 옵션 가능
                    encoding: Encoding.UTF8
                    )
                    .CreateLogger();
                
                Log.Logger = log;             // Serilog를 RevitBoxSeumteo에서 사용할 수 있도록 설정 (전역 로그)
            }
            catch (Exception e)
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
                var generatedType = currentMethod.DeclaringType;
                var originalType = generatedType.DeclaringType;
                var foundMethod = originalType.GetMethods(
                      BindingFlags.Instance | BindingFlags.Static
                    | BindingFlags.Public | BindingFlags.NonPublic
                    | BindingFlags.DeclaredOnly)
                    .Single(m => m.GetCustomAttribute<AsyncStateMachineAttribute>()?.StateMachineType == generatedType);
                //string test =  foundMethod.DeclaringType.Name + "." + foundMethod.Name;
                // string test = foundMethod.DeclaringType.FullName + "." + foundMethod.Name;

                // fullMethodPath = "[" + currentMethod.ReflectedType.ReflectedType.FullName.Replace("+", " | ").Replace("d__77", "") + "] : ";
                fullMethodPath = "[" + foundMethod.DeclaringType.FullName + " | " + foundMethod.Name + "] : ";
                return fullMethodPath;
            }
            catch (Exception e)
            {
                // var loggerPath = MethodBase.GetCurrentMethod().DeclaringType.FullName;
                Log.Error(Logger.GetMethodPath(currentLoggerMethod) + Logger.errorMessage + e.Message);
                throw;

            }
        }

        #endregion GetMethodPath
    }
}
