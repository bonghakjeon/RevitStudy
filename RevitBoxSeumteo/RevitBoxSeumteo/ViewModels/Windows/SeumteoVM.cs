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

        /// <summary>
        /// 검색 Command
        /// </summary>
        public ICommand SearchCommand { get; set; }

        // TODO : ICommand 클래스 객체 "DoSomethingCommand" 필요시 싱글톤 패턴으로 구현 예정 (2023.10.10 jbh)
        /// <summary>
        /// DoSomethingCommand - 싱글톤 패턴
        /// </summary>
        //public ICommand DoSomethingCommand 
        //{ 
        //    get
        //    {
        //        // 싱글톤 패턴
        //        if (_DoSomethingCommand == null)
        //        {
        //            _DoSomethingCommand = new ButtonCommand(DoSomething, CanExecuteMethod);
        //        }
        //        return _DoSomethingCommand;
        //    } 
        //    set => _DoSomethingCommand = value; 
        //}
        //private ICommand _DoSomethingCommand;

        // TODO : ICommand 클래스 객체 "ShowMessageCommand" 필요시 싱글톤 패턴으로 구현 예정 (2023.10.10 jbh)
        /// <summary>
        /// ShowMessageCommand - 싱글톤 패턴
        /// </summary>
        //public ICommand ShowMessageCommand 
        //{ 
        //    get
        //    {
        //        // 싱글톤 패턴 
        //        if (_ShowMessageCommand == null)
        //        {
        //            _ShowMessageCommand = new ButtonCommand(ShowMessageBox, CanExecuteMethod);
        //        }
        //        return _ShowMessageCommand;
        //    }
        //    set => _ShowMessageCommand = value; 
        //}
        //private ICommand _ShowMessageCommand;

        #endregion 프로퍼티 

        #region 생성자 

        public SeumteoVM()
        {
            DoSomethingCommand = new ButtonCommand(DoSomething, CanExecuteMethod);
            ShowMessageCommand = new ButtonCommand(ShowMessageBox, CanExecuteMethod);
            SearchCommand      = new ButtonCommand(SearchAsync, CanExecuteMethod); 
            // SearchCommand = new ButtonCommand(Search, CanExecuteMethod);
        }

        #endregion 생성자 

        #region 기본 메소드 

        // TODO : 메서드 "CanExecuteMethod" 필요시 수정 예정 (2023.10.6 jbh)
        private bool CanExecuteMethod(object obj)
        {
            return true;
        }

        #endregion 기본 메소드

        #region Search

        //private void Search(object obj)
        //{
        //    try
        //    {
        //        MessageBox.Show("검색 기능 구현 예정");
        //        return;
        //    }
        //    catch (Exception e)
        //    {
        //        // TODO : 추후 로그 클래스(Logger.cs) 및 에러 로그(Error) 구현 예정 (2023.10.11 jbh)  
        //        // 참고할 프로젝트 파일 - "CobimUtil"
        //        // Log.Error(LogHelper.GetMethodPath(currentMethod) + e.Message);
        //        MessageBox.Show(e.Message);
        //    }
        //    return;
        //}

        //#endregion Search

        //#region SearchAsync

        private async Task SearchAsync(object obj)
        {
            try
            {
                MessageBox.Show("검색 기능 구현 예정");
                return;
            }
            catch (Exception e)
            {
                // TODO : 추후 로그 클래스(Logger.cs) 및 에러 로그(Error) 구현 예정 (2023.10.11 jbh)  
                // 참고할 프로젝트 파일 - "CobimUtil"
                // Log.Error(LogHelper.GetMethodPath(currentMethod) + e.Message);
                MessageBox.Show(e.Message);
            }
            return;
        }

        #endregion SearchAsync

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
