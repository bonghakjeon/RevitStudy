using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TestIronPython.Service;

namespace TestIronPython.Views.Pages
{
    // TODO : 메서드 "ShowDialog" 실행해서 ShellV.xaml 화면을 출력하기 위해 인터페이스 "IDialogWindow" 구현 (2023.10.4 jbh)
    // 참고 URL - https://nonstop-antoine.tistory.com/29
    /// <summary>
    /// ShellV.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ShellV : Window // , IDialogWindow
    {
        public ShellV()
        {
            InitializeComponent();
        }
    }
}
