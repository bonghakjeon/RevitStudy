using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RevitBox.Data.Models.Interface;

namespace RevitBox.Data.Models.RevitBoxBase.AISParams
{
    public class AISParamsCreate : TAISParams, ICreateData
    {
        /// <summary>
        /// 데이터 생성 
        /// </summary>
        public bool CreateData()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 데이터 삭제
        /// </summary> 
        public bool DropData()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 데이터 초기화
        /// </summary>
        public bool InitData()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 데이터 재생성
        /// </summary>
        public bool ReCreateData()
        {
            throw new NotImplementedException();
        }
    }
}
