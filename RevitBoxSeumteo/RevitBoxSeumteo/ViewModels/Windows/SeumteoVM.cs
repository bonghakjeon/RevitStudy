using Serilog;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using RevitBoxSeumteo.Commands;
using RevitBoxSeumteo.Common.AISParam;
using RevitBoxSeumteo.Common.LogManager;

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

        /// <summary>
        /// 내보내기 Command
        /// </summary>
        public ICommand ExportCommand { get; set; }

        /// <summary>
        /// 가져오기 Command
        /// </summary>
        public ICommand ImportCommand { get; set; }

        /// <summary>
        /// 일괄변경 Command
        /// </summary>
        public ICommand ChangeCommand { get; set; }

        /// <summary>
        /// 종료 Command
        /// </summary>
        public ICommand ExitCommand { get; set; }

        /// <summary>
        /// 반환형 void 메소드 타입
        /// </summary>
        // public const string MethodType = "void";

        /// <summary>
        /// 비동기 메소드 타입
        /// </summary>
        // public const string AsyncMethodType = "async";

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
            DoSomethingCommand = new ButtonCommand(AISParamsHelper.MethodType, DoSomething, CanExecuteMethod);
            ShowMessageCommand = new ButtonCommand(AISParamsHelper.MethodType, ShowMessageBox, CanExecuteMethod);
            SearchCommand      = new ButtonCommand(AISParamsHelper.AsyncMethodType, SearchAsync, CanExecuteMethod);
            // SearchCommand = new ButtonCommand(Search, CanExecuteMethod);
            ExportCommand      = new ButtonCommand(AISParamsHelper.AsyncMethodType, ExportExcelAsync, CanExecuteMethod);
            ImportCommand      = new ButtonCommand(AISParamsHelper.AsyncMethodType, ImportExcelAsync, CanExecuteMethod);
            ChangeCommand      = new ButtonCommand(AISParamsHelper.AsyncMethodType, ChangeDataAsync, CanExecuteMethod);
            ExitCommand        = new ButtonCommand(AISParamsHelper.MethodType, Exit, CanExecuteMethod);

            SeumteoTypeCreate();
        }

        #endregion 생성자 

        #region 기본 메소드 

        // TODO : 메서드 "CanExecuteMethod" 필요시 수정 예정 (2023.10.6 jbh)
        private bool CanExecuteMethod(object obj)
        {
            return true;
        }

        #endregion 기본 메소드

        #region SeumteoTypeCreate

        /// <summary>
        /// 카테고리, 속성값 필터 ComboBox 바인딩 객체 리스트 생성 
        /// </summary>
        private void SeumteoTypeCreate()
        {
            // TODO : 로그 기록시 현재 실행 중인 메서드 위치 기록하기 (2023.10.10 jbh)
            // 참고 URL - https://slaner.tistory.com/73
            // 참고 2 URL - https://stackoverflow.com/questions/4132810/how-can-i-get-a-method-name-with-the-namespace-class-name
            // 참고 3 URL - https://stackoverflow.com/questions/44153/can-you-use-reflection-to-find-the-name-of-the-currently-executing-method
            var currentMethod = MethodBase.GetCurrentMethod();

            try
            {
                MessageBox.Show("카테고리, 속성값 필터 ComboBox 바인딩 리스트 구현 예정");
                Log.Information(Logger.GetMethodPath(currentMethod) + "카테고리, 속성값 필터 ComboBox 바인딩 리스트 구현 예정");

                // TODO : 테스트 코드 - 강제로 오류 발생하도록 Exception 생성 하도록 구현 (필요시 사용) (2023.10.19 jbh)
                // 참고 URL - https://morm.tistory.com/187
                // 참고 2 URL - https://learn.microsoft.com/ko-kr/dotnet/csharp/fundamentals/exceptions/creating-and-throwing-exceptions
                // throw new Exception();
            }
            catch (Exception e)
            {
                // TODO : 추후 로그 클래스(Logger.cs) 및 에러 로그(Error) 구현 예정 (2023.10.11 jbh)  
                // 참고할 프로젝트 파일 - "CobimUtil"
                Log.Error(Logger.GetMethodPath(currentMethod) + e.Message);
                MessageBox.Show(e.Message);
                return;
            }
        }

        #endregion SeumteoTypeCreate

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
            // TODO : 로그 기록시 현재 실행 중인 메서드 위치 기록하기 (2023.10.10 jbh)
            // 참고 URL - https://slaner.tistory.com/73
            // 참고 2 URL - https://stackoverflow.com/questions/4132810/how-can-i-get-a-method-name-with-the-namespace-class-name
            // 참고 3 URL - https://stackoverflow.com/questions/44153/can-you-use-reflection-to-find-the-name-of-the-currently-executing-method
            var currentMethod = MethodBase.GetCurrentMethod();

            try
            {
                MessageBox.Show("검색 기능 구현 예정");
                return;
            }
            catch (Exception e)
            {
                // TODO : 추후 로그 클래스(Logger.cs) 및 에러 로그(Error) 구현 예정 (2023.10.11 jbh)  
                // 참고할 프로젝트 파일 - "CobimUtil"
                Log.Error(Logger.GetMethodPath(currentMethod) + e.Message);
                MessageBox.Show(e.Message);
                // return;
            }
            return;
        }

        #endregion SearchAsync

        #region ExportExcelAsync

        public async Task ExportExcelAsync(object obj)
        {
            // TODO : 로그 기록시 현재 실행 중인 메서드 위치 기록하기 (2023.10.10 jbh)
            // 참고 URL - https://slaner.tistory.com/73
            // 참고 2 URL - https://stackoverflow.com/questions/4132810/how-can-i-get-a-method-name-with-the-namespace-class-name
            // 참고 3 URL - https://stackoverflow.com/questions/44153/can-you-use-reflection-to-find-the-name-of-the-currently-executing-method
            var currentMethod = MethodBase.GetCurrentMethod();

            try
            {
                MessageBox.Show("엑셀 내보내기 구현 예정");
                return;
            }
            catch (Exception e)
            {
                // TODO : 추후 로그 클래스(Logger.cs) 및 에러 로그(Error) 구현 예정 (2023.10.11 jbh)  
                // 참고할 프로젝트 파일 - "CobimUtil"
                Log.Error(Logger.GetMethodPath(currentMethod) + e.Message);
                MessageBox.Show(e.Message);
                return;
            }
        }

        #endregion ExportExcelAsync

        #region ImportExcelAsync

        public async Task ImportExcelAsync(object obj)
        {
            // TODO : 로그 기록시 현재 실행 중인 메서드 위치 기록하기 (2023.10.10 jbh)
            // 참고 URL - https://slaner.tistory.com/73
            // 참고 2 URL - https://stackoverflow.com/questions/4132810/how-can-i-get-a-method-name-with-the-namespace-class-name
            // 참고 3 URL - https://stackoverflow.com/questions/44153/can-you-use-reflection-to-find-the-name-of-the-currently-executing-method
            var currentMethod = MethodBase.GetCurrentMethod();

            try
            {
                MessageBox.Show("엑셀 가져오기 구현 예정");
                return;
            }
            catch (Exception e)
            {
                // TODO : 추후 로그 클래스(Logger.cs) 및 에러 로그(Error) 구현 예정 (2023.10.11 jbh)  
                // 참고할 프로젝트 파일 - "CobimUtil"
                Log.Error(Logger.GetMethodPath(currentMethod) + e.Message);
                MessageBox.Show(e.Message);
                return;
            }
        }

        #endregion ImportExcelAsync

        #region ChangeDataAsync

        public async Task ChangeDataAsync(object obj)
        {
            // TODO : 로그 기록시 현재 실행 중인 메서드 위치 기록하기 (2023.10.10 jbh)
            // 참고 URL - https://slaner.tistory.com/73
            // 참고 2 URL - https://stackoverflow.com/questions/4132810/how-can-i-get-a-method-name-with-the-namespace-class-name
            // 참고 3 URL - https://stackoverflow.com/questions/44153/can-you-use-reflection-to-find-the-name-of-the-currently-executing-method
            var currentMethod = MethodBase.GetCurrentMethod();

            try
            {
                MessageBox.Show("일괄변경 구현 예정");
                return;
            }
            catch (Exception e)
            {
                // TODO : 추후 로그 클래스(Logger.cs) 및 에러 로그(Error) 구현 예정 (2023.10.11 jbh)  
                // 참고할 프로젝트 파일 - "CobimUtil"
                Log.Error(Logger.GetMethodPath(currentMethod) + e.Message);
                MessageBox.Show(e.Message);
                return;
            }
        }

        #endregion ChangeDataAsync

        #region Exit

        private void Exit(object obj)
        {
            // TODO : 로그 기록시 현재 실행 중인 메서드 위치 기록하기 (2023.10.10 jbh)
            // 참고 URL - https://slaner.tistory.com/73
            // 참고 2 URL - https://stackoverflow.com/questions/4132810/how-can-i-get-a-method-name-with-the-namespace-class-name
            // 참고 3 URL - https://stackoverflow.com/questions/44153/can-you-use-reflection-to-find-the-name-of-the-currently-executing-method
            var currentMethod = MethodBase.GetCurrentMethod();

            try
            {
                MessageBox.Show("종료 구현 예정");
            }
            catch (Exception e)
            {
                // TODO : 추후 로그 클래스(Logger.cs) 및 에러 로그(Error) 구현 예정 (2023.10.11 jbh)  
                // 참고할 프로젝트 파일 - "CobimUtil"
                Log.Error(Logger.GetMethodPath(currentMethod) + e.Message);
                MessageBox.Show(e.Message);
                return;
            }
        }

        #endregion Exit

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

        #region Sample

        #endregion Sample
    }
}
