using RevitBoxSeumteoNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitBox.Data.Models.RevitBoxBase.AISParams
{
    // TODO : 필요시 T_AISParams.cs 클래스 파일 구현 예정 (2023.11.15 jbh)
    public class TAISParams<T> : TAISParams where T : TAISParams<T> { }
    //public class T_AISParams : RevitModelBase<T_AISParams>
    //{
    //}

    // TODO : 일단 임시로 아래 처럼 구현해서 사용 추후 필요시 위에 주석 친 소스코드 처럼 변경 및 
    //        RevitModelBase 클래스 구현 예정 (2023.11.15 jbh)     
    public class TAISParams : RbModelBase<TAISParams>  // BindableBase
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

        #region AIS 매개변수 

        #region 공통 - 카테고리 레벨

        /// <summary>
        /// AIS_분류코드 - 타입 코드(string)
        /// </summary>
        public string ais_LevelCode
        {
            get => this._ais_LevelCode;
            set
            {
                this._ais_LevelCode = value;
                this.Changed(nameof(ais_LevelCode));
            }
        }
        private string _ais_LevelCode;

        #endregion 공통 - 카테고리 레벨 

        #region 개요 - 카테고리 면적

        /// <summary>
        /// AIS_개요분류코드 - 타입 코드(string)
        /// </summary>
        public string ais_AreaCode
        {
            get => this._ais_AreaCode;
            set
            {
                this._ais_AreaCode = value;
                this.Changed(nameof(ais_AreaCode));
            }
        }
        private string _ais_AreaCode;

        // TODO : AIS_연면적제외 프로퍼티 "ais_Exc_TotalArea" 구현 (2023.11.17 jbh)
        // 연면적 - 하나의 건축물 각 층의 바닥면적을 합한 면적 
        // 참고 URL - https://why-not-now.tistory.com/entry/%EA%B1%B4%EC%B6%95%EB%AC%BC-%EB%B0%94%EB%8B%A5%EB%A9%B4%EC%A0%81-%EC%82%B0%EC%A0%95%EA%B8%B0%EC%A4%80-%EB%B0%8F-%EC%A0%9C%EC%99%B8%EA%B8%B0%EC%A4%80-%EB%93%B1-%EA%B1%B4%EC%B6%95%EB%B2%95-%ED%95%B4%EC%84%A4
        /// <summary>
        /// AIS_연면적제외 - 타입(bool)
        /// </summary>
        public bool ais_IsExceptYn_TotalFloorArea
        {
            get => this._ais_IsExceptYn_TotalFloorArea;
            set
            {
                this._ais_IsExceptYn_TotalFloorArea = value;
                this.Changed(nameof(ais_IsExceptYn_TotalFloorArea));
            }
        }
        private bool _ais_IsExceptYn_TotalFloorArea;

        // TODO : AIS_용적률제외 프로퍼티 "ais_Exc_FloorAreaRatio" 구현 (2023.11.17 jbh)
        // 용적률 - (하나의 건축물의 연면적 / 대지면적 * 100) 
        // 참고 URL - https://m.blog.naver.com/bbodaese/222026730414
        /// <summary>
        /// AIS_용적률제외 - 타입(bool)
        /// </summary>
        public bool ais_IsExceptYn_FloorAreaRatio
        {
            get => this._ais_IsExceptYn_FloorAreaRatio;
            set
            {
                this._ais_IsExceptYn_FloorAreaRatio = value;
                this.Changed(nameof(ais_IsExceptYn_FloorAreaRatio));
            }
        }
        private bool _ais_IsExceptYn_FloorAreaRatio;

        // 바닥면적 - 하나의 건축물에 속하는 각 층의 개별 면적 
        // 참고 URL - https://www.midascad.com/cad_archive/buildinglaws-2
        /// <summary>
        /// AIS_바닥면적제외 - 타입(bool)
        /// </summary>
        public bool ais_IsExceptYn_FloorArea
        {
            get => this._ais_IsExceptYn_FloorArea;
            set
            {
                this._ais_IsExceptYn_FloorArea = value;
                this.Changed(nameof(ais_IsExceptYn_FloorArea));
            }
        }
        private bool _ais_IsExceptYn_FloorArea;


        #endregion 개요 - 카테고리 면적

        #region 건축 

        #region 카테고리 - 룸

        /// <summary>
        /// AIS_건축분류코드 - 타입 코드(string)
        /// </summary>
        public string ais_ArchCode
        {
            get => this._ais_ArchCode;
            set
            {
                this._ais_ArchCode = value;
                this.Changed(nameof(ais_ArchCode));
            }
        }
        private string _ais_ArchCode;

        // TODO : AIS_비상용승강기 프로퍼티 "ais_IsEmergencyElevatorUseYn" 구현 (2023.11.17 jbh)
        // 비상용 승강기 - 화재 및 재난 발생시 소화 및 구조활동에 적합하게 제작된 엘리베이터 
        // 참고 URL - https://m.blog.naver.com/metalzon79/221178522439
        /// <summary>
        /// AIS_비상용승강기 - 타입 bool
        /// </summary>
        public bool ais_IsEmergencyElevatorUseYn
        {
            get => this._ais_IsEmergencyElevatorUseYn;
            set
            {
                this._ais_IsEmergencyElevatorUseYn = value;
                this.Changed(nameof(ais_IsEmergencyElevatorUseYn));
            }
        }
        private bool _ais_IsEmergencyElevatorUseYn;

        // TODO : AIS_피난용승강기 프로퍼티 "ais_IsEvacuationElevatorUseYn" 구현 (2023.11.17 jbh)
        // 피난용승강기 - 화재 및 재난 발생시 거주자의 피난활동에 쓸수 있게 제작된 엘리베이터 
        // 참고 URL - https://m.blog.naver.com/metalzon79/221178522439
        /// <summary>
        /// AIS_피난용승강기 - 타입 bool
        /// </summary>
        public bool ais_IsEvacuationElevatorUseYn
        {
            get => this._ais_IsEvacuationElevatorUseYn;
            set
            {
                this._ais_IsEvacuationElevatorUseYn = value;
                this.Changed(nameof(ais_IsEvacuationElevatorUseYn));
            }
        }
        private bool _ais_IsEvacuationElevatorUseYn;

        // 특별피난계단 + 피난계단 용어 설명
        // 참고 URL - https://www.midascad.com/cad_archive/buildingact-6
        /// <summary>
        /// AIS_특별피난계단 - 타입 bool
        /// </summary>
        public bool ais_IsSpecialEscStairsUseYn
        {
            get => this._ais_IsSpecialEscStairsUseYn;
            set
            {
                this._ais_IsSpecialEscStairsUseYn = value;
                this.Changed(nameof(ais_IsSpecialEscStairsUseYn));
            }
        }
        private bool _ais_IsSpecialEscStairsUseYn;


        /// <summary>
        /// AIS_피난계단 - 타입 bool
        /// </summary>
        public bool ais_IsfireEscStairsUseYn
        {
            get => this._ais_IsfireEscStairsUseYn;
            set
            {
                this._ais_IsfireEscStairsUseYn = value;
                this.Changed(nameof(ais_IsfireEscStairsUseYn));
            }
        }
        private bool _ais_IsfireEscStairsUseYn;

        // TODO : AIS_바닥마감 프로퍼티 "ais_FinishFloor" 구현 (2023.11.17 jbh)
        // 참고 URL - https://notsunmoon.tistory.com/870
        // 참고 2 URL - https://archi-material.tistory.com/159 
        /// <summary>
        /// AIS_바닥마감 - 타입 문자(char) 또는 문자열(string) / 형식(단위) - 바닥마감재
        /// </summary>
        public string ais_FinishFloor
        {
            get => this._ais_FinishFloor;
            set
            {
                this._ais_FinishFloor = value;
                this.Changed(nameof(ais_FinishFloor));
            }
        }
        private string _ais_FinishFloor;

        /// <summary>
        /// AIS_바닥번호 - 타입 문자(char) 또는 문자열(string) / 형식(단위) - 바닥마감상세번호
        /// </summary>
        public string ais_NumberFloor
        {
            get => this._ais_NumberFloor;
            set
            {
                this._ais_NumberFloor = value;
                this.Changed(nameof(ais_NumberFloor));
            }
        }
        private string _ais_NumberFloor;

        // TODO : AIS_걸레받이마감 프로퍼티 "" 구현 (2023.11.17 jbh)
        // 걸레받이마감(바닥몰딩) - 걸레받이는 벽면, 바닥면, 가구 등의 접합부를 깔끔하게 마감해 주며,
        // 장판 시공 후 장판 위를 걸레질할 때 장판과 벽면이 손상 및 오염을 방지해 주는 자재를 뜻한다.
        // 걸레받이의 또 다른 명칭은 몰딩, 굽도리라고도 불린다.
        // 참고 URL - https://zipdoc.co.kr/story/detail?cno=431
        // 참고 2 URL - https://m.post.naver.com/viewer/postView.naver?volumeNo=35135504&memberNo=41665055&searchKeyword=%EB%AA%B0%EB%94%A9%20%EC%8B%9C%EA%B3%B5
        // 참고 3 URL - https://m.blog.naver.com/ifudyto822/221633726658
        /// <summary>
        /// AIS_걸레받이마감 - 타입 문자(char) 또는 문자열(string) / 형식(단위) - 걸레받이마감재
        /// </summary>
        public string ais_BaseBoard
        {
            get => this._ais_BaseBoard;
            set
            {
                this._ais_BaseBoard = value;
                this.Changed(nameof(ais_BaseBoard));
            }
        }
        private string _ais_BaseBoard;

        /// <summary>
        /// AIS_걸레받이번호 - 타입 문자(char) 또는 문자열(string) / 형식(단위) - 걸레받이마감상세번호
        /// </summary>
        public string ais_NumberBaseBoard
        {
            get => this._ais_NumberBaseBoard;
            set
            {
                this._ais_NumberBaseBoard = value;
                this.Changed(nameof(ais_NumberBaseBoard));
            }
        }
        private string _ais_NumberBaseBoard;

        /// <summary>
        /// AIS_벽마감 - 타입 문자(char) 또는 문자열(string) / 형식(단위) - 벽마감재
        /// </summary>
        public string ais_FinishWall
        {
            get => this._ais_FinishWall;
            set
            {
                this._ais_FinishWall = value;
                this.Changed(nameof(ais_FinishWall));
            }
        }
        private string _ais_FinishWall;

        /// <summary>
        /// AIS_벽번호 - 타입 문자(char) 또는 문자열(string) / 형식(단위) - 벽마감상세번호
        /// </summary>
        public string ais_NumberWall
        {
            get => this._ais_NumberWall;
            set
            {
                this._ais_NumberWall = value;
                this.Changed(nameof(ais_NumberWall));
            }
        }
        private string _ais_NumberWall;

        /// <summary>
        /// AIS_천장마감 - 타입 문자(char) 또는 문자열(string) / 형식(단위) - 천장마감재
        /// </summary>
        public string ais_FinishCeiling
        {
            get => this._ais_FinishCeiling;
            set
            {
                this._ais_FinishCeiling = value;
                this.Changed(nameof(ais_FinishCeiling));
            }
        }
        private string _ais_FinishCeiling;

        /// <summary>
        /// AIS_천장번호 - 타입 문자(char) 또는 문자열(string) / 형식(단위) - 천장마감상세번호
        /// </summary>
        public string ais_NumberCeiling
        {
            get => this._ais_NumberCeiling;
            set
            {
                this._ais_NumberCeiling = value;
                this.Changed(nameof(ais_NumberCeiling));
            }
        }
        private string _ais_NumberCeiling;

        #endregion 카테고리 - 룸

        #region 카테고리 - 문

        /// <summary>
        /// AIS_방화등급 - 타입 문자(char) 또는 문자열(string) / 형식(단위) - 갑/을
        /// </summary>
        public string ais_FireGrade
        {
            get => this._ais_FireGrade;
            set
            {
                this._ais_FireGrade = value;
                this.Changed(nameof(ais_FireGrade));
            }
        }
        private string _ais_FireGrade;

        #endregion 카테고리 - 문

        #region 카테고리 - 창

        // TODO : AIS_배연창 프로퍼티 "ais_IsSmokeVentilatorUseYn" 구현 (2023.11.17 jbh)
        // 배연창 - 화재발생시 창문을 자동으로 강제 개방하여 연기 및 유독가스를 배출하여줌으로써 질식사고로 인한 인명피해를 최소화하는 목적으로 사용
        // 참고 URL - https://m.blog.naver.com/na40103/130046995496
        /// <summary>
        /// AIS_배연창 - 타입 bool
        /// </summary>
        public bool ais_IsSmokeVentilatorUseYn 
        {
            get => this._ais_IsSmokeVentilatorUseYn;
            set
            {
                this._ais_IsSmokeVentilatorUseYn = value;
                this.Changed(nameof(ais_IsSmokeVentilatorUseYn));
            }
        }
        private bool _ais_IsSmokeVentilatorUseYn;

        #endregion 카테고리 - 창

        #region 카테고리 - 주차(건물내부)

        // TODO : AIS_건축주차구획(건물내부) 프로퍼티 "" 구현 (2023.11.17 jbh)
        // 주차구획 - 자동차 1대를 주차할 수 있는 구획을 "주차단위구획"이라 하며, 1개 이상의 "주차단위구획"으로 이루어진 구획 전체를 "주차구획"이라고 한다.
        // 참고 URL - https://share1.tistory.com/141
        /// <summary>
        /// AIS_건축주차구획(건물내부) - 타입 문자(char)  또는 문자열(string) / 형식(단위) - 일반/장애인/경차/여성
        /// </summary>
        public string ais_InsideParkingArea
        {
            get => this._ais_InsideParkingArea;
            set
            {
                this._ais_InsideParkingArea = value;
                this.Changed(nameof(ais_InsideParkingArea));
            }
        }
        private string _ais_InsideParkingArea;

        #endregion 카테고리 - 주차(건물내부)

        #endregion 건축

        #region 대지(배치)

        #region 카테고리 - 면적

        // TODO : AIS_대지분류코드 프로퍼티 "ais_SiteCode" 구현 (2023.11.17 jbh)
        // 참고 URL - https://m.blog.naver.com/economystar/223063715226
        /// <summary>
        /// AIS_대지분류코드 - 타입 코드(string)
        /// </summary>
        public string ais_SiteCode
        {
            get => this._ais_SiteCode;
            set 
            {
                this._ais_SiteCode = value;
                this.Changed(nameof(ais_SiteCode));
            }
        }
        private string _ais_SiteCode;

        /// <summary>
        /// AIS_실사용(대지) - 타입 bool
        /// </summary>
        public bool ais_IsSiteUseYn
        {
            get => this._ais_IsSiteUseYn;
            set
            {
                this._ais_IsSiteUseYn = value;
                this.Changed(nameof(ais_IsSiteUseYn));
            }
        }
        private bool _ais_IsSiteUseYn;

        #endregion 카테고리 - 면적

        #region 카테고리 - 프로젝트정보

        // TODO : AIS_공구상면적이 맞는지 아니면 AIS_공부상면적이 맞는지 차주에 "조원호 팀장님"과 소통 진행해서 확인하기(2023.11.17 jbh)
        /// <summary>
        /// AIS_공구상면적 - 타입 실수(double)
        /// </summary>
        


        #endregion 카테고리 - 프로젝트정보

        #region 카테고리 - 주차

        /// <summary>
        /// AIS_대지주차구획 - 타입 문자(char) 또는 문자열(string) / 형식(단위) - 일반/장애인/경차/여성
        /// </summary>
        public string ais_OutsideParkingArea
        {
            get => this._ais_OutsideParkingArea;
            set
            {
                this._ais_OutsideParkingArea = value;
                this.Changed(nameof(ais_OutsideParkingArea));
            }
        }
        private string _ais_OutsideParkingArea;

        #endregion 카테고리 - 주차

        #endregion 대지(배치)


        #endregion AIS 매개변수 

        #endregion AISParamsBoardVM

        #endregion 프로퍼티




        #region 생성자 

        #endregion 생성자 


    }
}
