using System;
using System.Reflection;

namespace RevitUpdater.Common.UpdaterBase
{
    // TODO : Revit AddIn 개발 소스를 비쥬얼스튜디오 2022 .net Core 버전(8.0)을 사용하려면 Revit 2025 버전 부터 사용이 가능하므로 현 시점에서 해당 소스는 .net FrameWork 4.8에서만 구동시킬 수 있다. (2024.03.11 jbh)
    public class UpdaterHelper
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
        /// 어셈블리 이름 
        /// </summary>
        // public static string AssemblyName = $"{Assembly.GetExecutingAssembly().GetName().Name}";
        public static string AssemblyName = Assembly.GetExecutingAssembly().GetName().Name;

        #endregion 어셈블리

        #region 폴더(디렉토리) 경로 

        // TODO : C# 문자열 보간 기능 사용해서 로그파일 생성할 상위 폴더 경로 문자열로 변환 (2024.01.22 jbh)
        // 참고 URL - https://gaeunhan.tistory.com/61
        // TODO : 다른 PC에서도 로그파일 생성 및 기록 기능이 실행될 수 있도록 로그 파일 생성할 전체 경로 중 루트 디렉토리 값을 내문서 폴더 (Environment.SpecialFolder.MyDocuments)로 설정 (2024.03.11 jbh)
        // 참고 URL - https://learn.microsoft.com/ko-kr/dotnet/api/system.environment.specialfolder?view=net-7.0
        /// <summary>
        /// 로그(Logs) 폴더(디렉토리) 경로 
        /// </summary>
        public static string LogDirPath = $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\{AssemblyName}\\Logs";

        // TODO : Revit MEP Updater 실행시 작성되는 로그 기록이 다른 Revit 애드인 프로그램의 로그 파일에 기록되서 꼬이므로,
        // 로그 파일 경로를 내문서((Environment.SpecialFolder.MyDocuments)가 아니라 임시로 D드라이브로 이동함. (2024.03.22 jbh) 
        // public static string LogDirPath = $"D:\\RevitUpdater\\{AssemblyName}\\Logs";

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

        #endregion 공통

        #region MEPUpdater

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

        /// <summary>
        /// SearchLookUpEdit - 컬럼 (카테고리 이름)
        /// </summary>
        public const string CategoryName = "CategoryName";

        /// <summary>
        /// SearchLookUpEdit - 컬럼 Caption (카테고리 이름)
        /// </summary>
        public const string CategoryNameCaption = "카테고리 이름";

        /// <summary>
        /// SearchLookUpEdit - 컬럼 (카테고리 타입)
        /// </summary>
        public const string CategoryType = "CategoryType";

        /// <summary>
        /// SearchLookUpEdit - 컬럼 Caption (카테고리 타입)
        /// </summary>
        public const string CategoryTypeCaption = "카테고리 타입";

        /// <summary>
        /// SearchLookUpEdit - 매개변수 선택이 안 된 경우 표시될 텍스트
        /// </summary>
        public const string NullText = "카테고리를 선택하세요.";

        #endregion MEPUpdater

        #region RequestHandler

        #region MEPUpdaterRequestHandler

        #endregion MEPUpdaterRequestHandler

        #endregion RequestHandler

    }
}
