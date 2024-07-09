using Serilog;
using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Autodesk.Revit.UI;
using HTSBIM2019.Common.HTSBase;
using HTSBIM2019.Common.LogBase;
using System.Drawing.Drawing2D;

namespace HTSBIM2019.Common.Managers
{
    public static class ImageManager
    {
        #region 프로퍼티


        #region EnumExistFile

        // TODO : Enum 열거형 구조체 "EnumExistFile" 필요시 구현 예정 (2024.07.08 jbh)
        /// <summary>
        /// 파일 존재 - 원본 이미지 파일 존재 여부 
        /// </summary>
        //private enum EnumExistFile: int
        //{
        //    [Description("원본 이미지 파일 존재 안함.")]
        //    NONE = 0,
        //    [Description("원본 이미지 파일 존재함.")]
        //    EXIST = 1,
        //}

        #endregion EnumExistFile

        #endregion 프로퍼티

        #region GetInsertImageFilePath

        /// <summary>
        /// 삽입 처리할 이미지 저장할 파일 경로 가져오기
        /// </summary>
        public static string GetInsertImageFilePath(string pOrgImageFilePath)
        {
            string dirPath = string.Empty;                       // 이미지 삽입 처리 하고자 하는 파일의 상위 디렉토리 경로
            string fileName = string.Empty;                      // 이미지 삽입 처리 하고자 하는 파일 이름(파일 확장자 포함) 
            string fileNameWithoutExtension = string.Empty;      // 이미지 삽입 처리 하고자 하는 파일 이름(파일 확장자 제외) 
            string extensionName = string.Empty;                 // 파일 확장자 이름(.png, .jpg, .gif 등등...)
            string insertFilePath = string.Empty;                // 삽입 처리할 이미지를 저장할 파일 경로(파일 전체 경로 + 이름 중복일 때 변경된 파일 이름) 

            var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

            try
            {
                // TODO : 메서드 "Path.GetDirectoryName" 사용해서 이미지 삽입 처리 하고자 하는 원본 이미지 파일의 상위 디렉토리 경로 문자열로 구하기 (2024.06.25 jbh)
                // 참고 URL - https://learn.microsoft.com/ko-kr/dotnet/api/system.io.path.getdirectoryname?view=net-8.0
                // 참고 2 URL - https://afsdzvcx123.tistory.com/entry/C-%EB%AC%B8%EB%B2%95-%ED%8C%8C%EC%9D%BC-%EA%B2%BD%EB%A1%9C%EC%97%90%EC%84%9C-%EB%94%94%EB%A0%89%ED%86%A0%EB%A6%AC-%EA%B2%BD%EB%A1%9C-%EA%B0%80%EC%A0%B8%EC%98%A4%EA%B8%B0
                dirPath = Path.GetDirectoryName(pOrgImageFilePath);

                // TODO : 메서드 "Path.GetFileNameWithoutExtension" 사용해서
                //        이미지 삽입 처리 하고자 하는 원본 이미지 파일의 확장자 이름(.png, .jpg, .gif 등등...) 빼고 파일 이름 문자열로 구하기 (2024.06.25 jbh)
                // 참고 URL - https://learn.microsoft.com/ko-kr/dotnet/api/system.io.path.getfilenamewithoutextension?view=net-8.0
                // 참고 2 URL - https://terrorjang.tistory.com/entry/C-openFileDialogFileName%EC%97%90%EC%84%9C-%ED%8C%8C%EC%9D%BC-%EC%9D%B4%EB%A6%84%EB%A7%8CPathGetFileName
                fileNameWithoutExtension = Path.GetFileNameWithoutExtension(pOrgImageFilePath);

                fileName = Path.GetFileName(pOrgImageFilePath);

                // TODO : 메서드 "Path.GetExtension" 사용해서
                //        이미지 삽입 처리 하고자 하는 원본 이미지 파일의 확장자 이름(.png, .jpg, .gif 등등...) 문자열로 구하기 (2024.06.25 jbh)
                // 참고 URL - https://learn.microsoft.com/ko-kr/dotnet/api/system.io.path.getextension?view=net-8.0
                extensionName = Path.GetExtension(pOrgImageFilePath);

                // TODO : 저장할 파일명이 중복일 때, 기존 파일명 + N 으로 생성하도록 구현(2024.06.26 jbh)
                // 참고 URL - https://pseudo-code.tistory.com/166
                if (fileName.Length > 0)
                {
                    bool bExist = true;   // 중복된 파일 존재여부 확인
                    int fileCnt = 0;      // 중복된 파일 저장시 파일 이름에 붙일 숫자(N) 

                    while (bExist)
                    {
                        string pathCombine = Path.Combine(dirPath, fileName);

                        // 해당 파일 경로에 똑같은 이름의 파일이 존재하는 경우 
                        if (true == File.Exists(pathCombine))
                        {
                            fileCnt++;
                            fileName = fileNameWithoutExtension + "(" + fileCnt + ")" + extensionName;
                        }
                        // 똑같은 이름의 파일이 존재하지 않는 경우 
                        else
                        {
                            // TODO : 필요시 원본 이미지 파일의 이름이 변경 되거나 삭제된 경우 구현 예정 (2024.07.08 jbh)
                            // 원본 이미지 파일의 이름이 변경 되거나 삭제된 경우 
                            // if(fileCnt.Equals((int)EnumExistFile.NONE)) fileName = fileNameWithoutExtension + "(" + fileCnt + ")" + extensionName;
                            bExist = false;
                        }
                    }
                }

                insertFilePath = Path.Combine(dirPath, fileName);

                return insertFilePath;
            }
            catch (Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                throw;   // 오류 발생시 상위 호출자 예외처리 전달 throw
            }
        }

        #endregion GetInsertImageFilePath

        #region BlackConvert

        // TODO : 이미지 흑백 전환 기능 구현 (2024.07.04 jbh)
        // MS 공식 문서
        // GetPixel 참고 URL - http://msdn.microsoft.com/ko-kr/library/system.drawing.bitmap.getpixel(v=vs.110).aspx 
        // SetPixel 참고 URL - http://msdn.microsoft.com/ko-kr/library/system.drawing.bitmap.setpixel(v=vs.110).aspx 
        // FromArgb 참고 URL - http://msdn.microsoft.com/en-us/library/cce5h557(v=vs.110).aspx 

        // 블로그 문서 
        // 참고 URL - https://tctt.tistory.com/129
        // 참고 2 URL - https://blog.naver.com/nersion/140141133683
        // 참고 3 URL - https://blog.naver.com/PostView.naver?blogId=kgg1959&logNo=30182499708
        // 참고 4 URL - https://son10001.blogspot.com/2014/04/blog-post_14.html

        // ChatGPT 문서
        // 참고 URL - https://chatgpt.com/c/772919c6-4936-4b8d-b796-0a4b4d02e6ef

        /// <summary>
        /// 이미지 흑백 전환 + 이진화 처리
        /// </summary>
        /// <param name="parameter">pConvertBmp - 흑백 전환 + 이진화 처리할 비트맵 객체</param>
        public static void BlackConvert(ref Bitmap pConvertBmp)
        {
            int average = 0;                       // RGB 색상값 평균
            Color orgColor = Color.Transparent;    // 특정 픽셀의 기존 RGB 색상 (투명 초기화)
                                                   // Color blackColor = Color.Transparent;     // 특정 픽셀의 흑백 전환 RGB 색상 (투명색 초기화 - Color.Transparent)
            Color binaryColor = Color.Transparent;    // 특정 픽셀의 이진화 처리 RGB 색상 (투명색 초기화 - Color.Transparent)

            var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

            try
            {
                Log.Information(Logger.GetMethodPath(currentMethod) + "흑백 전환 + 이진화 처리 작업 시작");

                // 1. for문 돌면서 이미지 흑백 전환 처리 
                // x - 흑백 전환할 비트맵 객체 pConvertBmp의 너비(Width)
                // y - 흑백 전환할 비트맵 객체 pConvertBmp의 높이(Height)
                // for(int x = 0; x < pConvertBmp.Width; x++)
                for (int x = 0; x < pConvertBmp.Size.Width; x++)
                {
                    // for(int y = 0; y < pConvertBmp.Height; y++)
                    for (int y = 0; y < pConvertBmp.Size.Height; y++)
                    {
                        // 2. 흑백 전환 + 이진화 처리할 비트맵 객체 pConvertBmp에 존재하는 특정 픽셀의 기존 RGB 색상을 가져오기
                        orgColor = pConvertBmp.GetPixel(x, y);
                        average = (orgColor.R + orgColor.G + orgColor.B) / 3;   // orgColor의 RGB 색상값 평균 구하기 

                        // 3. 흑백 전환 + 이진화 처리 RGB 색상 가져오기
                        // binaryColor = (average <= RevitBoxHelper.ThresholdValue) ? Color.Black : Color.White;
                        binaryColor = (average < HTSHelper.ThresholdValue) ? Color.Black : Color.White;   // 삼항 연산자 적용하여 특정 픽셀의 이진화 처리 RGB 색상 구하기

                        // 4. 흑백 전환 + 이진화 처리할 비트맵 객체 pConvertBmp의 특정 픽셀의 RGB 색상을 이진화(binaryColor) 설정 
                        pConvertBmp.SetPixel(x, y, binaryColor);

                        // TODO : 아래 주석친 코드 필요시 참고 (2024.07.08 jbh)
                        // 흑백 전환 RGB 색상 가져오기
                        // 메서드 FromArgb 호출해서 RGB 색상을 흑백으로 바꾸는 수식 적용
                        // blackColor = Color.FromArgb(average, average, average);

                        // 흑백 전환할 비트맵 객체 pConvertBmp의 특정 픽셀의 RGB 색상을 흑백(blackColor)으로 설정 
                        // pConvertBmp.SetPixel(x, y, blackColor);
                    }
                }

                Log.Information(Logger.GetMethodPath(currentMethod) + "흑백 전환 + 이진화 처리 작업 종료");
            }
            catch (Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                throw;   // 오류 발생시 상위 호출자 예외처리 전달 throw
            }
            return;
        }

        #endregion BlackConvert

        #region CropImage

        // TODO : static 클래스 ImageExtension에 속하는 메서드 "Crop" 호출
        //        -> Bitmap 클래스 메서드 "Clone" 호출시 오류 메시지 "Out of memory." 출력
        //        해당 오류 해결하고자 메서드 "CropImage" 새로 구현 (2024.06.18 jbh)
        // 오류 메시지 "Out of memory." 발생한 원인
        // - 사용자가 마우스로 드래드앤 드롭해서 이미지를 자르고 복제 하려는 범위(selection)가 비트맵 범위를 벗어나서 발생한 오류로 확인

        // 참고 URL - https://learn.microsoft.com/ko-kr/dotnet/api/system.drawing.bitmap.clone?view=net-8.0&viewFallbackFrom=dotnet-plat-ext-8.0
        // 참고 2 URL - https://stackoverflow.com/questions/199468/c-sharp-image-clone-out-of-memory-exception
        // 참고 3 URL - https://www.codingdefined.com/2015/04/solved-bitmapclone-out-of-memory.html

        /// <summary>
        /// 이미지를 사용자가 마우스로 드래그앤 드롭한 사이즈 만큼 이미지 자르기 
        /// </summary>
        //public static Image CropImage(this Image image, Rectangle RectCropArea)
        //public static Image CropImage(this PictureBox pictureBox, Rectangle RectCropArea)
        public static Image CropImage(this PictureBox pictureBox, Rectangle pRectCropArea)
        {
            var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

            try
            {
                //Prepare a new Bitmap on which the cropped image will be drawn
                Bitmap sourceBmp = new Bitmap(pictureBox.Image, pictureBox.Width, pictureBox.Height);

                // Scale:
                double scaleY = (double)sourceBmp.Width / pictureBox.Width;
                double scaleX = (double)sourceBmp.Height / pictureBox.Height;
                double scale = scaleY < scaleX ? scaleX : scaleY;


                // Crop the image:
                //Bitmap cropBmp = new Bitmap(pRectCropArea.Width, pRectCropArea.Height);
                // Create new CropBitmap:
                // Bitmap cropBitmap = new Bitmap((int)((double)sourceBmp.Width / scale),
                //                                (int)((double)sourceBmp.Height / scale));

                // 사용자가 자르고 싶어하는 영역(직사각형) 만큼의 크기로 Bitmap 클래스 객체 cropBitmap 생성 
                Bitmap cropBitmap = new Bitmap((int)((double)pRectCropArea.Width / scale),
                                               (int)((double)pRectCropArea.Height / scale));

                // Set resolution of the new CropBitmap image:
                cropBitmap.SetResolution(
                           sourceBmp.HorizontalResolution,
                           sourceBmp.VerticalResolution);

                using (Graphics gph = Graphics.FromImage(cropBitmap))
                //using(Graphics gph = pictureBox.CreateGraphics())
                {
                    // TODO : 자른 이미지를 고해상도로 그리기 위해 InterpolationMode 열거형 구조체 멤버 HighQualityBicubic 사용 (2024.06.27 jbh)
                    // 참고 URL - https://learn.microsoft.com/ko-kr/dotnet/api/system.drawing.drawing2d.interpolationmode?view=net-8.0&viewFallbackFrom=dotnet-plat-ext-8.0
                    gph.InterpolationMode = InterpolationMode.HighQualityBicubic;

                    // Draw the image on the Graphics object with the new dimesions
                    gph.DrawImage(sourceBmp, new Rectangle(0, 0, pRectCropArea.Width, pRectCropArea.Height), pRectCropArea, GraphicsUnit.Pixel);
                }   // 여기서 Graphics gph - Dispose (리소스 해제) 처리 

                // Release the resources:
                sourceBmp.Dispose();
                // pictureBox.Image.Dispose();

                return cropBitmap;
            }
            catch (Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                TaskDialog.Show(HTSHelper.ErrorTitle, ex.Message);
                // return image as Bitmap;
                return pictureBox.Image as Bitmap;
            }
        }

        #endregion CropImage

        #region Sample

        // TODO : 이미지 흑백 전환 처리 메서드 "BlackConvert" 필요시 참고 (2024.07.08 jbh)

        //#region BlackConvert

        //// TODO : 이미지 흑백 전환 기능 구현 (2024.07.04 jbh)
        //// MS 공식 문서
        //// GetPixel 참고 URL - http://msdn.microsoft.com/ko-kr/library/system.drawing.bitmap.getpixel(v=vs.110).aspx 
        //// SetPixel 참고 URL - http://msdn.microsoft.com/ko-kr/library/system.drawing.bitmap.setpixel(v=vs.110).aspx 
        //// FromArgb 참고 URL - http://msdn.microsoft.com/en-us/library/cce5h557(v=vs.110).aspx 

        //// 블로그 문서 
        //// 참고 URL - https://tctt.tistory.com/129
        //// 참고 2 URL - https://blog.naver.com/nersion/140141133683
        //// 참고 3 URL - https://blog.naver.com/PostView.naver?blogId=kgg1959&logNo=30182499708
        //// 참고 4 URL - https://son10001.blogspot.com/2014/04/blog-post_14.html

        //// ChatGPT 문서
        //// 참고 URL - https://chatgpt.com/c/772919c6-4936-4b8d-b796-0a4b4d02e6ef

        ///// <summary>
        ///// 이미지 흑백 전환 처리
        ///// </summary>
        ///// <param name="parameter">pBlackConvertBmp - 흑백 전환 처리할 비트맵 객체</param>
        //public static void BlackConvert(ref Bitmap pBlackConvertBmp)
        //{
        //    int average = 0;                       // RGB 색상값 평균
        //    Color orgColor = Color.Transparent;    // 특정 픽셀의 기존 RGB 색상 (투명 초기화)
        //    Color blackColor = Color.Transparent;     // 특정 픽셀의 흑백 전환 RGB 색상 (투명색 초기화 - Color.Transparent)

        //    var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

        //    try
        //    {
        //        Logger.Information(currentMethod, "흑백 전환 작업 시작");

        //        // 1. for문 돌면서 이미지 흑백 전환 처리 
        //        // x - 흑백 전환할 비트맵 객체 pBlackConvertBmp의 너비(Width)
        //        // y - 흑백 전환할 비트맵 객체 pBlackConvertBmp의 높이(Height)
        //        // for(int x = 0; x < pBlackConvertBmp.Width; x++)
        //        for (int x = 0; x < pBlackConvertBmp.Size.Width; x++)
        //        {
        //            // for(int y = 0; y < pBlackConvertBmp.Height; y++)
        //            for (int y = 0; y < pBlackConvertBmp.Size.Height; y++)
        //            {
        //                // 2. 흑백 전환할 비트맵 객체 pBlackConvertBmp에 존재하는 특정 픽셀의 기존 RGB 색상을 가져오기
        //                orgColor = pBlackConvertBmp.GetPixel(x, y);
        //                average = (orgColor.R + orgColor.G + orgColor.B) / 3;   // orgColor의 RGB 색상값 평균 구하기 

        //                // 3. 흑백 전환 RGB 색상 가져오기
        //                // 메서드 FromArgb 호출해서 RGB 색상을 흑백으로 바꾸는 수식 적용
        //                blackColor = Color.FromArgb(average, average, average);

        //                // 4. 흑백 전환할 비트맵 객체 pBlackConvertBmp의 특정 픽셀의 RGB 색상을 흑백(blackColor)으로 설정 
        //                pBlackConvertBmp.SetPixel(x, y, blackColor);


        //                // 2. 이진화 처리할 비트맵 객체 pBinaryConvertBmp에 존재하는 특정 픽셀의 기존 RGB 색상을 가져오기
        //                blackColor = pBinaryConvertBmp.GetPixel(x, y);
        //                average = (blackColor.R + blackColor.G + blackColor.B) / 3;   // blackColor의 RGB 색상값 평균 구하기 

        //                // 3. 이진화 처리 RGB 색상 가져오기
        //                // binaryColor = (average <= RevitBoxHelper.ThresholdValue) ? Color.Black : Color.White;
        //                binaryColor = (average < RevitBoxHelper.ThresholdValue) ? Color.Black : Color.White;   // 삼항 연산자 적용하여 특정 픽셀의 이진화 처리 RGB 색상 구하기

        //                // 4. 이진화 처리할 비트맵 객체 pBinaryConvertBmp의 특정 픽셀의 RGB 색상을 이진화(binaryColor) 설정 
        //                pBinaryConvertBmp.SetPixel(x, y, binaryColor);
        //            }
        //        }

        //        Logger.Information(currentMethod, "흑백 전환 작업 종료");
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Error(currentMethod, Logger.errorMessage + ex.Message);
        //        throw;   // 오류 발생시 상위 호출자 예외처리 전달 throw
        //    }
        //    return;
        //}

        //#endregion BlackConvert


        // TODO : 이진화 처리 메서드 "BinaryConvert" 필요시 참고 (2024.07.08 jbh)
        //#region BinaryConvert

        //// Tip : 이진화 처리 메서드 "BinaryConvert" 호출 하기 전, 
        ////       이미지 흑백 전환 처리 메서드 "BlackConvert" 호출 먼저 해야 원하는 데이터를 얻을 확률이 높다.
        //// 참고 URL - https://tctt.tistory.com/129 

        //// TODO : 이진화 처리 메서드 "BinaryConvert" 구현 (2024.07.05 jbh)
        //// 참고 URL - https://tctt.tistory.com/129
        //// 참고 2 URL - https://holdiron.tistory.com/9
        //// 참고 3 URL - https://chatgpt.com/c/6cae8b63-d072-4f41-bc24-e509b3a95513

        ///// <summary>
        ///// 이미지 흑백 전환 처리 후 이진화 처리 
        ///// </summary>
        ///// <param name="parameter">pBinaryConvertBmp - 흑백 전환 처리 완료 및 이진화 처리할 비트맵 객체</param>
        //public static void BinaryConvert(ref Bitmap pBinaryConvertBmp)
        //{
        //    int average = 0;                          // RGB 색상값 평균
        //    Color blackColor = Color.Transparent;     // 특정 픽셀의 흑백 전환 RGB 색상 (투명색 초기화 - Color.Transparent)
        //    Color binaryColor = Color.Transparent;    // 특정 픽셀의 이진화 처리 RGB 색상 (투명색 초기화 - Color.Transparent)

        //    var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

        //    try
        //    {
        //        Logger.Information(currentMethod, "이진화 처리 작업 시작");

        //        // 1. for문 돌면서 이미지 이진화 처리
        //        // x - 이진화 처리할 비트맵 객체 pBinaryConvertBmp의 너비(Width)
        //        // y - 이진화 처리할 비트맵 객체 pBinaryConvertBmp의 높이(Height)
        //        for (int x = 0; x < pBinaryConvertBmp.Size.Width; x++)
        //        {
        //            for (int y = 0; y < pBinaryConvertBmp.Size.Height; y++)
        //            {
        //                // 2. 이진화 처리할 비트맵 객체 pBinaryConvertBmp에 존재하는 특정 픽셀의 기존 RGB 색상을 가져오기
        //                blackColor = pBinaryConvertBmp.GetPixel(x, y);
        //                average = (blackColor.R + blackColor.G + blackColor.B) / 3;   // blackColor의 RGB 색상값 평균 구하기 

        //                // 3. 이진화 처리 RGB 색상 가져오기
        //                // binaryColor = (average <= RevitBoxHelper.ThresholdValue) ? Color.Black : Color.White;
        //                binaryColor = (average < RevitBoxHelper.ThresholdValue) ? Color.Black : Color.White;   // 삼항 연산자 적용하여 특정 픽셀의 이진화 처리 RGB 색상 구하기

        //                // 4. 이진화 처리할 비트맵 객체 pBinaryConvertBmp의 특정 픽셀의 RGB 색상을 이진화(binaryColor) 설정 
        //                pBinaryConvertBmp.SetPixel(x, y, binaryColor);
        //            }
        //        }

        //        Logger.Information(currentMethod, "이진화 처리 작업 종료");
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Error(currentMethod, Logger.errorMessage + ex.Message);
        //        throw;   // 오류 발생시 상위 호출자 예외처리 전달 throw
        //    }
        //    return;
        //}

        //#endregion BinaryConvert

        #region Crop

        // TODO : 아래 메서드 "Crop"은 호출시 오류 메시지 "Out of memory."가 출력되어 이미지 자르기 기능이 실행이 안 되어서 주석 처리 진행 (2024.06.18 jbh)
        /// <summary>
        /// Crops an image according to a selection rectangle
        /// </summary>
        /// <param name="image">
        /// the image to be cropped
        /// </param>
        /// <param name="selection">
        /// the selection
        /// </param>
        /// <returns>
        /// cropped image
        /// </returns>
        //public static Image Crop(this Image image, Rectangle selection)
        //{
        //    var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

        //    try
        //    {
        //        Bitmap bmp = image as Bitmap;

        //        // Check if it is a bitmap:
        //        if (bmp == null)
        //            throw new ArgumentException("Kein gültiges Bild (Bitmap)");

        //        // Bitmap 클래스 메서드 "Clone" 호출시 오류 메시지 "Out of memory." 출력
        //        // Crop the image:
        //        Bitmap cropBmp = bmp.Clone(selection, bmp.PixelFormat);

        //        // Release the resources:
        //        image.Dispose();

        //        return cropBmp;
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
        //        TaskDialog.Show(HTSHelper.ErrorTitle, ex.Message);
        //        return image as Bitmap;
        //    }
        //}

        #endregion Crop

        #endregion Sample
    }
}
