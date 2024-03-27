﻿using System.ComponentModel;

using Autodesk.Revit.DB;

namespace RevitUpdater.Models.UpdaterBase.MEPUpdater
{
    /// <summary>
    /// 테스트용 MEP 업데이터 데이터 모델
    /// </summary>
    public class UpdaterView
    {

    }

    #region EnumExistElements

    /// <summary>
    /// 특정 객체 존재 여부 확인
    /// </summary>
    public enum EnumExistElements : int
    {
        [Description("객체 존재 안 함.")]
        NONE  = 0,
        [Description("객체 존재함.")]
        EXIST = 1
    }

    #endregion EnumExistElements


    #region EnumExistParameters

    /// <summary>
    /// 특정 매개변수 존재 여부 확인
    /// </summary>
    public enum EnumExistParameters : int
    {
        [Description("매개변수 존재 안 함.")]
        NONE = 0,
        [Description("매개변수 존재함.")]
        EXIST = 1
    }

    #endregion EnumExistParameters

    #region EnumExistKeyInputData

    /// <summary>
    /// 키보드 입력 문자열 존재여부 확인
    /// </summary>
    public enum EnumExistKeyInputData : int
    {
        [Description("키보드 입력 문자열 존재 안 함.")]
        NONE = 0,
        [Description("키보드 입력 문자열 존재함.")]
        EXIST = 1
    }

    #endregion EnumExistKeyInputData

    #region EnumCategoryInfo

    /// <summary>
    /// 동일 카테고리 존재여부 확인
    /// </summary>
    public enum EnumCategoryInfo : int
    {
        [Description("동일 카테고리 존재 안 함.")]
        NONE = 0,
        [Description("동일 카테고리 존재함.")]
        EXIST = 1
    }

    #endregion EnumCategoryInfo

    #region BuiltInParamView

    /// <summary>
    /// BuiltInParameter 매개변수 정보
    /// </summary>
    public class BuiltInParamView
    {
        #region 프로퍼티

        /// <summary>
        /// 매개변수 이름
        /// </summary>
        public string paramName { get; set; }

        /// <summary>
        /// 매개변수 값
        /// </summary>
        public long paramValue { get; set; }

        #endregion 프로퍼티

        #region 생성자

        public BuiltInParamView(string rvParamName, long rvParamValue)
        {
            this.paramName = rvParamName;
            this.paramValue = rvParamValue;
        }

        #endregion 생성자
    }

    #endregion BuiltInParamView

    #region CategoryInfoView

    /// <summary>
    /// 카테고리 정보 
    /// </summary>
    public class CategoryInfoView
    {
        #region 프로퍼티 

        /// <summary>
        /// 카테고리 이름
        /// </summary>
        public string categoryName { get; set; }

        /// <summary>
        /// 카테고리
        /// </summary>
        public BuiltInCategory category { get; set; }

        // TODO : 매개변수 그룹 이름 프로퍼티 "paramGroupName" 필요시 사용 예정 (2024.03.25 jbh)
        /// <summary>
        /// 특정 객체에 속하는 매개변수 그룹 이름
        /// </summary>
        //public string paramGroupName { get; set; }

        #endregion 프로퍼티

        #region 생성자

        //public CategoryInfoView (string rvCategoryName, string rvParamGroupName)
        public CategoryInfoView(string rvCategoryName, BuiltInCategory rvCategory)
        {
            this.categoryName = rvCategoryName;
            //this.paramGroupName   = rvParamGroupName;
            this.category     = rvCategory; 
        }

        #endregion 생성자
    }


    #endregion CategoryInfoView

    #region BuiltInCategoryView

    /// <summary>
    /// BuiltInCategory 정보
    /// </summary>
    public class BuiltInCategoryView
    {
        #region 프로퍼티

        /// <summary>
        /// 카테고리 이름
        /// </summary>
        public string categoryName { get; set; }

        /// <summary>
        /// 카테고리 값
        /// </summary>
        public long categoryValue { get; set; }

        #endregion 프로퍼티

        #region 생성자

        public BuiltInCategoryView(string rvCategoryName, long rvCategoryValue)
        {
            this.categoryName = rvCategoryName;
            this.categoryValue = rvCategoryValue;
        }

        #endregion 생성자
    }

    #endregion BuiltInCategoryView

    #region SetParamInfoView

    /// <summary>
    /// 입력할 값이 할당된 매개변수 정보
    /// </summary>
    public class SetParamInfoView
    {
        #region 프로퍼티

        /// <summary>
        /// 매개변수 이름
        /// </summary>
        public string paramName { get; set; }

        /// <summary>
        /// 매개변수에 입력한 값
        /// </summary>
        public string paramValue { get; set; }

        #endregion 프로퍼티

        #region 생성자

        public SetParamInfoView(string rvParamName, string rvParamValue)
        {
            this.paramName = rvParamName;
            this.paramValue = rvParamValue;
        }

        #endregion 생성자
    }

    #endregion SetParamInfoView
}
