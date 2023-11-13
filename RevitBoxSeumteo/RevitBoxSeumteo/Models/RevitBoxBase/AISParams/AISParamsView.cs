using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using RevitBoxSeumteo.Common.AISParam;
using RevitBoxSeumteo.Converters;
using RevitBoxSeumteoNet;

namespace RevitBoxSeumteo.Models.RevitBoxBase.ParamsCreate
{
    public class AISParamsView : BindableBase
    {
        #region 프로퍼티 

        /// <summary>
        /// AISParamsCreateBoardV.xaml 비트맵 이미지(BitmapSource)
        /// </summary>
        public BitmapSource ParamsSource { get; set; }

        /// <summary>
        /// Title - AIS_매개변수 생성
        /// </summary>
        public string TitleParamsCreate
        {
            get
            {
                return _TitleParamsCreate;
            }
            set
            {
                _TitleParamsCreate = value;
                Changed();
            }
        }
        private string _TitleParamsCreate = string.Empty;

        /// <summary>
        /// Text - 버튼 AIS 매개변수 생성을 클릭하세요. / AIS 매개변수 생성 중...
        /// </summary>
        public string TxtParamsCreate
        {
            get
            {
                return _TxtParamsCreate;
            }
            set
            {
                _TxtParamsCreate = value;
                Changed();
            }
        }
        private string _TxtParamsCreate = string.Empty;


        /// <summary>
        /// Button - AIS 매개변수 생성
        /// </summary>
        public string BtnParamsCreate
        {
            get
            {
                return _BtnParamsCreate;
            }
            set
            {
                _BtnParamsCreate = value;
                Changed();
            }
        }
        private string _BtnParamsCreate = string.Empty;

        #endregion 프로퍼티

        #region 생성자

        public AISParamsView()
        {
            ParamsSource = BitmapConverter.ConvertFromBitmap(RevitBoxSeumteo.Properties.Resources.SeumteoParams);

            TitleParamsCreate = AISParamsHelper.매개변수생성;
            TxtParamsCreate = AISParamsHelper.매개변수생성클릭;
            BtnParamsCreate = AISParamsHelper.매개변수생성;
        }

        #endregion 생성자
    }
}
