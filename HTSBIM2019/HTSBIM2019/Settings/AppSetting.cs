using HTSBIMNet;

namespace HTSBIM2019.Settings
{
    public class AppSetting : BindableBase
    {
        // TODO : AppSetting.cs -> 응용 프로그램 기본 설정 프로퍼티 "Default" 필요시 수정 예정 (2024.04.02 jbh)
        /// <summary>
        /// 응용 프로그램 기본 설정
        /// </summary>
        public static AppSetting Default
        {
            get => AppSetting._Default ?? (AppSetting._Default = new AppSetting());
            set
            {
                AppSetting._Default = value;
                BindableBase.StaticChanged(nameof(Default));
            }
        }
        private static AppSetting _Default;

        /// <summary>
        /// 로그인 기본 설정 
        /// </summary>
        public LoginSetting Login
        {
            get => this._Login ?? (this._Login = new LoginSetting());
            set
            {
                this._Login = value;
                this.Changed(nameof(Login));
            }
        }
        private LoginSetting _Login;

        /// <summary>
        /// 업데이터 설정
        /// </summary>
        public UpdaterSetting UpdaterBase
        {
            get => this._UpdaterBase ?? (this._UpdaterBase = new UpdaterSetting());
            set
            {
                this._UpdaterBase = value;
                this.Changed(nameof(UpdaterBase));
            }
        }
        private UpdaterSetting _UpdaterBase;
    }

    #region Sample

    #endregion Sample
}
