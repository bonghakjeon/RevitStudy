using System;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Imaging;

namespace RevitBoxSeumteo.Converters
{
    public class BitmapConverter
    {
        #region convertFromBitmap

        // TODO : static 메서드 "CreateRibbonControl"에서 메서드 "ConvertFromBitmap" 호출하기 위해 메서드 "ConvertFromBitmap"을 static 메서드로 구현 (2023.10.6 jbh) 
        // 참고 URL - https://blog.naver.com/PostView.naver?blogId=anakt&logNo=222370700059&parentCategoryNo=&categoryNo=&viewDate=&isShowPopularPosts=false&from=postView
        // 참고 2 URL - https://www.technical-recipes.com/2015/wpf-binding-image-using-xaml-and-mvvm-pattern/
        // TODO : 클래스 "BitmapSource", "Bitmap" 사용하기 위해 using문 "System.Windows.Media.Imaging" 추가 (2023.10.6 jbh)
        /// <summary>
        /// 리소스에 등록된 이미지 파일(.png, .jpg 등등...)을 BitmapSource으로 convert 처리 해주는 메서드 
        /// </summary>
        public static BitmapSource ConvertFromBitmap(Bitmap bitmap)
        {
            // RevitBoxSeumteo 프로젝트 파일 -> 참조 -> WindowsBase.dll 파일 추가  
            return System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                bitmap.GetHbitmap(),
                IntPtr.Zero,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());
        }

        #endregion convertFromBitmap
    }
}
