using System;
using System.Threading;

using DevExpress.XtraWaitForm;
using DevExpress.XtraSplashScreen;

namespace HTSBIM2019.UI.UpdaterLoading
{
    // TODO : 업데이터 + Triggers 등록 작업 진행시 대기 처리 C# 윈폼 DevExpress WaitForm 화면 구현 (2024.04.24 jbh)
    // 유튜브 참고 URL - https://www.youtube.com/watch?v=JNdYEwC0TOY
    // 참고 URL - https://codekiller.tistory.com/1679

    public partial class UpdaterLoadingForm : WaitForm
    {
        #region 프로퍼티 

        public enum WaitFormCommand
        {

        }

        #endregion 프로퍼티

        #region 생성자

        public UpdaterLoadingForm()
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

        #region ShowLoadingForm

        /// <summary>
        /// 업데이터 + Triggers 등록 대기처리 화면 출력
        /// </summary>
        public void ShowLoadingForm()
        {
            // TODO : 사용 기록 관리 Updater + Triggers 등록 대기 처리 화면 (WaitForm) 출력 기능 (SplashScreenManager.ShowForm()) 구현 (2024.04.24 jbh)
            // 참고 URL - https://chat.openai.com/c/710da82a-ca7f-4dba-9aba-2266bf1f9019
            // 대기 중인 동안에 실행될 작업을 시작합니다.
            // 사용 기록 관리 매개변수 생성 대기 처리 화면(WaitForm - CreateParams) "Revit 응용 프로그램"의 가운데로 출력
            if(SplashScreenManager.Default is null) SplashScreenManager.ShowForm(this.ParentForm, typeof(UpdaterLoadingForm), true, true, false);

            // Thread.Sleep(10000);   // 테스트 코드 - 사용 기록 관리 Updater + Triggers 등록 대기 처리 화면 (WaitForm) 출력 후 10초간 대기 필요시 사용 (지정된 시간 동안 현재 동작하는 쓰레드만 일시 중단)
        }

        #endregion ShowLoadingForm

        #region CloseLoadingForm

        /// <summary>
        /// 업데이터 + Triggers 등록 대기처리 화면 종료
        /// </summary>
        public void CloseLoadingForm()
        {
            // TODO : 사용 기록 관리 Updater + Triggers 등록 대기 처리 화면 (WaitForm) 종료 기능(SplashScreenManager.CloseForm) 구현 (2024.04.24 jbh)
            // 대기 중인 동안에 실행될 작업이 완료되면 대기 화면 닫기
            if(SplashScreenManager.Default is not null) SplashScreenManager.CloseForm(false);
        }

        #endregion CloseLoadingForm
    }
}