using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    // TODO : C# 윈폼 계산기 구현 (2024.01.19 jbh)
    // 참고 URL - https://e2hwan.tistory.com/21
    // 참고 2 URL - https://e2hwan.tistory.com/22
    // 참고 3 URL - https://e2hwan.tistory.com/23
    // 참고 4 URL - https://e2hwan.tistory.com/24
    public partial class frmCalculator : DevExpress.XtraEditors.XtraForm
    {
        public frmCalculator()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 숫자키 이벤트 메서드
        /// 각 버튼에 입력 되는 숫자, 연산자를 Display 에 업데이트 해주는 이벤트 함수
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNum_Click(object sender, EventArgs e)
        {
            tbDisplay.Text += ((SimpleButton)sender).Text;
        }

        /// <summary>
        /// Clear 'C' 버튼 클릭 이벤트 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            tbDisplay.Text = string.Empty;
            tbResult.Text  = string.Empty;
        }

        /// <summary>
        /// Equal '=' 버튼 클릭 이벤트 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEqual_Click(object sender, EventArgs e)
        {
            // tbDisplay 에 표시된 계산할 식을 가져와서, 숫자와 연산자로 구분            
            // 연산자를 하나씩 꺼내면서 결과 계산
            string strCalc  = tbDisplay.Text;         // 텍스트 박스의 문자열을 가져옴   
            char[] arrCalc  = strCalc.ToCharArray();  // 문자열을 문자 배열로 변경
            double[] arrNum = new double[100];        // 연산 숫자 배열   
            char[] arrOp    = new char[100];          // 연산자 배열   
            
            int numCnt   = 0;    
            int opCnt    = 0;    
            bool contNum = false;                    // 두자릿수 이상 연속된 숫자 인지 체크

            for (int i = 0; i < arrCalc.Length; i++)
            {
                if ((arrCalc[i] >= '0') && (arrCalc[i] <= '9'))
                {
                    if (contNum) arrNum[numCnt] = (arrNum[numCnt] * 10) + double.Parse(arrCalc[i].ToString()); 
                    else arrNum[numCnt] = double.Parse(arrCalc[i].ToString());

                    contNum = true;
                }
                else
                {
                    numCnt++; arrOp[opCnt++] = arrCalc[i];
                    contNum = false;
                }
            }

            // 'x', '/' 의 경우 뒤에서 부터 연산자를 하나씩 꺼내면서 결과 계산
            // 계산 결과를 앞 숫자 배열에 넣고 뒷 숫자 배열은 0 으로 바꿔준다.
            // 계산한 연산자 'x', '/' 는 '+'로 바꿔준다
            double resultCalc = 0;
            for (int i = opCnt; i >= 0; i--) 
            { 
                switch (arrOp[i]) 
                { 
                    case 'x': 
                        arrNum[i] = arrNum[i] * arrNum[i + 1]; 
                        arrNum[i + 1] = 0; 
                        arrOp[i] = '+'; 
                        break; 
                    case '/': arrNum[i] = arrNum[i] / arrNum[i + 1]; 
                        arrNum[i + 1] = 0; 
                        arrOp[i] = '+'; 
                        break; 
                    default: 
                        break; 
                } 
                resultCalc = arrNum[i + 1]; 
            }

            // '+', '-' 연산은 앞에서 부터 계산한다.
            for (int i = 0; i < opCnt; i++)
            {
                switch (arrOp[i])
                {
                    case '+': 
                        arrNum[i + 1] = arrNum[i] + arrNum[i + 1]; 
                        break;
                    case '-': 
                        arrNum[i + 1] = arrNum[i] - arrNum[i + 1]; 
                        break;
                    default: 
                        break;
                }
                resultCalc = arrNum[i + 1];
            }
            Console.WriteLine(resultCalc); 
            tbResult.Text = resultCalc.ToString();
        }
    }
}
