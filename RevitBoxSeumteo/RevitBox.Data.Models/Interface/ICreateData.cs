using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitBox.Data.Models.Interface
{
    public interface ICreateData
    {
        /// <summary>
        /// 데이터 재생성
        /// </summary>
        bool ReCreateData();

        /// <summary>
        /// 데이터 생성 
        /// </summary>
        bool CreateData();

        /// <summary>
        /// 데이터 삭제
        /// </summary> 
        bool DropData();

        /// <summary>
        /// 데이터 초기화 
        /// </summary>
        bool InitData();
    }
}
