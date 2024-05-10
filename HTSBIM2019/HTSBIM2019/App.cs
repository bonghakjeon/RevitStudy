using Serilog;

using System;
using System.Reflection;
using System.Collections.Generic;

using HTSBIM2019.Common.HTSBase;
using HTSBIM2019.Common.LogBase;
using HTSBIM2019.Common.RibbonBase;
using HTSBIM2019.Common.Managers;
using HTSBIM2019.Settings;
using HTSBIM2019.Utils.MEPUpdater;
using HTSBIM2019.Utils.Failure;
using HTSBIM2019.UI.CreateParams;

using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Events;
using Autodesk.Revit.UI.Events;

namespace HTSBIM2019
{
    // TODO : Revit AddIn 개발 소스를 비쥬얼스튜디오 2022 .net Core 버전(8.0)을 사용하려면 Revit 2025 버전 부터 사용이 가능하므로 현 시점에서 해당 소스는 .net FrameWork 4.8에서만 구동시킬 수 있다. (2024.03.11 jbh)

    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    internal class App : IExternalApplication   // Revit이 실행될 때, Ribbon(메뉴, 탭, 버튼)이 등록되어야 하기 때문에 인터페이스 "IExternalApplication"를 상속 받아야 함.
    {
        #region 프로퍼티

        /// <summary>
        /// 활성화된 Revit 문서 
        /// </summary>
        public Document RevitDoc { get; set; }

        #endregion 프로퍼티 

        #region 기본 메소드

        /// <summary>
        /// Revit 시작 (OnStartup)
        /// </summary>
        public Result OnStartup(UIControlledApplication application)
        {
            // string dllParentDirPath = string.Empty;              // dll 파일의 부모 폴더 경로

            // TODO : 로그 기록시 현재 실행 중인 메서드 위치 기록하기 (2024.01.22 jbh)
            // 참고 URL - https://slaner.tistory.com/73
            // 참고 2 URL - https://stackoverflow.com/questions/4132810/how-can-i-get-a-method-name-with-the-namespace-class-name
            // 참고 3 URL - https://stackoverflow.com/questions/44153/can-you-use-reflection-to-find-the-name-of-the-currently-executing-method
            var currentMethod = MethodBase.GetCurrentMethod();

            try
            {
                //Log.Information(Logger.GetMethodPath(currentMethod) + "HTS Revit 업데이터 프로그램 시작");
                Log.Information(Logger.GetMethodPath(currentMethod) + $"{HTSHelper.AssemblyName} 프로그램 시작");

                AppSetting.Default.DirectoryBase.ParentDirPath = DirectoryManager.GetDllParentDirectoryPath(HTSHelper.AssemblyFilePath);  // dll 파일(HTSBIM2019.dll)이 속한 부모 폴더 경로 가져오기 
                AppSetting.Default.DirectoryBase.LogDirPath = HTSHelper.LogDirPath;

                AddInId addInId = application.ActiveAddInId;                       // 활성화된 애드인 애플리케이션(또는 Command) 아이디

                AppSetting.Default.UpdaterBase = UpdaterSetting.GetUpdaterInstance(addInId);

                // TODO : 로그 폴더 디렉토리 경로 변경 (2024.04.23 jbh)
                //        경로 변경 사유 - 폴더 경로 "C:\Program Files\ImagineBuilder\HTSBIM2019"에 접근불가 
                // 참고 URL - https://www.c-sharpcorner.com/blogs/access-to-path-is-denied-permissions-error

                // dllParentDirPath = DirectoryManager.GetDllParentDirectoryPath(HTSHelper.AssemblyFilePath);  // dll 파일(HTSBIM2019.dll)이 속한 부모 폴더 경로 가져오기 
                //Logger.ConfigureLogger(HTSHelper.AssemblyName, HTSHelper.LogDirPath);   // Serilog 로그 초기 설정 
                //Logger.ConfigureLogger(HTSHelper.LogFileCountLimit, HTSHelper.AssemblyName, dllParentDirPath);   // Serilog 로그 초기 설정 
                Logger.ConfigureLogger(HTSHelper.LogFileCountLimit, HTSHelper.AssemblyName, AppSetting.Default.DirectoryBase.LogDirPath);   // Serilog 로그 초기 설정 

                Ribbon.CreateRibbonControl(application);   // 리본 메뉴 등록

                // AddInId addInId = application.ActiveAddInId;
                // AppSetting.Default.UpdaterBase.MEPUpdater = new MEPUpdater(addInId);
                // AppSetting.Default.UpdaterBase.MEPUpdater = UpdaterSetting.GetUpdaterInstance(addInId);

                // TODO : 필요시 아래 주석친 DocumentCreated, DocumentOpened 이벤트 메소드 가입 코드 사용 예정 (2024.04.17 jbh)
                // TODO : DocumentCreated 이벤트 메소드 App_DocumentCreated 가입 구현 (2024.04.16 jbh)
                // 참고 URL - https://www.revitapidocs.com/2015/89f514bf-f067-308d-0518-f050450bde72.htm
                // application.ControlledApplication.DocumentCreated += new EventHandler<DocumentCreatedEventArgs>(App_DocumentCreated);

                // TODO : DocumentOpened 이벤트 메소드 App_DocumentOpened 가입 구현 (2024.04.16 jbh)
                // 참고 URL - https://www.revitapidocs.com/2015/7e5bc7a1-0475-b2ec-0aec-c410015737fe.htm
                // application.ControlledApplication.DocumentOpened += new EventHandler<DocumentOpenedEventArgs>(App_DocumentOpened);

                // TODO : DocumentClosing 이벤트 메소드 App_DocumentClosing 가입 구현 (2024.05.09 jbh)
                // 참고 URL - https://www.revitapidocs.com/2015/2f0a7a6f-ed8b-0518-c5f8-edb14b321296.htm
                // application.ControlledApplication.DocumentClosing += new EventHandler<DocumentClosingEventArgs>(App_DocumentClosing);

                // TODO : DialogBoxShowing 이벤트 메소드 App_DialogBoxShowing 가입 구현 (2024.05.09 jbh)
                // application.DialogBoxShowing += new EventHandler<DialogBoxShowingEventArgs>(App_DialogBoxShowing);

                return Result.Succeeded;
            }
            catch(Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                TaskDialog.Show(HTSHelper.ErrorTitle, ex.Message);
                return Result.Failed;
            }
        }

        /// <summary>
        /// Revit 종료 (OnShutdown)
        /// </summary>
        public Result OnShutdown(UIControlledApplication application)
        {
            // TODO : 필요시 아래 주석친 DocumentCreated, DocumentOpened 이벤트 메소드 해제 코드 사용 예정 (2024.04.17 jbh)
            // TODO : DocumentCreated 이벤트 메소드 App_DocumentCreated 해제 구현 (2024.04.16 jbh)
            // 참고 URL - https://www.revitapidocs.com/2015/89f514bf-f067-308d-0518-f050450bde72.htm
            // application.ControlledApplication.DocumentCreated -= new EventHandler<DocumentCreatedEventArgs>(App_DocumentCreated);

            // TODO : DocumentOpened 이벤트 메소드 App_DocumentOpened 해제 구현 (2024.04.16 jbh)
            // 참고 URL - https://www.revitapidocs.com/2015/7e5bc7a1-0475-b2ec-0aec-c410015737fe.htm
            // application.ControlledApplication.DocumentOpened -= new EventHandler<DocumentOpenedEventArgs>(App_DocumentOpened);

            // TODO : DocumentClosing 이벤트 메소드 App_DocumentClosing 해제 구현 (2024.05.10 jbh)
            // 참고 URL - https://www.revitapidocs.com/2015/2f0a7a6f-ed8b-0518-c5f8-edb14b321296.htm
            // application.ControlledApplication.DocumentClosing -= new EventHandler<DocumentClosingEventArgs>(App_DocumentClosing);

            // TODO : DialogBoxShowing 이벤트 메소드 App_DialogBoxShowing 해제 구현 (2024.05.10 jbh)
            // application.DialogBoxShowing -= new EventHandler<DialogBoxShowingEventArgs>(App_DialogBoxShowing);

            // TODO : 에러 처리 필요시 메서드 "OnShutdown" 몸체 안에 try - catch문으로 구현 예정 (2024.01.22 jbh)
            return Result.Succeeded;
        }

        #endregion 기본 메소드

        #region App_DocumentCreated

        /// <summary>
        /// Revit이 새로 생성한 문서(Document)를 열고 난 직후 실행되는 이벤트 메소드
        /// 전용 문서(Document) 수정 작업도 가능
        /// </summary>
        private void App_DocumentCreated(object sender, DocumentCreatedEventArgs args)
        {
            var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

            try
            {
                RevitDoc = args.Document;   // 이벤트 인수 args에서 Revit 전용문서(Document) 가져오기

                // TODO : Revit 전용문서(Document) 가져온 후 매개변수 생성되는 로직 아래에 구현하기 (2024.04.16 jbh)
                TaskDialogResult dialogResult = TaskDialog.Show(HTSHelper.NoticeTitle, "MEP 사용 기록 관리\r\n매개변수 생성을 원하십니까?", TaskDialogCommonButtons.Yes | TaskDialogCommonButtons.No);

                if(TaskDialogResult.Yes == dialogResult)
                {
                    // TODO : Revit Updater 매개변수 추가 및 대기 처리 화면(WaitForm) 구현 (2024.04.16 jbh)
                    CreateParams createParams = new CreateParams();
                    createParams.ToCreateUpdaterParameter(RevitDoc);
                }
            }
            catch(Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                TaskDialog.Show(HTSHelper.ErrorTitle, ex.Message);
            }
        }

        #endregion App_DocumentCreated

        #region App_DocumentOpened

        /// <summary>
        /// Revit이 기존에 생성한 문서(Document)를 열고 난 직후 실행되는 이벤트 메소드 
        /// 전용 문서(Document) 수정 작업도 가능
        /// </summary>
        private void App_DocumentOpened(object sender, DocumentOpenedEventArgs args)
        {
            var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

            try
            {
                RevitDoc = args.Document;   // 이벤트 인수 args에서 Revit 전용문서(Document) 가져오기

                // 해당 Transaction이 끝날 때까지는 화면 상에서는 다른 기능을 실행할 수 있고 다른 기능의 화면도 출력되지만
                // 다른 기능을 실행해서 데이터를 변경할 수 없다.(다른 작업이나 Command 명령이 끼어들 수 없다.)
                using(Transaction transaction = new Transaction(RevitDoc))
                {
                    // TODO : 필요시 아래 주석친 코드 사용 예정 (2024.05.09 jbh)
                    //FailureHandlingOptions failureOptions = transaction.GetFailureHandlingOptions();
                    //FailurePreprocessor preproccessor = new FailurePreprocessor();

                    //failureOptions.SetFailuresPreprocessor(preproccessor);
                    //transaction.SetFailureHandlingOptions(failureOptions);

                    Log.Information(Logger.GetMethodPath(currentMethod) + "App_DocumentOpened 작업 시작");

                    // transaction.Start(HTSHelper.Start); 부터 transaction.Commit(); 까지가 연산처리를 하는 하나의 작업단위이다.
                    transaction.Start(HTSHelper.Start);  // 해당 "HTSBIM2019" 프로젝트에서 연산처리(객체 생성, 정보 변경 및 삭제 등등... ) 시작

                    transaction.Commit();    // 해당 "HTSBIM2019" 프로젝트에서 연산처리(객체 생성, 정보 변경 및 삭제 등등... )된 결과 커밋

                    Log.Information(Logger.GetMethodPath(currentMethod) + "App_DocumentOpened 작업 완료");
                }   // 여기서 Dispose (리소스 해제) 처리 

                // TODO : 필요시 아래 주석친 코드 사용 예정 (2024.05.09 jbh)
                // TODO : Revit 전용문서(Document) 가져온 후 매개변수 생성되는 로직 아래에 구현하기 (2024.04.16 jbh)
                // TaskDialogResult dialogResult = TaskDialog.Show(HTSHelper.NoticeTitle, "MEP 사용 기록 관리\r\n매개변수 생성을 원하십니까?", TaskDialogCommonButtons.Yes | TaskDialogCommonButtons.No);

                // if(TaskDialogResult.Yes == dialogResult)
                // {
                //     // TODO : Revit Updater 매개변수 추가 및 대기 처리 화면(WaitForm) 구현 (2024.04.16 jbh)
                //     CreateParams createParams = new CreateParams();
                //     createParams.ToCreateUpdaterParameter(RevitDoc);
                // }
            }
            catch (Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                TaskDialog.Show(HTSHelper.ErrorTitle, ex.Message);
            }
        }

        #endregion App_DocumentOpened

        #region App_DocumentClosing

        /// <summary>
        /// Revit이 기존에 생성한 문서(Document)를 막 닫으려고 할 때 실행되는 이벤트 메소드 
        /// </summary>
        private void App_DocumentClosing(object sender, DocumentClosingEventArgs args)
        {
            var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

            try
            {
                RevitDoc = args.Document;   // 이벤트 인수 args에서 Revit 전용문서(Document) 가져오기

                MEPUpdater updater = AppSetting.Default.UpdaterBase.MEPUpdater;
                UpdaterId updaterId = updater.GetUpdaterId();

                // 업데이터가 기존에 이미 등록된 경우만 실행 
                if (true == UpdaterRegistry.IsUpdaterRegistered(updaterId, RevitDoc))
                {
                    // UpdaterRegistry.RemoveAllTriggers(updaterId);                  // 지정된 updaterId를 가진 업데이터와 연결된 모든 트리거 제거. 업데이터 등록을 취소하지 않음.

                    UpdaterRegistry.UnregisterUpdater(updaterId, RevitDoc);     // Revit 문서(RevitDoc)에 지정된 updaterId를 가진 업데이터와 연결된 업데이터 프로그램 등록 취소 (해당 트리거 포함 레지스트리에서 완전 제거 처리)
                }

                // TODO : MEP 업데이터 싱글톤 객체 프로퍼티 MEPUpdater null 초기화 소스코드 필요 없을시 주석 처리 예정 (2024.05.09 jbh)
                // AppSetting.Default.UpdaterBase.MEPUpdater = null;
            }
            catch (Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                TaskDialog.Show(HTSHelper.ErrorTitle, ex.Message);
            }
        }

        #endregion App_DocumentClosing

        #region App_DialogBoxShowing

        /// <summary>
        /// Modal 형식 경고 메시지 Dialog 팝업 화면 출력시 실행되는 이벤트 메서드
        /// </summary>
        private void App_DialogBoxShowing(object sender, DialogBoxShowingEventArgs args)
        {
            var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

            try
            {
                TaskDialogShowingEventArgs tdArgs = args as TaskDialogShowingEventArgs;

                string testMsg = tdArgs.Message;
            }
            catch (Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                TaskDialog.Show(HTSHelper.ErrorTitle, ex.Message);
            }
        }

        #endregion App_DialogBoxShowing

        #region Sample

        #endregion Sample
    }
}
