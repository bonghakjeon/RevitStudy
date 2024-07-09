using HTSBIMNet;

namespace HTSBIM2019.Settings
{
    // TODO : 자바 디자인 패턴 "컴포짓 패턴(Composite Pattern)" 사용해서 클래스 "AppSetting"에 속하는
    //        어댑터 클래스 "LoginSetting, UpdaterSetting, ImagineBuilderSetting" 구현 (2024.04.12 jbh)
    // 유튜브 참고 URL - 
    // https://youtu.be/78uNgDSHw-k?si=fqLynr8tvtn_LlaM
    // 참고 URL - https://velog.io/@newtownboy/%EB%94%94%EC%9E%90%EC%9D%B8%ED%8C%A8%ED%84%B4-%EC%BB%B4%ED%8D%BC%EC%A7%80%ED%8A%B8%ED%8C%A8%ED%84%B4Composite-Pattern

    // TODO : 자바 디자인 패턴 "어댑터 패턴(Adapter Pattern)" 사용해서 클래스 "AppSetting"에 속하는
    //        어댑터 클래스 "LoginSetting, UpdaterSetting, ImagineBuilderSetting" 구현 (2024.04.12 jbh)
    // 유튜브 참고 URL - 
    // https://youtu.be/gJDZ7pcvlAU?si=JwdbdL6BIkMtY35r

    // TODO : 조합(Composition) 사용해서  클래스 "AppSetting"에 속하는 클래스 "LoginSetting, UpdaterSetting, ImagineBuilderSetting" 구현 (2024.04.12 jbh)
    // 상속 VS 조합 
    // 참고 URL - https://tecoble.techcourse.co.kr/post/2020-05-18-inheritance-vs-composition/

    /// <summary>
    /// 응용 프로그램 기본 설정 
    /// </summary>
    public class AppSetting : BindableBase
    {
        // TODO : AppSetting.cs -> 응용 프로그램 기본 설정 프로퍼티 "Default" 필요시 수정 예정 (2024.04.02 jbh)
        /// <summary>
        /// 응용 프로그램 기본 설정
        /// </summary>
        public static AppSetting Default
        {
            get => _Default ?? (_Default = new AppSetting());
            set
            {
                _Default = value;
                BindableBase.StaticChanged(nameof(Default));
            }
        }
        private static AppSetting _Default;

        /// <summary>
        /// 로그인 기본 설정 
        /// </summary>
        public LoginSetting Login
        {
            get => _Login ?? (_Login = new LoginSetting());
            set
            {
                _Login = value;
                this.Changed(nameof(Login));
            }
        }
        private LoginSetting _Login;

        /// <summary>
        /// 디렉토리(폴더) 설정
        /// </summary>
        public DirectorySetting DirectoryBase
        {
            get => _DirectoryBase ?? (_DirectoryBase = new DirectorySetting());
            set 
            {
                _DirectoryBase = value;
                this.Changed(nameof(DirectoryBase));
            }
        }
        private DirectorySetting _DirectoryBase;

        /// <summary>
        /// 업데이터 설정
        /// </summary>
        //public UpdaterSetting UpdaterBase
        //{
        //    get => _UpdaterBase ?? (_UpdaterBase = new UpdaterSetting());
        //    set
        //    {
        //        _UpdaterBase = value;
        //        this.Changed(nameof(UpdaterBase));
        //    }
        //}
        //private UpdaterSetting _UpdaterBase;
        public UpdaterSetting UpdaterBase { get => _UpdaterBase; set { _UpdaterBase = value; this.Changed(nameof(UpdaterBase)); } }
        private UpdaterSetting _UpdaterBase;

        /// <summary>
        /// (주)상상진화 기본 설정
        /// </summary>
        public ImagineBuilderSetting ImagineBuilderBase
        {
            get => _ImagineBuilderBase ?? (_ImagineBuilderBase = new ImagineBuilderSetting());
            set
            {
                _ImagineBuilderBase = value;
                this.Changed(nameof(ImagineBuilderBase));
            }
        }
        private ImagineBuilderSetting _ImagineBuilderBase;

        public ImageEditorSetting ImageEditorBase { get => _ImageEditorBase; set { _ImageEditorBase = value; this.Changed(nameof(ImageEditorBase)); } }
        private ImageEditorSetting _ImageEditorBase;
    }

    #region Sample

    #endregion Sample
}
