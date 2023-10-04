using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestIronPython.Common.LogManager
{
    // TODO : 로그 메시지 출력 관련 클래스 구현 예정 (2023.10.4 jbh) 
    // 참고 URL - https://github.com/canton7/Stylet/wiki/Logging

    public class TestLogger : Stylet.Logging.ILogger
    {
        public TestLogger(string loggerName)
        {
            // TODO
        }

        public void Error(Exception exception, string message = null)
        {
            // TODO
        }

        public void Info(string format, params object[] args)
        {
            // TODO
        }

        public void Warn(string format, params object[] args)
        {
            // TODO
        }
    }
}
