using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitBoxSeumteo.Common.RibbonBase
{
    public class RibbonHelper
    {
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
        //        해결하기 위해 string 객체 dllPath에 할당되는 문자열 맨 앞에 특수문자 "@" 추가 (2023.10.6 jbh)
        // 참고 URL - https://vesselsdiary.tistory.com/64

        // TODO : dll 파일(RevitBoxSeumteo.dll) 경로 추후 수정 예정 (2023.10.6 jbh)
        /// <summary>
        /// dll 파일(RevitBoxSeumteo.dll) 경로
        /// </summary>
        public const string dllPath = @"D:\bhjeon\RevitStudy\RevitBoxSeumteo\RevitBoxSeumteo\bin\x64\Debug\RevitBoxSeumteo.dll";

        /// <summary>
        /// Command 명령 실행 위치
        /// </summary>
        public const string CommandPath = "RevitBoxSeumteo.Command";

        /// <summary>
        /// ParameterCommand 명령 실행 위치
        /// </summary>
        public const string ParameterCommandPath = "RevitBoxSeumteo.ParameterCommand";
    }
}
