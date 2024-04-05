using HTSBIM2019.Utils.MEPUpdater;

using HTSBIMNet;

namespace HTSBIM2019.Settings
{
    // TODO : 해당 "UpdaterSetting" 클래스는 상속용이 아닌 다른 클래스(AppSetting)에 조합(Composition) 용도로 사용하는 클래스로 구현 (2024.04.02 jbh)
    // 상속 VS 조합 
    // 참고 URL - https://tecoble.techcourse.co.kr/post/2020-05-18-inheritance-vs-composition/
    public class UpdaterSetting : BindableBase
    {
        // TODO : MEP 업데이터 필요시 싱글톤 객체로 구현 예정 (2024.04.02 jbh)
        /// <summary>
        /// MEP 업데이터 
        /// </summary>
        public MEPUpdater MEPUpdater
        {
            get => this._MEPUpdater;
            set
            {
                this._MEPUpdater = value;
                this.Changed(nameof(MEPUpdater));
            }
        }
        private MEPUpdater _MEPUpdater;
    }

    #region Sample

    #endregion Sample
}
