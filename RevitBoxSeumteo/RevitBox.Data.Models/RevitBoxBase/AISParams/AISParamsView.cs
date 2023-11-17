using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RevitBox.Data.Models.Common.AISParam;
using RevitBoxSeumteoNet;

namespace RevitBox.Data.Models.RevitBoxBase.AISParams
{
    public class AISParamsView<T> : AISParamsView where T : AISParamsView<T> { }

    // TODO : 필요시 T_AISParams.cs 클래스 파일 구현 예정 (2023.11.15 jbh)
    // public class AISParamsView : T_AISParams<AISParamsView> 
    // {
    // 
    // }

    /// <summary>
    /// RevitBox 세움터 
    /// AIS 매개변수 데이터 모델
    /// </summary>
    public class AISParamsView : TAISParams<AISParamsView>
    {
        #region 프로퍼티 

        #region AISParamsCreateBoardVM

        /// <summary>
        /// Title - AIS_매개변수 생성
        /// </summary>
        public string TitleParamsCreate
        {
            get
            {
                return _TitleParamsCreate;
            }
            set
            {
                _TitleParamsCreate = value;
                Changed();
            }
        }
        private string _TitleParamsCreate = string.Empty;

        /// <summary>
        /// Text - 버튼 AIS 매개변수 생성을 클릭하세요. / AIS 매개변수 생성 중...
        /// </summary>
        public string TxtParamsCreate
        {
            get
            {
                return _TxtParamsCreate;
            }
            set
            {
                _TxtParamsCreate = value;
                Changed();
            }
        }
        private string _TxtParamsCreate = string.Empty;


        /// <summary>
        /// Button - AIS 매개변수 생성
        /// </summary>
        public string BtnParamsCreate
        {
            get
            {
                return _BtnParamsCreate;
            }
            set
            {
                _BtnParamsCreate = value;
                Changed();
            }
        }
        private string _BtnParamsCreate = string.Empty;

        #endregion AISParamsCreateBoardVM

        #region AISParamsBoardVM

        // TODO : 멀티 콤보박스(Multi ComboBox) 구현시 콤보박스에 사용할 프로퍼티 구현 예정 (2023.11.14 jbh)
        // 참고 URL - https://m.blog.naver.com/goldrushing/221230210966
        /// <summary>
        /// 멀티 콤보박스 좌측 AIS 매개변수 항목
        /// </summary>
        public class AISParams_Type
        {
            /// <summary>
            /// 시퀀스(자동증가)
            /// </summary>
            public int Seq { get; set; }

            /// <summary>
            /// AIS 매개변수 타입 분류 코드 
            /// </summary>
            public string DivCode { get; set; }

            /// <summary>
            /// AIS 매개변수 타입 분류 명
            /// </summary>
            public string DivName { get; set; }

            /// <summary>
            /// 정렬 순서 
            /// </summary>
            public int OrderIdx { get; set; }
        }

        /// <summary>
        /// 멀티 콤보박스 우측 AIS 매개변수 속성값 항목 
        /// </summary>
        public class AISParams_Value
        {
            /// <summary>
            /// 시퀀스(자동증가)
            /// </summary>
            public int Seq { get; set; }

            /// <summary>
            /// AIS 매개변수 타입 분류 코드 
            /// </summary>
            public string DivCode { get; set; }

            /// <summary>
            /// AIS 매개변수에 속하는 
            /// 속성값 소 분류 코드 
            /// </summary>
            public string SubDivCode { get; set; }

            /// <summary>
            /// AIS 매개변수에 속하는 
            /// 속성값 소 분류 코드명
            /// </summary>
            public string SubDivName { get; set; }

            /// <summary>
            /// 정렬 순서 
            /// </summary>
            public int OrderIdx { get; set; }
        }

        #endregion AISParamsBoardVM

        #endregion 프로퍼티

        #region 생성자

        public AISParamsView() : base() { }


        #endregion 생성자

        #region ToColumnInfo

        // TODO : 필요시 구현 예정 (2023.11.15 jbh)
        //public override Dictionary<string, TTableColumn> ToColumnInfo()
        //{

        //}

        #endregion ToColumnInfo

        #region Clear

        // TODO : 필요시 구현 예정 (2023.11.15 jbh)
        //public override void Clear()
        //{

        //}

        #endregion Clear

        #region Clone

        // TODO : 필요시 구현 예정 (2023.11.15 jbh)
        // protected override RevitModelBase CreateClone => new AISParamsView();
        // public override object Clone()
        // {
        // 
        // }

        #endregion Clone

        #region PutData

        // TODO : 추후 메서드 "PutData" 필요시 수정 예정 (2023.11.15 jbh)
        /// <summary>
        /// AIS 매개변수 데이터 모델에 속한 프로퍼티에 데이터 추가 
        /// </summary>
        /// <param name="pSource"></param>
        public void PutData(AISParamsView pSource)
        {
            // this.PutData((RevitModelBase)pSource);
            this.TitleParamsCreate = pSource.TitleParamsCreate;
            this.TxtParamsCreate = pSource.TxtParamsCreate;
            this.BtnParamsCreate = pSource.BtnParamsCreate;
        }

        #endregion PutData


        #region SelectData

        // TODO : 추후 메서드 "SelectData" 필요시 수정 예정 (2023.11.15 jbh)
        /// <summary>
        /// AIS 매개변수 데이터 모델에 속한 프로퍼티에 할당된 데이터 가져오기 
        /// </summary>
        public void SelectData()
        {
            // AISParamsCreateBoardVM 전용 프로퍼티 
            this.TitleParamsCreate = AISParamsHelper.매개변수생성;
            this.TxtParamsCreate   = AISParamsHelper.매개변수생성클릭;
            this.BtnParamsCreate   = AISParamsHelper.매개변수생성;

            
        }

        #endregion SelectData

        #region Sample

        #endregion Sample
    }
}
