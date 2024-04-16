﻿using Serilog;

using System;
using System.Reflection;

using HTSBIM2019.Common.HTSBase;
using HTSBIM2019.Common.LogBase;
using HTSBIM2019.Common.RibbonBase;
using HTSBIM2019.Common.Managers;
using HTSBIM2019.Settings;
using HTSBIM2019.Utils.MEPUpdater;

using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;

namespace HTSBIM2019
{
    // TODO : Revit AddIn 개발 소스를 비쥬얼스튜디오 2022 .net Core 버전(8.0)을 사용하려면 Revit 2025 버전 부터 사용이 가능하므로 현 시점에서 해당 소스는 .net FrameWork 4.8에서만 구동시킬 수 있다. (2024.03.11 jbh)

    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    internal class App : IExternalApplication   // Revit이 실행될 때, Ribbon(메뉴, 탭, 버튼)이 등록되어야 하기 때문에 인터페이스 "IExternalApplication"를 상속 받아야 함.
    {
        #region 프로퍼티

        #endregion 프로퍼티 

        #region 기본 메소드

        /// <summary>
        /// Revit 시작 (OnStartup)
        /// </summary>
        public Result OnStartup(UIControlledApplication application)
        {
            string dllParentDirPath = string.Empty;              // dll 파일의 부모 폴더 경로

            // TODO : 로그 기록시 현재 실행 중인 메서드 위치 기록하기 (2024.01.22 jbh)
            // 참고 URL - https://slaner.tistory.com/73
            // 참고 2 URL - https://stackoverflow.com/questions/4132810/how-can-i-get-a-method-name-with-the-namespace-class-name
            // 참고 3 URL - https://stackoverflow.com/questions/44153/can-you-use-reflection-to-find-the-name-of-the-currently-executing-method
            var currentMethod = MethodBase.GetCurrentMethod();

            try
            {
                dllParentDirPath = DirectoryManager.GetDllParentDirectoryPath(HTSHelper.AssemblyFilePath);  // dll 파일(HTSBIM2019.dll)이 속한 부모 폴더 경로 가져오기 
                //Logger.ConfigureLogger(HTSHelper.AssemblyName, HTSHelper.LogDirPath);   // Serilog 로그 초기 설정 
                Logger.ConfigureLogger(HTSHelper.LogFileCountLimit, HTSHelper.AssemblyName, dllParentDirPath);   // Serilog 로그 초기 설정 

                Log.Information(Logger.GetMethodPath(currentMethod) + "HTS Revit 업데이터 프로그램 시작");

                Ribbon.CreateRibbonControl(application);   // 리본 메뉴 등록

                AddInId addInId = application.ActiveAddInId;
                AppSetting.Default.UpdaterBase.MEPUpdater = new MEPUpdater(addInId);

                return Result.Succeeded;
            }
            catch (Exception ex)
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
            // TODO : 에러 처리 필요시 메서드 "OnShutdown" 몸체 안에 try - catch문으로 구현 예정 (2024.01.22 jbh)
            return Result.Succeeded;
        }

        #endregion 기본 메소드

        #region Sample

        #endregion Sample
    }
}