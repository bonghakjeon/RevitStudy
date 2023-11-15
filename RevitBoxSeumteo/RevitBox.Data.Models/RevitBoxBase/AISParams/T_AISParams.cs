using RevitBoxSeumteoNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitBox.Data.Models.RevitBoxBase.AISParams
{
    // TODO : 필요시 T_AISParams.cs 클래스 파일 구현 예정 (2023.11.15 jbh)
    public class T_AISParams<T> : T_AISParams where T : T_AISParams<T> { }
    //public class T_AISParams : RevitModelBase<T_AISParams>
    //{
    //}

    // TODO : 일단 임시로 아래 처럼 구현해서 사용 추후 필요시 위에 주석 친 소스코드 처럼 변경 및 
    //        RevitModelBase 클래스 구현 예정 (2023.11.15 jbh)     
    public class T_AISParams : BindableBase
    {

    }
}
