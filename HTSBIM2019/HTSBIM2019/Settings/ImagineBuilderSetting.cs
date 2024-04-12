using HTSBIM2019.Utils.CompanyHomePage;
using HTSBIM2019.Utils.TechnicalSupport;
using HTSBIMNet;

namespace HTSBIM2019.Settings
{
    // TODO : 해당 "ImagineBuilderSetting" 클래스는 상속용이 아닌 다른 클래스(AppSetting)에 조합(Composition) 용도로 사용하는 클래스로 구현 (2024.04.12 jbh)
    // 상속 VS 조합 
    // 참고 URL - https://tecoble.techcourse.co.kr/post/2020-05-18-inheritance-vs-composition/
    public class ImagineBuilderSetting : BindableBase
    {
        /// <summary>
        /// (주)상상진화 기술지원 문의 - 상상플렉스 커뮤니티 웹사이트 연결 Utils 객체 
        /// </summary>
        public TechnicalSupport TechSupport { get => _TechSupport; set { _TechSupport = value; NotifyOfPropertyChange(nameof(TechSupport)); } }
        private TechnicalSupport _TechSupport;

        /// <summary>
        /// (주)상상진화 기업 홈페이지 - 상상진화 기업 홈페이지 연결 Utils 객체 
        /// </summary>
        public CompanyHomePage HomePage { get => _HomePage; set { _HomePage = value; NotifyOfPropertyChange(nameof(HomePage)); } }
        private CompanyHomePage _HomePage;
    }
}
