using Serilog;

using System;
using System.Reflection;

using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

using RevitUpdater.Common.LogManager;
using RevitUpdater.Common.UpdaterBase;
using RevitUpdater.UI.MEPUpdater;

namespace RevitUpdater
{
    #region Command

    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    /// <summary>
    /// 테스트용 Command
    /// </summary>
    public class Command
    {

    }

    #endregion Command

    // TODO : Revit AddIn 개발 소스를 비쥬얼스튜디오 2022 .net Core 버전(8.0)을 사용하려면 Revit 2025 버전 부터 사용이 가능하므로 현 시점에서 해당 소스는 .net FrameWork 4.8에서만 구동시킬 수 있다. (2024.03.11 jbh)

    // 참고 URL - https://forums.autodesk.com/t5/revit-api-forum/iupdater-simple-example-needed/m-p/9893248
    // 참고 2 URL - https://adndevblog.typepad.com/aec/2016/02/revitapi-how-to-use-dmu-dynamic-model-update-api.html

    #region CmdMEPUpdater

    // TODO : RevitBox 업데이터 구현 (2024.03.13 jbh)
    // 참고 URL   - https://forums.autodesk.com/t5/revit-api-forum/iupdater-simple-example-needed/m-p/9893248
    // 참고 2 URL - https://thebuildingcoder.typepad.com/blog/2012/06/documentchanged-versus-dynamic-model-updater.html

    // RevitBox 업데이터 Command 클래스 CmdMEPUpdater class 접근 제한자 intenal 설정 (2024.03.14 jbh)
    // 접근 제한자 internal - internal 로 선언한 변수 또는 클래스는 CmdMEPUpdater 프로젝트 내에서만 접근이 가능하고, 외부에선 접근이 불가능
    // 참고 URL - https://slaner.tistory.com/69

    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    /// <summary>
    /// RevitBox 업데이터 Command
    /// </summary>
    internal class CmdMEPUpdater : IExternalCommand
    {
        #region 기본 메소드

        // TODO : 콜백 함수 Execute 구현 (2024.03.11 jbh)
        // 콜백(CallBack) 함수란? 시스템이 사용자가 요청한 처리를 하다가 특정 이벤트를 발생시켜 해당 이벤트를 처리해달라고 역으로 전달해 오는 함수
        // 참고 URL - https://nephrolepis.tistory.com/12
        // 참고 2 URL - https://todaycode.tistory.com/24
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            // TODO : 로그 기록시 현재 실행 중인 메서드 위치 기록하기 (2024.01.22 jbh)
            // 참고 URL - https://slaner.tistory.com/73
            // 참고 2 URL - https://stackoverflow.com/questions/4132810/how-can-i-get-a-method-name-with-the-namespace-class-name
            // 참고 3 URL - https://stackoverflow.com/questions/44153/can-you-use-reflection-to-find-the-name-of-the-currently-executing-method
            var currentMethod = MethodBase.GetCurrentMethod();

            try
            {
                Log.Information(Logger.GetMethodPath(currentMethod) + "RevitBox 업데이터 Command - Execute 시작");

                UIApplication uiapp = commandData.Application;            // 애플리케이션 객체 
                Document doc = uiapp.ActiveUIDocument.Document;           // 활성화된 Revit 문서 
                AddInId addInId = uiapp.ActiveAddInId;                    // RevitBox 업데이터 Command 아이디

                TaskDialog.Show("RevitBox Update...", "테스트 진행 중...");

                var mepUpdater = new MEPUpdater(doc, addInId);
                mepUpdater.ShowDialog();

                Log.Information(Logger.GetMethodPath(currentMethod) + "RevitBox 업데이터 Command - Execute 종료");

                return Result.Succeeded;
            }
            catch (Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                TaskDialog.Show(UpdaterHelper.ErrorTitle, ex.Message);
                return Result.Failed;
            }
        }

        #endregion 기본 메소드
    }

    #endregion CmdMEPUpdater

    #region Sample

    #endregion Sample
}
