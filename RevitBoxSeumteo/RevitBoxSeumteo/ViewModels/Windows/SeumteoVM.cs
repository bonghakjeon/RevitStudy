using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using RevitBoxSeumteo.Commands;

namespace RevitBoxSeumteo.ViewModels.Windows
{
    public class SeumteoVM
    {
        #region 프로퍼티 

        public ICommand DoSomethingCommand { get; set; }

        public ICommand ShowMessageCommand { get; set; }

        #endregion 프로퍼티 

        #region 생성자 

        public SeumteoVM()
        {
            DoSomethingCommand = new ButtonCommand(DoSomething, CanExecuteMethod);
            ShowMessageCommand = new ButtonCommand(ShowMessageBox, CanExecuteMethod);
        }

        #endregion 생성자 

        #region 기본 메소드 

        // TODO : 메서드 "CanExecuteMethod" 필요시 수정 예정 (2023.10.6 jbh)
        private bool CanExecuteMethod(object obj)
        {
            return true;
        }

        #endregion 기본 메소드

        #region TEST

        public void DoSomething(object param)
        {
            // Debug.WriteLine("DoSomething called");
            MessageBox.Show("SeumteoVM called");
        }

        public void ShowMessageBox(object param)
        {
            // Debug.WriteLine("DoSomething called");
            MessageBox.Show("Welcome to Revit 2024");
        }

        #endregion TEST
    }
}
