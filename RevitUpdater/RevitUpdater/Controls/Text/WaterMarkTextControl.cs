using Serilog;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

using RevitUpdater.Common.LogManager;
using RevitUpdater.Common.UpdaterBase;

namespace RevitUpdater.Controls.Text
{
    // TODO : 워터마크(Water Mark) TextEdit 사용자 정의 컨트롤을 DevExpress 폼 화면에서 사용시 출력되는
    //        오류 메시지 "도구 상자 항목 'WaterMarkTextControl'을(를) 로드하지 못했습니다. 해당 항목은 도구 상자에서 제거됩니다."
    //        해결 하기 위해 솔루션은 Debug - x64 / 프로젝트 파일 - 속성 - 빌드 -> 항목 "플랫폼 대상(G):"는 "Any CPU" 설정
    // 참고 URL - https://gdlseed.tistory.com/55


    // TODO : 워터마크(Water Mark) TextEdit 사용자 정의 컨트롤 "WaterMarkTextControl" 구현 (2024.03.15)
    // 참고 URL   - https://program-day.tistory.com/2
    // 참고 2 URL - https://program-day.tistory.com/21
    // 참고 3 URL - https://chat.openai.com/c/64847ab0-2ee4-4951-873b-daafbe264915

    public class WaterMarkTextControl : TextBox
    {
        #region 프로퍼티 

        /// <summary>
        /// 워터마크(WaterMark) 문자열(Text) 입력 가능 여부 
        /// </summary>
        private bool WaterMarkState;

        /// <summary>
        /// 
        /// </summary>
        private Color TempFontColor;

        [Category("WaterMark")]
        [Description("워터마크 문자열(WaterMark Text)")]
        /// <summary>
        /// 워터마크(Water Mark) 문자열(Text)
        /// </summary>
        public string WaterMarkText
        {
            get { return _WaterMarkText; }
            set
            {
                _WaterMarkText = value;
                if (this.Text == "")
                {
                    TempFontColor = this.ForeColor;

                    this.Text = _WaterMarkText;
                    this.ForeColor = WaterMarkColor;
                    WaterMarkState = true;     // 워터마크(WaterMark) 상태에서 -> 문자열(Text) 입력 가능 상태 변경 
                }

                Invalidate();
            }
        }
        private string _WaterMarkText;

        [AmbientValue(typeof(Color), "Empty")]
        [Category("WaterMark")]
        [DefaultValue(typeof(Color), "Gray")]
        [Description("워터마크 문자열(WaterMark Text) 색상")]
        /// <summary>
        /// 워터마크(Water Mark) 문자열(Text) 색상 
        /// </summary>
        public Color WaterMarkColor
        {
            get { return _WaterMarkColor; }
            set
            {
                _WaterMarkColor = value;
                Invalidate();
            }
        }
        private Color _WaterMarkColor = Color.Gray;

        #endregion 프로퍼티

        #region 생성자

        public WaterMarkTextControl()
        {
            WaterMarkState = true;
        }

        #endregion 생성자

        #region OnCreateControl

        /// <summary>
        /// 컨트롤 생성
        /// </summary>
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
        }

        #endregion OnCreateControl

        #region OnHandleCreated

        /// <summary>
        /// Handle 생성
        /// </summary>
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
        }

        #endregion OnHandleCreated

        #region OnEnter

        /// <summary>
        /// OnEnter 이벤트 메서드
        /// </summary>
        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);
        }

        #endregion OnEnter

        #region OnLeave

        /// <summary>
        /// 워터마크(Water Mark) 생성 이벤트 메서드 
        /// </summary>
        protected override void OnLeave(EventArgs e)
        {
            base.OnLeave(e);
        }

        #endregion OnLeave

        #region OnKeyPress

        // TODO : 키보드 키입력 이벤트 메서드 "OnKeyDown" 구현 (2024.03.15 jbh)
        // 참고 URL   - https://devut90.tistory.com/16
        // 참고 2 URL - https://swkdn.tistory.com/entry/C-Winform-3%EA%B0%95-%ED%82%A4%EB%B3%B4%EB%93%9C1-%ED%82%A4%EC%99%80-%EC%9D%B4%EB%B2%A4%ED%8A%B8-KeyDown

        /// <summary>
        /// 키보드 키입력 이벤트 메서드
        /// </summary>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

            try
            {
                Log.Information(Logger.GetMethodPath(currentMethod) + "키보드 키입력 이벤트 시작");

                // 워터마크 상태일 경우
                if (true == WaterMarkState)
                {
                    // TODO : 삼항 연산자 사용해서 키보드로 입력받은 키 데이터(e.KeyData) 값이
                    //        BackSpace(Keys.Back)일 경우 워터마크 문자열, 문자열 색상, 워터마크 상태 그대로 유지
                    //        BackSpace(Keys.Back)가 아닐 경우 문자열 색상, 워터마크 상태 변경 및 문자열 공백 처리 (2024.03.15 jbh)
                    this.Text      = e.KeyData == Keys.Back ? WaterMarkText : string.Empty;                  
                    this.ForeColor = e.KeyData == Keys.Back ? WaterMarkColor : TempFontColor;  // 문자열 색상 TempFontColor 설정
                    WaterMarkState = e.KeyData == Keys.Back? true : false;                     // 워터마크(WaterMark) 문자열(Text) 입력 가능 여부 변경 
                }

                // 엔터 키일 경우 
                if (e.KeyData == Keys.Enter) e.Handled = true;
                

                base.OnKeyDown(e);

                Log.Information(Logger.GetMethodPath(currentMethod) + "키보드 키입력 이벤트 종료");
            }
            catch (Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                MessageBox.Show(UpdaterHelper.ErrorTitle, ex.Message);
            }
        }

        #endregion OnKeyPress

        #region OnTextChanged

        /// <summary>
        /// 텍스트 변경 이벤트 메서드
        /// </summary>
        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);

            var currentMethod = MethodBase.GetCurrentMethod();   // 로그 기록시 현재 실행 중인 메서드 위치 기록

            try
            {
                Log.Information(Logger.GetMethodPath(currentMethod) + "텍스트 변경 이벤트 시작");

                // BackSpace키 누르기 직전 문자열이 존재해서 워터마크 상태가 아니고
                // BackSpace키 누르고 난 직 후 this.Text에 남은 문자열이 null 또는 공백일 경우 
                if (false == WaterMarkState
                    && true == string.IsNullOrWhiteSpace(this.Text))
                {
                    WaterMarkState = true;     // 워터마크(WaterMark) 문자열(Text) 입력 가능 상태 변경 
                    this.ForeColor = WaterMarkColor;
                    this.Text = WaterMarkText;
                }

                base.OnTextChanged(e);

                Log.Information(Logger.GetMethodPath(currentMethod) + "텍스트 변경 이벤트 종료");
            }
            catch (Exception ex)
            {
                Log.Error(Logger.GetMethodPath(currentMethod) + Logger.errorMessage + ex.Message);
                MessageBox.Show(UpdaterHelper.ErrorTitle, ex.Message);
            }
        }

        #endregion OnTextChanged
    }
}
