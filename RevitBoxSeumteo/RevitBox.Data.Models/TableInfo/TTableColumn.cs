using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RevitBoxSeumteoNet;

namespace RevitBox.Data.Models.TableInfo
{
    public class TTableColumn : BindableBase
    {
        #region 프로퍼티 

        /// <summary>
        /// 프로퍼티 명칭
        /// </summary>
        public string tc_origin_name
        {
            get => this._tc_origin_name;
            set
            {
                this._tc_origin_name = value;
                this.Changed(nameof(tc_origin_name));
            }
        }
        private string _tc_origin_name;

        /// <summary>
        /// 프로퍼티 한글명칭
        /// </summary>
        public string tc_trans_name
        {
            get => this._tc_trans_name;
            set
            {
                this._tc_trans_name = value;
                this.Changed(nameof(tc_trans_name));
            }
        }
        private string _tc_trans_name;

        // TODO : 부모 프로퍼티 명칭 프로퍼티 "tc_parents_name" 필요시 사용 예정 (2023.11.22 jbh)
        /// <summary>
        /// 부모 프로퍼티 명칭
        /// </summary>
        //public string tc_parents_name
        //{
        //    get => this._tc_parents_name;
        //    set
        //    {
        //        this._tc_parents_name = value;
        //        this.Changed(nameof(tc_parents_name));
        //    }
        //}
        //private string _tc_parents_name;

        // TODO : 테이블 컬럼 상태 프로퍼티 "tc_col_status" 필요시 사용 예정 (2023.11.22 jbh)
        /// <summary>
        /// 테이블 컬럼 상태
        /// </summary>
        //public int tc_col_status
        //{
        //    get => this._tc_col_status;
        //    set
        //    {
        //        this._tc_col_status = value;
        //        this.Changed(nameof(tc_col_status));
        //    }
        //}
        //private int _tc_col_status;

        // TODO : 아래 주석친 코드 필요시 사용 예정 (2023.11.22 jbh)
        //public string StatusDesc => this.tc_col_status != 0 ? Enum2StrConverter.ToTableColumnStatus(this.tc_col_status).ToDescription() : string.Empty;

        //public bool IsJoin => (this.tc_col_status & 1) == 1;

        //public bool IsAttribute => (this.tc_col_status & 2) == 2;

        //public bool IsKey => (this.tc_col_status & 4) == 4;

        //public bool IsRequired => (this.tc_col_status & 8) == 8;

        // TODO : 테이블 컬럼 상태 프로퍼티 "tc_comm_code" 필요시 사용 예정 (2023.11.22 jbh)
        /// <summary>
        /// 테이블에 속한 컬럼 구분 짓는 컬럼 코드명칭
        /// </summary>
        //public int tc_comm_code
        //{
        //    get => this._tc_comm_code;
        //    set
        //    {
        //        this._tc_comm_code = value;
        //        this.Changed(nameof(tc_comm_code));
        //    }
        //}
        //private int _tc_comm_code;

        // TODO : 테이블에 속한 컬럼 설명 (Hint) 프로퍼티 "tc_col_hint" 필요시 사용 예정 (2023.11.22 jbh)
        /// <summary>
        /// 테이블에 속한 컬럼 설명 (Hint)
        /// </summary>
        //public string tc_col_hint
        //{
        //    get => this._tc_col_hint;
        //    set
        //    {
        //        this._tc_col_hint = value;
        //        this.Changed(nameof(tc_col_hint));
        //    }
        //}
        //private string _tc_col_hint;

        // TODO : 테이블에 속한 컬럼 화면 출력 여부 프로퍼티 "tc_col_visible" 필요시 사용 예정 (2023.11.22 jbh)
        /// <summary>
        /// 테이블에 속한 컬럼 화면 출력 여부 (IsVisible)
        /// </summary>
        //public bool tc_col_visible
        //{
        //    get => this._tc_col_visible;
        //    set
        //    {
        //        this._tc_col_visible = value;
        //        this.Changed(nameof(tc_col_visible));
        //    }
        //}
        //private bool _tc_col_visible = true;

        #endregion 프로퍼티 

        #region 생성자 

        public TTableColumn() => this.Clear();

        #endregion 생성자

        #region Clear

        // TODO : 메서드 Clear 추후 구현 예정 (2023.11.22 jbh)
        public void Clear()
        {
            this.tc_origin_name = string.Empty;
            this.tc_trans_name = string.Empty;
            //this.tc_parents_name = string.Empty;
            //this.tc_col_status = 0;
            //this.tc_comm_code = 0;
            //this.tc_col_hint = string.Empty;
            //this.tc_col_visible = true;
        }

        #endregion Clear
    }
}
