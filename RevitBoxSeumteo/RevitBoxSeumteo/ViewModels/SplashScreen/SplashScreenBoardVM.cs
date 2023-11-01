using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using RevitBoxSeumteo.Converters;

namespace RevitBoxSeumteo.ViewModels.SplashScreen
{
    public class SplashScreenBoardVM
    {
        #region 프로퍼티 

        // TODO : 비트맵 이미지 프로퍼티 "BitmapImage SplashImage" 필요시 사용 (2023.10.26 jbh)
        /// <summary>
        /// Splash Screen 비트맵 이미지
        /// </summary>
        // public BitmapImage SplashImage { get; set; }

        // TODO : 리소스에 등록된 이미지 파일(.png, .jpg 등등...)을 BitmapSource으로 convert 처리한 BitmapSource 클래스 프로퍼티 "SplashSource" 구현 (2023.10.26 jbh)
        // 참고 URL - https://www.technical-recipes.com/2015/wpf-binding-image-using-xaml-and-mvvm-pattern/
        // TODO : BitmapSource 클래스 프로퍼티 "SplashSource" 싱글톤 패턴으로 구현 해야할 시 아래 코드 참고 (2023.10.26 jbh)
        /// <summary>
        /// Splash Screen 비트맵 이미지(BitmapSource)
        /// </summary>
        //public BitmapSource SplashSource
        //{
        //    get
        //    {
        //        // 싱글톤 패턴
        //        if (_SplashSource == null)
        //        {
        //            // 리소스에 등록된 이미지 파일(.png, .jpg 등등...)을 BitmapSource으로 convert 처리 
        //            _SplashSource = BitmapConverter.ConvertFromBitmap(RevitBoxSeumteo.Properties.Resources.SeumteoLogo);
        //        }
        //        return _SplashSource;
        //    }
        //    set => _SplashSource = value;
        //}
        //private BitmapSource _SplashSource;

        /// <summary>
        /// Splash Screen 비트맵 이미지(BitmapSource)
        /// </summary>
        public BitmapSource SplashSource { get; set; }
        

        #endregion 프로퍼티 

        #region 생성자

        public SplashScreenBoardVM() 
        {
            SplashSource = BitmapConverter.ConvertFromBitmap(RevitBoxSeumteo.Properties.Resources.SeumteoLogo);
        }

        #endregion 생성자

        #region 기본 메소드

        #endregion 기본 메소드 

        #region Sample

        #endregion Sample
    }
}
