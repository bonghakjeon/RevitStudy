using System.Collections.Generic;
using System.ComponentModel;

using Autodesk.Revit.DB;

using HTSBIMNet;

namespace HTSBIM2019.Models.HTSBase.MEPUpdater
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
        NONE = 0,
        [Description("객체 존재함.")]
        EXIST = 1
    }

    #endregion EnumExistElements

    #region EnumExistFamilySymbols

    /// <summary>
    /// 타입 FamilySymbol 존재 여부 확인
    /// </summary>
    public enum EnumExistFamilySymbols : int
    {
        [Description("FamilySymbol 존재 안 함.")]
        NONE = 0,
        [Description("FamilySymbol 존재함.")]
        EXIST = 1
    }

    #endregion EnumExistFamilySymbols

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

    #region EnumEqualCategoryInfo

    /// <summary>
    /// 동일 카테고리 존재여부 확인
    /// </summary>
    public enum EnumEqualCategoryInfo : int
    {
        [Description("동일 카테고리 존재 안 함.")]
        NONE = 0,
        [Description("동일 카테고리 존재함.")]
        EXIST = 1
    }

    #endregion EnumEqualCategoryInfo

    #region EnumCategoryInfo

    /// <summary>
    /// 카테고리 종류
    /// </summary>
    public enum EnumCategoryInfo : int
    {
        [Description("없음")]
        NONE = -1,
        [Description("배관")]
        OST_PIPE_CURVES = 0,
        [Description("배관 단열재")]
        OST_PIPE_INSULATIONS = 1,
        [Description("배관 밸브류")]
        OST_PIPE_ACCESSORY = 2,
        [Description("배관 부속류")]
        OST_PIPE_FITTING = 3,
    }

    #endregion EnumCategoryInfo

    #region EnumMainCategoryInfo

    /// <summary>
    /// 상위 카테고리 종류 
    /// </summary>
    public enum EnumMainCategoryInfo : int
    {
        [Description("배관")]
        PIPE = 0,
        [Description("전기/제어")]
        ELECTRIC_CONTROL = 1
    }

    #endregion EnumMainCategoryInfo

    #region EnumCheckedCategoryInfo

    /// <summary>
    /// 카테고리 체크 여부
    /// </summary>
    public enum EnumCheckedCategoryInfo : int
    {
        [Description("카테고리 체크 안 함.")]
        NONE = 0,
        [Description("카테고리 체크함.")]
        EXIST = 1
    }

    #endregion EnumCheckedCategoryInfo

    #region BuiltInParamView

    /// <summary>
    /// BuiltInParameter 매개변수 정보
    /// </summary>
    public class BuiltInParamView : BindableBase
    {
        #region 프로퍼티

        /// <summary>
        /// 매개변수 이름
        /// </summary>
        public string ParamName { get => _ParamName; set { _ParamName = value; NotifyOfPropertyChange(); } }
        private string _ParamName;

        /// <summary>
        /// 매개변수 값
        /// </summary>
        public long ParamValue { get => _ParamValue; set { _ParamValue = value; NotifyOfPropertyChange(); } }
        private long _ParamValue;

        #endregion 프로퍼티

        #region 생성자

        public BuiltInParamView(string rvParamName, long rvParamValue)
        {
            this._ParamName = rvParamName;
            this._ParamValue = rvParamValue;
        }

        #endregion 생성자
    }

    #endregion BuiltInParamView

    #region CategoryInfoView

    /// <summary>
    /// 카테고리 정보 
    /// </summary>
    public class CategoryInfoView : BindableBase
    {
        #region 프로퍼티 

        /// <summary>
        /// 카테고리 이름
        /// </summary>
        public string CategoryName { get => _CategoryName; set { _CategoryName = value; NotifyOfPropertyChange(); } }
        private string _CategoryName;

        /// <summary>
        /// 카테고리
        /// </summary>
        public BuiltInCategory Category { get => _Category; set { _Category = value; NotifyOfPropertyChange(); } }
        private BuiltInCategory _Category;

        // TODO : 매개변수 그룹 이름 프로퍼티 "paramGroupName" 필요시 사용 예정 (2024.03.25 jbh)
        /// <summary>
        /// 특정 객체에 속하는 매개변수 그룹 이름
        /// </summary>
        // public string ParamGroupName { get => _ParamGroupName; set { _ParamGroupName = value; NotifyOfPropertyChange(); } }
        // private string _ParamGroupName;

        #endregion 프로퍼티

        #region 생성자

        // public CategoryInfoView(string rvCategoryName, string rvParamGroupName)
        public CategoryInfoView(string rvCategoryName, BuiltInCategory rvCategory)
        {
            this._CategoryName = rvCategoryName;
            // this._ParamGroupName   = rvParamGroupName;
            this._Category = rvCategory;
        }

        #endregion 생성자
    }


    #endregion CategoryInfoView

    #region BuiltInCategoryView

    /// <summary>
    /// BuiltInCategory 정보
    /// </summary>
    public class BuiltInCategoryView : BindableBase
    {
        #region 프로퍼티

        /// <summary>
        /// 카테고리 이름
        /// </summary>
        public string CategoryName { get => _CategoryName; set { _CategoryName = value; NotifyOfPropertyChange(); } }
        private string _CategoryName;

        /// <summary>
        /// 카테고리 값
        /// </summary>
        public long CategoryValue { get => _CategoryValue; set { _CategoryValue = value; NotifyOfPropertyChange(); } }
        private long _CategoryValue;

        #endregion 프로퍼티

        #region 생성자

        public BuiltInCategoryView(string rvCategoryName, long rvCategoryValue)
        {
            this._CategoryName = rvCategoryName;
            this._CategoryValue = rvCategoryValue;
        }

        #endregion 생성자
    }

    #endregion BuiltInCategoryView

    #region CreateParamView

    /// <summary>
    /// 새로 생성할 매개변수 정보
    /// </summary>
    public class CreateParamView : BindableBase
    {
        #region 프로퍼티
    
        /// <summary>
        /// 매개변수가 속한 상위 카테고리 셋
        /// </summary>
        public CategorySet CategorySet { get => _CategorySet; set { _CategorySet = value; NotifyOfPropertyChange(); } }
        private CategorySet _CategorySet;
    
        /// <summary>
        /// 매개변수 이름
        /// </summary>
        public string ParamName { get => _ParamName; set { _ParamName = value; NotifyOfPropertyChange(); } }
        private string _ParamName;

        /// <summary>
        /// 매개변수 상위 그룹
        /// </summary>

        public BuiltInParameterGroup ParameterGroup { get => _ParameterGroup; set { _ParameterGroup = value; NotifyOfPropertyChange(); } }
        private BuiltInParameterGroup _ParameterGroup;

        /// <summary>
        /// 매개변수 자료형(유형)
        /// </summary>
        public ParameterType ParameterType { get => _ParameterType; set { _ParameterType = value; NotifyOfPropertyChange(); } }
        private ParameterType _ParameterType;
    
        /// <summary>
        /// 사용자가 화면 상에서 매개변수에 매핑된 값 수정 가능 여부
        /// </summary>
        public bool UserModifiable { get => _UserModifiable; set { _UserModifiable = value; NotifyOfPropertyChange(); } }
        private bool _UserModifiable;

        /// <summary>
        /// 매개변수가 화면상으로 사용자에게 표시 여부 
        /// </summary>
        public bool ParamVisible { get => _ParamVisible; set { _ParamVisible = value; NotifyOfPropertyChange(); } }
        private bool _ParamVisible;
            
        /// <summary>
        /// 매개변수 데이터에 속하는 매개변수 타입 (인스턴스 또는 유형)
        /// </summary>
        public bool InstanceBinding { get => _InstanceBinding; set { _InstanceBinding = value; NotifyOfPropertyChange(); } }
        private bool _InstanceBinding;

        #endregion 프로퍼티

        #region 생성자

        public CreateParamView(CategorySet rvCatSet, string rvParamName, BuiltInParameterGroup rvParameterGroup, ParameterType rvParamType, bool rvUserModifiable, bool rvParamVisible, bool rvInstanceBinding)
        {
            this._CategorySet = rvCatSet;
            this._ParamName = rvParamName;
            this._ParameterGroup = rvParameterGroup;
            this._ParameterType = rvParamType;
            this._UserModifiable = rvUserModifiable;
            this._ParamVisible = rvParamVisible;
            this._InstanceBinding = rvInstanceBinding;
        }
    
        #endregion 생성자
    
    }

    #endregion CreateParamView

    #region SetParamView

    /// <summary>
    /// 입력할 값이 할당된 매개변수 정보
    /// </summary>
    public class SetParamView : BindableBase
    {
        #region 프로퍼티

        /// <summary>
        /// 매개변수 이름
        /// </summary>
        public string ParamName { get => _ParamName; set { _ParamName = value; NotifyOfPropertyChange(); } }
        private string _ParamName;

        /// <summary>
        /// 매개변수에 입력한 값
        /// </summary>
        public string ParamValue { get => _ParamValue; set { _ParamValue = value; NotifyOfPropertyChange(); } }
        private string _ParamValue;

        #endregion 프로퍼티

        #region 생성자

        public SetParamView(string rvParamName, string rvParamValue)
        {
            this._ParamName = rvParamName;
            this._ParamValue = rvParamValue;
        }

        #endregion 생성자
    }

    #endregion SetParamView

    #region Updater_Parameters

    /// <summary>
    /// UPDATER_PARAMETERS - MEP Updater 매개변수
    /// </summary>
    public class Updater_Parameters : BindableBase
    {
        #region 프로퍼티 

        /// <summary>
        /// 매개변수 이름
        /// </summary>
        public string ParamName { get => _ParamName; set { _ParamName = value; NotifyOfPropertyChange(); } }
        private string _ParamName;

        /// <summary>
        /// 매개변수 타입
        /// </summary>
        public string ParamType { get => _ParamType; set { _ParamType = value; NotifyOfPropertyChange(); } }
        private string _ParamType;

        // TODO : 분야 프로퍼티 "Domain" 추후 의미 혼동시 수정 예정 (2024.01.24 jbh)
        // 참고 URL - https://brunch.co.kr/@myner/56
        /// <summary>
        /// 분야 - ALL모델(공통)
        /// </summary>
        public string Domain { get => _Domain; set { _Domain = value; NotifyOfPropertyChange(); } }
        private string _Domain;

        /// <summary>
        /// 데이터유형
        /// </summary>
        public string DataType { get => _DataType; set { _DataType = value; NotifyOfPropertyChange(); } }
        private string _DataType;

        /// <summary>
        /// 그룹매개변수 (매개변수 상위 그룹)
        /// </summary>
        public string DefinitionGroup { get => _DefinitionGroup; set { _DefinitionGroup = value; NotifyOfPropertyChange(); } }
        private string _DefinitionGroup;

        /// <summary>
        /// 매개변수 상위 카테고리셋(카테고리 리스트)
        /// </summary>
        // public List<string> CategorySet { get => _CategorySet; set { _CategorySet = value; NotifyOfPropertyChange(); } }
        // private List<string> _CategorySet;

        /// <summary>
        /// Revit 애플리케이션 한글 - 매개변수 상위 카테고리셋(카테고리 리스트)
        /// </summary>
        public List<string> KOR_CategorySet { get => _KOR_CategorySet; set { _KOR_CategorySet = value; NotifyOfPropertyChange(); } }
        private List<string> _KOR_CategorySet;

        /// <summary>
        /// Revit 애플리케이션 미국 영문 - 매개변수 상위 카테고리셋(카테고리 리스트)
        /// </summary>
        public List<string> ENU_CategorySet { get => _ENU_CategorySet; set { _ENU_CategorySet = value; NotifyOfPropertyChange(); } }
        private List<string> _ENU_CategorySet;

        /// <summary>
        /// Revit 애플리케이션 영국 영문 - 매개변수 상위 카테고리셋(카테고리 리스트)
        /// </summary>
        public List<string> ENG_CategorySet { get => _ENG_CategorySet; set { _ENG_CategorySet = value; NotifyOfPropertyChange(); } }
        private List<string> _ENG_CategorySet;

        #endregion 프로퍼티 

        #region 생성자

        // TODO : AIS 매개변수 클래스 "Updater_Parameters" 생성자 필요시 구현 예정 (2024.03.12 jbh)
        //public Updater_Parameters()
        //{
        //    this._ParamName = ;
        //    this._ParamType = ;
        //    this._Domain    = ;
        //    this._DataType  = ;
        //    this._DefinitionGroup = ;
        //    this._CategorySet = ; 
        //}

        #endregion 생성자 
    }

    #endregion Updater_Parameters
}
