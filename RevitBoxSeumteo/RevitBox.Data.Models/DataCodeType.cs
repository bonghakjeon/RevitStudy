using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitBox.Data.Models
{
    public enum DataCodeType
    {
        [Description("-데이터-")] 
        Unknown = 0,
        [Description("AIS_매개변수")]
        AISParams = 1,
        // RevitBox에서 사용할 매개변수(또는 데이터) 목록 필요시 추후 추가 예정 (2023.11.21 jbh) 
    }
}
