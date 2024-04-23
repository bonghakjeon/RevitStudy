using HTSBIMNet;

namespace HTSBIM2019.Settings
{
    public class DirectorySetting : BindableBase
    {
        #region 프로퍼티

        /// <summary>
        /// dll 파일(HTSBIM2019.dll)의 부모 폴더 경로
        /// </summary>
        public string ParentDirPath { get => _ParentDirPath; set { _ParentDirPath = value; NotifyOfPropertyChange(nameof(ParentDirPath)); } }
        private string _ParentDirPath;

        /// <summary>
        /// 로그(Logs) 폴더(디렉토리) 경로 
        /// </summary>
        public string LogDirPath { get => _LogDirPath; set { _LogDirPath = value; NotifyOfPropertyChange(nameof(LogDirPath)); } }
        private string _LogDirPath;

        #endregion 프로퍼티 

        #region Sample

        #endregion Sample
    }
}
