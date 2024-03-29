using Serilog;

using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

using RevitUpdater.Common.LogBase;
using RevitUpdater.Common.UpdaterBase;
using RevitUpdater.Models.UpdaterBase.MEPUpdater;

namespace RevitUpdater.Controls.Text
{
    // TODO : 워터마크 텍스트 박스 컨트롤 WaterMarkTextControl.cs 구현 (2024.03.21 jbh)
    // 참고 URL - https://www.codeproject.com/Articles/27849/WaterMark-TextBox-For-Desktop-Applications-Using-C

    public class WaterMarkTextControl : TextBox
    {
        #region 프로퍼티

        /// <summary>
        /// 워터마크 텍스트(WaterMarkText) 활성화 하기 직전 현재 폰트
        /// </summary>
        private Font OldFont = null;

        /// <summary>
        /// 워터마크(WaterMark) 텍스트 활성화 여부 
        /// </summary>
        private bool WaterMarkTextEnabled = false;

        /// <summary>
        /// 워터마크(Water Mark) 문자열(Text)
        /// </summary>
        public string WaterMarkText
        {
            get { return _WaterMarkTextt; }
            set { _WaterMarkTextt = value; Invalidate(); }
        }
        private string _WaterMarkTextt;

        /// <summary>
        /// 워터마크(Water Mark) 문자열(Text) 색상 
        /// </summary>
        public Color WaterMarkColor
        {
            get { return _WaterMarkColor; }
            set { _WaterMarkColor = value; Invalidate(); }
        }
        private Color _WaterMarkColor = Color.Gray;

        #endregion 프로퍼티

        #region 생성자

        public WaterMarkTextControl()
        {
            bool eventJoin = true;  // 이벤트 가입 처리 진행 
            InitSetting(eventJoin);
        }

        #endregion 생성자

        #region InitSetting

        /// <summary>
        /// 이벤트 가입 초기 셋팅
        /// </summary>
        private void InitSetting(bool pEventJoin)
        {
            if(true == pEventJoin)
            {
                this.TextChanged += new EventHandler(this.WaterMark_Toggel);
                this.LostFocus   += new EventHandler(this.WaterMark_Toggel);
                this.FontChanged += new EventHandler(this.WaterMark_FontChanged);

                // 위 이벤트 중 어느 것도 즉시 시작되지 않음.
                // TextBox 컨트롤이 아직 생성 중이므로,
                // WaterMark_Toggle 내에서 글꼴 객체(예:)를 포착할 수 없다.
                // 따라서 TextBox가 완전히 생성된 후 OnCreateControl을 통해 WaterMark_Toggel을 호출합니다.
                // 단 한 번만 호출됩니다.
            }
        }

        #endregion InitSetting

        #region OnCreateControl

        /// <summary>
        /// 컨트롤 생성
        /// </summary>
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            WaterMark_Toggel(null, null);
        }

        #endregion OnCreateControl

        #region OnPaint

        /// <summary>
        /// OnPaint - 텍스트 또는 워터마크 그리기
        /// </summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

            try
            {
                Log.Information(Logger.GetMethodPath(currentMethod) + "OnPaint 이벤트 시작");

                // 기본 클래스에 정의된 것과 동일 폰트 사용
                Font drawFont = new Font(Font.FontFamily, Font.Size, Font.Style, Font.Unit);

                // gray 색상으로 새 브러시를 생성 
                SolidBrush drawBrush = new SolidBrush(WaterMarkColor);  // 워터마크 색상(WaterMarkColor) 사용

                // 텍스트 또는 워터마크 그리기 (삼항 연산자 사용)
                e.Graphics.DrawString((WaterMarkTextEnabled ? WaterMarkText : Text), drawFont, drawBrush, new PointF(0.0F, 0.0F));

                base.OnPaint(e);

                Log.Information(Logger.GetMethodPath(currentMethod) + "OnPaint 이벤트 종료");
            }
            catch(Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                MessageBox.Show(UpdaterHelper.ErrorTitle, ex.Message);
            }
        }

        #endregion OnPaint

        #region WaterMark_Toggel

        private void WaterMark_Toggel(object sender, EventArgs e)
        {
            // 키보드로 부터 입력받은 텍스트(this.Text) 길이가 0보다 작거나 같으면 (텍스트가 존재하지 않으면)
            if(this.Text.Length <= (int)EnumExistKeyInputData.NONE)
                EnableWaterMark();    // 워터마크 활성화
            // 키보드로 부터 입력받은 텍스트(this.Text)가 존재하면 
            else
                DisbaleWaterMark();   // 워터마크 비활성화
        }

        #endregion WaterMark_Toggel

        #region EnableWaterMark

        /// <summary>
        /// 워터마크 텍스트(WaterMarkText) 활성화
        /// </summary>
        private void EnableWaterMark()
        {
            var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

            try
            {
                Log.Information(Logger.GetMethodPath(currentMethod) + "워터마크 텍스트(WaterMarkText) 활성화 시작");

                // UserPaint 스타일을 false로 반환할 때까지 워터마크 텍스트(WaterMarkText) 활성화 하기 직전 현재 폰트를 프로퍼티 OldFont에 저장 
                OldFont = new Font(Font.FontFamily, Font.Size, Font.Style, Font.Unit);

                // this.SetStyle 메서드 호출 -> OnPaint 이벤트 메서드(핸들러) 활성화
                this.SetStyle(ControlStyles.UserPaint, true);   // UserPaint 스타일 true
                this.WaterMarkTextEnabled = true;

                // 메서드 Refresh 호출 -> OnPaint 이벤트 메서드(핸들러) 즉시 트리거
                Refresh();

                Log.Information(Logger.GetMethodPath(currentMethod) + "워터마크 텍스트(WaterMarkText) 활성화 완료");
            }
            catch(Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                MessageBox.Show(UpdaterHelper.ErrorTitle, ex.Message);
            }
        }

        #endregion EnableWaterMark

        #region DisbaleWaterMark

        /// <summary>
        /// 워터마크 텍스트(WaterMarkText) 비활성화
        /// </summary>
        private void DisbaleWaterMark()
        {
            var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

            try
            {
                Log.Information(Logger.GetMethodPath(currentMethod) + "워터마크 텍스트(WaterMarkText) 비활성화 시작");

                // this.SetStyle 메서드 호출 -> OnPaint 이벤트 메서드(핸들러) 비활성화
                this.WaterMarkTextEnabled = false;

                this.SetStyle(ControlStyles.UserPaint, false);   // UserPaint 스타일 false

                // OldFont가 존재하는 경우 다시 반환
                if(OldFont is not null)
                    this.Font = new Font(OldFont.FontFamily, OldFont.Size, OldFont.Style, OldFont.Unit);

                Log.Information(Logger.GetMethodPath(currentMethod) + "워터마크 텍스트(WaterMarkText) 비활성화 완료");
            }
            catch(Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                MessageBox.Show(UpdaterHelper.ErrorTitle, ex.Message);
            }
        }

        #endregion DisbaleWaterMark

        #region WaterMark_FontChanged

        private void WaterMark_FontChanged(object sender, EventArgs e)
        {
            var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

            try
            {
                Log.Information(Logger.GetMethodPath(currentMethod) + "WaterMark_FontChanged 이벤트 시작");

                // 워터마크 텍스트(WaterMarkText)가 활성화 된 경우 
                if(true == WaterMarkTextEnabled)
                {
                    OldFont = new Font(Font.FontFamily, Font.Size, Font.Style, Font.Unit);
                    Refresh();
                }

                Log.Information(Logger.GetMethodPath(currentMethod) + "WaterMark_FontChanged 이벤트 완료");
            }
            catch(Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                MessageBox.Show(UpdaterHelper.ErrorTitle, ex.Message);
            }
        }

        #endregion WaterMark_FontChanged
    }
}
