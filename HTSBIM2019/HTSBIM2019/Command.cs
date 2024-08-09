using Serilog;

using System;
using System.Reflection;

using HTSBIM2019.Common.LogBase;
using HTSBIM2019.Common.Managers;
using HTSBIM2019.Common.HTSBase;
using HTSBIM2019.Common.RequestBase;
using HTSBIM2019.Settings;

using HTSBIM2019.UI.ImageEditor;
using HTSBIM2019.Utils.CompanyHomePage;
using HTSBIM2019.Utils.TechnicalSupport;
using HTSBIM2019.Interface.Command;


using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.ApplicationServices;

namespace HTSBIM2019
{
    #region Command

    // TODO : TransactionAttribute - TransactionMode.Manual 설정 구현 (2024.08.09 jbh)
    // 참고 URL - https://manzoo.tistory.com/107
    [Transaction(TransactionMode.Manual)]       // Transaction 사용이 필요한 경우 (Document 내에서 수정/삭제/추가 등의 작업이 필요한 경우)
    [Regeneration(RegenerationOption.Manual)]
    /// <summary>
    /// Test Command
    /// </summary>
    internal class CmdTestUpdater
    {

    }

    #endregion Command

    #region CmdCompanyHomePage

    // TODO : TransactionAttribute - TransactionMode.Manual 설정 구현 (2024.08.09 jbh)
    // 참고 URL - https://manzoo.tistory.com/107
    [Transaction(TransactionMode.Manual)]       // Transaction 사용이 필요한 경우 (Document 내에서 수정/삭제/추가 등의 작업이 필요한 경우)
    [Regeneration(RegenerationOption.Manual)]
    /// <summary>
    /// (주)상상진화 기업 홈페이지 Command - 상상플렉스 커뮤니티
    /// </summary>
    internal class CmdCompanyHomePage : IExternalCommand, IHTSCommand
    {
        #region 프로퍼티

        /// <summary>
        /// Revit UI 애플리케이션 객체
        /// </summary>
        public UIApplication RevitUIApp { get; set; }

        /// <summary>
        /// 활성화된 Revit 문서 
        /// </summary>
        public Document RevitDoc { get; set; }

        #endregion 프로퍼티

        #region 기본 메소드

        // TODO : 콜백 함수 Execute 구현 (2024.03.11 jbh)
        // 콜백(CallBack) 함수란? 시스템이 사용자가 요청한 처리를 하다가 특정 이벤트를 발생시켜 해당 이벤트를 처리해달라고 역으로 전달해 오는 함수
        // 참고 URL - https://nephrolepis.tistory.com/12
        // 참고 2 URL - https://todaycode.tistory.com/24
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

            try
            {
                Log.Information(Logger.GetMethodPath(currentMethod) + "(주)상상진화 기업 홈페이지 Command - Execute 시작");

                // TaskDialog.Show("(주)상상진화 기업 홈페이지...", "구현 예정...");

                RevitUIApp = commandData.Application;               // Revit UI 애플리케이션 객체
                RevitDoc = RevitUIApp.ActiveUIDocument.Document;         // 활성화된 Revit 문서 

                // (주)상상진화 기업 홈페이지 URL 주소로 연동
                AppSetting.Default.ImagineBuilderBase.HomePage = new CompanyHomePage(RevitDoc, HTSHelper.ImagineBuilder_URL);

                Log.Information(Logger.GetMethodPath(currentMethod) + "(주)상상진화 기업 홈페이지 Command - Execute 종료");

                return Result.Succeeded;
            }
            catch(Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                TaskDialog.Show(HTSHelper.ErrorTitle, ex.Message);
                return Result.Failed;
            }
        }

        #endregion 기본 메소드
    }

    #endregion CmdCompanyHomePage

    // TODO : Revit AddIn 개발 소스를 비쥬얼스튜디오 2022 .net Core 버전(8.0)을 사용하려면 Revit 2025 버전 부터 사용이 가능하므로 현 시점에서 해당 소스는 .net FrameWork 4.8에서만 구동시킬 수 있다. (2024.03.11 jbh)

    // 참고 URL - https://forums.autodesk.com/t5/revit-api-forum/iupdater-simple-example-needed/m-p/9893248
    // 참고 2 URL - https://adndevblog.typepad.com/aec/2016/02/revitapi-how-to-use-dmu-dynamic-model-update-api.html

    #region CmdMEPUpdater

    // TODO : HTS Revit 업데이터 구현 (2024.03.13 jbh)
    // 참고 URL   - https://forums.autodesk.com/t5/revit-api-forum/iupdater-simple-example-needed/m-p/9893248
    // 참고 2 URL - https://thebuildingcoder.typepad.com/blog/2012/06/documentchanged-versus-dynamic-model-updater.html

    // HTS Revit 업데이터 Command 클래스 CmdMEPUpdater class 접근 제한자 intenal 설정 (2024.03.14 jbh)
    // 접근 제한자 internal - internal 로 선언한 변수 또는 클래스는 CmdMEPUpdater 프로젝트 내에서만 접근이 가능하고, 외부에선 접근이 불가능
    // 참고 URL - https://slaner.tistory.com/69

    // TODO : TransactionAttribute - TransactionMode.Manual 설정 구현 (2024.08.09 jbh)
    // 참고 URL - https://manzoo.tistory.com/107
    [Transaction(TransactionMode.Manual)]       // Transaction 사용이 필요한 경우 (Document 내에서 수정/삭제/추가 등의 작업이 필요한 경우)
    [Regeneration(RegenerationOption.Manual)]
    /// <summary>
    /// 1. MEP 사용 기록 관리 Command
    /// </summary>
    internal class CmdMEPUpdater : IExternalCommand, IHTSCommand
    {
        #region 프로퍼티

        /// <summary>
        /// MEPUpdater 폼 객체 
        /// </summary>
        // private static MEPUpdaterForm FormMEPUpdater { get; set; }

        /// <summary>
        /// Revit UI 애플리케이션 객체
        /// </summary>
        public UIApplication RevitUIApp { get; set; }

        /// <summary>
        /// 활성화된 Revit 문서 
        /// </summary>
        public Document RevitDoc { get; set; }

        #endregion 프로퍼티

        #region 기본 메소드

        // TODO : 콜백 함수 Execute 구현 (2024.03.11 jbh)
        // 콜백(CallBack) 함수란? 시스템이 사용자가 요청한 처리를 하다가 특정 이벤트를 발생시켜 해당 이벤트를 처리해달라고 역으로 전달해 오는 함수
        // 참고 URL - https://nephrolepis.tistory.com/12
        // 참고 2 URL - https://todaycode.tistory.com/24
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            bool isVisible = false;                               // Modaless 폼 객체(AppSetting.Default.UpdaterBase.MEPUpdaterForm) 화면 출력 여부(.Show())

            // TODO : 로그 기록시 현재 실행 중인 메서드 위치 기록하기 (2024.01.22 jbh)
            // 참고 URL - https://slaner.tistory.com/73
            // 참고 2 URL - https://stackoverflow.com/questions/4132810/how-can-i-get-a-method-name-with-the-namespace-class-name
            // 참고 3 URL - https://stackoverflow.com/questions/44153/can-you-use-reflection-to-find-the-name-of-the-currently-executing-method
            var currentMethod = MethodBase.GetCurrentMethod();

            try
            {
                Log.Information(Logger.GetMethodPath(currentMethod) + "1. MEP 사용 기록 관리 Command - Execute 시작");

                RevitUIApp = commandData.Application;                             // Revit UI 애플리케이션 객체 
                RevitDoc = RevitUIApp.ActiveUIDocument.Document;                  // 활성화된 Revit 문서 
                // AddInId addInId = revitUIApp.ActiveAddInId;                    // HTS Revit 업데이터 Command 아이디

                Application revitApp = RevitUIApp.Application;                    // Revit 애플리케이션 객체 

                // TODO : 로그인 아이디, 작업자(사용자) 이름 구하기 (2024.03.02 jbh)
                // 참고 URL   - https://www.revitapidocs.com/2018/8d3b257a-7b99-a6ee-b146-f635c35f425c.htm
                // 참고 2 URL - https://www.revitapidocs.com/2015/2a7c8664-de0d-7a43-e670-2e733e579609.htm?query=LoginUser        
                AppSetting.Default.Login.LoginUserId = revitApp.LoginUserId;      // 로그인 아이디
                AppSetting.Default.Login.Username    = revitApp.Username;         // 작업자(사용자) 이름


                // AddInId addInId = RevitUIApp.ActiveAddInId;                       // 활성화된 애드인 애플리케이션(또는 Command) 아이디
                MEPUpdaterRequestHandler mepHandler = new MEPUpdaterRequestHandler();    // MEP 업데이터 외부 요청 핸들러 객체 mepHandler 생성 
                ExternalEvent exEvent = ExternalEvent.Create(mepHandler);                // MEP 업데이터 폼 객체가 사용할 외부 이벤트 생성 

                // AppSetting.Default.UpdaterBase = UpdaterSetting.GetUpdaterInstance(addInId, exEvent, mepHandler, RevitUIApp);  // MEP 업데이터 폼 객체 싱글톤 객체로 생성
                AppSetting.Default.UpdaterBase.MEPUpdaterForm = UpdaterSetting.GetUpdaterFormInstance(exEvent, mepHandler, RevitUIApp);  // MEP 업데이터 폼 객체 싱글톤 객체로 생성

                // TODO : 프로퍼티 "Visible" 사용해서 Modaless 폼 객체(AppSetting.Default.UpdaterBase.MEPUpdaterForm) 화면 출력 여부 확인
                // 참고 URL - https://learn.microsoft.com/ko-kr/dotnet/api/system.windows.forms.control.visible?view=windowsdesktop-8.0
                isVisible = AppSetting.Default.UpdaterBase.MEPUpdaterForm.Visible;

                // Modaless 폼 객체(AppSetting.Default.UpdaterBase.MEPUpdaterForm) 화면 출력 안 된 경우 
                if(false == isVisible) AppSetting.Default.UpdaterBase.MEPUpdaterForm.Show();   // Show 메서드 호출해서 화면 출력하기  

                // TaskDialog.Show("HTS Revit Update...", "테스트 진행 중...");

                // TODO : 아래 주석친 코드 필요시 사용 예정 (2024.04.15 jbh)
                // if(RevitUIApp.ActiveUIDocument is null)   // Revit 문서를 열지 않은 경우 
                // if(RevitDoc is null)   // Revit 문서를 열지 않은 경우 
                //     throw new Exception("MEP Updater 기능 실행하기 전에\r\nRevit 문서를 열어 주시기 바랍니다.");

                // MEPUpdaterForm = new MEPUpdater(uiapp, addInId);
                // MEPUpdaterForm.ShowDialog();
                // MEPUpdaterForm.Show();
                // FormManager.ShowModalessForm(AppSetting.Default.UpdaterBase.MEPUpdaterForm, RevitUIApp, typeof(MEPUpdaterForm));
                // FormManager.ShowModalessForm(RevitUIApp, typeof(MEPUpdaterForm));
                // FormManager.ShowModalessForm(AppSetting.Default.UpdaterBase.MEPUpdaterForm, typeof(MEPUpdaterForm));

                // TestMEPUpdater testMEPUpdater = new TestMEPUpdater();
                // testMEPUpdater.Show();

                Log.Information(Logger.GetMethodPath(currentMethod) + "1. MEP 사용 기록 관리 Command - Execute 종료");

                return Result.Succeeded;
            }
            catch(Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                TaskDialog.Show(HTSHelper.ErrorTitle, ex.Message);
                return Result.Failed;
            }
        }

        #endregion 기본 메소드

        #region WakeFormUp

        /// <summary>
        /// 대기 상태에서 MEPUpdater 폼 화면 깨우기
        /// </summary>
        public static void WakeFormUp()
        {
            //if(FormMEPUpdater is not null) FormMEPUpdater.WakeUp();
            if(AppSetting.Default.UpdaterBase.MEPUpdaterForm is not null) AppSetting.Default.UpdaterBase.MEPUpdaterForm.WakeUp();
        }

        #endregion WakeFormUp
    }

    #endregion CmdMEPUpdater

    #region CmdTechnicalSupport

    // TODO : TransactionAttribute - TransactionMode.Manual 설정 구현 (2024.08.09 jbh)
    // 참고 URL - https://manzoo.tistory.com/107
    [Transaction(TransactionMode.Manual)]       // Transaction 사용이 필요한 경우 (Document 내에서 수정/삭제/추가 등의 작업이 필요한 경우)
    [Regeneration(RegenerationOption.Manual)]
    /// <summary>
    /// 2. (주)상상진화 기술지원 문의 Command - 상상플렉스 커뮤니티
    /// </summary>
    internal class CmdTechnicalSupport : IExternalCommand, IHTSCommand
    {
        #region 프로퍼티

        /// <summary>
        /// Revit UI 애플리케이션 객체
        /// </summary>
        public UIApplication RevitUIApp { get; set; }

        /// <summary>
        /// 활성화된 Revit 문서 
        /// </summary>
        public Document RevitDoc { get; set; }

        #endregion 프로퍼티

        #region 기본 메소드

        // TODO : 콜백 함수 Execute 구현 (2024.03.11 jbh)
        // 콜백(CallBack) 함수란? 시스템이 사용자가 요청한 처리를 하다가 특정 이벤트를 발생시켜 해당 이벤트를 처리해달라고 역으로 전달해 오는 함수
        // 참고 URL - https://nephrolepis.tistory.com/12
        // 참고 2 URL - https://todaycode.tistory.com/24
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

            try
            {
                Log.Information(Logger.GetMethodPath(currentMethod) + "2. (주)상상진화 기술지원 문의 Command - Execute 시작");

                // TaskDialog.Show("(주)상상진화 기술지원 문의...", "구현 예정...");

                RevitUIApp = commandData.Application;                    // Revit UI 애플리케이션 객체
                RevitDoc = RevitUIApp.ActiveUIDocument.Document;         // 활성화된 Revit 문서 

                // 상상플렉스 웹사이트 URL 주소로 연동
                AppSetting.Default.ImagineBuilderBase.TechSupport = new TechnicalSupport(RevitDoc, HTSHelper.SangSangFlex_URL);

                Log.Information(Logger.GetMethodPath(currentMethod) + "2. (주)상상진화 기술지원 문의 Command - Execute 종료");

                return Result.Succeeded;
            }
            catch(Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                TaskDialog.Show(HTSHelper.ErrorTitle, ex.Message);
                return Result.Failed;
            }
        }

        #endregion 기본 메소드
    }

    #endregion CmdTechnicalSupport

    #region CmdImageEditor


    // TODO : TransactionAttribute - TransactionMode.Manual 설정 구현 (2024.08.09 jbh)
    // 참고 URL - https://manzoo.tistory.com/107
    [Transaction(TransactionMode.Manual)]       // Transaction 사용이 필요한 경우 (Document 내에서 수정/삭제/추가 등의 작업이 필요한 경우)
    [Regeneration(RegenerationOption.Manual)]
    /// <summary>
    /// 3. 이미지 편집
    /// </summary>
    class CmdImageEditor : IExternalCommand
    {
        #region 프로퍼티

        // TODO : 아래 주석친 코드 필요시 참고 (2024.06.28 jbh)
        // public static Autodesk.Revit.Creation.Application app = null;
        // public static Document doc;
        // public static UIApplication uiapp;
        // public static UIDocument uidoc;

        /// <summary>
        /// 이미지 편집 폼 객체 
        /// </summary>
        private static ImageEditorForm ImageEditorForm { get; set; }


        /// <summary>
        /// Revit UI 애플리케이션 객체
        /// </summary>
        private UIApplication RevitUIApp { get; set; }

        // TODO : 프로퍼티 "UIDoc" 필요시 사용 예정 (2024.06.24 jbh)
        /// <summary>
        /// 활성화된 Revit 문서 
        /// </summary>
        // private UIDocument UIDoc { get; set; }

        // TODO : 프로퍼티 "RevitDoc" 필요시 사용 예정 (2024.06.24 jbh)
        /// <summary>
        /// Revit 문서 
        /// </summary>
        // private Document RevitDoc { get; set; }

        #endregion 프로퍼티

        #region ShowForm

        /// <summary>
        /// 이미지 편집 폼 화면(ImageEditorForm) Modaless(.show()) 형식 출력 
        /// </summary>
        private void ShowForm(UIApplication rvUIApp)
        {
            // Modaless 폼 객체가 null이거나 삭제된 경우 
            if (ImageEditorForm is null || ImageEditorForm.IsDisposed)
            {
                ImageEditorRequestHandler imageHandler = new ImageEditorRequestHandler();    // 이미지 편집 외부 요청 핸들러 객체 imageHandler 생성 
                ExternalEvent exEvent = ExternalEvent.Create(imageHandler);      // 이미지 편집 폼 객체(ImageEditorForm)가 사용할 외부 이벤트 생성 

                //ImageEditorForm = new ImageEditorForm(imageHandler, exEvent, rvUIApp);
                //ImageEditorForm = new ImageEditorForm(imageHandler, exEvent);
                ImageEditorForm = new ImageEditorForm(exEvent, imageHandler);

                ImageEditorForm.Show();   // Show 메서드 호출해서 화면 출력하기  
                // ImageEditorForm.Show(System.Windows.Forms.Control.FromHandle(System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle));
            }
            else ImageEditorForm.Activate();   // 이미지 편집 폼(ImageEditorForm.cs) 다시 활성화
        }

        #endregion ShowForm

        #region SetSelectedImageFilePath

        /// <summary>
        /// 객체 선택한 이미지 파일 경로 셋팅 
        /// </summary>
        public static void SetSelectedImageFilePath(bool pIsSelectedElement, string pSelectedImageFilePath)
        {
            ImageEditorForm.IsSelectedElement = pIsSelectedElement;
            ImageEditorForm.SelectedImageFilePath = string.Empty;
            ImageEditorForm.SelectedImageFilePath = pSelectedImageFilePath;
        }

        #endregion SetSelectedImageFilePath

        #region GetInsertedImageFilePath

        /// <summary>
        /// 이미지 편집 하고자 하는 이미지 파일 경로 가져오기 
        /// </summary>
        public static string GetInsertedImageFilePath()
        {
            return ImageEditorForm.InsertedImageFilePath;
        }

        #endregion GetInsertedImageFilePath

        #region SetInsertedImageFile

        /// <summary>
        /// 이미지 편집 처리 여부 셋팅 
        /// </summary>
        public static void SetInsertedImageFile(bool pIsInsertedImageFile)
        {
            ImageEditorForm.IsInsertedImageFile = pIsInsertedImageFile;
        }

        #endregion SetInsertedImageFile

        #region WakeFormUp

        /// <summary>
        /// 대기 상태에서 이미지 편집 폼 화면(ImageEditorForm) 깨우기
        /// </summary>
        public static void WakeFormUp()
        {
            if (ImageEditorForm is not null) ImageEditorForm.WakeUp();
        }

        #endregion WakeFormUp

        #region 기본 메소드 

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

            try
            {
                Log.Information(Logger.GetMethodPath(currentMethod) + "이미지  Command - Execute 시작");

                RevitUIApp = commandData.Application;
                // UIDoc = RevitUIApp.ActiveUIDocument;
                // RevitDoc = UIDoc.Document;

                ShowForm(RevitUIApp);

                Log.Information(Logger.GetMethodPath(currentMethod) + "이미지 편집 Command - Execute 종료");

                return Result.Succeeded;
            }
            catch (Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                TaskDialog.Show(HTSHelper.ErrorTitle, ex.Message);
                return Result.Failed;
            }

            #region Sample

            // TODO : 아래 주석친 테스트 코드 필요시 참고 (2024.06.24 jbh)
            //            uiapp = commandData.Application;
            //            uidoc = uiapp.ActiveUIDocument;
            //            doc = uidoc.Document;

            //            TestImageEditorForm mainform = new TestImageEditorForm(uidoc);

            //            TestImageEditorForm.runtype = 0;

            //            DialogResult result = DialogResult.None;

            //            while (true)
            //            {
            //                result = mainform.ShowDialog();

            //                if (result == DialogResult.OK || result == DialogResult.Cancel)
            //                {
            //                    break;
            //                }

            //                if (result == DialogResult.Retry)
            //                {
            //                    try
            //                    {
            //                        Reference r = uidoc.Selection.PickObject(ObjectType.Element);

            //                        Element element = doc.GetElement(r);
            //#if (R2024 || R2025)
            //                        //if (element.Category.Id == new ElementId((long)-2000560))
            //                        if (element.Category.Id == new ElementId((long)BuiltInCategory.OST_RasterImages))
            //                        {
            //                            Element type = doc.GetElement(element.GetTypeId());
            //                            Parameter pa = type.get_Parameter(BuiltInParameter.RASTER_SYMBOL_FILENAME);
            //                            string path = pa.AsString();

            //                            TestImageEditorForm.revitimagefilepath = path;

            //                            TestImageEditorForm.runtype = 1;
            //                        }
            //#else
            //                                    if (element.Category.Id == new ElementId(-2000560))
            //                                    {
            //                                        Element type = doc.GetElement(element.GetTypeId());
            //                                        Parameter pa = type.get_Parameter(BuiltInParameter.RASTER_SYMBOL_FILENAME);
            //                                        string path = pa.AsString();

            //                                        TestImageEditorForm.revitimagefilepath = path;

            //                                        TestImageEditorForm.runtype = 1;
            //                                    }
            //#endif

            //                    }
            //                    catch
            //                    {
            //                        MessageBox.Show("잘못된 객체 입니다.", "확인");
            //                        TestImageEditorForm.runtype = 0;
            //                    }
            //                }

            //            }

            //            if (TestImageEditorForm.imagefilepath2 != null && TestImageEditorForm.okchk == true)
            //            {
            //                try
            //                {
            //#if (R2016 || R2017 || R2018 || R2019 || R2020)

            //                            ImageImportOptions opt = new ImageImportOptions();

            //                            Element elemet;
            //                            using (Transaction t = new Transaction(doc))
            //                            {
            //                                t.Start("Start");

            //                                doc.Import(ImageEditorForm.imagefilepath2, opt, doc.ActiveView, out elemet);

            //                                t.Commit();
            //                            }
            //#endif

            //#if (R2021 || R2022)



            //                using (Transaction t = new Transaction(doc))
            //                {
            //                    t.Start("Start");

            //                    ImageTypeOptions typeOptions = new ImageTypeOptions(TestImageEditorForm.imagefilepath2, true, ImageTypeSource.Import);
            //                    ImageType imageType = ImageType.Create(doc, TestImageEditorForm.imagefilepath2);

            //                    ImagePlacementOptions placementOptions = new ImagePlacementOptions();

            //                    ImageInstance imageInstance = ImageInstance.Create(doc, doc.ActiveView, imageType.Id, placementOptions);

            //                    t.Commit();
            //                }

            //#endif

            //#if (R2021 || R2022 || R2023 || R2024 || R2025)



            //                    using (Transaction t = new Transaction(doc))
            //                    {
            //                        t.Start("Start");

            //                        ImageTypeOptions typeOptions = new ImageTypeOptions(TestImageEditorForm.imagefilepath2, false, ImageTypeSource.Import);

            //                        ImageType imageType = ImageType.Create(doc, typeOptions);

            //                        ImagePlacementOptions placementOptions = new ImagePlacementOptions();

            //                        placementOptions.PlacementPoint = Autodesk.Revit.DB.BoxPlacement.Center;
            //                        placementOptions.Location = new XYZ(0, 0, 0);

            //                        ImageInstance imageInstance = ImageInstance.Create(doc, doc.ActiveView, imageType.Id, placementOptions);

            //                        t.Commit();
            //                    }

            //#endif
            //                }
            //                catch (Exception ex)
            //                {
            //                    MessageBox.Show(ex.Message, "확인");
            //                }


            //            }
            //            return Result.Succeeded;

            #endregion Sample
        }

        #endregion 기본 메소드
    }
    #endregion CmdImageEditor

    #region Sample

    #endregion Sample
}
