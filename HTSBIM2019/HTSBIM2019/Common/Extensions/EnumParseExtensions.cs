using Serilog;

using System;
using System.Reflection;

using HTSBIM2019.Common.LogBase;

namespace HTSBIM2019.Common.Extensions
{
    /// <summary>
    /// Enum 열거형 구조체 형변환 관련 Extensions
    /// </summary>
    public static class EnumParseExtensions<TEnum>
    {
        #region Parse - string to TEnum

        /// <summary>
        /// Enum 열거형 구조체 멤버변수 열거형 영어 이름(string) -> Enum 구조체 멤버변수 형변환
        /// </summary>
        public static TEnum Parse(string rvEnumMemberValName)
        {
            var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

            try
            {
                if (false == typeof(TEnum).IsEnum) return default(TEnum);       // Enum 열거형 구조체가 아닐 경우 
                return (TEnum)Enum.Parse(typeof(TEnum), rvEnumMemberValName);   // Enum 열거형 구조체일 경우 
            }
            catch (Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                throw;   // 오류 발생시 상위 호출자 예외처리 전달
            }
        }

        #endregion Parse - string to TEnum


        #region Parse - TEnum to string

        /// <summary>
        /// Enum 열거형 구조체 멤버변수 -> Enum 열거형 구조체 멤버변수 열거형 영어 이름(string) 형변환
        /// </summary>
        public static string Parse(TEnum rvEnumMemberVal)
        {
            var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

            try
            {
                if (false == typeof(TEnum).IsEnum) return string.Empty;       // Enum 열거형 구조체가 아닐 경우 
                return Enum.GetName(typeof(TEnum), rvEnumMemberVal);          // Enum 열거형 구조체일 경우 
            }
            catch (Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                throw;   // 오류 발생시 상위 호출자 예외처리 전달
            }
        }

        #endregion Parse - TEnum to string

        #region Parse - int to string

        /// <summary>
        /// Enum 구조체 멤버변수에 매핑된 값(int) -> Enum 열거형 구조체 멤버변수 열거형 영어 이름(string) 형변환
        /// </summary>
        public static string Parse(int rvEnumValue)
        {
            var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

            try
            {
                if (false == typeof(TEnum).IsEnum) return string.Empty;   // Enum 열거형 구조체가 아닐 경우 
                return Enum.GetName(typeof(TEnum), rvEnumValue);          // Enum 열거형 구조체일 경우 
            }
            catch (Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                throw;   // 오류 발생시 상위 호출자 예외처리 전달
            }
        }

        #endregion Parse - int to string

        #region Sample

        #endregion Sample
    }
}
