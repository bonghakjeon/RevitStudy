using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Lab1PlaceGroup
{
    public partial class frmMain : DevExpress.XtraEditors.XtraForm
    {
        #region 프로퍼티

        public const string googleAddr = "https://www.google.co.kr/";

        public const string naverAddr = "https://www.naver.com/";

        #endregion 프로퍼티 

        #region 생성자 

        public frmMain()
        {
            InitializeComponent();
        }

        #endregion 생성자 

        #region GoogleBtn_Click

        private void GoogleBtn_Click(object sender, EventArgs e)
        {
            // TODO : 버튼 클릭시 구글 웹사이트 화면 출력 구현 (2024.01.18 jbh)
            // 참고 URL - https://yongtech.tistory.com/58
            // System.Diagnostics.Process.Start(googleAddr);

            // TODO : 버튼 클릭시 크롬 브라우저로 구글 웹사이트 화면 출력 구현 (2024.01.18 jbh)
            // 참고 URL - https://sosopro.tistory.com/98
            Process.Start("chrome.exe", googleAddr);
        }

        #endregion GoogleBtn_Click

        #region NaverBtn_Click

        private void NaverBtn_Click(object sender, EventArgs e)
        {
            // TODO : 버튼 클릭시 네이버 웹사이트 화면 출력 구현 (2024.01.18 jbh)
            // 참고 URL - https://yongtech.tistory.com/58
            // System.Diagnostics.Process.Start(naverAddr);

            // TODO : 버튼 클릭시 크롬 브라우저로 네이버 웹사이트 화면 출력 구현 (2024.01.18 jbh)
            // 참고 URL - https://sosopro.tistory.com/98
            Process.Start("chrome.exe", naverAddr);
        }

        #endregion NaverBtn_Click
    }
}