using System;
using System.Windows.Forms;

namespace HTSBIM2019.Controls.Tree
{
    // TODO : C# 사용자 정의 컨트롤 "NewTreeViewControl"
    //        오류 메시지 출력 "도구 상자 항목 'NewTreeViewControl'을(를) 로드하지 못했습니다. 해당 항목은 도구 상자에서 제거됩니다."
    //        해결하기 위해 솔루션 파일 "HTSBIM2019.sln" Debug 모드 x64 그대로 유지
    //        프로젝트 파일 "HTSBIM2019" Debug 모드 x64 -> Any CPU 변경 

    // TODO : C# Winform(윈폼) 트리뷰 체크박스 더블클릭(체크 + 언체크) 체크 오류 버그 방지 해결책 구현 (2024.04.26 jbh)
    // 참고 URL - https://mintaku.tistory.com/33

    public class NewTreeViewControl : TreeView
    {
        #region 프로퍼티

        #endregion 프로퍼티

        #region 생성자

        public NewTreeViewControl()
        {

        }

        #endregion 생성자

        #region WndProc

        protected override void WndProc(ref Message m)
        {
            if(m.Msg == 0x203) { m.Result = IntPtr.Zero; }
            else base.WndProc(ref m);
        }

        #endregion WndProc
    }
}
