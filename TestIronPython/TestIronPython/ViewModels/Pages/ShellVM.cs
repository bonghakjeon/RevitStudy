using System;
using System.Windows;
using System.Windows.Input;
using Stylet;
using StyletIoC;
using TestIronPython.Commands;
using TestIronPython.Service;
using TestIronPython.Views.Pages;

namespace TestIronPython.ViewModels.Pages
{
    public class ShellVM : Screen
    {
        #region 프로퍼티 

        // IWindowManager 싱글톤으로 구현
        [Inject]
        //public IWindowManager WindowManager { get; set; }

        //public IContainer Container { get; set; }

        public ICommand DoSomethingCommand { get; set; }

        public ICommand ShowMessageCommand { get; set; }

        #endregion 프로퍼티 

        #region 생성자 

        public ShellVM()
        {
            DoSomethingCommand = new ButtonCommand(DoSomething, CanExecuteMethod);
            ShowMessageCommand = new ButtonCommand(ShowMessageBox, CanExecuteMethod);
        }

        #endregion 생성자 

        #region 기본 메소드 

        protected override void OnInitialActivate()
        {
            base.OnInitialActivate();
        }

        protected override void OnActivate()
        {
            base.OnActivate();
        }

        protected override void OnDeactivate()
        {
            base.OnDeactivate();
        }

        // TODO : 메서드 "CanExecuteMethod" 필요시 수정 예정 (2023.10.4 jbh)
        private bool CanExecuteMethod(object obj)
        {
            return true;
        }

        #endregion 기본 메소드 

        #region ToShowData

        // TODO : ShellV 화면 출력 메서드 "ToShowData" 필요시 구현 예정 (2023.10.4 jbh)
        //public ShellVM ToShowData()
        //{
        //    this.WindowManager.ShowDialog((object)this);
        //    return this;
        //}

        #endregion ToShowData

        #region TEST

        public void DoSomething(object param)
        {
            // Debug.WriteLine("DoSomething called");
            MessageBox.Show("ShellVM called");
        }

        public void ShowMessageBox(object param)
        {
            // Debug.WriteLine("DoSomething called");
            MessageBox.Show("Welcome to Revit 2024");
            //new CommonMsgBoxVM(container).Set("다운로드 실패", e.Message).ShowDialog();
            // EventAggregator.PublishOnUIThread(msg);
        }

        #endregion TEST

    }
}
