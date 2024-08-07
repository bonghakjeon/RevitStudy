﻿using Serilog;
using System;
using System.Reflection;
using System.Windows.Forms;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

using RevitUpdater.Common.LogBase;
using RevitUpdater.Common.UpdaterBase;
using RevitUpdater.Common.RequestBase;
using RevitUpdater.UI.MEPUpdater;

namespace RevitUpdater.Common.Managers
{
    public class FormManager
    {
        #region ShowForm

        /// <summary>
        /// Modaless 폼(.Show()) 형식 화면 출력 
        /// </summary>
        public static void ShowForm(System.Windows.Forms.Form pModalessForm, UIApplication rvUIApp, Type pModalessFormType)
        {
            string modalessFormName = string.Empty;               // Modaless 폼 객체 이름

            var currentMethod = MethodBase.GetCurrentMethod();    // 로그 기록시 현재 실행 중인 메서드 위치 

            try
            {
                modalessFormName = pModalessFormType.Name;

                // Modaless 폼 객체가 null이거나 삭제된 경우 
                if(pModalessForm is null || pModalessForm.IsDisposed)
                {
                    switch(modalessFormName)
                    {
                        case UpdaterHelper.MEPUpdaterFormName:   // MEP 업데이터인 경우 

                            AddInId addInId = rvUIApp.ActiveAddInId;                                 // RevitBox 업데이터 Command 아이디

                            MEPUpdaterRequestHandler mepHandler = new MEPUpdaterRequestHandler();    // MEP 업데이터 외부 요청 핸들러 객체 mepHandler 생성 
                            ExternalEvent exEvent = ExternalEvent.Create(mepHandler);                // MEP 업데이터 폼 객체가 사용할 외부 이벤트 생성 

                            pModalessForm = new MEPUpdaterForm(exEvent, mepHandler, rvUIApp, addInId); // MEP 업데이터 폼 객체 생성

                            break;

                        default:
                            TestRequestHandler testHandler = new TestRequestHandler();
                            break;
                    }

                    pModalessForm.Show();   // Modaless 폼(.Show()) 형식 화면 출력 
                }
            }
            catch(Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                throw;   // 오류 발생시 상위 호출자 예외처리 전달 throw 
            }
        }

        #endregion ShowForm

        #region GetForm

        // TODO : 특정 인터페이스를 상속 받는 폼 객체 찾기 메서드 "GetForm" 구현 (2024.03.20 jbh)
        // 참고 URL - https://blog.naver.com/jskimmail/222706926984
        // 참고 2 URL - https://learn.microsoft.com/ko-kr/dotnet/api/system.type.getinterface?view=net-8.0

        /// <summary>
        /// 특정 인터페이스를 상속 받는 현재 실행중인 폼 객체 찾기
        /// </summary>
        public static System.Windows.Forms.Form GetForm(Type pInterfaceType, Type pModalessFormType)
        {
            System.Windows.Forms.Form form = null;                // 특정 인터페이스를 상속 받는 폼 객체
            var currentMethod = MethodBase.GetCurrentMethod();    // 로그 기록시 현재 실행 중인 메서드 위치 

            try
            {
                Log.Information(Logger.GetMethodPath(currentMethod) + $"인터페이스 {pInterfaceType.Name} 상속 받은 폼 객체 {pModalessFormType.Name} 찾기 시작");

                // Revit 응용 프로그램에서 현재 실행 중인 모든 폼 화면 목록 가져오도록 구현  
                FormCollection openForms = Application.OpenForms;

                foreach(System.Windows.Forms.Form openForm in openForms)
                {
                    string openFormName = openForm.Name;
                    Type openFormInterface = openForm.GetType().GetInterface(pInterfaceType.Name);

                    if(openFormName.Equals(pModalessFormType.Name)
                        && openFormInterface is not null)
                    {
                        form = openForm;
                        break;
                    }
                }

                Log.Information(Logger.GetMethodPath(currentMethod) + $"인터페이스 {pInterfaceType.Name} 상속 받은 폼 객체 {pModalessFormType.Name} 찾기 완료");

                return form;
            }
            catch(Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                throw;   // 오류 발생시 상위 호출자 예외처리 전달 throw 
            }
        }

        #endregion GetForm
    }
}
