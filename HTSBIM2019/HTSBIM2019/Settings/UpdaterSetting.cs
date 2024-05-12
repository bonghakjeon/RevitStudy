using HTSBIM2019.UI.MEPUpdater;
using HTSBIM2019.UI.UpdaterLoading;
using HTSBIM2019.Utils.MEPUpdater;
using HTSBIM2019.Common.RequestBase;

using HTSBIMNet;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace HTSBIM2019.Settings
{
    // TODO : 해당 "UpdaterSetting" 클래스는 상속용이 아닌 다른 클래스(AppSetting)에 조합(Composition) 용도로 사용하는 클래스로 구현 (2024.04.02 jbh)
    // 상속 VS 조합 
    // 참고 URL - https://tecoble.techcourse.co.kr/post/2020-05-18-inheritance-vs-composition/
    public class UpdaterSetting : BindableBase
    {
        #region 프로퍼티 

        /// <summary>
        /// 싱글톤 객체 - 업데이터 기본 설정 객체 자기 자신(Self)
        /// </summary>
        //public static UpdaterSetting Self 
        //{ 
        //    get => _Self ?? (_Self = new UpdaterSetting()); 
        //    set 
        //    { 
        //        _Self = value; 
        //        BindableBase.StaticChanged(nameof(Self)); 
        //    } 
        //}
        //private static UpdaterSetting _Self;
        //public static UpdaterSetting Self { get => _Self; set { _Self = value; BindableBase.StaticChanged(nameof(Self)); } }
        private static UpdaterSetting _Self = null;


        // TODO : UpdaterSetting 클래스 필요시 싱글톤 객체로 구현 진행 (2024.04.02 jbh)
        // [자바 디자인 패턴 이해] 
        // 5강 싱글톤 패턴(Singleton Pattern)
        // 유튜브 참고 URL - 
        // https://youtu.be/5jgpu9-ywtY?si=rImw66r7Y4_DRHM9
        // GoF의 Design Pattern - 7. Singleton
        // 유튜브 참고 URL - 
        // https://youtu.be/kAnoWt7Uato?si=i81ZgxraI_AcWQxt
        /// <summary>
        /// 싱글톤 객체 - MEP 업데이터 
        /// </summary>
        public MEPUpdater MEPUpdater { get => _MEPUpdater; set { _MEPUpdater = value; NotifyOfPropertyChange(nameof(MEPUpdater)); } }
        private MEPUpdater _MEPUpdater;

        /// <summary>
        /// 싱글톤 객체 - MEPUpdater 폼 객체 
        /// </summary>
        public MEPUpdaterForm MEPUpdaterForm { get => _MEPUpdaterForm; set { _MEPUpdaterForm = value; NotifyOfPropertyChange(nameof(MEPUpdaterForm)); } }
        private MEPUpdaterForm _MEPUpdaterForm;

        /// <summary>
        /// 싱글톤 객체 - UpdaterLoading 폼 객체
        /// </summary>
        public UpdaterLoadingForm UpdaterLoadingForm { get => _UpdaterLoadingForm; set { _UpdaterLoadingForm = value; NotifyOfPropertyChange(nameof(UpdaterLoadingForm)); } }
        private UpdaterLoadingForm _UpdaterLoadingForm;


        #endregion 프로퍼티

        #region 생성자

        private UpdaterSetting()
        {

        }

        #endregion 생성자

        #region GetInstance

        #endregion GetInstance

        #region GetUpdaterInstance

        /// <summary>
        /// 싱글톤 객체 - 업데이터 기본 설정 가져오기
        /// </summary>
        //public static UpdaterSetting GetUpdaterInstance(AddInId rvAddInId, ExternalEvent rvExEvent, MEPUpdaterRequestHandler pHandler, UIApplication rvUIApp)
        //public static UpdaterSetting GetUpdaterInstance(AddInId rvAddInId)
        public static UpdaterSetting GetUpdaterInstance()
        {
            // 업데이터 기본 설정 객체 자기 자신(Self)이 null일 경우
            if(_Self is null) _Self = new UpdaterSetting();

            // MEP 업데이터 객체가 null인 경우 
            // if(_Self._MEPUpdater is null) _Self._MEPUpdater = new MEPUpdater(rvAddInId);

            return _Self;
        }

        #endregion GetUpdaterInstance

        #region GetMEPUpdaterInstance   

        /// <summary>
        /// 싱글톤 객체 - MEP 업데이터 객체 가져오기
        /// </summary>
        public static MEPUpdater GetMEPUpdaterInstance(AddInId rvAddInId)
        {
            // MEP 업데이터 객체가 null인 경우 
            if (_Self._MEPUpdater is null) _Self._MEPUpdater = new MEPUpdater(rvAddInId);

            return _Self._MEPUpdater;
        }

        #endregion GetMEPUpdaterInstance

        #region GetUpdaterFormInstance

        /// <summary>
        /// 싱글톤 객체 - MEPUpdater 폼 객체  가져오기
        /// </summary>
        public static MEPUpdaterForm GetUpdaterFormInstance(ExternalEvent rvExEvent, MEPUpdaterRequestHandler pHandler, UIApplication rvUIApp)
        {
            // TODO : 필요시 아래 주석친 코드 사용 예정 (2024.05.09 jbh)
            // 업데이터 기본 설정 객체 자기 자신(Self)이 null일 경우
            // if (_Self is null) _Self = new UpdaterSetting();

            // Modaless 폼 객체가 null이거나 삭제된 경우 
            if (_Self._MEPUpdaterForm is null || _Self._MEPUpdaterForm.IsDisposed) _Self._MEPUpdaterForm = new MEPUpdaterForm(rvExEvent, pHandler, rvUIApp);

            // UpdaterLoading 폼 객체가 null이거나 삭제된 경우 
            if (_Self._UpdaterLoadingForm is null || _Self._UpdaterLoadingForm.IsDisposed) _Self._UpdaterLoadingForm = new UpdaterLoadingForm();

            return _Self._MEPUpdaterForm;
        }

        #endregion GetUpdaterFormInstance

        #region GetUpdaterInstance

        // TODO : 아래 주석친 코드 필요시 참고 (2024.04.24 jbh)
        /// <summary>
        /// 싱글톤 객체 - MEP 업데이터 가져오기
        /// </summary>
        //public static MEPUpdater GetUpdaterInstance(AddInId rvAddInId)
        //{
        //    // 업데이터 기본 설정 객체 자기 자신(Self)이 null일 경우
        //    if(_Self is null) _Self = new UpdaterSetting();

        //    // MEP 업데이터 객체가 null인 경우 
        //    if(_Self._MEPUpdater is null) _Self._MEPUpdater = new MEPUpdater(rvAddInId);

        //    return _Self._MEPUpdater;
        //}

        #endregion GetUpdaterInstance

        #region GetUpdaterFormInstance

        // TODO : 아래 주석친 코드 필요시 참고 (2024.04.24 jbh)
        /// <summary>
        /// 싱글톤 객체 - MEP 업데이터 폼 객체 가져오기
        /// </summary>
        //public static MEPUpdaterForm GetUpdaterFormInstance(ExternalEvent rvExEvent, MEPUpdaterRequestHandler pHandler, UIApplication rvUIApp)
        //{
        //    // 업데이터 기본 설정 객체 자기 자신(Self)이 null일 경우
        //    if(_Self is null) _Self = new UpdaterSetting();

        //    // Modaless 폼 객체가 null이거나 삭제된 경우 
        //    if(_Self._MEPUpdaterForm is null || _Self._MEPUpdaterForm.IsDisposed) _Self._MEPUpdaterForm = new MEPUpdaterForm(rvExEvent, pHandler, rvUIApp);

        //    return _Self._MEPUpdaterForm; 
        //}

        #endregion GetUpdaterFormInstance
    }

    #region Sample

    #endregion Sample
}
