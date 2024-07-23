using System;
using System.Reflection;

namespace HTSBIM2019.Common.HTSBase
{
    // TODO : Revit AddIn 개발 소스를 비쥬얼스튜디오 2022 .net Core 버전(8.0)을 사용하려면 Revit 2025 버전 부터 사용이 가능하므로 현 시점에서 해당 소스는 .net FrameWork 4.8에서만 구동시킬 수 있다. (2024.03.11 jbh)
    public class HTSHelper
    {
        #region 공통

        #region 어셈블리

        /// <summary>
        /// 년도별 버전 이름 길이 enum 구조체 AssemblyVersion
        /// </summary>
        public enum AssemblyVersion : int
        {
            VersionLen = 4  // 버전 이름 길이 (2019, 2020, 2021, 2023, 2024 ..)
        }

        // TODO : 해당 string 객체 "AssemblyName" 오류 발생시 수정 예정 (2024.01.22 jbh)
        /// <summary>
        /// 어셈블리(.dll 파일) 이름 
        /// </summary>
        // public static string AssemblyName = $"{Assembly.GetExecutingAssembly().GetName().Name}";
        public static string AssemblyName = Assembly.GetExecutingAssembly().GetName().Name;

        // TODO : 추후 NSIS 셋업 프로그램 사용해서 해당 HTSBIM2019 인스톨 파일 만들어서 임의의 PC/노트북에 설치 후 dll 파일을 제대로 잘 찾는지 테스트 진행 예정 (2024.04.05 jbh)
        /// <summary>
        /// 어셈블리(.dll 파일) 경로
        /// </summary>
        public static string AssemblyFilePath = Assembly.GetExecutingAssembly().Location;

        #endregion 어셈블리

        #region 로그 파일

        /// <summary>
        /// 로그 파일 갯수 설정 (테스트용 30일 이전)
        /// </summary>
        public const int LogFileCountLimit = 30;   

        #endregion 로그 파일

        #region 폴더(디렉토리) 경로 

        // TODO : C# 문자열 보간 기능 사용해서 로그파일 생성할 상위 폴더 경로 문자열로 변환 (2024.01.22 jbh)
        // 참고 URL - https://gaeunhan.tistory.com/61
        // TODO : 다른 PC에서도 로그파일 생성 및 기록 기능이 실행될 수 있도록 로그 파일 생성할 전체 경로 중 루트 디렉토리 값을 내문서 폴더 (Environment.SpecialFolder.MyDocuments)로 설정 (2024.03.11 jbh)
        // 참고 URL - https://learn.microsoft.com/ko-kr/dotnet/api/system.environment.specialfolder?view=net-7.0

        // TODO : 로그(Logs) 폴더(디렉토리) 경로 "LogDirPath" 필요시 사용 예정 (2024.04.12 jbh)
        /// <summary>
        /// 로그(Logs) 폴더(디렉토리) 경로 
        /// </summary>
        public static string LogDirPath = $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\{AssemblyName}\\Logs";

        // TODO : Revit MEP Updater 실행시 작성되는 로그 기록이 다른 Revit 애드인 프로그램의 로그 파일에 기록되서 꼬이므로,
        // 로그 파일 경로를 내문서((Environment.SpecialFolder.MyDocuments)가 아니라 임시로 D드라이브로 이동함. (2024.03.22 jbh) 
        // public static string LogDirPath = $"D:\\RevitUpdater\\{AssemblyName}\\Logs";

        /// <summary>
        /// MEPUpdater Json 파일 경로  
        /// </summary>
        public const string MEPUpdaterJsonFilePath = @"\Json\Updater_Parameters.json";

        #endregion 폴더(디렉토리) 경로 

        #region 트랜잭션

        /// <summary>
        /// 트랜잭션 Start
        /// </summary>
        public const string Start = "Start";

        #endregion 트랜잭션 

        #region TaskDialog 타이틀

        /// <summary>
        /// 타이틀 - 테스트
        /// </summary>
        public const string TestTitle = "테스트";

        /// <summary>
        /// 타이틀 - 완료 
        /// </summary>
        public const string CompletedTitle = "완료";

        /// <summary>
        /// 타이틀 - 오류  
        /// </summary>
        public const string ErrorTitle = "오류";

        /// <summary>
        /// 타이틀 - 알림
        /// </summary>
        public const string NoticeTitle = "알림";

        #endregion TaskDialog 타이틀

        #region 파일 확장자 

        /// <summary>
        /// 텍스트 파일 확장자
        /// </summary>
        public const string TextFile = ".txt";

        #endregion 파일 확장자

        #endregion 공통

        #region CompanyHomePage

        /// <summary>
        /// (주)상상진화 기업 홈페이지 URL 주소
        /// </summary>
        public const string ImagineBuilder_URL = "https://imbu.co.kr";

        #endregion CompanyHomePage

        #region MEPUpdater

        /// <summary>
        /// Utils - MEPUpdater 업데이터 이름
        /// </summary>
        public const string MEPUpdaterName = "MEPUpdater";

        /// <summary>
        /// MEPUpdater 매개변수 리스트  
        /// </summary>
        public const string UPDATER_ParameterList = "UPDATER_PARAMETERS";

        #region MEPUpdater 매개변수 이름

        /// <summary>
        /// 매개변수 - 객체 생성 날짜
        /// </summary>
        public const string AddDate = "객체 생성 날짜";

        /// <summary>
        /// 매개변수 - 객체 생성자
        /// </summary>
        public const string AddWorker = "객체 생성자";

        /// <summary>
        /// 매개변수 - 최종 수정 날짜
        /// </summary>
        public const string LastModDate = "최종 수정 날짜";

        /// <summary>
        /// 매개변수 - 최종 수정자
        /// </summary>
        public const string LastModWorker = "최종 수정자";

        #endregion MEPUpdater 매개변수 이름

        #region MEPUpdater 매개변수 데이터 유형 (자료형)

        /// <summary>
        /// Text
        /// </summary>
        public const string text = "문자";

        #endregion MEPUpdater 매개변수 데이터 유형 (자료형)

        #endregion MEPUpdater

        #region MEPUpdaterForm

        /// <summary>
        /// MEPUpdater Modaless 폼 객체 이름 
        /// </summary>
        public const string MEPUpdaterFormName = "MEPUpdaterForm";

        /// <summary>
        /// 업데이터 아이디 생성시 필요한 GUID 문자열 프로퍼티
        /// </summary>
        public const string GId = "d42d28af-d2cd-4f07-8873-e7cfb61903d8";

        /// <summary>
        /// 객체 타입 - Element
        /// </summary>
        public const string ElementType = "Element";

        #region 카테고리 종류 - 배관

        #region 공통 (상위, 하위 카테고리)

        /// <summary>
        /// TreeView 컨트롤(treeViewCategory)에 바인딩할 대상 카테고리 - 배관
        /// </summary>
        public const string 배관 = "배관";

        #endregion 공통 (상위, 하위 카테고리)

        #region 하위 카테고리 

        /// <summary>
        /// TreeView 컨트롤(treeViewCategory)에 바인딩할 대상 하위 카테고리 - 배관 부속류
        /// </summary>
        public const string 배관부속류 = "배관 부속류";

        /// <summary>
        /// TreeView 컨트롤(treeViewCategory)에 바인딩할 대상 하위 카테고리 - 배관 단열재
        /// </summary>
        public const string 배관단열재 = "배관 단열재";

        /// <summary>
        /// TreeView 컨트롤(treeViewCategory)에 바인딩할 대상 하위 카테고리 - 배관 밸브류
        /// </summary>
        public const string 배관밸브류 = "배관 밸브류";

        /// <summary>
        /// TreeView 컨트롤(treeViewCategory)에 바인딩할 대상 하위 카테고리 - 일반 모델
        /// </summary>


        /// <summary>
        /// TreeView 컨트롤(treeViewCategory)에 바인딩할 대상 하위 카테고리 - 기계장비
        /// </summary>

        #endregion 하위 카테고리 

        #endregion 카테고리 종류 - 배관

        #region 카테고리 종류 - 전기/제어

        #region 상위 카테고리

        public const string 전기제어 = "전기/제어";

        #endregion 상위 카테고리

        #region 하위 카테고리

        /// <summary>
        /// TreeView 컨트롤(treeViewCategory)에 바인딩할 대상 카테고리 - 케이블 트레이
        /// </summary>
        // public const string = "케이블 트레이";

        /// <summary>
        /// TreeView 컨트롤(treeViewCategory)에 바인딩할 대상 카테고리 - 케이블 트레이 부속류
        /// </summary>
        // public const string = "케이블 트레이 부속류";

        /// <summary>
        /// TreeView 컨트롤(treeViewCategory)에 바인딩할 대상 카테고리 - 
        /// </summary>
        // public const string = "덕트";

        /// <summary>
        /// TreeView 컨트롤(treeViewCategory)에 바인딩할 대상 카테고리 - 
        /// </summary>
        // public const string = "덕트 부속";

        /// <summary>
        /// TreeView 컨트롤(treeViewCategory)에 바인딩할 대상 카테고리 - 
        /// </summary>
        // public const string = "덕트 액세서리";

        /// <summary>
        /// TreeView 컨트롤(treeViewCategory)에 바인딩할 대상 카테고리 - 
        /// </summary>
        // public const string = "배관 밸브류";

        /// <summary>
        /// TreeView 컨트롤(treeViewCategory)에 바인딩할 대상 카테고리 - 
        /// </summary>
        // public const string = "일반 모델";

        /// <summary>
        /// TreeView 컨트롤(treeViewCategory)에 바인딩할 대상 카테고리 - 
        /// </summary>
        // public const string = "전기 설비";

        /// <summary>
        /// TreeView 컨트롤(treeViewCategory)에 바인딩할 대상 카테고리 - 
        /// </summary>
        // public const string = "전기 시설물";

        /// <summary>
        /// TreeView 컨트롤(treeViewCategory)에 바인딩할 대상 카테고리 - 
        /// </summary>
        // public const string = "전선관";

        /// <summary>
        /// TreeView 컨트롤(treeViewCategory)에 바인딩할 대상 카테고리 - 
        /// </summary>
        // public const string = "전선관 부속류";

        /// <summary>
        /// TreeView 컨트롤(treeViewCategory)에 바인딩할 대상 카테고리 - 
        /// </summary>
        // public const string = "전화 장치";

        #endregion 하위 카테고리

        #endregion 카테고리 종류 - 전기/제어


        /// <summary>
        /// ComboBox 컨트롤(comboBoxCategory) - ValueMember
        /// </summary>
        public const string Category = "category";

        /// <summary>
        /// ComboBox 컨트롤(comboBoxCategory) - DisplayMember
        /// </summary>
        public const string CategoryName = "categoryName";

        #endregion MEPUpdaterForm

        #region RequestHandler

        #region MEPUpdaterRequestHandler

        #endregion MEPUpdaterRequestHandler

        #endregion RequestHandler

        #region TechnicalSupport

        /// <summary>
        /// 상상플렉스 커뮤니티 웹 사이트 URL 주소 
        /// </summary>
        public const string SangSangFlex_URL = "https://www.ssflex.co.kr/community/open";

        #endregion TechnicalSupport

        #region ImageEditorForm

        /// <summary>
        /// 이미지 삽입 Modaless 폼 객체 이름 
        /// </summary>
        public const string ImageEditorFormName = "ImageEditorForm";

        // TODO : Click, Mouse, Paint 이벤트 타입 const string 객체 필요시 사용 예정 (2024.07.05 jbh)
        /// <summary>
        /// Click 이벤트 타입 
        /// </summary>
        // public const string MethodTypeOfClick = "Click";

        /// <summary>
        /// Mouse 이벤트 타입 
        /// </summary>
        // public const string MethodTypeOfMouse = "Mouse";

        /// <summary>
        /// Paint 이벤트 타입 
        /// </summary>
        // public const string MethodTypeOfPaint = "Paint";

        /// <summary>
        /// 화면 초기 셋팅
        /// </summary>
        public const string TypeOfInitSetting = "화면 초기 셋팅";

        /// <summary>
        /// 기능 - 파일 선택
        /// </summary>
        public const string TypeOfSelectFile = "파일 선택";

        /// <summary>
        /// 기능 - 객체 선택
        /// </summary>
        public const string TypeOfSelectElement = "객체 선택";

        /// <summary>
        /// 기능 - 흑백 전환
        /// </summary>
        public const string TypeOfBlackConvert = "흑백 전환";

        /// <summary>
        /// 기능 - 원본 보기
        /// </summary>
        public const string TypeOfOrgImage = "원본 보기";

        /// <summary>
        /// 기능 - 이미지 자르기
        /// </summary>
        public const string TypeOfCropImage = "이미지 자르기";

        /// <summary>
        /// 기능 - 이미지 삽입
        /// </summary>
        public const string TypeOfInsertImage = "이미지 삽입";

        /// <summary>
        /// 기능 - 원본 이미지 pictureBox - MouseWheel
        /// 마우스 휠 사용시 원본 이미지 Width, Height 증/감 수치 배율 (X5)
        /// </summary>
        public const int OrgImageMagnification = 5;

        /// <summary>
        /// 기능
        /// 메서드 GetImageRatio - 이미지 파일 - Width, Height 비율 구하기
        /// 메서드 DisplaySetting - "파일 선택", "흑백 전환", "원본 보기"
        /// 백의 자릿수 미만 내림 처리
        /// </summary>
        public const int Hundreds = 100;

        /// <summary>
        /// 기능
        /// 메서드 GetImageRatio - 이미지 파일 - Width, Height 비율 구하기
        /// 십의 자릿수 미만 내림 처리
        /// </summary>
        public const int Tens = 10;

        /// <summary>
        /// 기능
        /// 메서드 GetImageRatio - 이미지 파일 - Width, Height 비율 구하기
        /// 원본 이미지 최소 Width
        /// </summary>
        public const int MinWidth = 100;

        /// <summary>
        /// 기능
        /// 메서드 GetImageRatio - 이미지 파일 - Width, Height 비율 구하기
        /// 원본 이미지 최소 Height
        /// </summary>
        public const int MinHeight = 100;

        #endregion ImageForm

        #region ImageManager - (ImageManager.cs)

        /// <summary>
        /// 흑백 전환 후 이진화 처리 한계값(임계값)
        /// </summary>
        //public const int ThresholdValue = 128;
        public const int ThresholdValue = 120;

        #endregion ImageManager - (ImageManager.cs)

        #region Sample

        #endregion Sample
    }
}
