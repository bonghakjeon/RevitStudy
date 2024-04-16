using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace HTSBIM2019.Interface.Revit
{
    // TODO : 필요시 인터페이스 "IRevitBase.cs" 추가 구현 예정 (2024.04.15 jbh) 
    public interface IRevitBase
    {
        /// <summary>
        /// Revit UI 애플리케이션 객체
        /// </summary>
        UIApplication RevitUIApp { get; set; }

        /// <summary>
        /// 활성화된 Revit 문서 
        /// </summary>
        Document RevitDoc { get; set; }
    }
}
