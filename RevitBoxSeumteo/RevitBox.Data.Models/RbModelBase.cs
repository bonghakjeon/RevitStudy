using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using RevitBox.Data.Models.Interface;
using RevitBox.Data.Models.TableInfo;
using RevitBoxSeumteoNet;

namespace RevitBox.Data.Models
{
    // TODO : 추후 필요시 "RbModelBase" 클래스 구현 예정 (2023.11.17 jbh)
    public class RbModelBase<T> : RbModelBase where T : RbModelBase<T> { }


    // TODO : 추후 필요시 "RbClass" 클래스, "ICloneable" 인터페이스 구현 예정 (2023.11.17 jbh)
    public class RbModelBase : BindableBase // RbModelBase // RbClass, ICloneable
    {
        #region 프로퍼티 

        /// <summary>
        /// 데이터코드 타입 - TableCodeType과 비슷한 역할 
        /// </summary>
        [JsonIgnore]
        public DataCodeType DataCode
        {
            get => this._DataCode;
            set => this._DataCode = value;
        }
        private DataCodeType _DataCode;

        // TODO : 아래 주석친 소스코드 필요시 구현 예정 (2023.11.22 jbh)
        //[JsonIgnore]
        //public ModelSatus RowCheckStatus { get; } = new ModelSatus();

        //protected static string _ClassName => MethodBase.GetCurrentMethod().ReflectedType.Name;

        //protected static string GetAsyncMethodName([CallerMemberName] string name = null) => name;

        #endregion 프로퍼티 

        #region CreateClone

        protected virtual RbModelBase CreateClone => new RbModelBase();

        #endregion CreateClone

        #region Clone

        // TODO : 메서드 Clone 필요시 구현 예정 (2023.11.22 jbh)
        public virtual object Clone()
        {
            RbModelBase createClone = this.CreateClone;
            //createClone.row_number = this.row_number;
            //createClone.db_status = this.db_status;
            //createClone.SetAdoDatabase(this.OleDB);
            return (object)createClone;
        }

        #endregion Clone

        #region PutData

        // TODO : 메서드 PutData 필요시 구현 예정 (2023.11.22 jbh)
        public virtual void PutData(RbModelBase pSource)
        {
            //this.row_number = pSource.row_number;
            //this.db_status = pSource.db_status;
            //this.SetAdoDatabase(pSource.OleDB);
        }

        #endregion PutData

        #region Clear

        // TODO : 메서드 Clear 필요시 구현 예정 (2023.11.22 jbh)
        public virtual void Clear()
        {
            //this.row_number = 0;
            //this.DB_STATUS = EnumDBStatus.NONE;
            //this._RowErrorStatus = EnumRowStatus.Success;
            //this.message = string.Empty;
        }

        #endregion Clear

        #region ToColumnInfo

        // TODO : 필요시 메서드 ToColumnInfo 구현 예정 (2023.11.22 jbh)
        public virtual Dictionary<string, TTableColumn> ToColumnInfo() => new Dictionary<string, TTableColumn>()
        {
            //{
            //    "row_number",
            //    new TTableColumn()
            //    {
            //      tc_origin_name = "row_number",
            //      tc_trans_name = "No"
            //    }
            //},
            //{
            //    "ModDate",
            //    new TTableColumn()
            //    {
            //      tc_origin_name = "ModDate",
            //      tc_trans_name = "수정일"
            //    }
            //},
            //{
            //    "EmpName",
            //    new TTableColumn()
            //    {
            //      tc_origin_name = "EmpName",
            //      tc_trans_name = "수정사원"
            //    }
            //}
        };

        #endregion ToColumnInfo
    }
}
