using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.UI;

namespace Calculator.Common.RibbonBase
{
    // TODO : 리본 탭에 속하는 버튼에 이미지 넣지 않고 텍스트만 넣어서 구현 (2024.01.22 jbh)
    // 참고 URL - https://help.autodesk.com/view/RVT/2024/ITA/?guid=Revit_API_Revit_API_Developers_Guide_Introduction_Add_In_Integration_Ribbon_Panels_and_Controls_html
    public class Ribbon
    {
        #region CreateRibbonControl

        /// <summary>
        /// 리본 컨트롤(메뉴) 등록(생성)
        /// </summary>s
        public static void CreateRibbonControl(UIControlledApplication application)
        {
            try
            {
                // 1 단계 : 리본 탭 "테스트-세움터" 생성
                // 참고 URL - https://www.revitapidocs.com/2024/8ce17489-75ee-ae81-306d-58f9c505c80c.htm
                application.CreateRibbonTab(RibbonHelper.tabName);

                // 2 단계 : 리본 탭 "테스트-계산기" 안에 속하는 리본 패널 "계산기 템플릿" 생성
                RibbonPanel panel = application.CreateRibbonPanel(RibbonHelper.tabName, RibbonHelper.panelName);

                // List<RibbonPanel> PanelList = new List<RibbonPanel>();
                // PanelList.Add(panel);

                // 3 단계 : 리본 패널 "세움터 템플릿"안에 속하는 리본 버튼 "테스트 AIS 매개변수 생성" 생성 
                // SplitButtonData ParameterButtonData = new SplitButtonData(RibbonHelper.ParameterbuttonName, RibbonHelper.ParameterbuttonName);   // buttonData 생성 및 buttonData 이름 "테스트 AIS 매개변수 생성" 설정
                // SplitButton ParameterButton = panel.AddItem(ParameterButtonData) as SplitButton;                       // 리본 패널 "세움터 템플릿"에 buttonData 추가 및 SplitButton (button) 생성 

                // 4 단계 : 리본 패널 "세움터 템플릿"안에 속하는 리본 버튼 "테스트 AIS 템플릿" 생성 
                // SplitButtonData ButtonData = new SplitButtonData(RibbonHelper.buttonName, RibbonHelper.buttonName);    // buttonData 생성 및 buttonData 이름 "테스트 AIS 템플릿" 설정
                // SplitButton Button = panel.AddItem(ButtonData) as SplitButton;              // 리본 패널 "세움터 템플릿"에 buttonData 추가 및 SplitButton (button) 생성 



                // SplitButtonData CalButtonData = new SplitButtonData(RibbonHelper.CalbuttonName, RibbonHelper.CalbuttonName);    // buttonData 생성 및 buttonData 이름 "테스트 AIS 템플릿" 설정
                // SplitButton CalButton = panel.AddItem(CalButtonData) as SplitButton;              // 리본 패널 "세움터 템플릿"에 buttonData 추가 및 SplitButton (button) 생성 



                // 5 단계 : ParameterCommand와 같은 실제 명령(namespace "Calculator" -> class "ParameterCommand")이 되는 리본 버튼 "테스트 AIS 매개변수 생성" 객체 생성 
                // "Calculator.ParameterCommand" - 실제 명령이 시작되는 위치 (namespace "Calculator" -> class "ParameterCommand")
                // Calculator.dll 파일 생성할 때, Debug - x64 모드로 컴파일 하므로 Calculator.dll 파일은 아래 파일 경로로 생성된다.
                // D:\bhjeon\RevitStudy\Calculator\Calculator\bin\x64\Debug\Calculator.dll
                // PushButton pushParameterButton = ParameterButton.AddPushButton(new PushButtonData(RibbonHelper.ParameterbuttonName, RibbonHelper.ParameterbuttonName, RibbonHelper.dllPath, RibbonHelper.ParameterCommandPath));



                // 마지막 단계 : Command와 같은 실제 명령(namespace "Calculator" -> class "Command")이 되는 리본 버튼 "테스트 AIS 템플릿" 객체 생성 
                // "Calculator.Command" - 실제 명령이 시작되는 위치 (namespace "Calculator" -> class "Command")
                // Calculator.dll 파일 생성할 때, Debug - x64 모드로 컴파일 하므로 Calculator.dll 파일은 아래 파일 경로로 생성된다.
                // D:\bhjeon\RevitStudy\Calculator\Calculator\bin\x64\Debug\Calculator.dll


                PushButtonData pushCalButtonData = new PushButtonData(RibbonHelper.CalbuttonName, RibbonHelper.CalbuttonName, RibbonHelper.dllPath, RibbonHelper.CommandPath);
                // panel.AddItem(pushCalButtonData);


                // PushButton pushCalButton          = panel.AddItem(pushCalButtonData) as PushButton;
                // pushCalButton.Image = null;
                // pushCalButton.LargeImage = null;
                // pushCalButton.LargeImage.
                // RibbonButtonDatra
                // RibbonButton CalButton = panel.AddItem(pushCalButtonData) as RibbonButton;


                PushButtonData pushTestButtonData = new PushButtonData(RibbonHelper.TestbuttonName, RibbonHelper.TestbuttonName, RibbonHelper.dllPath, RibbonHelper.TestCommandPath);
                // PushButton pushTestButton         = panel.AddItem(pushTestButtonData) as PushButton;

                // PushButton pushCalButton = CalButton.AddPushButton(new PushButtonData(RibbonHelper.CalbuttonName, RibbonHelper.CalbuttonName, RibbonHelper.dllPath, RibbonHelper.CommandPath));

                List<RibbonItem> projectButtons = new List<RibbonItem>();
                projectButtons.AddRange(panel.AddStackedItems(pushCalButtonData, pushTestButtonData));




                // TODO : 리본 버튼 2개 이상 구현 및 Command, ParameterCommand 바인딩하기 위해 List 객체 "testPanelButtonList" 생성 (2023.10.05 jbh)
                // 참고 - 솔루션 파일 "RevitBox2024.sln" - 프로젝트 파일 "RevitBoxRibbon2024" - "Class1.cs" 소스파일 - 메서드 "OnStartup"
                // var testPanelButtonList = new BindingList<PushButton>();
                // testPanelButtonList.Add(pushParameterButton);
                // testPanelButtonList.Add(pushButton);

                // 리본 버튼 "테스트 AIS 템플릿" 아이콘(이미지) 등록(셋팅) 
                // House 프로젝트 파일 -> 참조 -> PresentationCore.dll 파일 추가  
                // 리본 버튼 "테스트 AIS 템플릿" 아이콘 이미지 사이즈 (32 X 32) - 기존 Revit에 존재하는 다른 리본 탭 안에 속하는 버튼들 사이즈도 32 X 32 이기 때문
                // TODO : 세움터 리본 버튼 아이콘 이미지 파일(SeumteoBtn.png) 사이즈 (32 X 17) 설정 및 리소스 추가 (2023.10.6 jbh)
                // pushButton.LargeImage = BitmapConverter.ConvertFromBitmap(Calculator.Properties.Resources.SeumteoBtn);     // 아이콘 셋팅
                // pushButton.Image = BitmapConverter.ConvertFromBitmap(Calculator.Properties.Resources.SeumteoBtn);     // 아이콘 셋팅 
                // pushButton.ToolTip = "테스트 - 계산기";                                                                       // 툴팁 셋팅 
                // pushCalButton.ToolTip = "테스트 - 계산기";                                                                       // 툴팁 셋팅 

                // CalButton.ToolTip = "테스트 - 계산기"; // 툴팁 셋팅

            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
                // TODO : 오류 발생시 예외처리 throw 구현 (2023.10.6 jbh)
                // 참고 URL - https://devlog.jwgo.kr/2009/11/27/thrownthrowex/
                throw;
            }
        }

        #endregion CreateRibbonControl

        #region Sample

        #endregion Sample
    }
}
