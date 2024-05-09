using Serilog;

using System;
using System.Reflection;
using System.Collections.Generic;

using HTSBIM2019.Common.HTSBase;
using HTSBIM2019.Common.LogBase;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace HTSBIM2019.Utils.Failure
{
    // TODO : 필요시 아래 주석친 코드 사용 예정 (2024.05.09 jbh)
    // TODO : 경고, 실패 팝업 화면 삭제 처리 기능 클래스 "FailurePreprocessor" 구현 (2024.05.09 jbh)
    // 참고 URL - https://www.revitapidocs.com/2023/053c6262-d958-b1b6-44b7-35d0d83b5a43.htm
    // 유튜브 참고 URL - https://youtu.be/uqFH-7IJ6Hw?si=-De6nAigRasoVOA_
    //public class FailurePreprocessor : IFailuresPreprocessor
    //{
    //    public FailureProcessingResult PreprocessFailures(FailuresAccessor pFailuresAccessor)
    //    {
    //        var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

    //        try
    //        {
    //            IList<FailureMessageAccessor> failureMessages = pFailuresAccessor.GetFailureMessages();

    //            if(failureMessages.Count == 0) return FailureProcessingResult.Continue;

    //            // foreach 문 사용 - 실패 메시지 "fMsg" 모두 방문 
    //            foreach (FailureMessageAccessor fMsg in failureMessages)
    //            {
    //                pFailuresAccessor.DeleteWarning(fMsg); // 실패 메시지 "fMsg" 삭제

    //                // string test = fMsg.GetDescriptionText();
    //                // FailureDefinitionId testId = fMsg.GetFailureDefinitionId();
    //            }

    //            return FailureProcessingResult.ProceedWithCommit;
    //        }
    //        catch(Exception ex)
    //        {
    //            Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
    //            // TaskDialog.Show(HTSHelper.ErrorTitle, ex.Message);
    //            throw;
    //        }
    //    }
    //}
}
