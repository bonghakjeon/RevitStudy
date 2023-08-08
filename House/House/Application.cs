using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autodesk.Revit;
using System.Diagnostics;
using System.IO;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.UI.Events;
using System.Drawing;   // House 프로젝트 파일 -> 참조 -> System.Drawing.dll 파일 추가  
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

// Revit 리본 만들기 
// 참고 URL - https://blog.naver.com/sojunbeer/221756535707

// Revit Ribbon API 문서 
// 참고 URL - https://help.autodesk.com/view/RVT/2024/ENU/?guid=Revit_API_Revit_API_Developers_Guide_Introduction_Add_In_Integration_External_Application_html
// 참고 2 URL - https://help.autodesk.com/view/RVT/2024/ENU/?guid=Revit_API_Revit_API_Developers_Guide_Introduction_Add_In_Integration_Ribbon_Panels_and_Controls_html

namespace House
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    [Autodesk.Revit.Attributes.Regeneration(Autodesk.Revit.Attributes.RegenerationOption.Manual)]
    public class CreateRibbon : IExternalApplication // Revit이 실행될 때, Ribbon(메뉴, 탭, 버튼)이 등록되어야 하기 때문에 인터페이스 "IExternalApplication"를 상속 받아야 함.
    {
        #region IExternalApplication Members

        /// <summary>
        /// Revit 시작 (OnStartup)
        /// </summary>
        /// <param name="application"></param>
        /// <returns></returns>
        public Result OnStartup(UIControlledApplication application)
        {
            CreateRibbonSamplePanel(application); // 리본 메뉴 등록 

            return Result.Succeeded;
        }

        /// <summary>
        /// Revit 종료 (OnShutdown)
        /// </summary>
        /// <param name="application"></param>
        /// <returns></returns>
        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }

        /// <summary>
        /// 리본 메뉴 등록
        /// </summary>
        private void CreateRibbonSamplePanel(UIControlledApplication application)
        {
            try
            {
                // 1 단계 : 리본 탭 "House" 생성 
                string tabName = "HOUSE";
                application.CreateRibbonTab(tabName);   // 참고 URL - https://www.revitapidocs.com/2024/8ce17489-75ee-ae81-306d-58f9c505c80c.htm

                // 2 단계 : 리본 탭 "House" 안에 속하는 리본 패널 "Create Wall" 생성 
                string panelName = "Create Wall";
                RibbonPanel panel = application.CreateRibbonPanel(tabName, panelName);

                // 3 단계 : 리본 패널 "Create Wall"안에 속하는 리본 버튼 "Wall" 생성 
                SplitButtonData buttonData = new SplitButtonData("Wall", "Wall"); // buttonData 생성 및 buttonData 이름 "Wall" 설정
                SplitButton button = panel.AddItem(buttonData) as SplitButton;    // 리본 패널 "Create Wall"에 buttonData 추가 및 SplitButton (button) 생성 

                // 마지막 단계 : Command와 같은 실제 명령(namespace "House" -> class "Command")이 되는 리본 버튼 "Wall" 객체 생성 
                // "House.Command" - 실제 명령이 시작되는 위치 (namespace "House" -> class "Command")
                // House.dll 파일 생성할 때, Debug - x64 모드로 컴파일 하므로 House.dll 파일은 아래 파일 경로로 생성된다.
                // D:\bhjeon\RevitStudy\House\House\bin\x64\Debug\House.dll
                PushButton pushbutton = button.AddPushButton(new PushButtonData("Wall",
                  "Wall", @"D:\bhjeon\RevitStudy\House\House\bin\x64\Debug\House.dll", "House.Command"));

                // 리본 버튼 "Wall" 아이콘(이미지) 등록(셋팅) 
                // House 프로젝트 파일 -> 참조 -> PresentationCore.dll 파일 추가  
                // 리본 버튼 "Wall" 아이콘 이미지 사이즈 (32 X 32) - 기존 Revit에 존재하는 다른 리본 탭 안에 속하는 버튼들 사이즈도 32 X 32 이기 때문
                pushbutton.LargeImage = convertFromBitmap(House.Properties.Resources.settings);     // 아이콘 셋팅
                pushbutton.Image = convertFromBitmap(House.Properties.Resources.settings);     // 아이콘 셋팅 
                pushbutton.ToolTip = "벽을 작성 합니다.";                                        // 툴팁 셋팅 
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return;
            }

        }

        /// <summary>
        /// 리소스에 등록된 이미지 파일(.png, .jpg 등등...)을 BitmapSource으로 convert 해주는 메서드 
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        BitmapSource convertFromBitmap(Bitmap bitmap)
        {
            // House 프로젝트 파일 -> 참조 -> WindowsBase.dll 파일 추가  
            return System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
               bitmap.GetHbitmap(),
               IntPtr.Zero,
               Int32Rect.Empty,
               BitmapSizeOptions.FromEmptyOptions());
        }

        #endregion IExternalApplication Members
    }
}
