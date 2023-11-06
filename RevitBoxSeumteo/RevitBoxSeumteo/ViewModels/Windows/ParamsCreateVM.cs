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
    public class ParamsCreateVM :BindableBase
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
        public string TitleParamsCreate { get; set; } = string.Empty;

        /// <summary>
        /// Text - 버튼 AIS 매개변수 생성을 클릭하세요. (싱글톤 패턴)
        /// </summary>
        public string TxtParamsCreate { get; set; } = string.Empty;
        

        /// <summary>
        /// Button - AIS 매개변수 생성 (싱글톤 패턴)
        /// </summary>
        public string BtnParamsCreate { get; set; } = string.Empty;


        /// <summary>
        /// 백그라운드 워커 
        /// </summary>
        //private BackgroundWorker ThreadParamsCreate = null;
        private BackgroundWorker worker;

        //public event PropertyChangedEventHandler PropertyChanged;
        //
        //public double CurrentProgress { get; set; }

        /// <summary>
        /// isLoading
        /// </summary>
        public bool IsLoading 
        {
            get 
            {
                return isLoading;
            } 
            set
            {
                isLoading = value;
                Changed();
            } 
        }

        private bool isLoading = false;

        public int Percent
        {
            get { return _Percent; }
            set
            {
                _Percent = value;
                Changed();
            }
        }

        private int _Percent = 0;

        #endregion 프로퍼티

        #region 생성자

        public ParamsCreateVM()
        {
            ParamsCreateCommand = new ButtonCommand(AISParamsHelper.AsyncMethodType, ParamsCreateAsync, CanExecuteMethod);
            ParamsSource = BitmapConverter.ConvertFromBitmap(RevitBoxSeumteo.Properties.Resources.SeumteoParams);

            TitleParamsCreate = AISParamsHelper.Title_매개변수생성;
            TxtParamsCreate = AISParamsHelper.Text_매개변수생성클릭;
            BtnParamsCreate = AISParamsHelper.Btn_매개변수생성;

            // 백그라운드 워커 초기화
            // worker = new BackgroundWorker();
            // worker.WorkerReportsProgress = true;
            // worker.ProgressChanged += (sender, args) =>
            // {
            //     int visibilitySetting = args.ProgressPercentage;
            //     IsLoading = visibilitySetting == 0 ? false : true;
            // };
            // worker.DoWork += ParamsCreateDoWork;
            // worker.RunWorkerAsync();


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
                // TODO : ProgressBar 뷰모델에서 구현하기 (2023.11.02 jbh)
                // 참고 URL - https://afsdzvcx123.tistory.com/entry/WPF-WPF-ProgressBar-%EC%BB%A8%ED%8A%B8%EB%A1%A4-MVVM-%ED%8C%A8%ED%84%B4%EC%9C%BC%EB%A1%9C-%EA%B5%AC%ED%98%84%ED%95%98%EA%B8%B0

                // 백그라운드 워커 초기화
                worker = new BackgroundWorker();
                worker.WorkerReportsProgress = true;
                worker.ProgressChanged += (sender, args) =>
                {
                    int visibilitySetting = args.ProgressPercentage;
                    IsLoading = visibilitySetting == 0 ? false : true;
                };
                worker.DoWork += ParamsCreateDoWork;
                //worker.ProgressChanged += ProgressChanged;
                worker.RunWorkerCompleted += RunWorkerCompleted;
                worker.RunWorkerAsync();


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
            catch (Exception e)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + e.Message);
                MessageBox.Show(e.Message);
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
            worker.ReportProgress(1);//shows progress bar
            Thread.Sleep(1500);     //load data
            worker.ReportProgress(0);//hides progress bar

            for (int i = 0; i <= 100; i++)
            {
                Percent += 1;
                Thread.Sleep(100);                      // 0.1초
                worker.ReportProgress(i);               // 값을 ReportProgress 매개변수로 전달

                // Wait(100);
            }

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

        #endregion ParamsCreateDoWork

        #region ProgressChanged

        private void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // throw new NotImplementedException();
            // CurrentProgress = e.ProgressPercentage;
            //for (int i = 0; i <= 100; i++)
            //{
            //    Percent += 1;
            //    Thread.Sleep(100);                      // 0.1초
            //    // worker.ReportProgress(i);   // 값을 ReportProgress 매개변수로 전달

            //    // Wait(100);
            //}


        }

        #endregion ProgressChanged

        #region 

        /// <summary>
        /// 프로그래스바 컨트롤 작업 끝났을 때
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            // throw new NotImplementedException();
            MessageBox.Show("작업 완료!!");
        }

        #endregion 

        #region Sample

        #endregion Sample
    }
}
