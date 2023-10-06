using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI;

using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.Generic;

using RevitBoxSeumteo.Common.RibbonBase;


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
}
