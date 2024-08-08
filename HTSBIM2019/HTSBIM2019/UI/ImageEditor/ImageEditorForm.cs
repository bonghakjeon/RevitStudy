using Serilog;

using System;
using System.IO;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

using Color = System.Drawing.Color;
using Rectangle = System.Drawing.Rectangle;

using Autodesk.Revit.DB;
using TaskDialog = Autodesk.Revit.UI.TaskDialog;


using Autodesk.Revit.UI;
//using DevExpress.Map.Dashboard;

using HTSBIM2019.Common.LogBase;
using HTSBIM2019.Common.HTSBase;
using HTSBIM2019.Common.StrBase;
using HTSBIM2019.Common.RequestBase;
using HTSBIM2019.Common.Managers;

using DevExpress.XtraEditors;

namespace HTSBIM2019.UI.ImageEditor
{
    public partial class ImageEditorForm : XtraForm
    {
        #region 프로퍼티

        #region EnumSelectFile

        /// <summary>
        /// 파일 선택 - 파일 선택 여부
        /// </summary>
        private enum EnumSelectFile : int
        {
            [Description("파일 선택 안함.")]
            NONE = 0,
            [Description("파일 선택함.")]
            SELECT = 1,
        }

        #endregion EnumSelectFile

        #region EnumImageType

        /// <summary>
        /// 유효성 검사 - 이미지 타입 
        /// </summary>
        private enum EnumImageType : int
        {
            [Description("원본 이미지")]
            ORIGINAL = 0,
            [Description("편집 이미지")]
            EDIT = 1
        }

        #endregion EnumImageType

        #region EnumSizeChangeType

        /// <summary>
        /// PictureBox 컨트롤 - 이미지 Width/Height - 증가/감소/유지 여부 
        /// </summary>
        private enum EnumSizeChangeType : int
        {
            [Description("Width/Height 감소")]
            DECREMENT = -1,
            [Description("Width/Height 유지")]
            MAINTENANCE = 0,
            [Description("Width/Height 증가")]
            INCREMENT = 1,
        }

        #endregion EnumSizeChangeType

        #region EnumReSizeType

        /// <summary>
        /// PictureBox 컨트롤 - 이미지 Width/Height 변경 가능 여부 
        /// </summary>
        private enum EnumReSizeType : int
        {
            [Description("Width/Height 변경 불가")]
            IMPOSSIBLE = 0,
            [Description("Width/Height 변경 가능")]
            POSSIBLE = 1
        }

        #endregion EnumReSizeType

        /// <summary>
        /// Modaless 폼(.Show()) 형식에 의해 발생하는 외부 요청 핸들러 프로퍼티 
        /// </summary>
        private ImageEditorRequestHandler RequestHandler { get; set; }

        /// <summary>
        /// 외부 이벤트 프로퍼티
        /// </summary>
        private ExternalEvent ExEvent { get; set; }

        // TODO : 프로퍼티 "UIDoc" 필요시 사용 예정 (2024.06.24 jbh)
        /// <summary>
        /// Revit 사용자가 직접 열은 Revit 프로젝트 문서
        /// 참고 URL - https://www.revitapidocs.com/2018/295b48c8-0571-ad5c-eead-baea84a6787c.htm
        /// </summary>
        // private UIDocument UIDoc { get; set; }

        // TODO : 필요시 프로퍼티 "RevitDoc" 사용 예정 (2024.07.22 jbh)
        /// <summary>
        /// Revit 문서 
        /// </summary>
        // private Document RevitDoc { get; set; }

        /// <summary>
        /// 이미지 자를 영역(직사각형)
        /// </summary>
        //private Rectangle RectCropArea { get; set; } = new Rectangle();
        private Rectangle RectCropArea { get; set; } = Rectangle.Empty;

        #region 메시지

        /// <summary>
        /// 알림 메시지(이미지 파일 경로) 출력 
        /// </summary>
        private TaskDialog TaskNoticeDialog { get; set; }

        /// <summary>
        /// 오류 메시지(유효성 검사 - 이미지 존재 여부) 출력
        /// </summary>
        private TaskDialog TaskErrorDialog { get; set; }

        #endregion 메시지

        #region 이미지

        /// <summary>
        /// 원본 이미지 
        /// </summary>
        private Image OrgImage { get; set; }

        /// <summary>
        /// 편집 이미지(이미지 자르기)
        /// </summary>
        private Image EditImage { get; set; }

        /// <summary>
        /// 흑백 전환 처리된 비트맵 이미지 객체 
        /// </summary>
        private Bitmap BlackConvertBmp { get; set; }

        // TODO : 프로퍼티 "CropImage" 필요시 사용 예정 (2024.07.03 jbh)
        /// <summary>
        /// 자른 이미지 
        /// </summary>
        // private Image CropImage { get; set; }

        /// <summary>
        /// 원본 이미지 Width, Height 비율
        /// </summary>
        private ImageRatio OrgImageRatio { get; set; }

        #endregion 이미지 

        #region 원본 이미지 pictureBox(pictureBoxOrgImage)

        /// <summary>
        /// 마우스로 이미지 자르기 실행 여부(드래그앤드롭) 
        /// </summary>
        private bool IsDragging { get; set; }


        /// <summary>
        /// MouseDown - X 좌표 
        /// </summary>
        private int DownX { get; set; } = 0;

        /// <summary>
        /// MouseDown - Y 좌표 
        /// </summary>
        private int DownY { get; set; } = 0;

        /// <summary>
        /// MouseMove - X 좌표 
        /// </summary>
        private int MoveX { get; set; } = 0;

        /// <summary>
        /// MouseMove - Y 좌표 
        /// </summary>
        private int MoveY { get; set; } = 0;

        /// <summary>
        /// MouseUp - X 좌표 
        /// </summary>
        // private int UpX { get; set; } = 0;

        /// <summary>
        /// MouseUp - Y 좌표 
        /// </summary>
        // private int UpY { get; set; } = 0;

        #endregion 원본 이미지 pictureBox(pictureBoxOrgImage) 

        #region 파일 선택

        /// <summary>
        /// 파일 선택한 원본 이미지 파일 경로 
        /// </summary>
        public string OrgImageFilePath { get; set; } = string.Empty;

        #endregion 파일 선택

        #region 객체 선택

        // SelectElement

        /// <summary>
        /// 객체 선택 완료 여부 
        /// </summary>
        public bool IsSelectedElement { get; set; }

        /// <summary>
        /// Revit 도면 상에 선택한 래스터 이미지 파일(BuiltInCategory.OST_RasterImages) 경로 
        /// </summary>
        public string SelectedImageFilePath { get; set; } = string.Empty;

        #endregion 객체 선택 

        #region 흑백 전환

        // TODO : 프로퍼티 "BlackConvertBmp" 필요시 사용 예정 (2024.07.04 jbh)
        /// <summary>
        /// 흑백 전환할 비트맵 객체
        /// </summary>
        // public Bitmap BlackConvertBmp { get; set; }

        /// <summary>
        /// 흑백 전환 완료 여부 
        /// </summary>
        public bool IsBlackConverted { get; set; }

        #endregion 흑백 전환

        #region 원본 보기

        #endregion 원본 보기 

        #region 이미지 자르기 

        // TODO : 이미지 자르기 완료 여부 프로퍼티 "IsCroppedImageFile" 필요시 사용 예정 (2024.07.03 jbh)
        /// <summary>
        /// 이미지 자르기 완료 여부 
        /// </summary>
        // public bool IsCroppedImageFile { get; set; }


        // TODO : 프로퍼티 "SaveCroppedImagePath" 필요시 사용 예정 (2024.06.27 jbh)
        /// <summary>
        /// 자른 이미지 파일(BuiltInCategory.OST_RasterImages)이 저장된 경로
        /// </summary>
        // public string SaveCroppedImagePath { get; set; } = string.Empty;


        #endregion 이미지 자르기

        #region 이미지 삽입

        /// <summary>
        /// 이미지 삽입 완료 여부 
        /// </summary>
        public bool IsInsertedImageFile { get; set; }

        /// <summary>
        /// Revit 도면상에 이미지 삽입 처리 하고자 하는 이미지 파일(BuiltInCategory.OST_RasterImages) 경로
        /// </summary>
        public string InsertedImageFilePath { get; set; } = string.Empty;

        #endregion 이미지 삽입

        #region 취소

        #endregion 취소 

        #endregion 프로퍼티

        #region 생성자

        //public ImageEditorForm(UIApplication rvUIApp)
        //public ImageEditorForm(ImageEditorRequestHandler pHandler, ExternalEvent rvExEvent, UIApplication rvUIApp)
        //public ImageEditorForm(ImageEditorRequestHandler pHandler, ExternalEvent rvExEvent)
        public ImageEditorForm(ExternalEvent rvExEvent, ImageEditorRequestHandler pHandler)
        {
            InitializeComponent();
            //InitSetting(pHandler, rvExEvent, rvUIApp);   // 이미지 삽입 초기 셋팅 
            InitSetting(pHandler, rvExEvent);   // 이미지 삽입 초기 셋팅 
        }

        #endregion 생성자 

        #region InitSetting

        /// <summary>
        /// 이미지 삽입 초기 셋팅
        /// </summary>
        //private void InitSetting(ImageEditorRequestHandler pHandler, ExternalEvent rvExEvent, UIApplication rvUIApp)
        private void InitSetting(ImageEditorRequestHandler pHandler, ExternalEvent rvExEvent)
        {
            var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

            try
            {
                Log.Information(Logger.GetMethodPath(currentMethod) + "이미지 삽입 초기 셋팅 시작");


                // 1. 외부 이벤트 프로퍼티 할당 
                ExEvent = rvExEvent;

                // 2. 외부 요청 핸들러 프로퍼티 할당 
                RequestHandler = pHandler;


                // 3. Revit 사용자가 직접 열은 Revit 프로젝트 문서 프로퍼티 "UIDoc" 할당
                // UIDoc = rvUIApp.ActiveUIDocument;

                // TODO : 필요시 프로퍼티 "RevitDoc" 사용 예정 (2024.07.22 jbh)
                // 4. Revit 문서 프로퍼티 "RevitDoc" 할당
                // RevitDoc = rvUIApp.ActiveUIDocument.Document;    // 활성화된 Revit 문서

                // TODO : 필요시 프로퍼티 IsDragging 초기화 코드 사용 (2024.07.05 jbh)
                // 5. 마우스로 이미지 자르기 실행 여부 초기화
                // IsDragging = false;

                // TODO : 필요시 프로퍼티 "TaskNoticeDialog" 사용 예정 (2024.07.10 jbh)
                // 6. 이미지 파일 경로 알림 메시지 출력 초기화
                // TaskNoticeDialog = new TaskDialog(HTSHelper.NoticeTitle);

                // 7. 유효성 검사 오류 메시지 출력 초기화
                TaskErrorDialog = new TaskDialog(HTSHelper.ErrorTitle);

                DisplaySetting(HTSHelper.TypeOfInitSetting);

                Log.Information(Logger.GetMethodPath(currentMethod) + "이미지 삽입 초기 셋팅 완료");
            }
            catch (Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                TaskDialog.Show(HTSHelper.ErrorTitle, ex.Message);
            }
            finally
            {
                // TODO : finally문 안에 코드 필요시 구현 예정 (2024.06.24 jbh)
            }
        }

        #endregion InitSetting

        #region ImageForm_FormClosed

        /// <summary>
        /// 이미지 삽입 폼 종료 이벤트 
        /// </summary>
        private void ImageForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

            try
            {
                Log.Information(Logger.GetMethodPath(currentMethod) + "이미지 삽입 화면 FormClosed 이벤트 시작");

                // 이미지 삽입 Modaless 폼(ImageForm) 화면 닫기 전에 외부 이벤트 프로퍼티 "ExEvent" 리소스 해제 
                ExEvent.Dispose();
                ExEvent = null;             // 외부 이벤트 null로 초기화
                RequestHandler = null;      // 외부 요청 핸들러 null로 초기화

                // base.OnFormClosed(e);    // 폼화면 닫기 (해당 메서드 base.OnFormClosed(e); 호출시 이벤트 메서드 ImageForm_FormClosed 두번 실행되서 오류 발생하므로 주석처리 진행)

                Log.Information(Logger.GetMethodPath(currentMethod) + "이미지 삽입 화면 FormClosed 이벤트 종료");
            }
            catch (Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                TaskDialog.Show(HTSHelper.ErrorTitle, ex.Message);
            }
            finally
            {
                // TODO : finally문 안에 코드 필요시 구현 예정 (2024.06.24 jbh)
            }
        }

        #endregion ImageForm_FormClosed

        #region MakeRequest

        /// <summary>
        /// 요청(Request) 생성
        /// </summary>
        private void MakeRequest(ImageEditorRequestId pRequest)
        {
            RequestHandler.Request.Make(pRequest);
            ExEvent.Raise();

            // 주의사항 - 이미지 삽입 폼 객체(ImageForm)에 속한 모든 컨트롤 비활성화 메서드 DozeOff 호출시
            // 화면의 버튼 기능("파일 선택", "객체 선택", "흑백 전환" 등등...)을 사용할 수 없으므로 해당 메서드는 호출 안 함.
            // DozeOff(); 
        }

        #endregion MakeRequest

        #region DozeOff

        /// <summary>
        /// 이미지 삽입 폼 객체(ImageForm)에 속한 모든 컨트롤 비활성화
        /// </summary>
        private void DozeOff()
        {
            EnableCommands(false);
        }

        #endregion DozeOff

        #region WakeUp

        /// <summary>
        /// 이미지 삽입 폼 객체(ImageForm)에 속한 모든 컨트롤 활성화
        /// </summary>
        public void WakeUp()
        {
            var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

            try
            {
                Log.Information(Logger.GetMethodPath(currentMethod) + "이미지 삽입 화면 메서드 WakeUp 시작");

                EnableCommands(true);

                // "객체 선택" 완료된 경우 
                // if(true == IsSelectedElement && false == string.IsNullOrWhiteSpace(SelectedImageFilePath)) DisplaySetting(HTSHelper.TypeOfSelectElement, SelectedImageFilePath);
                if (true == IsSelectedElement)
                {
                    DisplaySetting(HTSHelper.TypeOfSelectElement, SelectedImageFilePath);
                    this.Activate();   // 폼 화면(ImageForm.cs) 다시 활성화
                }

                // "이미지 삽입" 완료된 경우 
                // if (true == IsInsertedImageFile && false == string.IsNullOrWhiteSpace(InsertedImageFilePath)) DisplaySetting(HTSHelper.TypeOfInsertImage, InsertedImageFilePath);
                if (true == IsInsertedImageFile)
                {
                    DisplaySetting(HTSHelper.TypeOfInsertImage, InsertedImageFilePath);
                    this.Dispose();   // 폼(ImageForm.cs) 객체 모든 리소스 해제 
                }

                Log.Information(Logger.GetMethodPath(currentMethod) + "이미지 삽입 화면 메서드 WakeUp 종료");
            }
            catch (Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                TaskDialog.Show(HTSHelper.ErrorTitle, ex.Message);
            }
            // TODO : 필요시 finally문 구현 예정 (2024.07.12 jbh) 
            // finally
            // {
            //     // TODO : 메서드 "this.Activate();" 호출해서 메시지 박스 종료(TaskDialog.Show)후
            //     //        부모 폼(ImageForm.cs) 다시 활성화 구현 (2024.06.27 jbh)
            //     // 참고 URL - https://chatgpt.com/c/1986f7ac-47e3-45d8-a983-c27adc21b4cb
            //     this.Activate();   // 메시지 박스 종료(TaskDialog.Show)후 부모 폼(ImageForm.cs) 다시 활성화
            // }
        }

        #endregion WakeUp

        #region EnableCommands

        /// <summary>
        /// 폼 객체에 속한 모든 컨트롤 활성화 / 비활성화
        /// </summary>
        private void EnableCommands(bool pStatus)
        {
            foreach (System.Windows.Forms.Control ctrl in this.Controls)
            {
                ctrl.Enabled = pStatus;
            }

            // TODO : 아래 if절 로직 필요시 사용 예정 (2024.06.24 jbh)
            // if(false == pStatus)
            // {
            //     this.btnON.Enabled  = true;
            //     this.btnOFF.Enabled = true;
            // }
        }

        #endregion EnableCommands


        #region DisplaySetting

        /// <summary>
        /// pictureBox 컨트롤 원본 이미지(pictureBoxOrgImage) 또는 편집 이미지(pictureBoxEditImage) 출력 셋팅
        /// Default 파라미터 - pImageFilePath
        /// </summary>
        //private void DisplaySetting(string pDisplayType, string pImageFilePath)
        private void DisplaySetting(string pDisplayType, string pImageFilePath = null)
        {
            string orgFileName = string.Empty;                   // 원본 이미지 파일 이름(파일 확장자 포함) 
            string imageFileName = string.Empty;                 // 메세지에 출력할 이미지 파일 이름 

            int resizedOrgWidth = 0;                             // 사이즈 변경후 원본 이미지에 출력될  pictureBoxOrgImage의 예측 너비 
            int resizedOrgHeight = 0;                            // 사이즈 변경후 원본 이미지에 출력될  pictureBoxOrgImage의 예측 높이 

            Bitmap resizedOrgBmp = null;                         // 사이즈 변경후 원본 이미지 비트맵 객체 초기화

            Bitmap convertBmp = null;                            // 이미지 흑백 전환 처리할 원본 이미지(OrgImage) 객체 초기화
            var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

            try
            {
                // 기능 - "파일 선택", "객체 선택" 제외한 나머지 기능 실행시
                // 원본 이미지 파일이 존재하지 않는 경우 
                if(false == pDisplayType.Equals(HTSHelper.TypeOfInitSetting)
                   && false == pDisplayType.Equals(HTSHelper.TypeOfSelectFile)
                   && false == pDisplayType.Equals(HTSHelper.TypeOfSelectElement)
                   && false == File.Exists(OrgImageFilePath))
                {
                    // TODO : 아래 주석친 코드 필요시 사용 예정 (2024.07.10 jbh)
                    // OrgImage = null;   // 유효성 검사할 때 사용할 원본 이미지 초기화 
                    orgFileName = Path.GetFileName(OrgImageFilePath);
                    TaskErrorDialog.MainInstruction = $"[{EnumImageType.ORIGINAL.ToDescription()}] - {orgFileName}\r\n파일 존재 안 함!\r\n다시 확인 바랍니다.";

                    // TODO : 아래 주석친 코드 필요시 참고 (2024.07.10 jbh)
                    // TaskErrorDialog.MainContent = $"* 파일 경로 *\r\n{OrgImageFilePath}";
                    TaskErrorDialog.Show();

                    Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + $"오류 - [{EnumImageType.ORIGINAL.ToDescription()}] - {orgFileName} 파일 존재 안 함! 다시 확인 바랍니다.\r\n파일 경로 - {OrgImageFilePath}");

                    return;   // 이벤트 메서드 "btnInsertImage_Click" 종료 
                }

                // 기능 "화면 초기 셋팅"이 아니고 "파일 선택", "객체 선택", "흑백 전환", "원본 보기" 중 하나인 경우 
                if(false == pDisplayType.Equals(HTSHelper.TypeOfInitSetting)
                   && (true == pDisplayType.Equals(HTSHelper.TypeOfSelectFile)
                       || true == pDisplayType.Equals(HTSHelper.TypeOfSelectElement)
                       || true == pDisplayType.Equals(HTSHelper.TypeOfOrgImage)
                       || true == pDisplayType.Equals(HTSHelper.TypeOfBlackConvert)))
                {
                    // TODO : 파일 경로 "pLoadImageFilePath"에 존재하는 이미지 불러올 때, Lock이 걸리지 않고 이미지 만들기 구현 (2024.06.24 jbh)
                    // 참고 URL - https://chunter.tistory.com/555
                    using(FileStream fileStream = new FileStream(pImageFilePath, FileMode.Open, FileAccess.Read))
                    {
                        OrgImage = Image.FromStream(fileStream);
                        OrgImageRatio = ImageManager.GetImageRatio(OrgImage);

                        // 기능 "객체 선택"이 아니고 "파일 선택", "흑백 전환", "원본 보기" 중 하나인 경우 
                        if(false == pDisplayType.Equals(HTSHelper.TypeOfSelectElement))
                        {
                            //resizedOrgWidth = (splitContainerImage.Width / HTSHelper.Hundreds) * HTSHelper.Hundreds;
                            //resizedOrgHeight = (splitContainerImage.Height / HTSHelper.Hundreds) * HTSHelper.Hundreds;

                            // 원본 이미지 비율 프로퍼티 "OrgImageRatio" 사용해서 splitContainerImage에 맞게 사이즈 조정하기 
                            resizedOrgWidth = (splitContainerImage.Width / OrgImageRatio.WidthRatio) * OrgImageRatio.WidthRatio;
                            resizedOrgHeight = (splitContainerImage.Height / OrgImageRatio.HeightRatio) * OrgImageRatio.HeightRatio;

                            resizedOrgBmp = new Bitmap(OrgImage, resizedOrgWidth, resizedOrgHeight);

                            resizedOrgBmp.SetResolution(OrgImage.HorizontalResolution,
                                                        OrgImage.VerticalResolution);
                        }
                    }   // 여기서 Dispose (리소스 해제) 처리 

                    // 편집 이미지 속성 "Image", "EditImage" 데이터 존재시 null 초기화 처리
                    //if(EditImage is not null || pictureBoxEditImage.Image is not null)
                    if(true == IsValidator(EnumImageType.EDIT) || pictureBoxEditImage.Image is not null)
                    {
                        EditImage = null;   // 유효성 검사할 때 사용할 편집 이미지 초기화 
                        pictureBoxEditImage.Image = null;

                        groupBoxEditImage.Visible = false;

                        // TODO : splitContainerImage 컨트롤에서 원본 이미지 picturBoxOrgImage가 출력되도록 
                        //        속성 "PanelVisibility" 사용해서 해당 속성에 값 "SplitPanelVisibility.Panel1" 할당 (2024.07.11 jbh)
                        // 참고 URL - https://stackoverflow.com/questions/645518/how-can-i-hide-a-panel-that-is-on-a-splitcontainer
                        splitContainerImage.PanelVisibility = SplitPanelVisibility.Panel1;
                    }
                }



                Log.Information(Logger.GetMethodPath(currentMethod) + "화면 셋팅 시작");

                pictureBoxOrgImage.Refresh();
                pictureBoxEditImage.Refresh();

                imageFileName = Path.GetFileName(pImageFilePath);   // 이미지 파일 이름(확장자 포함) 가져오기

                switch (pDisplayType)
                {
                    // "화면 초기 셋팅"한 경우 
                    case HTSHelper.TypeOfInitSetting:
                        groupBoxEditImage.Visible = false;

                        // TODO : splitContainerImage 컨트롤에서 원본 이미지 picturBoxOrgImage가 출력되도록 
                        //        속성 "PanelVisibility" 사용해서 해당 속성에 값 "SplitPanelVisibility.Panel1" 할당 (2024.07.11 jbh)
                        // 참고 URL - https://stackoverflow.com/questions/645518/how-can-i-hide-a-panel-that-is-on-a-splitcontainer
                        splitContainerImage.PanelVisibility = SplitPanelVisibility.Panel1;
                        break;

                    // "파일 선택", "객체 선택", "원본 보기"한 경우 
                    case HTSHelper.TypeOfSelectFile:
                    case HTSHelper.TypeOfSelectElement:
                    case HTSHelper.TypeOfOrgImage:
                        // TaskNoticeDialog.MainInstruction = $"{imageFileName} 파일 선택 완료!\r\n해당 이미지는 원본 이미지로 출력됩니다.";
                        // TaskNoticeDialog.MainInstruction = $"파일명 : {imageFileName}\r\n해당 이미지는 원본 이미지로 출력됩니다.";

                        // TODO : 필요시 아래 주석친 코드 참고 (2024.07.10 jbh)
                        // TaskNoticeDialog.MainInstruction = $"{pDisplayType} 완료!\r\n파일명 : {imageFileName}\r\n해당 이미지는 원본 이미지로 출력됩니다.";

                        OrgImageFilePath = string.Empty;
                        OrgImageFilePath = pImageFilePath;

                        // 기능 "객체 선택"이 아니고 "파일 선택", "흑백 전환", "원본 보기" 중 하나인 경우 
                        if (false == pDisplayType.Equals(HTSHelper.TypeOfSelectElement)
                            && (OrgImage.Width >= resizedOrgWidth || OrgImage.Height >= resizedOrgHeight))
                            pictureBoxOrgImage.Image = resizedOrgBmp;

                        // 기능 "객체 선택"이거나 원본 이미지 Width, Height가 resizedOrgWidth, resizedOrgHeight 보다 작은 경우
                        else pictureBoxOrgImage.Image = OrgImage;

                        // 흑백 전환 처리 초기화
                        IsBlackConverted = false;
                        BlackConvertBmp = null;

                        // "객체 선택"일 경우
                        if (true == pDisplayType.Equals(HTSHelper.TypeOfSelectElement)) IsSelectedElement = false;   // 객체 선택 초기화

                        // "파일 선택"일 경우 
                        // if(true == pDisplayType.Equals(HTSHelper.TypeOfSelectFile)) 
                        // {
                        //     // 편집 이미지 관련 컨트롤(groupBoxEditImage, panelEditImage, pictureBoxEditImage 등등...) 비활성화 처리 
                        // }

                        break;

                    // TODO : 원본 이미지 pictureBoxOrgImage의 속성 "Size" (Width + Height) 값을 변경하려면 
                    //        pictureBoxOrgImage 속성 "Dock" 값을 아래처럼 DockStyle.None으로 할당해야 함. (2024.07.03 jbh)
                    // pictureBoxOrgImage.Dock = DockStyle.None;

                    // Size orgSize = new Size(OrgImage.Width, OrgImage.Height);
                    // pictureBoxOrgImage.Size = orgSize;

                    // TODO : 원본 이미지 파일 고해상도로 읽어오기 필요시 참고 (2024.07.02 jbh)
                    // 참고 URL - https://s-engineer.tistory.com/m/289?category=1024598
                    // 참고 2 URL - https://chatgpt.com/c/76f6b17d-874a-4ad3-827f-ef03cf0d396d
                    // OrgImage = Image.FromStream(fileStream);
                    // pictureBoxOrgImage.Image = new Bitmap(OrgImage);

                    // pictureBoxOrgImage.Cursor = Cursors.Cross;                      

                    // "흑백 전환"한 경우 
                    case HTSHelper.TypeOfBlackConvert:
                        // TODO : 필요시 아래 주석친 코드 참고 (2024.07.10 jbh)
                        // TaskNoticeDialog.MainInstruction = $"{imageFileName} 흑백 전환 완료!\r\n해당 이미지는 원본 이미지로 출력됩니다.";

                        // 원본 이미지 프로퍼티 "OrgImage" Width, Height 값이 사이즈 조정된 resizedOrgWidth, resizedOrgHeight 보다 크거나 같은 경우 
                        if (OrgImage.Width >= resizedOrgWidth || OrgImage.Height >= resizedOrgHeight) convertBmp = resizedOrgBmp;
                        // 원본 이미지 프로퍼티 "OrgImage" Width, Height 값이 사이즈 조정된 resizedOrgWidth, resizedOrgHeight 보다 작은 경우 
                        else convertBmp = OrgImage as Bitmap;

                        ImageManager.BlackConvert(ref convertBmp);    // 이미지 흑백 전환 처리 시작 

                        BlackConvertBmp = convertBmp;

                        pictureBoxOrgImage.Image = BlackConvertBmp;

                        IsBlackConverted = true;   // 흑백 전환 처리 완료

                        // TODO : 아래 주석친 테스트 코드 필요시 참고 (2024.07.22 jbh)
                        // IsBlackConverted = false;   // 흑백 전환 처리 시작

                        // 이미지 흑백 전환 처리할 원본 이미지(OrgImage) 불러오기
                        //convertBmp = OrgImage as Bitmap;

                        // 이미지 흑백 전환 처리할 사이즈 변경된 원본 이미지(resizedOrgImage) 불러오기
                        // convertBmp = resizedOrgImage;
                        // BlackConvertBmp = resizedOrgBmp;
                        // convertBmp.SetResolution(OrgImage.HorizontalResolution,
                        //                          OrgImage.VerticalResolution);

                        //ImageManager.BlackConvert(ref convertBmp);    // 이미지 흑백 전환 처리 시작 

                        //BlackConvertBmp = convertBmp;
                        // BlackConvertBmp.SetResolution(OrgImage.HorizontalResolution,
                        //                               OrgImage.VerticalResolution);

                        // TODO : 추후 필요시 메서드 "ImageManager.BinaryConvert" 다시 구현 진행 (2024.07.08 jbh)
                        // ImageManager.BinaryConvert(ref convertBmp);   // 이미지 이진화 처리 시작 

                        //pictureBoxOrgImage.Image = convertBmp;


                        break;

                    // "이미지 자르기"한 경우 
                    case HTSHelper.TypeOfCropImage:
                        // TODO : 필요시 아래 주석친 코드 참고 (2024.07.12 jbh)
                        // TaskDialog.Show(HTSHelper.NoticeTitle, "이미지 자르기 완료!\r\n편집 이미지로 출력됩니다.");
                        // TaskNoticeDialog.MainInstruction = $"{imageFileName}\r\n이미지 자르기 완료!\r\n편집 이미지로 출력됩니다.";
                        // TaskDialog.Show(HTSHelper.NoticeTitle, $"{imageFileName}\r\n이미지 자르기 완료!\r\n편집 이미지로 출력됩니다.");

                        groupBoxEditImage.Visible = true;

                        // TODO : splitContainerImage 컨트롤에서 원본/편집 이미지 picturBoxOrgImage, picturBoxEditImage가 출력되도록 
                        //        속성 "PanelVisibility" 사용해서 해당 속성에 값 "SplitPanelVisibility.Both" 할당 (2024.07.11 jbh)
                        // 참고 URL - https://stackoverflow.com/questions/645518/how-can-i-hide-a-panel-that-is-on-a-splitcontainer
                        splitContainerImage.PanelVisibility = SplitPanelVisibility.Both;

                        // TODO : 자른 이미지 pictureBoxEditImage의 속성 "Size" (Width + Height) 값을 변경하려면 
                        //        pictureBoxEditImage 속성 "Dock" 값을 아래처럼 DockStyle.None으로 할당해야 함. (2024.07.03 jbh)
                        // pictureBoxEditImage.Dock = DockStyle.None;
                        // pictureBoxEditImage.Size = new Size(RectCropArea.Width, RectCropArea.Height);
                        Size cropSize = new Size(RectCropArea.Width, RectCropArea.Height);
                        pictureBoxEditImage.Size = cropSize;

                        // 이미지 자르기 실행 및 자른 이미지를 편집 이미지에 할당 
                        EditImage = pictureBoxOrgImage.CropImage(RectCropArea);

                        pictureBoxEditImage.Image = EditImage;

                        // TODO : "이미지 자르기" 완료후 Rectangle 프로퍼티 "RectCropArea" 초기화 구현 (2024.07.04 jbh)
                        // 참고 URL - https://learn.microsoft.com/ko-kr/dotnet/api/system.windows.rect.empty?view=windowsdesktop-8.0
                        RectCropArea = Rectangle.Empty;

                        btnCropImage.Enabled = false;   // 버튼 "이미지 자르기" 비활성화

                        // TODO : 아래 주석친 코드 필요시 참고 (2024.07.04 jbh)
                        // IsCroppedImageFile = true;     // 이미지 자르기 완료
                        // EditImage = Image.FromStream(fileStream);

                        // SaveCroppedImagePath = string.Empty;
                        // SaveCroppedImagePath = pLoadImageFilePath;
                        // pictureBoxEditImage.Image = EditImage;
                        // IsCroppedImageFile = false;   // 이미지 자르기 초기화
                        break;

                    // "이미지 삽입"한 경우 
                    case HTSHelper.TypeOfInsertImage:
                        // TaskNoticeDialog.MainInstruction = $"{imageFileName}\r\n이미지 삽입 완료!\r\nRevit 도면에 삽입된 이미지가 출력됩니다.";
                        TaskDialog.Show(HTSHelper.NoticeTitle, $"{imageFileName}\r\n이미지 삽입 완료!\r\nRevit 도면에 이미지가 출력됩니다.");

                        // TODO : "이미지 삽입" 레이블 "case HTSHelper.TypeOfInsertImage:" 필요시 구현 예정 (2024.06.27 jbh)
                        // TaskDialog.Show(HTSHelper.NoticeTitle, $"이미지 {InsertedImageFileName} 삽입 완료!\r\n삽입된 이미지는 Revit도면에 출력됩니다.\r\n* 파일 경로 *\r\n{pImageFilePath}");
                        IsInsertedImageFile = false;   // 이미지 삽입 초기화
                        break;

                    // TODO : default: 레이블 필요시 구현 예정 (2024.06.27 jbh)
                    default:
                        throw new Exception("화면 셋팅 오류!\r\n관리자에게 문의하세요.");
                        break;

                        // TODO : 필요시 "객체 선택" 레이블 case HTSHelper.TypeOfSelectElement: 구현 예정 (2024.06.27 jbh)
                        // "객체 선택"한 경우 
                        //case HTSHelper.TypeOfSelectElement:
                        //    TaskNoticeDialog.MainInstruction = $"{imageFileName} 객체 선택 완료!\r\n해당 이미지는 원본 이미지로 출력됩니다.";

                        //    // 편집 이미지 속성 "Image" 데이터 존재시 null 초기화 처리
                        //    if(pictureBoxEditImage.Image is not null) pictureBoxEditImage.Image = null;

                        //    OrgImageFilePath = string.Empty;
                        //    OrgImageFilePath = pImageFilePath;

                        //    OrgImage = Image.FromStream(fileStream);

                        //    pictureBoxOrgImage.Image = OrgImage;
                        //    // pictureBoxOrgImage.Cursor = Cursors.Cross;
                        //    IsSelectedElement = false;   // 객체 선택 초기화
                        //    break;
                }

                IsDragging = false;

                Log.Information(Logger.GetMethodPath(currentMethod) + "화면 셋팅 완료");

                // TODO : 아래 주석친 코드 필요시 참고 (2024.07.10 jbh)
                // 프로퍼티 "TaskNoticeDialog" 사용 이미지 파일 경로 메시지 출력  
                // "이미지 자르기"한 경우
                // 편집 이미지가 아직 사용자 PC 로컬에 저장되지 않았으므로 이미지 파일 경로 메시지로 출력 안함.
                // if(pDisplayType.Equals(HTSHelper.TypeOfCropImage)) TaskNoticeDialog.MainContent = string.Empty;

                // 그 외 나머지 경우 
                // 사용자 PC 로컬에 저장된 이미지 파일 경로 메시지로 출력.
                // else TaskNoticeDialog.MainContent = $"* 파일 경로 *\r\n{pImageFilePath}";
                // TaskNoticeDialog.Show();

                // IsDragging = false;

                // TODO : 이미지 파일(OrgImage, EditImage) 사이즈(Width, Height)가
                //        PictureBox 컨트롤 (pictureBoxOrgImage, pictureBoxEditImage) 보다 클 때, 
                //        이미지 파일 전체를 볼 수 있도록 Scroll Bar(AutoScroll) 구현 (2024.07.03 jbh)
                // 참고 URL - https://blog.naver.com/gooodwanny/222274360385
                // 참고 2 URL - https://stackoverflow.com/questions/4710145/how-can-i-get-scrollbars-on-picturebox
                // 1. Panel(panelOrgImage, panelEditImage) 만들기

                // 2. Panel(panelOrgImage, panelEditImage)에 존재하는 속성 "AutoScroll" 값 true 할당 
                //    panelOrgImage.AutoScroll = true;
                //    panelEditImage.AutoScroll = true;

                // 3. panelOrgImage, panelEditImage 위에 각각 PictureBox(pictureBoxOrgImage, pictureBoxEditImage) 올림

                // 4. pictureBoxOrgImage, pictureBoxOrgImage 속성 "Dock" 값을 아래처럼 DockStyle.None으로 할당 
                //    이렇게 해야 5,6번의 SizeMode로 설정한 PictureBoxSizeMode.AutoSize; 가 적용됨.

                // 5. pictureBoxOrgImage.SizeMode = PictureBoxSizeMode.AutoSize;
                // 6. pictureBoxEditImage.SizeMode = PictureBoxSizeMode.AutoSize;

                // TODO : PictureBox 컨트롤 (pictureBoxOrgImage, pictureBoxEditImage) SizeMode 설정 (2024.07.02 jbh)
                // 참고 URL - https://coding-abc.kr/111
                // 참고 2 URL - https://learn.microsoft.com/ko-kr/dotnet/api/system.windows.forms.pictureboxsizemode?view=windowsdesktop-8.0
                // 참고 3 URL - https://learn.microsoft.com/ko-kr/dotnet/api/system.windows.forms.borderstyle?view=windowsdesktop-8.0
                // 참고 4 URL - https://blog.naver.com/coding-abc/222275770439


                // TODO : Bitmap클래스의 FromFile() 메서드 사용해서 사용자가 Revit 도면 상에 선택한
                //        래스터 이미지 파일(BuiltInCategory.OST_RasterImages) 경로에 존재하는 이미지 파일 읽어와서 이미지 만들기 구현 (2024.06.24 jbh)
                // 참고 URL - https://www.csharpstudy.com/WinForms/WinForms-picturebox.aspx
                // 참고 2 URL - https://tyen.tistory.com/74
                // pictureBoxOrgImage.Load(pLoadImageFilePath);

                // OrgImage = Image.FromFile(pLoadImageFilePath);
                // pictureBoxOrgImage.Image = OrgImage;
            }
            catch(Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                throw;   // 오류 발생시 상위 호출자 예외처리 전달 throw
            }
            // TODO : 필요시 finally문 구현 예정 (2024.07.08 jbh) 
            finally
            {
                // this.Activate();   // 메시지 박스 종료(TaskDialog.Show)후 부모 폼(ImageForm.cs) 다시 활성화
            }
        }

        #endregion DisplaySetting

        #region IsValidator

        // TODO : 추후 필요시 유효성 검사 메서드 IsValidator 수정 예정 (2024.07.03 jbh)
        /// <summary>
        /// 유효성 검사 - 원본 이미지(OrgImage), 편집 이미지(EditImage) 존재 여부 확인
        /// </summary>
        //private bool IsValidator(Image pImage, EnumImageType pEnumImageType)
        private bool IsValidator(EnumImageType pEnumImageType)
        {
            var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

            try
            {
                switch (pEnumImageType)
                {
                    case EnumImageType.ORIGINAL:   // 원본 이미지
                        if (OrgImage is null) throw new Exception($"[{EnumImageType.ORIGINAL.ToDescription()}] 가\r\n화면에 출력되지 않았습니다!");
                        break;

                    case EnumImageType.EDIT:       // 편집 이미지 
                        if (EditImage is null) throw new Exception($"[{EnumImageType.EDIT.ToDescription()}] 가\r\n화면에 출력되지 않았습니다!");
                        break;

                    default:
                        throw new Exception("유효성 검사 오류!!\r\n관리자에게 문의 하세요.");
                        // break;
                }
                return true;
            }
            catch (Exception ex)
            {
                // TODO : 필요시 에러 로그 기록 사용 예정 (2024.07.03 jbh)
                // Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                //TaskDialog.Show(HTSHelper.ErrorTitle, ex.Message);
                TaskErrorDialog.MainInstruction = ex.Message;
                return false;
            }
        }

        #endregion IsValidator

        #region btnSelectFile_Click

        /// <summary>
        /// 파일 선택
        /// </summary>
        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

            try
            {
                Log.Information(Logger.GetMethodPath(currentMethod) + "파일 선택 이벤트 시작");

                // TODO : 해당 using 문장을 벗어나면 OpenFileDialog 클래스 객체 openFileDialog Dipose 처리 (리소스 해제 및 자원 반납) 구현 (2024.06.24 jbh)
                // 참고 URL - https://hongjinhyeon.tistory.com/92
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = "이미지 파일|*.gif;*.jpg;*.jpeg;*.bmp;*.wmf;*.png";

                    // TODO : 프로퍼티 "RestoreDirectory" 사용해서 OpenFileDialog 화면 닫기 전에 이전에 선택한 디렉터리로 복원 설정 구현 (2024.06.24 jbh)
                    // 참고 URL - https://learn.microsoft.com/ko-kr/dotnet/api/system.windows.forms.filedialog.restoredirectory?view=windowsdesktop-8.0
                    // 참고 2 URL - https://devit.koreacreatorfesta.com/entry/C-OpenFileDialog
                    openFileDialog.RestoreDirectory = true;

                    // OpenFileDialog 화면 출력 및 결과 저장 (버튼 "열기(O)", "취소", "X" 클릭 결과)
                    var selectResult = openFileDialog.ShowDialog();

                    // 이미지 파일 선택 후 버튼 "열기(O)"을 클릭 했을 경우 
                    if (openFileDialog.FileName.Length >= (int)EnumSelectFile.SELECT
                       && DialogResult.OK == selectResult)
                    {
                        // 전체 경로(+ 파일명 포함)는 openFileDialog.FileName  
                        // 참고 - 선택한 파일명은 openFileDialog.SafeFileName
                        DisplaySetting(HTSHelper.TypeOfSelectFile, openFileDialog.FileName);   // pictureBox(pictureBoxOrgImage)컨트롤 원본 이미지 출력 
                    }

                }   // 여기서 Dispose (리소스 해제) 처리 

                Log.Information(Logger.GetMethodPath(currentMethod) + "파일 선택 이벤트 종료");
            }
            catch (Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                TaskDialog.Show(HTSHelper.ErrorTitle, ex.Message);
            }
            finally
            {
                this.Activate();   // 메시지 박스 종료(TaskDialog.Show)후 부모 폼(ImageForm.cs) 다시 활성화
            }
        }

        #endregion btnSelectFile_Click

        #region btnSelectElement_Click

        /// <summary>
        /// 객체 선택 
        /// </summary>
        private void btnSelectElement_Click(object sender, EventArgs e)
        {
            var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

            try
            {
                Log.Information(Logger.GetMethodPath(currentMethod) + "객체 선택 이벤트 시작");

                MakeRequest(ImageEditorRequestId.SelectElement);   // 객체 선택 요청 

                Log.Information(Logger.GetMethodPath(currentMethod) + "객체 선택 이벤트 종료");
            }
            catch (Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                TaskDialog.Show(HTSHelper.ErrorTitle, ex.Message);
            }
            finally
            {
                this.Activate();   // 메시지 박스 종료(TaskDialog.Show)후 부모 폼(ImageForm.cs) 다시 활성화
            }
        }

        #endregion btnSelectElement_Click

        #region btnBlackConvert_Click

        /// <summary>
        /// 흑백 전환
        /// </summary>
        private void btnBlackConvert_Click(object sender, EventArgs e)
        {
            string imageFileName = string.Empty;                 // 흑백 전환 완료 처리된 이미지 파일 이름

            var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

            try
            {
                // 원본 이미지(OrgImage) 존재하지 않으면 이벤트 종료
                if (false == IsValidator(EnumImageType.ORIGINAL))
                {
                    TaskErrorDialog.Show();
                    return;   // 흑백 전환 이벤트 메서드 "btnBlackConvert_Click" 종료
                }

                Log.Information(Logger.GetMethodPath(currentMethod) + "흑백 전환 이벤트 시작");

                // 이미 흑백 전환 처리 완료된 경우
                if (true == IsBlackConverted)
                {
                    // imageFileName = Path.GetFileName(OrgImageFilePath);   // 이미지 파일 이름(확장자 포함) 가져오기
                    // TaskDialog.Show(HTSHelper.NoticeTitle, $"이미지 파일 {imageFileName}\r\n흑백 전환 처리 완료.");

                    Log.Information(Logger.GetMethodPath(currentMethod) + $"흑백 전환 이벤트 종료 - 사유 : 이미지 파일 {imageFileName} 흑백 전환 기완료.");
                    return;   // 흑백 전환 이벤트 메서드 "btnBlackConvert_Click" 종료
                }
                // 흑백 전환 처리 안한 경우 
                else
                {
                    // TODO : 아래 주석친 코드 필요시 참고 (2024.07.10 jbh)
                    // TaskDialog.Show(HTSHelper.NoticeTitle, "흑백 전환 기능은\r\n원본 이미지만 적용됩니다!");

                    DisplaySetting(HTSHelper.TypeOfBlackConvert, OrgImageFilePath);
                    Log.Information(Logger.GetMethodPath(currentMethod) + "흑백 전환 이벤트 종료");
                }


                // TODO : 아래 주의사항 필요시 참고 (2024.07.05 jbh)
                // *** 흑백 전환 기능 구현시 주의사항 ***
                // 아래 코드 2가지 사용시 오류 발생 
                // [1] Bitmap blackConvertBmp = OrgImage as Bitmap;
                // [2] pictureBoxEditImage.Image = blackConvertBmp;

                // 어떤 오류가 발생하냐면 아래 2가지 코드 사용 후 실행시 
                // 원본 이미지(pictureBoxOrgImage.Image)를 흑백 전환하여 편집 이미지(pictureBoxEditImage.Image)엔 정상 출력되지만
                // 원본 이미지(pictureBoxOrgImage.Image)를 마우스로 좌클릭 하면 마우스 이벤트(MouseDown, MouseMove, MouseUp 등등...)가 실행되며,
                // 원본 이미지(pictureBoxOrgImage.Image) 또한 기존 이미지가 아니라 편집 이미지(pictureBoxEditImage.Image)처럼
                // 흑백 전환한 상태로 바뀐다. 

                // 원본 이미지(pictureBoxOrgImage.Image)와 편집 이미지(pictureBoxEditImage.Image) 둘 다 흑백 전환 처리되는 이유???
                // 위의 2가지 코드 사용시 원본 이미지와 편집 이미지 둘다 동일한 이미지 객체(OrgImage)를 공유하고 있기 때문 이다.
                // 아무리 as 연산자와 Bitmap 클래스 객체 blackConvertBmp 를 선언해서 새로 할당하여도 
                // Image 객체 자체가 참조 타입이 때문에 편집 이미지에서 흑백 전환 되면 다른 원본 이미지에서도 흑백 전환 처리 된다.

                // 아래 코드 둘중 하나 사용시 오류 메시지 "Out of memory." 출력 됨.
                // new Bitmap(); 생성자 호출 또는 Bitmap 클래스 메서드 "Clone" 호출시 오류 메시지 "Out of memory." 출력
                // [1] Bitmap blackConvertBmp = new Bitmap(OrgImage);
                // [2] Bitmap blackConvertBmp = (Bitmap)OrgImage.Clone();

                // 오류 메시지 "Out of memory." 발생 원인
                // - 사용자가 마우스로 드래드앤 드롭해서 이미지를 자르고 복제 하려는 범위(selection)가 비트맵 범위를 벗어나서 발생한 오류로 확인

                // 참고 URL - https://learn.microsoft.com/ko-kr/dotnet/api/system.drawing.bitmap.clone?view=net-8.0&viewFallbackFrom=dotnet-plat-ext-8.0
                // 참고 2 URL - https://stackoverflow.com/questions/199468/c-sharp-image-clone-out-of-memory-exception
                // 참고 3 URL - https://www.codingdefined.com/2015/04/solved-bitmapclone-out-of-memory.html

                // Bitmap 클래스 메서드 "Clone" 호출시 오류 메시지 "Out of memory." 출력
                // 참고 URL - https://chatgpt.com/c/674d25d1-4baf-47ce-b5a7-e2e34b25d26d
                // 참고 2 URL - https://something-is-code.tistory.com/1
            }
            catch (Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                TaskDialog.Show(HTSHelper.ErrorTitle, ex.Message);
            }
            finally
            {
                this.Activate();   // 메시지 박스 종료(TaskDialog.Show)후 부모 폼(ImageForm.cs) 다시 활성화
            }
        }

        #endregion btnBlackConvert_Click

        #region btnOrgImage_Click

        /// <summary>
        /// 원본 보기 - 흑백 전환 처리된 이미지의 원본 이미지 가져오기
        /// </summary>
        private void btnOrgImage_Click(object sender, EventArgs e)
        {
            var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

            try
            {
                // 원본 이미지(OrgImage) 존재하지 않으면 이벤트 종료
                if (false == IsValidator(EnumImageType.ORIGINAL))
                {
                    TaskErrorDialog.Show();

                    return;   // 원본 보기 이벤트 메서드 "btnOrgImage_Click" 종료
                }

                Log.Information(Logger.GetMethodPath(currentMethod) + "원본 보기 이벤트 시작");

                DisplaySetting(HTSHelper.TypeOfOrgImage, OrgImageFilePath);

                Log.Information(Logger.GetMethodPath(currentMethod) + "원본 보기 이벤트 종료");
            }
            catch (Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                TaskDialog.Show(HTSHelper.ErrorTitle, ex.Message);
            }
            finally
            {
                this.Activate();   // 메시지 박스 종료(TaskDialog.Show)후 부모 폼(ImageForm.cs) 다시 활성화
            }
        }

        #endregion btnOrgImage_Click

        #region btnCropImage_Click

        /// <summary>
        /// 이미지 자르기 
        /// </summary>
        private void btnCropImage_Click(object sender, EventArgs e)
        {
            var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

            try
            {
                // 원본 이미지(OrgImage) 존재하지 않으면 이벤트 종료
                if (false == IsValidator(EnumImageType.ORIGINAL))
                {
                    TaskErrorDialog.Show();

                    return;   // 이미지 자르기 이벤트 메서드 "btnCropImage_Click" 종료
                }

                Log.Information(Logger.GetMethodPath(currentMethod) + "이미지 자르기 이벤트 시작");

                //Log.Information(Logger.GetMethodPath(currentMethod) + "이미지 자르기 작업 시작");

                DisplaySetting(HTSHelper.TypeOfCropImage, OrgImageFilePath);

                //Log.Information(Logger.GetMethodPath(currentMethod) + "이미지 자르기 작업 완료");

                Log.Information(Logger.GetMethodPath(currentMethod) + "이미지 자르기 이벤트 종료");

                // TODO : 아래 주석친 코드 필요시 참고 (2024.07.12 jbh)
                // IsCroppedImageFile = false;   // 이미지 자르기 초기화

                // DialogResult cropResult = MessageBox.Show("해당 이미지 자르기 진행할까요?", "이미지 자르기", MessageBoxButtons.YesNo);
                // TaskDialogResult cropResult = TaskDialog.Show(HTSHelper.NoticeTitle, "해당 이미지 자르기 진행할까요?", TaskDialogCommonButtons.Yes | TaskDialogCommonButtons.No);

                // 이미지 자르기 작업 요청한 경우 
                // if(DialogResult.Yes == cropResult)
                // if(TaskDialogResult.Yes == cropResult)
                // {
                //     Log.Information(Logger.GetMethodPath(currentMethod) + "이미지 자르기 작업 시작");

                //     DisplaySetting(HTSHelper.TypeOfCropImage, OrgImageFilePath);

                //     Log.Information(Logger.GetMethodPath(currentMethod) + "이미지 자르기 작업 완료");
                // }
                // // 이미지 자르기 작업 취소한 경우 
                // else
                // {
                //     Log.Information(Logger.GetMethodPath(currentMethod) + "이미지 자르기 작업 취소");

                //     pictureBoxOrgImage.Refresh();
                // }
            }
            catch (Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                TaskDialog.Show(HTSHelper.ErrorTitle, ex.Message);
            }
            finally
            {
                this.Activate();   // 메시지 박스 종료(TaskDialog.Show)후 부모 폼(ImageForm.cs) 다시 활성화
            }
        }

        #endregion btnCropImage_Click

        #region btnInsertImage_Click

        /// <summary>
        /// 이미지 삽입
        /// </summary>
        private void btnInsertImage_Click(object sender, EventArgs e)
        {
            bool isFileExist = false;                            // 삽입할 이미지 파일(원본 이미지, 편집 이미지) 존재 여부 초기화
            string orgFileName = string.Empty;                   // 이미지 삽입 처리 하고자 하는 원본 이미지 파일 이름(파일 확장자 포함) 
            var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

            try
            {
                // 원본 이미지(OrgImage) 존재하지 않으면 이벤트 종료
                if (false == IsValidator(EnumImageType.ORIGINAL))
                {
                    TaskErrorDialog.Show();

                    return;   // 이미지 삽입 이벤트 메서드 "btnInsertImage_Click" 종료
                }

                Log.Information(Logger.GetMethodPath(currentMethod) + "이미지 삽입 이벤트 시작");

                InsertedImageFilePath = string.Empty;
                InsertedImageFilePath = ImageManager.GetInsertImageFilePath(OrgImageFilePath);

                TaskDialogResult InsertResult = TaskDialog.Show(HTSHelper.NoticeTitle, "편집 이미지로 삽입 진행할까요?\r\n버튼 예(Y) : 편집 이미지 삽입\r\n버튼 아니요(N) : 원본 이미지 삽입", TaskDialogCommonButtons.Yes | TaskDialogCommonButtons.No);

                // 원본 이미지 파일이 존재하지 않는 경우 
                if(false == File.Exists(OrgImageFilePath))
                {
                    // TODO : 아래 주석친 코드 필요시 사용 예정 (2024.07.10 jbh)
                    // OrgImage = null;   // 유효성 검사할 때 사용할 원본 이미지 초기화 
                    orgFileName = Path.GetFileName(OrgImageFilePath);
                    TaskErrorDialog.MainInstruction = $"[{EnumImageType.ORIGINAL.ToDescription()}] - {orgFileName}\r\n파일 존재 안 함!\r\n다시 확인 바랍니다.";
                    // TODO : 아래 주석친 코드 필요시 참고 (2024.07.10 jbh)
                    // TaskErrorDialog.MainContent = $"* 파일 경로 *\r\n{OrgImageFilePath}";
                    TaskErrorDialog.Show();

                    Log.Error(Logger.GetMethodPath(currentMethod) + $"오류 - [{EnumImageType.ORIGINAL.ToDescription()}] - {orgFileName} 파일 존재 안 함! 다시 확인 바랍니다.\r\n파일 경로 - {OrgImageFilePath}");

                    return;   // 이미지 삽입 이벤트 메서드 "btnInsertImage_Click" 종료 
                }

                switch(InsertResult)
                {
                    case TaskDialogResult.Yes:   // 사용자가 자른 이미지로 이미지 삽입 요청한 경우 
                                                 // 편집 이미지(EditImage) 존재하지 않으면 이벤트 종료 
                        if (false == IsValidator(EnumImageType.EDIT))
                        {
                            TaskErrorDialog.Show();

                            return;   // 이벤트 메서드 "btnInsertImage_Click" 종료 
                        }

                        // TODO : pictureBoxEditImage.Image.Save 메서드 사용해서 자른 이미지 파일로 저장하기 구현 (2024.06.27 jbh)
                        // 참고 URL - https://www.csharpstudy.com/WinForms/WinForms-picturebox.aspx
                        // 참고 2 URL - https://tyen.tistory.com/74
                        pictureBoxEditImage.Image.Save(InsertedImageFilePath);
                        break;

                    case TaskDialogResult.No:      // 사용자가 원본 이미지로 이미지 삽입 요청한 경우 
                                                   // 원본 이미지가 흑백 전환 처리된 경우 
                        if (true == IsBlackConverted)
                        {
                            TaskDialogResult blackConvertResult = TaskDialog.Show(HTSHelper.NoticeTitle, "흑백 전환된 이미지로 삽입 진행할까요?", TaskDialogCommonButtons.Yes | TaskDialogCommonButtons.No);

                            // 사용자가 흑백 전환 처리된 이미지로 이미지 삽입 요청한 경우        
                            if (TaskDialogResult.Yes == blackConvertResult)
                            {
                                // TODO : pictureBoxOrgImage.Image.Save 메서드 사용해서 흑백 전환 처리된 이미지 파일로 저장하기 구현 (2024.07.05 jbh)
                                // 참고 URL - https://www.csharpstudy.com/WinForms/WinForms-picturebox.aspx
                                // 참고 2 URL - https://tyen.tistory.com/74
                                pictureBoxOrgImage.Image.Save(InsertedImageFilePath);
                            }

                            // 사용자가 원본 이미지로 이미지 삽입 요청한 경우       
                            else if (TaskDialogResult.No == blackConvertResult)
                            {
                                TaskDialog.Show(HTSHelper.NoticeTitle, "원본 이미지로 삽입 진행합니다.");
                                InsertedImageFilePath = OrgImageFilePath;
                            }

                            // 사용자가 출력된 메시지 박스에서 버튼 "X"(닫기) 클릭한 경우 
                            else return;   // 이벤트 메서드 "btnInsertImage_Click" 종료    
                        }
                        // 원본 이미지가 흑백 전환 처리되지 않은 경우 
                        else InsertedImageFilePath = OrgImageFilePath;

                        break;

                    // TODO : "닫기" 레이블 "case TaskDialogResult.Cancel:" 필요시 구현 예정 (2024.07.08 jbh)
                    // case TaskDialogResult.Cancel:  // 사용자가 출력된 메시지 박스에서 버튼 "X"(닫기) 누른 경우 
                    //     return;                    // 이벤트 메서드 "btnInsertImage_Click" 종료 

                    // TODO : default: 레이블(그 외 나머지 경우) 필요시 구현 예정 (2024.07.08 jbh)
                    default:
                        // break;
                        return;   // 이벤트 메서드 "btnInsertImage_Click" 종료 
                }

                isFileExist = true;

                // 삽입할 이미지 파일(원본 이미지, 편집 이미지) 존재할 경우 
                if (true == isFileExist) MakeRequest(ImageEditorRequestId.InsertImage);   // 이미지 삽입 요청 

                Log.Information(Logger.GetMethodPath(currentMethod) + "이미지 삽입 이벤트 종료");

                // TODO : 아래 주석친 코드 필요시 참고 (2024.07.08 jbh)
                // // 사용자가 자른 이미지로 이미지 삽입 요청한 경우 
                // if(TaskDialogResult.Yes == InsertResult)
                // {
                //     // 편집 이미지(EditImage) 존재하지 않으면 이벤트 종료 
                //     //if(EditImage is null)
                //     if(false == IsValidator(EnumImageType.EDIT))
                //     {
                //         TaskErrorDialog.Show();
                // 
                //         return;
                //     }
                // 
                //     // InsertedImageFilePath = ImageManager.GetInsertImageFilePath(OrgImageFilePath);
                // 
                //     // TODO : pictureBoxEditImage.Image.Save 메서드 사용해서 자른 이미지 파일로 저장하기 구현 (2024.06.27 jbh)
                //     // 참고 URL - https://www.csharpstudy.com/WinForms/WinForms-picturebox.aspx
                //     // 참고 2 URL - https://tyen.tistory.com/74
                //     pictureBoxEditImage.Image.Save(InsertedImageFilePath);
                // }
                // 사용자가 원본 이미지로 이미지 삽입 요청한 경우 
                // else 
                // {
                //     // 원본 이미지가 흑백 전환 처리된 경우 
                //     if(true == IsBlackConverted) 
                //     {
                //         TaskDialogResult blackConvertResult = TaskDialog.Show(HTSHelper.NoticeTitle, "흑백 전환된 이미지로 삽입 진행할까요?", TaskDialogCommonButtons.Yes | TaskDialogCommonButtons.No);
                // 
                //         // 사용자가 흑백 전환 처리된 이미지로 이미지 삽입 요청한 경우        
                //         if (TaskDialogResult.Yes == blackConvertResult)
                //         {
                //             // TODO : pictureBoxOrgImage.Image.Save 메서드 사용해서 흑백 전환 처리된 이미지 파일로 저장하기 구현 (2024.07.05 jbh)
                //             // 참고 URL - https://www.csharpstudy.com/WinForms/WinForms-picturebox.aspx
                //             // 참고 2 URL - https://tyen.tistory.com/74
                //             pictureBoxOrgImage.Image.Save(InsertedImageFilePath);
                //         }
                //         // 사용자가 원본 이미지로 이미지 삽입 요청한 경우       
                //         else
                //         {
                //             TaskDialog.Show(HTSHelper.NoticeTitle, "원본 이미지로 삽입 진행합니다.");
                //             InsertedImageFilePath = OrgImageFilePath;
                //         }
                //     }
                //     // 원본 이미지가 흑백 전환 처리되지 않은 경우 
                //     else InsertedImageFilePath = OrgImageFilePath;
                // }
            }
            catch (Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                TaskDialog.Show(HTSHelper.ErrorTitle, ex.Message);
            }
            finally
            {
                // 삽입할 이미지 파일(원본 이미지, 편집 이미지) 존재하지 않을 경우 
                if (false == isFileExist) this.Activate();   // 메시지 박스 종료(TaskDialog.Show)후 부모 폼(ImageForm.cs) 다시 활성화
            }
        }

        #endregion btnInsertImage_Click

        #region btnExit_Click

        /// <summary>
        /// 종료 - 폼(ImageForm.cs) 화면 종료 
        /// </summary>
        private void btnExit_Click(object sender, EventArgs e)
        {
            var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

            try
            {
                Log.Information(Logger.GetMethodPath(currentMethod) + "종료 이벤트 시작");

                TaskDialogResult exitResult = TaskDialog.Show(HTSHelper.NoticeTitle, "이미지 삽입 화면을 종료 할까요?", TaskDialogCommonButtons.Yes | TaskDialogCommonButtons.No);

                // 사용자가 화면 종료 요청한 경우 (버튼 "예(Y)" 클릭)
                if (TaskDialogResult.Yes == exitResult) this.Dispose();   // 폼(ImageForm.cs) 객체 모든 리소스 해제  

                // 사용자가 화면 종료 요청하지 않은 경우 (버튼 "아니요(N)", 버튼 "X"(닫기) 클릭)
                else this.Activate();   // 메시지 박스 종료(TaskDialog.Show)후 부모 폼(ImageForm.cs) 다시 활성화

                Log.Information(Logger.GetMethodPath(currentMethod) + "종료 이벤트 종료");
            }
            catch (Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                TaskDialog.Show(HTSHelper.ErrorTitle, ex.Message);
            }
        }

        #endregion btnExit_Click

        #region pictureBoxOrgImage_MouseDown

        /// <summary>
        /// 원본 이미지 pictureBox - MouseDown 이벤트 메서드 
        /// </summary>
        private void pictureBoxOrgImage_MouseDown(object sender, MouseEventArgs e)
        {
            var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

            try
            {
                // Log.Information(Logger.GetMethodPath(currentMethod) + "원본 이미지 MouseDown 이벤트 시작");

                // if(OrgImage is null) return;   // 원본 이미지(OrgImage) 존재하지 않으면 이벤트 종료 
                if (false == IsValidator(EnumImageType.ORIGINAL)) return;   // 원본 이미지(OrgImage) 존재하지 않으면 이벤트 종료 

                // 마우스 왼쪽 버튼 클릭했을 경우 
                if (MouseButtons.Left == e.Button)
                {
                    pictureBoxOrgImage.Refresh();

                    IsDragging = true;

                    DownX = e.X;
                    DownY = e.Y;
                    btnCropImage.Enabled = true;
                }

                // Log.Information(Logger.GetMethodPath(currentMethod) + "원본 이미지 MouseDown 이벤트 종료");
            }
            catch (Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                TaskDialog.Show(HTSHelper.ErrorTitle, ex.Message);
            }
        }

        #endregion pictureBoxOrgImage_MouseDown

        #region pictureBoxOrgImage_MouseMove

        /// <summary>
        /// 원본 이미지 pictureBox - MouseMove 이벤트 메서드 
        /// </summary>
        private void pictureBoxOrgImage_MouseMove(object sender, MouseEventArgs e)
        {
            Rectangle rec = Rectangle.Empty;                     // 사용자가 이미지를 자르고자 하는 영역(직사각형) 초기화 
            var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

            try
            {
                // Log.Information(Logger.GetMethodPath(currentMethod) + "원본 이미지 MouseMove 이벤트 시작");

                // if(OrgImage is null) return;   // 원본 이미지(OrgImage) 존재하지 않으면 이벤트 종료 
                if (false == IsValidator(EnumImageType.ORIGINAL)) return;   // 원본 이미지(OrgImage) 존재하지 않으면 이벤트 종료 

                // 마우스 왼쪽 버튼 클릭했을 경우 
                if (true == IsDragging && MouseButtons.Left == e.Button)
                {
                    // pictureBoxOrgImage.Image.Clone();
                    pictureBoxOrgImage.Refresh();

                    MoveX = e.X;
                    MoveY = e.Y;

                    rec = new Rectangle(DownX, DownY, Math.Abs(MoveX - DownX), Math.Abs(MoveY - DownY));

                    RectCropArea = rec;
                    btnCropImage.Enabled = true;
                }

                // Log.Information(Logger.GetMethodPath(currentMethod) + "원본 이미지 MouseMove 이벤트 종료");
            }
            catch (Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                TaskDialog.Show(HTSHelper.ErrorTitle, ex.Message);
            }
        }

        #endregion pictureBoxOrgImage_MouseMove

        #region pictureBoxOrgImage_Paint

        /// <summary>
        /// 원본 이미지 pictureBox - Paint 이벤트 메서드 
        /// 마우스로 원본 이미지 드래그 앤 드롭시 사용자가 자르고 싶은 영역을 직사각형 형태(색상 - YellowGreen)로 그려줌.
        /// </summary>
        private void pictureBoxOrgImage_Paint(object sender, PaintEventArgs e)
        {
            var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

            try
            {
                // Log.Information(Logger.GetMethodPath(currentMethod) + "원본 이미지 Paint 이벤트 시작");

                // if(OrgImage is null) return;   // 원본 이미지(OrgImage) 존재하지 않으면 이벤트 종료 
                if (false == IsValidator(EnumImageType.ORIGINAL)) return;   // 원본 이미지(OrgImage) 존재하지 않으면 이벤트 종료 

                if (true == IsDragging)
                {
                    using (Pen pen = new Pen(Color.YellowGreen, 3))
                    {
                        //pictureBoxOrgImage.CreateGraphics().DrawRectangle(pen, RectCropArea);
                        e.Graphics.DrawRectangle(pen, RectCropArea);
                    }   // 여기서 Dispose (리소스 해제) 처리 
                }

                // Log.Information(Logger.GetMethodPath(currentMethod) + "원본 이미지 Paint 이벤트 종료");
            }
            catch (Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                TaskDialog.Show(HTSHelper.ErrorTitle, ex.Message);
            }
        }

        #endregion pictureBoxOrgImage_Paint

        #region pictureBoxOrgImage_MouseUp

        /// <summary>
        /// 원본 이미지 pictureBox - MouseUp 이벤트 메서드 
        /// </summary>
        private void pictureBoxOrgImage_MouseUp(object sender, MouseEventArgs e)
        {
            var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

            try
            {
                // Log.Information(Logger.GetMethodPath(currentMethod) + "원본 이미지 MouseUp 이벤트 시작");

                // if(OrgImage is null) return;   // 원본 이미지(OrgImage) 존재하지 않으면 이벤트 종료 
                if (false == IsValidator(EnumImageType.ORIGINAL)) return;   // 원본 이미지(OrgImage) 존재하지 않으면 이벤트 종료 

                // 마우스 왼쪽 버튼 클릭했을 경우 
                if (true == IsDragging && MouseButtons.Left == e.Button)
                {
                    pictureBoxOrgImage.Refresh();

                    IsDragging = false;
                }

                // TODO : 아래 주석친 코드 필요시 참고 (2024.06.25 jbh)
                // 마우스 왼쪽 버튼 클릭했을 경우 
                //if(MouseButtons.Left == e.Button)
                //{
                //    // pictureBoxOrgImage.Image.Clone();
                //    pictureBoxOrgImage.Refresh();
                //    UpX = e.X;
                //    UpY = e.Y;

                //    Rectangle rec = new Rectangle(DownX, DownY, Math.Abs(UpX - DownX), Math.Abs(UpY - DownY));

                //    using (Pen pen = new Pen(Color.YellowGreen, 3))
                //    {
                //        pictureBoxOrgImage.CreateGraphics().DrawRectangle(pen, rec);
                //    }
                //    RectCropArea = rec;
                //    btnCropImage.Enabled = true;
                //}

                // Log.Information(Logger.GetMethodPath(currentMethod) + "원본 이미지 MouseUp 이벤트 종료");
            }
            catch (Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                TaskDialog.Show(HTSHelper.ErrorTitle, ex.Message);
            }
        }

        #endregion pictureBoxOrgImage_MouseUp

        #region pictureBoxOrgImage_MouseWheel

        // TODO : PictureBox 컨트롤에 출력되는 이미지를 마우스 휠을 사용해서 이미지 확대/축소 기능 구현 (2024.07.11 jbh)
        // 참고 URL - https://cs-solution.tistory.com/m/11

        // ChatGPT 참고 URL 
        // https://chatgpt.com/c/7cc93196-4334-45ee-8fd3-9525d12c449d

        // ChatGPT 참고 2 URL
        // https://chatgpt.com/c/06d0ab64-06e8-4568-9e42-dd6b42a97460

        // 유튜브 참고 URL
        // https://youtu.be/POJBq_a1Ea4?si=A7SgcIOOaoftMxhx

        /// <summary>
        /// 원본 이미지 pictureBox - MouseWheel 이벤트 메서드 
        /// </summary>
        private void pictureBoxOrgImage_MouseWheel(object sender, MouseEventArgs e)
        {
            int sizeChangeValue = 0;                             // 마우스 휠 이벤트가 실행된 후 .e.Delta 값에 따라 원본 이미지 Width, Height (증/감 수치 * 배율) 초기화      
            int magnification = 0;                               // 마우스 휠 이벤트가 실행된 후 .e.Delta 값에 따라 원본 이미지 Width, Height 증/감 배율 (X10, X20 등등...) 초기화      
            int resizedOrgWidth = 0;                             // 마우스 휠 이벤트가 실행된 후 사이즈 변경될 pictureBoxOrgImage의 예측 너비 
            int resizedOrgHeight = 0;                            // 마우스 휠 이벤트가 실행된 후 사이즈 변경될 pictureBoxOrgImage의 예측 높이 

            Bitmap resizedOrgImage = null;                       // 마우스 휠 이벤트가 실행된 후 사이즈 변경 완료된 원본 이미지 비트맵 객체 초기화

            var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

            try
            {
                // Log.Information(Logger.GetMethodPath(currentMethod) + "원본 이미지 MouseWheel 이벤트 시작");

                // if(pictureBoxOrgImage.Image.Width >= 1000 || pictureBoxOrgImage.Image.Height >= 1000) magnification = 2;
                // magnification = HTSHelper.OrgImageMagnification;   // 원본 이미지 Width, Height 증/감 수치 배율(X3) 설정
                // else magnification = 5;

                magnification = HTSHelper.OrgImageMagnification;   // 원본 이미지 Width, Height 증/감 수치 배율(X5) 설정

                // e.Delta 값이 1보다 크거나 같으면 Witdh, Height 증가(EnumSizeChangeType.INCREMENT)
                // e.Delta 값이 0이면 Witdh, Height 유지(EnumSizeChangeType.MAINTENANCE)
                // e.Delta 값이 -1보다 작거나 같으면 Witdh, Height 감소(EnumSizeChangeType.DECREMENT) 

                // 증가 
                if (e.Delta >= (int)EnumSizeChangeType.INCREMENT) sizeChangeValue = magnification * (int)EnumSizeChangeType.INCREMENT;

                // 유지
                else if (e.Delta == (int)EnumSizeChangeType.MAINTENANCE) sizeChangeValue = (int)EnumSizeChangeType.MAINTENANCE;

                // 감소 
                else sizeChangeValue = magnification * (int)EnumSizeChangeType.DECREMENT;


                resizedOrgWidth = pictureBoxOrgImage.Image.Width + (sizeChangeValue * OrgImageRatio.WidthRatio);
                resizedOrgHeight = pictureBoxOrgImage.Image.Height + (sizeChangeValue * OrgImageRatio.HeightRatio);

                // 마우스 휠 이벤트가 실행되어 원본 이미지 pictureBoxOrgImage 의 Width, Height 증감할 때, 
                // 증감한 Width, Height의 예측 값이 0보다 커서
                // Width, Height 변경 가능한 경우 
                if (resizedOrgWidth >= (int)EnumReSizeType.POSSIBLE && resizedOrgHeight >= (int)EnumReSizeType.POSSIBLE)
                {
                    if (true == IsBlackConverted) resizedOrgImage = new Bitmap(BlackConvertBmp, resizedOrgWidth, resizedOrgHeight);

                    else resizedOrgImage = new Bitmap(OrgImage, resizedOrgWidth, resizedOrgHeight);

                    resizedOrgImage.SetResolution(OrgImage.HorizontalResolution,
                                                  OrgImage.VerticalResolution);

                    pictureBoxOrgImage.Image = resizedOrgImage;


                    // TODO : 아래 주석친 코드 사용시 마우스 휠 이벤트 실행되면 원본 이미지 확대/축소시 화면이 깨지므로 사용 안함. (2024.07.16 jbh)
                    // OrgImage = resizedOrgImage;
                    // pictureBoxOrgImage.Image = OrgImage;
                }

                // TODO : 아래 주석친 코드 필요시 참고 (2024.07.12 jbh)
                // resizedChangeType = (e.Delta > (int)EnumSizeChangeType.MAINTENANCE) ? (int)EnumSizeChangeType.INCREMENT : ((e.Delta == (int)EnumSizeChangeType.MAINTENANCE) ? 0 : -1);
                // PictureBox pictureBoxOrg = sender as PictureBox;
                // resizedOrgWidth = pictureBoxOrgImage.Image.Width + e.Delta;
                // resizedOrgHeight = pictureBoxOrgImage.Image.Height + e.Delta;

                // pictureBoxOrgImage.Width = resizedOrgWidth;
                // pictureBoxOrgImage.Height = resizedOrgHeight;
                // pictureBoxOrgImage.Width += e.Delta;
                // pictureBoxOrgImage.Height += e.Delta;

                // Log.Information(Logger.GetMethodPath(currentMethod) + "원본 이미지 MouseWheel 이벤트 종료");
            }
            catch (Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                TaskDialog.Show(HTSHelper.ErrorTitle, ex.Message);
            }
        }

        #endregion pictureBoxOrgImage_MouseWheel

        #region Sample

        // TODO : 필요시 편집 이미지 마우스 휠 이벤트 메서드 "pictureBoxEditImage_MouseWheel" 참고 (2024.07.11 jbh)
        #region pictureBoxEditImage_MouseWheel

        // TODO : PictureBox 컨트롤에 출력되는 이미지를 마우스 휠을 사용해서 이미지 확대/축소 기능 구현 (2024.07.11 jbh)
        // 참고 URL - https://cs-solution.tistory.com/m/11

        // 유튜브 참고 URL
        // https://youtu.be/POJBq_a1Ea4?si=A7SgcIOOaoftMxhx

        /// <summary>
        /// 편집 이미지 pictureBox - MouseWheel 이벤트 메서드 
        /// </summary>
        //private void pictureBoxEditImage_MouseWheel(object sender, MouseEventArgs e)
        //{
        //    int resizeEditWidth = 0;                             // 마우스 휠 이벤트가 실행된 후 사이즈 변경될 pictureBoxEditImage의 예측 너비 
        //    int resizeEditHeight = 0;                            // 마우스 휠 이벤트가 실행된 후 사이즈 변경될 pictureBoxEditImage의 예측 높이 

        //    var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

        //    try
        //    {
        //        // Log.Information(Logger.GetMethodPath(currentMethod) + "편집 이미지 MouseWheel 이벤트 시작");

        //        resizeEditWidth = pictureBoxEditImage.Width + e.Delta;
        //        resizeEditHeight = pictureBoxEditImage.Height + e.Delta;

        //        // 마우스 휠 이벤트가 실행되어 편집 이미지 pictureBoxEditImage 의 Width, Height 증감할 때, 
        //        // 증감한 Width, Height의 예측 값이 0보다 커서
        //        // Width, Height 변경 가능한 경우 
        //        // if(resizeEditWidth > 0 && resizeEditHeight > 0)
        //        if(resizeEditWidth >= (int)EnumReSizeType.POSSIBLE && resizeEditHeight >= (int)EnumReSizeType.POSSIBLE)
        //        {
        //            pictureBoxEditImage.Width += e.Delta;
        //            pictureBoxEditImage.Height += e.Delta;
        //        }

        //        // Log.Information(Logger.GetMethodPath(currentMethod) + "편집 이미지 MouseWheel 이벤트 종료");
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
        //        TaskDialog.Show(HTSHelper.ErrorTitle, ex.Message);
        //    }
        //}

        #endregion pictureBoxEditImage_MouseWheel

        #endregion Sample
    }
}