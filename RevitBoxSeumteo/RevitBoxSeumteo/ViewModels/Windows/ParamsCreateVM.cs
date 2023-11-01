using Serilog;
using System;
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

namespace RevitBoxSeumteo.ViewModels.Windows
{
    // TODO : Background Worker 클래스 사용해서 ProgressBar 화면 뷰(ParamsCreateV.xaml) + 뷰모델(ParamsCreateVM.cs) 구현 (2023.10.30 jbh)
    // 참고 URL - http://ojc.asia/bbs/board.php?bo_table=WPF&wr_id=40
    // 참고 2 URL - https://afsdzvcx123.tistory.com/entry/WPF-WPF-ProgressBar-%EC%82%AC%EC%9A%A9%ED%95%98%EA%B8%B0%ED%94%84%EB%A1%9C%EA%B7%B8%EB%A0%88%EC%8A%A4%EB%B0%94
    public class ParamsCreateVM
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
        private BackgroundWorker ThreadParamsCreate = null;

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
            // 작업의 진행율이 바뀔때 ProgressChanged 이벤트 발생여부
            // 작업취소 가능여부 true로 설정
            ThreadParamsCreate = new BackgroundWorker()
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };

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

        #region Sample

        #endregion Sample
    }
}
