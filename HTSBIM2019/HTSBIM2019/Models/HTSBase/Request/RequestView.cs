using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HTSBIMNet;

using Autodesk.Revit.DB;

namespace HTSBIM2019.Models.HTSBase.Request
{
    /// <summary>
    /// 테스트용 Request 데이터 모델
    /// </summary>
    //public class RequestView
    //{

    //}

    // TODO : 필요시 클래스 "RequestView" 사용 예정 (2024.04.17 jbh)
    public class RequestView : BindableBase
    {
        #region 프로퍼티

        /// <summary>
        /// Revit 업데이터 + Triggers 등록 여부
        /// </summary>
        public bool IsUpdaterRegistered { get => _IsUpdaterRegistered; set { _IsUpdaterRegistered = value; NotifyOfPropertyChange();  } }
        private bool _IsUpdaterRegistered;

        // TODO : 폼 화면(MEPUpdater.cs) 출력시 .ShowDialog(Modal - 부모 창 제어 X)하는 방식이 아니라
        //        .Show(Modaless - 부모 창 제어 O)으로 하는 방식으로 수정해야 하면
        //        부모창 제어가 가능하므로 사용자가 Revit 문서가 1개가 아니라 여러 개를 열어서 작업할 수 있으므로
        //        아래처럼 프로퍼티 "RevitDoc"로 구현하면 안 되고,
        //        Command.cs에서 Revit 문서 여러 개를 인자로 한 번에 받도록 구현 해야함. (2024.03.14 jbh) 
        // Modal VS Modaless 차이
        // 참고 URL   - https://blog.naver.com/PostView.naver?blogId=wlsdml1103&logNo=220512538948
        // 참고 2 URL - https://greensul.tistory.com/37
        /// <summary>
        /// Revit 문서 
        /// </summary>
        public Document RevitDoc { get => _RevitDoc; set { _RevitDoc = value; NotifyOfPropertyChange(); } }
        private Document _RevitDoc;

        /// <summary>
        /// 업데이터 아이디
        /// </summary>
        public UpdaterId Updater_Id { get => _Updater_Id; set { _Updater_Id = value; NotifyOfPropertyChange(); } }
        private UpdaterId _Updater_Id;

        /// <summary>
        /// 업데이터 + Triggers 등록하려는 카테고리 정보
        /// </summary>
        public BuiltInCategory UpdaterCategory { get => _UpdaterCategory; set { _UpdaterCategory = value; NotifyOfPropertyChange(); } }
        private BuiltInCategory _UpdaterCategory;

        /// <summary>
        /// 업데이터 + Triggers 등록하려는 카테고리 이름
        /// </summary>
        public string UpdaterCategoryName { get => _UpdaterCategoryName; set { _UpdaterCategoryName = value; NotifyOfPropertyChange(); } }
        private string _UpdaterCategoryName;

        /// <summary>
        /// 카테고리(객체) 필터 
        /// </summary>
        public ElementCategoryFilter CategoryFilter { get => _CategoryFilter; set { _CategoryFilter = value; NotifyOfPropertyChange(); } }
        private ElementCategoryFilter _CategoryFilter;

        #endregion 프로퍼티

        #region 생성자

        public RequestView(bool pIsUpdaterRegistered, Document rvRevitDoc, UpdaterId rvUpdaterId, BuiltInCategory rvUpdaterCategory, string rvUpdaterCategoryName, ElementCategoryFilter rvCategoryFilter)
        {
            this._IsUpdaterRegistered = pIsUpdaterRegistered;
            this._RevitDoc = rvRevitDoc;
            this._Updater_Id = rvUpdaterId;
            this._UpdaterCategory = rvUpdaterCategory;
            this._UpdaterCategoryName = rvUpdaterCategoryName;
            this._CategoryFilter = rvCategoryFilter;
        }

        #endregion 생성자
    }
}
