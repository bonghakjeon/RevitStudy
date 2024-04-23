using System;
using System.Threading;

using HTSBIM2019.Common.Managers;

using Autodesk.Revit.DB;

using DevExpress.XtraWaitForm;
using DevExpress.XtraSplashScreen;

namespace HTSBIM2019.UI.CreateParams
{
    public partial class CreateParams : WaitForm
    {
        #region 프로퍼티 

        public enum WaitFormCommand
        {

        }

        /// <summary>
        /// Revit 문서 안에 포함된 객체(Element)를 검색 및 필터링
        /// 참고 URL - https://www.revitapidocs.com/2024/263cf06b-98be-6f91-c4da-fb47d01688f3.htm
        /// </summary>
        private FilteredElementCollector Collector { get; set; }

        #endregion 프로퍼티

        #region 생성자

        public CreateParams()
        {
            InitializeComponent();
            this.progressPanel.AutoHeight = true;
        }

        #endregion 생성자 

        #region 기본 메소드 

        public override void SetCaption(string caption)
        {
            base.SetCaption(caption);
            this.progressPanel.Caption = caption;
        }

        public override void SetDescription(string description)
        {
            base.SetDescription(description);
            this.progressPanel.Description = description;
        }

        public override void ProcessCommand(Enum cmd, object arg)
        {
            base.ProcessCommand(cmd, arg);
        }

        #endregion 기본 메소드 

        #region ToCreateUpdaterParameter

        /// <summary>
        /// 활성화된 Revit 문서에 사용 기록 관리 매개변수 추가 
        /// </summary>
        public void ToCreateUpdaterParameter(Document rvDoc)
        {
            // TODO : 사용 기록 관리 매개변수 생성 대기 처리 화면 (WaitForm) 출력 기능 (SplashScreenManager.ShowForm()) 및 종료 기능 (SplashScreenManager.CloseForm) 구현 (2024.01.30 jbh)
            // 참고 URL - https://chat.openai.com/c/710da82a-ca7f-4dba-9aba-2266bf1f9019
            // 대기 중인 동안에 실행될 작업을 시작합니다.
            // 사용 기록 관리 매개변수 생성 대기 처리 화면(WaitForm - CreateParams) "Revit 응용 프로그램"의 가운데로 출력
            SplashScreenManager.ShowForm(this.ParentForm, typeof(CreateParams), true, true, false);

            Thread.Sleep(1000);   // 테스트 코드 - 사용 기록 관리 매개변수 생성 대기 처리 화면 (WaitForm) 출력 후 10초간 대기 필요시 사용 (지정된 시간 동안 현재 동작하는 쓰레드만 일시 중단)

            // 대기 중인 작업 실행
            CategoryManager.CreateCategorySet(rvDoc);   // 카테고리셋 추가 메서드 "CreateCategorySet" 실행 

            // 대기 중인 동안에 실행될 작업이 완료되면 대기 화면 닫기
            SplashScreenManager.CloseForm(false);
        }

        #endregion ToCreateUpdaterParameter
    }
}