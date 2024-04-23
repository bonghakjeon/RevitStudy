using System.Collections.Generic;

using HTSBIM2019.Models.HTSBase.MEPUpdater;

using Autodesk.Revit.DB;


namespace HTSBIM2019.Interface.Request
{
    // TODO : 필요시 인터페이스 "IHTSRequest.cs" 추가 구현 예정 (2024.04.15 jbh) 
    public interface IHTSRequest
    {
        #region 프로퍼티 

        /// <summary>
        /// Revit 업데이터 + Triggers 등록 여부
        /// </summary>
        bool IsUpdaterRegistered { get; set; }

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
        Document RevitDoc { get; set; }

        /// <summary>
        /// 업데이터 아이디
        /// </summary>
        UpdaterId Updater_Id { get; set; }

        /// <summary>
        /// 업데이터 + Triggers 등록하려는 카테고리 정보
        /// </summary>
        //BuiltInCategory UpdaterCategory { get; set; }

        /// <summary>
        /// 업데이터 + Triggers 등록하려는 카테고리 정보 리스트 
        /// </summary>
        public List<CategoryInfoView> UpdaterCategoryList { get; set; }

        /// <summary>
        /// 업데이터 + Triggers 등록하려는 카테고리 이름
        /// </summary>
        string UpdaterCategoryName { get; set; }

        // TODO : 아래 주석친 프로퍼티 카테고리(객체) 필터 프로퍼티 "CategoryFilter" 필요시 참고 (2024.04.19 jbh)
        /// <summary>
        /// 카테고리(객체) 필터 
        /// </summary>
        // ElementCategoryFilter CategoryFilter { get; set; }

        #endregion 프로퍼티

        #region Register

        /// <summary>
        /// 업데이터 + Triggers 등록
        /// </summary>
        // void Register(IUpdater updater, UpdaterId updaterId, Document doc, ElementCategoryFilter categoryFilter);
        // void Register(IUpdater updater, UpdaterId updaterId, Document doc, ElementCategoryFilter categoryFilter, string builtInCategoryName);
        // void Register(IUpdater updater, UpdaterId updaterId, Document doc, ElementCategoryFilter categoryFilter, List<CategoryInfoView> categoryInfoList);
        void Register(IUpdater updater, UpdaterId updaterId, Document doc, List<CategoryInfoView> categoryInfoList);

        #endregion Register

        #region Remove

        /// <summary>
        /// 업데이터 + Triggers 제거(해제)
        /// </summary>
        void Remove(Document doc, UpdaterId updaterId);

        #endregion Remove

        #region Sample

        #endregion Sample
    }
}
