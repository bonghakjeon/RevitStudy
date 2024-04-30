using Serilog;

using System;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

using HTSBIM2019.Common.LogBase;

namespace HTSBIM2019.Common.Extensions
{
    public class ParamsExtensions
    {
        #region GetRemoveWhiteSpacesString

        // TODO : 문자열 객체 pStr에서 Linq 확장 메서드 사용해서 공백 제거 (2024.02.27 jbh)
        // 참고 URL - https://developer-talk.tistory.com/660
        /// <summary>
        /// 문자열에서 공백 제거하기 (Linq 사용)
        /// </summary>
        /// <param name="pStr"></param>
        /// <returns></returns>
        public static string GetRemoveWhiteSpacesString(string pStr)
        {
            var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

            try
            {
                // Linq 확장 메서드 Where()에서 공백이 아닌 문자만 반환하는 람다식을 전달 후 공백이 아닌 문자를 문자열로 합치는 Concat() 메서드 사용 (2024.02.27 jbh)
                string removeWhiteSpacesResult = string.Concat(pStr.Where(c => false == Char.IsWhiteSpace(c)));
                return removeWhiteSpacesResult;
            }
            catch(Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                throw;   // 오류 발생시 상위 호출자 예외처리 전달
            }
        }

        #endregion GetRemoveWhiteSpacesString

        #region GetReplaceWhiteSpacesString

        // TODO : 문자열 객체 pStr에서 Regex 클래스의 Replace() 메서드를 사용해서 공백 제거(2024.02.27 jbh)
        // 참고 URL - https://developer-talk.tistory.com/660
        /// <summary>
        /// 문자열에서 공백 제거하기 (Regex.Replace 사용)
        /// </summary>
        /// <param name="pStr"></param>
        /// <returns></returns>
        public static string GetReplaceWhiteSpacesString(string pStr)
        {
            var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

            try
            {
                // Regex 클래스의 Replace() 메서드를 사용하여 문자열에 공백이 존재하는 경우 공백이 제거된 문자열 반환 (2024.02.27 jbh)
                string replaceWhiteSpacesResult = Regex.Replace(pStr, @"\s", "");
                return replaceWhiteSpacesResult;
            }
            catch(Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                throw;   // 오류 발생시 상위 호출자 예외처리 전달
            }
        }


        #endregion GetReplaceWhiteSpacesString

        #region Sample

        #endregion Sample
    }
}
