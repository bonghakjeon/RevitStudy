using Serilog;
using System;
using System.Threading;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using RevitBoxSeumteo.Commands;
using RevitBoxSeumteo.Common.AISParam;
using RevitBoxSeumteo.Converters;
using RevitBoxSeumteo.Common.LogManager;
using RevitBoxSeumteoNet;

namespace RevitBoxSeumteo.ViewModels.Windows
{
    // TODO : Background Worker 클래스 사용해서 ProgressBar 화면 뷰(ParamsCreateV.xaml) + 뷰모델(ParamsCreateVM.cs) 구현 (2023.10.30 jbh)
    // 참고 URL - http://ojc.asia/bbs/board.php?bo_table=WPF&wr_id=40
    // 참고 2 URL - https://afsdzvcx123.tistory.com/entry/WPF-WPF-ProgressBar-%EC%82%AC%EC%9A%A9%ED%95%98%EA%B8%B0%ED%94%84%EB%A1%9C%EA%B7%B8%EB%A0%88%EC%8A%A4%EB%B0%94
    // 참고 3 URL - https://stackoverflow.com/questions/14262220/wpf-progressbar-with-value-databinding
    // 참고 4 URL - https://afsdzvcx123.tistory.com/entry/WPF-WPF-ProgressBar-%EC%BB%A8%ED%8A%B8%EB%A1%A4-MVVM-%ED%8C%A8%ED%84%B4%EC%9C%BC%EB%A1%9C-%EA%B5%AC%ED%98%84%ED%95%98%EA%B8%B0
    // 참고 5 URL - https://www.codeproject.com/Questions/5292748/Open-new-progressbar-window-in-WPF-with-MVVM-patte
    public class ParamsCreateVM : BindableBase
    {
        #region 프로퍼티

        /// <summary>
        /// AIS 매개변수 생성 Command
        /// </summary>
        public ICommand ParamsCreateCommand { get; set; }

        /// <summary>
        /// ParamsCreateV.xaml 비트맵 이미지(BitmapSource)
        /// </summary>
        public BitmapSource ParamsSource { get; set; }

        /// <summary>
        /// Title - AIS_매개변수 생성
        /// </summary>
        public string TitleParamsCreate 
        { 
            get
            {
                return _TitleParamsCreate;
            }
            set
            {
                _TitleParamsCreate = value;
                Changed();
            }
        }
        private string _TitleParamsCreate = string.Empty;

        /// <summary>
        /// Text - 버튼 AIS 매개변수 생성을 클릭하세요. / AIS 매개변수 생성 중...
        /// </summary>
        public string TxtParamsCreate 
        { 
            get
            {
                return _TxtParamsCreate;
            }
            set
            {
                _TxtParamsCreate = value;
                Changed();
            }
        }
        private string _TxtParamsCreate = string.Empty;


        /// <summary>
        /// Button - AIS 매개변수 생성
        /// </summary>
        public string BtnParamsCreate 
        { 
            get
            {
                return _BtnParamsCreate;
            } 
            set
            {
                _BtnParamsCreate = value;
                Changed();
            }
        }
        private string _BtnParamsCreate = string.Empty;


        /// <summary>
        /// 백그라운드 워커 
        /// </summary>
        //private BackgroundWorker ThreadParamsCreate = null;
        private BackgroundWorker Worker;

        //public event PropertyChangedEventHandler PropertyChanged;
        //
        //public double CurrentProgress { get; set; }

        /// <summary>
        /// ProgressBar 작업 처리 진행 여부 
        /// </summary>
        public bool IsLoading 
        {
            get 
            {
                return _IsLoading;
            } 
            set
            {
                _IsLoading = value;
                Changed();
            } 
        }
        private bool _IsLoading = false;

        // TODO : Progress 처리 진행율 프로퍼티 "ProgressRate" 필요시 사용 예정 (2023.11.08 jbh)
        /// <summary>
        /// Progress 처리 진행율
        /// </summary>
        //public int ProgressRate
        //{
        //    get
        //    {
        //        return _ProgressRate;
        //    }
        //    set
        //    {
        //        _ProgressRate = value;
        //        Changed();
        //    }
        //}
        //private int _ProgressRate = 0;

        #endregion 프로퍼티

        #region 생성자

        public ParamsCreateVM()
        {
            ParamsCreateCommand = new ButtonCommand(AISParamsHelper.AsyncMethodType, ParamsCreateAsync, CanExecuteMethod);
            ParamsSource = BitmapConverter.ConvertFromBitmap(RevitBoxSeumteo.Properties.Resources.SeumteoParams);

            TitleParamsCreate = AISParamsHelper.매개변수생성;
            TxtParamsCreate   = AISParamsHelper.매개변수생성클릭;
            BtnParamsCreate   = AISParamsHelper.매개변수생성;

            // TODO : ProgressBar 뷰모델에서 구현하기 (2023.11.02 jbh)
            // 참고 URL - https://www.codeproject.com/Questions/5292748/Open-new-progressbar-window-in-WPF-with-MVVM-patte
            // 참고 2 URL - http://ojc.asia/bbs/board.php?bo_table=WPF&wr_id=40

            // 백그라운드 워커 초기화
            Worker = new BackgroundWorker();
            Worker.WorkerReportsProgress = true;
            Worker.ProgressChanged += (sender, args) =>
            {
                int visibilitySetting = args.ProgressPercentage;
                IsLoading = visibilitySetting == 0 ? false : true;
            };

            // 해야할 작업을 실행할 메소드 정의
            Worker.DoWork += ParamsCreateDoWork;

            // TODO : 이벤트 메서드 "ProgressChanged" 필요시 사용 예정 (2023.11.08 jbh)
            // UI쪽에 AIS 매개변수 생성 작업 진행사항을 보여주기 위해
            // WorkerReportsProgress 속성값이 true 일때만 이벤트 발생
            // Worker.ProgressChanged += ProgressChanged;

            // 작업이 완료되었을 때 실행할 콜백메소드 정의  
            Worker.RunWorkerCompleted += ParamsCreateCompleted;

            // 백그라운드 워커 초기화
            // 작업의 진행율이 바뀔때 ProgressChanged 이벤트 발생여부
            // 작업취소 가능여부 true로 설정
            //ThreadParamsCreate = new BackgroundWorker()
            //{
            //    WorkerReportsProgress = true,
            //    WorkerSupportsCancellation = true
            //};

            // 백그라운드에서 실행될 콜백 이벤트 생성
            // For the performing operation in the background.  

            // 해야할 작업을 실행할 메소드 정의
            // ThreadParamsCreate.DoWork += ThreadParamsCreate_DoWork;

            // UI쪽에 진행사항을 보여주기 위해
            // WorkerReportsProgress 속성값이 true 일때만 이벤트 발생
            // ThreadParamsCreate.ProgressChanged += ThreadParamsCreate_RunWorkerCompleted;

            MessageBox.Show("Worker 초기화");
        }

        #endregion 생성자 

        #region 기본 메소드 

        private bool CanExecuteMethod(object obj)
        {
            return true;
        }


        #endregion 기본 메소드 

        #region ParamsCreateAsync

        /// <summary>
        /// AIS 매개변수 생성
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private async Task ParamsCreateAsync(object obj)
        {
            // TODO : 로그 기록시 현재 실행 중인 메서드 위치 기록하기 (2023.10.10 jbh)
            // 참고 URL - https://slaner.tistory.com/73
            // 참고 2 URL - https://stackoverflow.com/questions/4132810/how-can-i-get-a-method-name-with-the-namespace-class-name
            // 참고 3 URL - https://stackoverflow.com/questions/44153/can-you-use-reflection-to-find-the-name-of-the-currently-executing-method
            var currentMethod = MethodBase.GetCurrentMethod();

            MessageBox.Show("AIS 매개변수 생성 기능 추후 구현 예정");

            try
            {
                Worker.RunWorkerAsync();
                //worker.ProgressChanged += ProgressChanged;
                // worker.RunWorkerCompleted += RunWorkerCompleted;
                


                //ThreadParamsCreate = new BackgroundWorker()
                //{
                //    WorkerReportsProgress = true,
                //    WorkerSupportsCancellation = true,
                //};
                //
                //ThreadParamsCreate.DoWork += ParamsCreateDoWork;
                //ThreadParamsCreate.ProgressChanged += ProgressChanged;
                //ThreadParamsCreate.RunWorkerCompleted += RunWorkerCompleted;
                //ThreadParamsCreate.RunWorkerAsync();

                // for (Percentage = 0; Percentage <= 100; Percentage++)
                // {
                //     // ThreadParamsCreate.ReportProgress(i);   // 값을 ReportProgress 매개변수로 전달
                //     // Thread.Sleep(100);                      // 0.1초
                //     // Wait(100);
                // }
            }
            catch (Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + ex.Message);
                MessageBox.Show(ex.Message);
                return;
            }
            return;
        }

        #endregion ParamsCreateAsync


        #region ParamsCreateDoWork

        /// <summary>
        /// ParamsCreateDoWork 스레드 이벤트 핸들러
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ParamsCreateDoWork(object sender, DoWorkEventArgs e)
        {
            // TODO : 로그 기록시 현재 실행 중인 메서드 위치 기록하기 (2023.11.08 jbh)
            var currentMethod = MethodBase.GetCurrentMethod();

            try
            {
                // worker.ReportProgress(1);   // shows progress bar

                // Thread.Sleep(1500);         // load data

                // TODO : 소스코드 "worker.ReportProgress(1); " ~ "worker.ReportProgress(0); " 안에 using문 사용하여 세움터 AIS_매개변수 생성하는 메서드 구현 예정 (2023.11.07 jbh) 
                // using ()
                // {
                // 
                // }

                // int count = (int)e.Argument;

                TxtParamsCreate = AISParamsHelper.매개변수생성중;

                for (int i = 1; i <= 100; i++)
                {
                    Thread.Sleep(200);          // load data (0.2초 간격)
                    // TODO : Progress 처리 진행율 프로퍼티 "ProgressRate" 필요시 사용 예정 (2023.11.08 jbh)
                    // ProgressRate += 1;
                    Worker.ReportProgress(i);   // shows progress bar (값을 ReportProgress 매개변수로 전달)
                }

                Worker.ReportProgress(0);       // hides progress bar

                // for (int i = 0; i <= 100; i++)
                // {
                //     Percent += 1;
                //     Thread.Sleep(100);                      // 0.1초
                //     worker.ReportProgress(i);               // 값을 ReportProgress 매개변수로 전달
                // 
                //     // Wait(100);
                // }

                //for (int i = 0; i < 100; i++)
                //{
                //    ThreadParamsCreate.ReportProgress(i);   // 값을 ReportProgress 매개변수로 전달
                //    // Thread.Sleep(100);                      // 0.1초
                //    // Wait(100);
                //}

                //for (Percentage = 0; Percentage <= 100; Percentage++)
                //{
                //    // ThreadParamsCreate.ReportProgress(i);   // 값을 ReportProgress 매개변수로 전달
                //    Thread.Sleep(100);                      // 0.1초
                //                                            // Wait(100);
                //}
            }
            catch (Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + ex.Message);
            }
        }

        #endregion ParamsCreateDoWork

        #region ProgressChanged

        // TODO : 이벤트 메서드 "ProgressChanged" 필요시 사용 예정 (2023.11.08 jbh)
        /// <summary>
        /// ProgressBar에 처리 진행률 출력
        /// 작업의 진행률이 바뀔때 발생, ProgressBar에 변경사항 출력
        /// 대체로 현재의 진행상태를 보여주는 코드 여기에 작성.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        // private void ProgressChanged(object sender, ProgressChangedEventArgs e)
        // {
        //     ProgressRate = e.ProgressPercentage;
        // }

        #endregion ProgressChanged

        #region ParamsCreateCompleted

        /// <summary>
        /// 작업완료
        /// ProgressBar 작업 끝났을 때 실행
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ParamsCreateCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // TODO : 로그 기록시 현재 실행 중인 메서드 위치 기록하기 (2023.11.08 jbh)
            var currentMethod = MethodBase.GetCurrentMethod();

            try
            {
                // throw new NotImplementedException();
                if (e.Error != null) MessageBox.Show("에러발생..." + e.Error);
                else
                {
                    MessageBox.Show("작업 완료!!");
                    // TODO : Progress 처리 진행율 프로퍼티 "ProgressRate" 필요시 사용 예정 (2023.11.08 jbh)
                    // ProgressRate = 0;
                    TxtParamsCreate = AISParamsHelper.매개변수생성클릭;
                }
            }
            catch (Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + ex.Message);
            }
        }

        #endregion ParamsCreateCompleted

        #region Sample

        #endregion Sample
    }
}
