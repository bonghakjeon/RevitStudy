using Serilog;

using System;
using System.Diagnostics;
using System.Reflection;

using HTSBIM2019.Common.HTSBase;
using HTSBIM2019.Common.LogBase;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace HTSBIM2019.Utils.CompanyHomePage
{
    /// <summary>
    /// (주)상상진화 기업 홈페이지
    /// </summary>
    public class CompanyHomePage
    {
        #region 프로퍼티 

        #endregion 프로퍼티 

        #region 생성자

        public CompanyHomePage(Document rvDoc, string pUrl)
        {
            ConnectImagineBuilder(rvDoc, pUrl);
        }

        #endregion 생성자

        #region ConnectImagineBuilder

        /// <summary>
        /// (주)상상진화 기업 홈페이지 연결
        /// </summary>
        private void ConnectImagineBuilder(Document rvDoc, string pUrl)
        {
            var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

            try
            {
                // 해당 Transaction이 끝날 때까지는 화면 상에서는 다른 기능을 실행할 수 있고 다른 기능의 화면도 출력되지만
                // 다른 기능을 실행해서 데이터를 변경할 수 없다.(다른 작업이나 Command 명령이 끼어들 수 없다.)
                // 해당 Transaction 기능은 부포 폼(Revit)의 쓰레드를 자식 폼(MEPUpdater)이 제어하는 과정이다.
                using(Transaction transaction = new Transaction(rvDoc))
                {
                    // transaction.Start(HTSHelper.Start); 부터 transaction.Commit(); 까지가 연산처리를 하는 하나의 작업단위이다.
                    transaction.Start(HTSHelper.Start);   // 연산처리(객체 생성, 정보 변경 및 삭제 등등... ) 시작

                    Log.Information(Logger.GetMethodPath(currentMethod) + "(주)상상진화 홈페이지 연결 시작");

                    // TODO : (주)상상진화 기업 홈페이지 팝업 화면 출력 구현 (2024.04.11 jbh)
                    // 참고 URL   - https://yongtech.tistory.com/58
                    // 참고 2 URL - https://findfun.tistory.com/485

                    // TODO : (주)상상진화 기업 홈페이지 구글 크롬(chrome.exe)으로 출력 구현 (2024.04.11 jbh)
                    // 참고 URL - https://www.codeproject.com/Questions/5286855/How-do-I-open-Google-chrome-in-Csharp
                    // Process.Start("chrome.exe", pUrl);
                    // Process.Start("firefox.exe", pUrl);

                    // TODO : .net FrameWork 말고 .net Core 6.0 이상 버전에서  (주)상상진화 기업 홈페이지 출력 오류시 아래 처럼 구현 (2024.04.11 jbh)
                    // 참고 URL - https://endev.tistory.com/m/237
                    Process.Start(new ProcessStartInfo(pUrl) { UseShellExecute = true });

                    Log.Information(Logger.GetMethodPath(currentMethod) + "(주)상상진화 홈페이지 연결 완료");

                    transaction.Commit();   // 연산처리(객체 생성, 정보 변경 및 삭제 등등... )된 결과 커밋
                }   // 여기서 Dispose (리소스 해제) 처리 
            }
            catch(Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                TaskDialog.Show(HTSHelper.ErrorTitle, ex.Message);
            }
        }

        #endregion ConnectImagineBuilder

        #region Sample

        #endregion Sample
    }
}
