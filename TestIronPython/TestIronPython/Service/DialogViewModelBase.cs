using Stylet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestIronPython.Service
{
    // TODO : 메서드 "ShowDialog" 실행해서 ShellV.xaml 화면을 출력하기 위해 추상 클래스 "DialogViewModelBase<T>" 구현 (2023.10.4 jbh)
    // 참고 URL - https://nonstop-antoine.tistory.com/29s
    public abstract class DialogViewModelBase<T> : Screen
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public T DialogResult { get; set; }

        public DialogViewModelBase() : this(string.Empty, string.Empty) { }

        public DialogViewModelBase(string title) : this(string.Empty, string.Empty) { }

        public DialogViewModelBase(string title, string message) 
        {
            Title = title;
            Message = message;
        } 

        public void CloseDialogWithResult(IDialogWindow dialog, T result)
        {
            DialogResult = result;
            if (dialog != null)
            {
                dialog.DialogResult = true;
            }
        }
    }
}
