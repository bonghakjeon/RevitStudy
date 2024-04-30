using Serilog;

using System;
using System.Collections.Generic;
using System.Reflection;

using HTSBIM2019.Common.LogBase;

namespace HTSBIM2019.Common.Extensions
{
    /// <summary>
    /// 리스트 객체 두 개 비교 관련 Extensions
    /// </summary>
    public static class ListCompareExtensions<T>
    {
        #region Equals - IList<T> Compare

        /// <summary>
        /// 리스트 객체 두 개가 같은지 확인 
        /// 비교할 때 두 개의 리스트 객체의 값(value)과 갯수(count)가 같은지 확인
        /// </summary>
        /// <param name="pListA">1st list.</param>
        /// <param name="pListB">2nd list.</param>
        /// <returns>두 개의 리스트 객체의 값(value)과 갯수(count)가 동일하면 true</returns>
        public static bool Equals(IList<T> pListA, IList<T> pListB)
        {
            var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

            try
            {
                if(pListA.Count != pListB.Count) return false;  // 두 리스트 객체 "pListA", "pListB"의 갯수가 다를 경우 false 리턴 

                foreach(T valA in pListA)
                    if(false == pListB.Contains(valA)) return false;   // 리스트 객체 "pListB"에 리스트 객체 "pListA"의 특정 원소 "valA" 가 존재하지 않는 경우 false 리턴 

                foreach(T valB in pListB)
                    if(false == pListA.Contains(valB)) return false;   // 리스트 객체 "pListA"에 리스트 객체 "pListB"의 특정 원소 "valB" 가 존재하지 않는 경우 false 리턴 

                return true;  // 두 리스트 객체 "pListA", "pListB"가 같은 경우 true 리턴 
            }
            catch(Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                throw;   // 오류 발생시 상위 호출자 예외처리 전달
            }
        }

        #endregion Equals - IList<T> Compare

        #region Equals - ICollection<T> Compare

        /// <summary>
        /// ICollection 객체 두 개가 같은지 확인 
        /// 비교할 때 두 개의 ICollection 객체의 값(value)과 갯수(count)가 같은지 확인
        /// </summary>
        /// <param name="pCollectionA">1st ICollection.</param>
        /// <param name="pCollectionB">2nd ICollection.</param>
        /// <returns>두 개의 ICollection 객체의 값(value)과 갯수(count)가 동일하면 true</returns>
        public static bool Equals(ICollection<T> pCollectionA, ICollection<T> pCollectionB)
        {
            var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

            try
            {
                if(pCollectionA.Count != pCollectionB.Count) return false;   // 두 개의 ICollection 객체 "pCollectionA", "pCollectionB"의 갯수가 다를 경우 false 리턴 

                foreach(T valA in pCollectionA)
                    if(false == pCollectionB.Contains(valA)) return false;   // ICollection 객체 "pCollectionB"에 ICollection 객체 "pCollectionA"의 특정 원소 "valA" 가 존재하지 않는 경우 false 리턴 

                foreach(T valB in pCollectionB)
                    if(false == pCollectionA.Contains(valB)) return false;   // ICollection 객체 "pCollectionA"에 ICollection 객체 "pCollectionB"의 특정 원소 "valB" 가 존재하지 않는 경우 false 리턴 

                return true;  // 두 ICollection 객체 "pCollectionA", "pCollectionB"가 같은 경우 true 리턴 
            }
            catch(Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                throw;   // 오류 발생시 상위 호출자 예외처리 전달
            }
        }

        #endregion Equals - ICollection<T> Compare

        #region Sample

        #endregion Sample
    }
}
