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
using RevitBoxSeumteo.Common.LogManager;
using RevitBoxSeumteo.Services.Page;
using RevitBox.Data.Models.Common.AISParam;
using RevitBox.Data.Models.RevitBoxBase.AISParams;
using static RevitBox.Data.Models.RevitBoxBase.AISParams.AISParamsView;
// using RevitBoxSeumteo.Models.RevitBoxBase.ParamsCreate;

namespace RevitBoxSeumteo.ViewModels.Windows
{
    public class AISParamsBoardVM : PageBase<AISParamsView>
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
        /// TestData
        /// </summary>
        public TestData Data
        {
            get
            {
                // 싱글톤 패턴 
                if (_Data == null)
                {
                    _Data = new TestData();
                }
                return _Data;
            }
            set => _Data = value;
        }
        private TestData _Data;

        /// <summary>
        /// ComboBox - AIS 매개변수 타입 
        /// </summary>
        public List<AISParams_Type> ParamsTypeList { get => Data.GetAllDivTypes(); set { _ParamsTypeList = value; NotifyOfPropertyChange(); } }
        private List<AISParams_Type> _ParamsTypeList = new List<AISParams_Type>();

        /// <summary>
        /// ComboBox - AIS 매개변수 타입에서 선택된 값을 담을 프로퍼티
        /// </summary>
        public AISParams_Type SelectedParamsType 
        { 
            get => _SelectedParamsType; 
            set 
            {
                _SelectedParamsType = value; 
                NotifyOfPropertyChange("SelectedParamsType");

                ParamsValueList = Data.FindSubDivValues(SelectedParamsType.DivCode);
                NotifyOfPropertyChange("ParamsValueList");
            } 
        }
        private AISParams_Type _SelectedParamsType;

        /// <summary>
        /// ComboBox - AIS 매개변수 속성값
        /// </summary>
        public List<AISParams_Value> ParamsValueList { get => _ParamsValueList; set { _ParamsValueList = value; NotifyOfPropertyChange("ParamsValueList"); } }
        private List<AISParams_Value> _ParamsValueList = new List<AISParams_Value>();

        /// <summary>
        /// ComboBox - AIS 매개변수 속성값에서 선택된 값을 담을 프로퍼티
        /// </summary>
        public AISParams_Value SelectedParamsValue
        { 
            get => _SelectedParamsValue; 
            set 
            {
                _SelectedParamsValue = value; 
                NotifyOfPropertyChange("SelParamsValue"); 
                if (_SelectedParamsValue != null)
                {
                    SeletedText = string.Format("[{0} : {1}]", _SelectedParamsValue.SubDivCode, _SelectedParamsValue.SubDivName);
                }
                else
                {
                    SeletedText = String.Empty;
                }
                NotifyOfPropertyChange("SeletedText");
            } 
        }
        private AISParams_Value _SelectedParamsValue;


        public string SeletedText { get; set; }

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

        public AISParamsBoardVM()
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
            catch (Exception ex)
            {
                // TODO : 추후 로그 클래스(Logger.cs) 및 에러 로그(Error) 구현 예정 (2023.10.11 jbh)  
                // 참고할 프로젝트 파일 - "CobimUtil"
                Log.Error(Logger.GetMethodPath(currentMethod) + ex.Message);
                MessageBox.Show(ex.Message);
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
        //    catch (Exception ex)
        //    {
        //        // TODO : 추후 로그 클래스(Logger.cs) 및 에러 로그(Error) 구현 예정 (2023.10.11 jbh)  
        //        // 참고할 프로젝트 파일 - "CobimUtil"
        //        // Log.Error(LogHelper.GetMethodPath(currentMethod) + ex.Message);
        //        MessageBox.Show(ex.Message);
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
            catch (Exception ex)
            {
                // TODO : 추후 로그 클래스(Logger.cs) 및 에러 로그(Error) 구현 예정 (2023.10.11 jbh)  
                // 참고할 프로젝트 파일 - "CobimUtil"
                Log.Error(Logger.GetMethodPath(currentMethod) + ex.Message);
                MessageBox.Show(ex.Message);
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
            catch (Exception ex)
            {
                // TODO : 추후 로그 클래스(Logger.cs) 및 에러 로그(Error) 구현 예정 (2023.10.11 jbh)  
                // 참고할 프로젝트 파일 - "CobimUtil"
                Log.Error(Logger.GetMethodPath(currentMethod) + ex.Message);
                MessageBox.Show(ex.Message);
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
            catch (Exception ex)
            {
                // TODO : 추후 로그 클래스(Logger.cs) 및 에러 로그(Error) 구현 예정 (2023.10.11 jbh)  
                // 참고할 프로젝트 파일 - "CobimUtil"
                Log.Error(Logger.GetMethodPath(currentMethod) + ex.Message);
                MessageBox.Show(ex.Message);
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
            catch (Exception ex)
            {
                // TODO : 추후 로그 클래스(Logger.cs) 및 에러 로그(Error) 구현 예정 (2023.10.11 jbh)  
                // 참고할 프로젝트 파일 - "CobimUtil"
                Log.Error(Logger.GetMethodPath(currentMethod) + ex.Message);
                MessageBox.Show(ex.Message);
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
            catch (Exception ex)
            {
                // TODO : 추후 로그 클래스(Logger.cs) 및 에러 로그(Error) 구현 예정 (2023.10.11 jbh)  
                // 참고할 프로젝트 파일 - "CobimUtil"
                Log.Error(Logger.GetMethodPath(currentMethod) + ex.Message);
                MessageBox.Show(ex.Message);
                return;
            }
        }

        #endregion Exit

        #region TEST

        public void DoSomething(object param)
        {
            // Debug.WriteLine("DoSomething called");
            MessageBox.Show("AISParamsBoardVM called");
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
