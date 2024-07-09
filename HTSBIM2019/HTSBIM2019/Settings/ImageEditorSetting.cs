using HTSBIM2019.Common.RequestBase;
using HTSBIM2019.UI.ImageEditor;
using HTSBIMNet;

using Autodesk.Revit.UI;

namespace HTSBIM2019.Settings
{
    // TODO : 해당 "ImageEditorSetting" 클래스는 상속용이 아닌 다른 클래스(AppSetting)에 조합(Composition) 용도로 사용하는 클래스로 구현 (2024.04.02 jbh)
    // 상속 VS 조합 
    // 참고 URL - https://tecoble.techcourse.co.kr/post/2020-05-18-inheritance-vs-composition/
    public class ImageEditorSetting : BindableBase
    {
        #region 프로퍼티 

        /// <summary>
        /// 싱글톤 객체 - 이미지 편집 기본 설정 객체 자기 자신(Self)
        /// </summary>
        //public static ImageEditorSetting Self 
        //{ 
        //    get => _Self ?? (_Self = new ImageEditorSetting()); 
        //    set 
        //    { 
        //        _Self = value; 
        //        BindableBase.StaticChanged(nameof(Self)); 
        //    } 
        //}
        //private static ImageEditorSetting _Self;
        //public static ImageEditorSetting Self { get => _Self; set { _Self = value; BindableBase.StaticChanged(nameof(Self)); } }
        private static ImageEditorSetting _Self = null;

        /// <summary>
        /// 싱글톤 객체 - MEPImageEditor 폼 객체 
        /// </summary>
        public ImageEditorForm ImageEditorForm { get => _ImageEditorForm; set { _ImageEditorForm = value; NotifyOfPropertyChange(nameof(ImageEditorForm)); } }
        private ImageEditorForm _ImageEditorForm;

        #endregion 프로퍼티

        #region 생성자

        private ImageEditorSetting()
        {

        }

        #endregion 생성자

        #region GetInstance

        #endregion GetInstance

        #region GetImageEditorInstance

        /// <summary>
        /// 싱글톤 객체 - 이미지 편집 기본 설정 가져오기
        /// </summary>
        //public static ImageEditorSetting GetImageEditorInstance(AddInId rvAddInId, ExternalEvent rvExEvent, MEPImageEditorRequestHandler pHandler, UIApplication rvUIApp)
        //public static ImageEditorSetting GetImageEditorInstance(AddInId rvAddInId)
        public static ImageEditorSetting GetImageEditorInstance()
        {
            // 업데이터 기본 설정 객체 자기 자신(Self)이 null일 경우
            if (_Self is null) _Self = new ImageEditorSetting();

            // MEP 업데이터 객체가 null인 경우 
            // if(_Self._MEPImageEditor is null) _Self._MEPImageEditor = new MEPImageEditor(rvAddInId);

            return _Self;
        }

        #endregion GetImageEditorInstance

        #region GetImageEditorFormInstance

        /// <summary>
        /// 싱글톤 객체 - MEPImageEditor 폼 객체  가져오기
        /// </summary>
        public static ImageEditorForm GetImageEditorFormInstance(ExternalEvent rvExEvent, ImageEditorRequestHandler pHandler, UIApplication rvUIApp)
        {
            // TODO : 필요시 아래 주석친 코드 사용 예정 (2024.05.09 jbh)
            // 업데이터 기본 설정 객체 자기 자신(Self)이 null일 경우
            // if (_Self is null) _Self = new ImageEditorSetting();

            // Modaless 폼 객체가 null이거나 삭제된 경우 
            if (_Self._ImageEditorForm is null || _Self._ImageEditorForm.IsDisposed) _Self._ImageEditorForm = new ImageEditorForm(rvExEvent, pHandler, rvUIApp);

            
            return _Self._ImageEditorForm;
        }

        #endregion GetImageEditorFormInstance

        #region Sample

        #endregion Sample
    }
}
