using Serilog;
using System;
using System.ComponentModel;
using System.Reflection;

using HTSBIM2019.Common.LogBase;

namespace HTSBIM2019.Common.StrBase
{
    public static class StrHelper
    {
        #region 프로퍼티

        #endregion 프로퍼티

        #region Enum - ToDescription

        /// <summary>
        /// Enum 열거형 구조체 멤버변수 Description 문자열 가져오기
        /// </summary>
        public static string ToDescription(this Enum source)
        {
            string enumMemberName = source.ToString();   // Enum 열거형 구조체 멤버변수명 가져오기 

            var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

            try
            {
                DescriptionAttribute[] customAttributes = (DescriptionAttribute[])source.GetType().GetField(enumMemberName).GetCustomAttributes(typeof(DescriptionAttribute), false);
                return customAttributes is not null && customAttributes.Length != 0 ? customAttributes[0].Description : enumMemberName;
            }
            catch (Exception ex)
            {
                // TODO : 추후 필요시 Log.Debug 안에있는 문자열로 로그 메시지 남기려면 Logger 클래스 Debug 메서드에 메서드 파라미터 부분 로직 수정 하기(2024.03.07 jbh)
                //Log.Debug("-----------------------------------\n 메소드 : " + MethodBase.GetCurrentMethod().ReflectedType.Name + "==>" + MethodBase.GetCurrentMethod().Name + "\n" + string.Format(" LINE : {0} 행\n", (object)new StackFrame(0, true).GetFileLineNumber()) + "-----------------------------------\n 내용 : " + ex.Message + "\n-----------------------------------\n");
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
            }

            return enumMemberName;
        }

        #endregion Enum - ToDescription


        #region Sample

        #endregion Sample
    }
}
