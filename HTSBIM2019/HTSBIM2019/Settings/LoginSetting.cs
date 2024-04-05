using HTSBIMNet;

namespace HTSBIM2019.Settings
{
    // TODO : 해당 "LoginSetting" 클래스는 상속용이 아닌 다른 클래스(AppSetting)에 조합(Composition) 용도로 사용하는 클래스로 구현 (2024.04.02 jbh)
    // 상속 VS 조합 
    // 참고 URL - https://tecoble.techcourse.co.kr/post/2020-05-18-inheritance-vs-composition/
    public class LoginSetting : BindableBase
    {
        /// <summary>
        /// Revit 로그인 아이디
        /// </summary>
        public string LoginUserId   
        {
            get => this._LoginUserId;
            set
            {
                this._LoginUserId = value;
                this.Changed(nameof(LoginUserId));
            }
        }
        private string _LoginUserId;


        /// <summary>
        /// 사용자 이름 
        /// </summary>
        public string Username
        {
            get => this._Username;
            set
            {
                this._Username = value;
                this.Changed(nameof(Username));
            }
        }
        private string _Username;

        #region Sample

        #endregion Sample
    }
}
