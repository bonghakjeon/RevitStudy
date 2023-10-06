using System;
// TODO : 클래스 "Bitmap" 사용하기 위해 using문 "System.Drawing" 추가 (2023.10.5 jbh)
// TODO : 클래스 "Bitmap" 사용하기 위해 dll 파일 "System.Drawing" 추가 (2023.10.5 jbh)
using System.Drawing;  
using System.Linq;
using System.Text;
using System.Windows;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.Generic;
// TODO : 클래스 "BitmapSource" 사용하기 위해 using문 "System.Windows.Media.Imaging" 추가 (2023.10.5 jbh)
using System.Windows.Media.Imaging;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI;


// Revit 리본 만들기 
// 참고 URL - https://blog.naver.com/sojunbeer/221756535707

// Revit Ribbon API 문서 
// 참고 URL - https://help.autodesk.com/view/RVT/2024/ENU/?guid=Revit_API_Revit_API_Developers_Guide_Introduction_Add_In_Integration_External_Application_html
// 참고 2 URL - https://help.autodesk.com/view/RVT/2024/ENU/?guid=Revit_API_Revit_API_Developers_Guide_Introduction_Add_In_Integration_Ribbon_Panels_and_Controls_html


namespace TestIronPython
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class CreateRibbon : IExternalApplication   // Revit이 실행될 때, Ribbon(메뉴, 탭, 버튼)이 등록되어야 하기 때문에 인터페이스 "IExternalApplication"를 상속 받아야 함.
    {
        #region 프로퍼티 

        /// <summary>
        /// 리본 탭 이름
        /// </summary>
        public const string tabName = "테스트-세움터";

        /// <summary>
        /// 리본 패널 이름
        /// </summary>
        public const string panelName = "세움터 템플릿";

        /// <summary>
        /// 리본 버튼 이름
        /// </summary>
        public const string ParameterbuttonName = "테스트 AIS 매개변수 생성";

        /// <summary>
        /// 리본 버튼 이름
        /// </summary>
        public const string buttonName = "테스트 AIS 템플릿";

        // TODO : 오류 코드 "CS1009" 오류 메시지 "인식할 수 없는 이스케이프 시퀀스입니다."
        //        해결하기 위해 string 객체 dllPath에 할당되는 문자열 맨 앞에 특수문자 "@" 추가 (2023.10.5 jbh)
        // 참고 URL - https://vesselsdiary.tistory.com/64
        /// <summary>
        /// dll 파일 이름(TestIronPython.dll)
        /// </summary>
        public const string dllPath = @"D:\bhjeon\RevitStudy\TestIronPython\TestIronPython\bin\x64\Debug\TestIronPython.dll";

        /// <summary>
        /// Command 명령 실행 위치
        /// </summary>
        public const string CommandPath = "TestIronPython.Command";

        /// <summary>
        /// ParameterCommand 명령 실행 위치
        /// </summary>
        public const string ParameterCommandPath = "TestIronPython.ParameterCommand";

        #endregion 프로퍼티 

        #region IExternalApplication Members

        /// <summary>
        /// Revit 시작 (OnStartup)
        /// </summary>
        /// <param name="application"></param>
        /// <returns></returns>
        public Result OnStartup(UIControlledApplication application)
        {
            CreateRibbonSamplePanel(application);   // 리본 메뉴 등록

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
                // 1 단계 : 리본 탭 "테스트-세움터" 생성
                // 참고 URL - https://www.revitapidocs.com/2024/8ce17489-75ee-ae81-306d-58f9c505c80c.htm
                application.CreateRibbonTab(tabName);

                // 2 단계 : 리본 탭 "테스트-세움터" 안에 속하는 리본 패널 "세움터 템플릿" 생성
                RibbonPanel panel = application.CreateRibbonPanel(tabName, panelName);

                // 3 단계 : 리본 패널 "세움터 템플릿"안에 속하는 리본 버튼 "테스트 AIS 매개변수 생성" 생성 
                SplitButtonData ParameterbuttonData = new SplitButtonData(ParameterbuttonName, ParameterbuttonName);   // buttonData 생성 및 buttonData 이름 "테스트 AIS 매개변수 생성" 설정
                SplitButton Parameterbutton = panel.AddItem(ParameterbuttonData) as SplitButton;                       // 리본 패널 "세움터 템플릿"에 buttonData 추가 및 SplitButton (button) 생성 

                // 4 단계 : 리본 패널 "세움터 템플릿"안에 속하는 리본 버튼 "테스트 AIS 템플릿" 생성 
                SplitButtonData buttonData = new SplitButtonData(buttonName, buttonName);   // buttonData 생성 및 buttonData 이름 "테스트 AIS 템플릿" 설정
                SplitButton button = panel.AddItem(buttonData) as SplitButton;              // 리본 패널 "세움터 템플릿"에 buttonData 추가 및 SplitButton (button) 생성 

                // 5 단계 : ParameterCommand와 같은 실제 명령(namespace "TestIronPython" -> class "ParameterCommand")이 되는 리본 버튼 "테스트 AIS 매개변수 생성" 객체 생성 
                // "TestIronPython.ParameterCommand" - 실제 명령이 시작되는 위치 (namespace "TestIronPython" -> class "ParameterCommand")
                // TestIronPython.dll 파일 생성할 때, Debug - x64 모드로 컴파일 하므로 TestIronPython.dll 파일은 아래 파일 경로로 생성된다.
                // D:\bhjeon\RevitStudy\TestIronPython\TestIronPython\bin\x64\Debug\TestIronPython.dll
                PushButton pushParameterButton = Parameterbutton.AddPushButton(new PushButtonData(ParameterbuttonName, ParameterbuttonName, dllPath, ParameterCommandPath));

                

                // 마지막 단계 : Command와 같은 실제 명령(namespace "TestIronPython" -> class "Command")이 되는 리본 버튼 "테스트 AIS 템플릿" 객체 생성 
                // "TestIronPython.Command" - 실제 명령이 시작되는 위치 (namespace "TestIronPython" -> class "Command")
                // TestIronPython.dll 파일 생성할 때, Debug - x64 모드로 컴파일 하므로 TestIronPython.dll 파일은 아래 파일 경로로 생성된다.
                // D:\bhjeon\RevitStudy\TestIronPython\TestIronPython\bin\x64\Debug\TestIronPython.dll
                PushButton pushButton = button.AddPushButton(new PushButtonData(buttonName, buttonName, dllPath, CommandPath));

                

                // TODO : 리본 버튼 2개 이상 구현 및 Command, ParameterCommand 바인딩하기 위해 List 객체 "testPanelButtonList" 생성 (2023.10.05 jbh)
                // 참고 - 솔루션 파일 "RevitBox2024.sln" - 프로젝트 파일 "RevitBoxRibbon2024" - "Class1.cs" 소스파일 - 메서드 "OnStartup"
                // var testPanelButtonList = new BindingList<PushButton>();
                // testPanelButtonList.Add(pushParameterButton);
                // testPanelButtonList.Add(pushButton);

                // TODO : 리본 버튼 아이콘 이미지 필요시 추가 예정 (2023.10.5 jbh)
                // 리본 버튼 "테스트 AIS 템플릿" 아이콘(이미지) 등록(셋팅) 
                // House 프로젝트 파일 -> 참조 -> PresentationCore.dll 파일 추가  
                // 리본 버튼 "테스트 AIS 템플릿" 아이콘 이미지 사이즈 (32 X 32) - 기존 Revit에 존재하는 다른 리본 탭 안에 속하는 버튼들 사이즈도 32 X 32 이기 때문
                // pushbutton.LargeImage = convertFromBitmap(House.Properties.Resources.settings);     // 아이콘 셋팅
                // pushbutton.Image = convertFromBitmap(House.Properties.Resources.settings);          // 아이콘 셋팅 
                // pushbutton.ToolTip = "벽을 작성 합니다.";                                           // 툴팁 셋팅 

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return;
            }
        }

        // TODO : 클래스 "BitmapSource", "Bitmap" 사용하기 위해 using문 "System.Windows.Media.Imaging" 추가 (2023.10.5 jbh)
        /// <summary>
        /// 리소스에 등록된 이미지 파일(.png, .jpg 등등...)을 BitmapSource으로 convert 해주는 메서드 
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        BitmapSource convertFromBitmap(Bitmap bitmap)
        {
            // TestIronPython 프로젝트 파일 -> 참조 -> WindowsBase.dll 파일 추가  
            return System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                bitmap.GetHbitmap(),
                IntPtr.Zero,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());
        }

        #endregion IExternalApplication Members
    }
}
