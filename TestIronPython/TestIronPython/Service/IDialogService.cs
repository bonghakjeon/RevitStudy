using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestIronPython.Service
{
    // TODO : 메서드 "ShowDialog" 실행해서 ShellV.xaml 화면을 출력하기 위해 인터페이스 "IDialogService" 구현 (2023.10.4 jbh)
    // 참고 URL - https://nonstop-antoine.tistory.com/29s

    public interface IDialogService
    {
        T OpenDialog<T>(DialogViewModelBase<T> viewModel);
    }
}
