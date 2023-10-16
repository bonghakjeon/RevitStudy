using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RevitBoxSeumteo.Commands
{
    // TODO: ButtonCommand 필요시 추후 로직 변경 예정 (2023.10.4 jbh)
    // 참고 URL - https://www.codeproject.com/Tips/813345/Basic-MVVM-and-ICommand-Usage-Example
    // 참고 2 URL - https://youtu.be/s7pt3EkDyq4
    // 참고 3 URL - https://blog.naver.com/PostView.naver?blogId=goldrushing&logNo=221243250136 
    // Command 참고 URL - https://blog.naver.com/goldrushing/221243250136
    public class ButtonCommand : ICommand
    {
        #region 프로퍼티 

        /// <summary>
        /// 비동기 메소드 타입
        /// </summary>
        public const string asyncMethod = "Async";

        // Action은 반환 타입이 void인 메소드를 위해 특별히 설계된 제네릭 델리게이트 의미
        // 참고 URL - https://blog.joe-brothers.com/csharp-delegate-func-action/
        Action<object> _executeMethod;            // 반환 타입 void 메소드

        Func<object, Task> _executeAsyncMethod;   // 비동기 메소드

        Func<object, bool> _canexecuteMethod;

        public event EventHandler CanExecuteChanged;

        #endregion 프로퍼티 

        #region 생성자

        // 반환 타입이 void인 메소드와 바인딩하는 "ButtonCommand" 생성자
        public ButtonCommand(Action<object> executeMethod, Func<object, bool> canexecuteMethod)
        {
            this._executeMethod = executeMethod;
            this._canexecuteMethod = canexecuteMethod;
        }

        // 비동기 메소드(async)와 바인딩하는 "ButtonCommand" 생성자
        public ButtonCommand(Func<object, Task> executeAsyncMethod, Func<object, bool> canexecuteMethod)
        {
            this._executeAsyncMethod = executeAsyncMethod;
            this._canexecuteMethod = canexecuteMethod;
        }

        #endregion 생성자

        #region 기본 메소드

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var methodType = parameter.ToString();
            // 비동기 메소드인 경우 
            if (methodType.Equals(asyncMethod)) _executeAsyncMethod(parameter);
            // 반환 타입이 void인 메소드인 경우
            else _executeMethod(parameter);
        }

        #endregion 기본 메소드
    }
}
