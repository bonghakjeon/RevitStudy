using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitBox.Data.Models.Sys
{
    // TODO : 추후 DB 테이블 MySQL(또는 MSSQL) 구현 후 필요시 클새스 "TTable" 구현 예정 (2023.11.17 jbh)
    //public class TTable<T> : TTable where T : TTable<T>
    //{
    //}
    //public class TTable : : UbModelBase<TTable>
    //{
    //}

    public class TTable<T> : TTable where T : TTable<T> { }
    public class TTable
    {

    }
}
