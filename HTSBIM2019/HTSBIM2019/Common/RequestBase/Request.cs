using System.Threading;

namespace HTSBIM2019.Common.RequestBase
{
    #region EnumMEPRequestId

    /// <summary>
    /// MEPUpdater Request 유형 열거형 구조체
    /// </summary>
    public enum EnumMEPUpdaterRequestId : int
    {
        /// <summary>
        /// NONE
        /// </summary>
        NONE = 0,

        /// <summary>
        /// 업데이터 + Triggers 등록
        /// </summary>
        REGISTER = 1,

        /// <summary>
        /// 업데이터 + Triggers 제거(해제)
        /// </summary>
        REMOVE = 2
    }

    #endregion EnumMEPRequestId

    #region TestRequest

    public class TestRequest
    {

    }

    #endregion TestRequest

    #region MEPUpdaterRequest

    /// <summary>
    /// MEPUpdater 요청(Request) 클래스 
    /// 부모 폼(Revit 응용프로그램) 쓰레드 접근 가능 
    /// </summary>
    public class MEPUpdaterRequest
    {
        #region 프로퍼티

        /// <summary>
        /// 열거형 구조체 EnumMEPUpdaterRequestId를 정수로 변환한 값 할당 프로퍼티 
        /// 값을 일반 int로 저장하면 더 간단하게 연동할 수 있다.
        /// </summary>
        private int RequestIdValue = (int)EnumMEPUpdaterRequestId.NONE;

        #endregion 프로퍼티 

        #region Take

        /// <summary>
        /// 최신 요청(Request)한 열거형 구조체 EnumMEPUpdaterRequestId 얻기
        /// </summary>
        public EnumMEPUpdaterRequestId Take()
        {
            // TODO : 클래스 Interlocked의 메서드 Exchange 사용해서 최신 요청(Request)한 열거형 구조체 EnumMEPUpdaterRequestId 얻기 구현 (2024.03.29 jbh)
            // 참고 URL - https://learn.microsoft.com/en-us/dotnet/api/system.threading.interlocked?view=net-8.0
            // 참고 2 URL - https://velog.io/@yarogono/C-Interlocked%EC%97%90-%EB%8C%80%ED%95%B4-%EC%95%8C%EC%95%84%EB%B3%B4%EC%9E%90
            return (EnumMEPUpdaterRequestId)Interlocked.Exchange(ref RequestIdValue, (int)EnumMEPUpdaterRequestId.NONE);
        }

        #endregion Take

        #region Make

        /// <summary>
        /// 사용자가 명령 버튼을 누를 때 대화 상자가 해당 메서드(Make) 호출
        /// </summary>
        public void Make(EnumMEPUpdaterRequestId pRequest)
        {
            // TODO : 클래스 Interlocked의 메서드 Exchange 사용해서 사용자가 명령 버튼을 누를 때 대화 상자가 해당 메서드(Make) 호출 구현 (2024.03.29 jbh)
            // 참고 URL - https://learn.microsoft.com/en-us/dotnet/api/system.threading.interlocked?view=net-8.0
            // 참고 2 URL - https://velog.io/@yarogono/C-Interlocked%EC%97%90-%EB%8C%80%ED%95%B4-%EC%95%8C%EC%95%84%EB%B3%B4%EC%9E%90
            Interlocked.Exchange(ref RequestIdValue, (int)pRequest);
        }

        #endregion Make
    }

    #endregion MEPUpdaterRequest
}
