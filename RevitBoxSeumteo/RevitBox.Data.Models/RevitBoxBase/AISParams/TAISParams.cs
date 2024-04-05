using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using RevitBox.Data.Models.Common.AISParam;
using RevitBox.Data.Models.TableInfo;
using RevitBoxSeumteoNet;

namespace RevitBox.Data.Models.RevitBoxBase.AISParams
{
    // TODO : 필요시 T_AISParams.cs 클래스 파일 구현 예정 (2023.11.15 jbh)
    public class TAISParams<T> : TAISParams where T : TAISParams<T> { }
    //public class T_AISParams : RbModelBase<T_AISParams>
    //{
    //}

    // TODO : 일단 임시로 아래 처럼 구현해서 사용 추후 필요시 위에 주석 친 소스코드 처럼 변경 및 
    //        RbModelBase 클래스 구현 예정 (2023.11.15 jbh)     
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
        // ---- 


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

        public TAISParams() => this.Clear();

        #endregion 생성자 

        #region CreateDataTable

        // TODO : 엑셀 데이터 (Export - Import) 하는 과정에서 필요한 AIS 매개변수 데이터를 DataTable로 만들어 주기 메서드 "CreateDataTable" 추후 구현 예정 (2023.12.06 jbh)
        /// <summary>
        /// 엑셀 데이터 (Export - Import) 하는 과정에서 필요한 AIS 매개변수 데이터를 DataTable로 만들어주기 
        /// </summary>
        /// <returns></returns>
        //public override DataTable CreateDataTable()
        //{

        //}

        #endregion CreateDataTable

        #region ToColumnInfo

        // TODO : 메서드 "ToColumnInfo" 구현 예정 (2023.11.20 jbh)
        public override Dictionary<string, TTableColumn> ToColumnInfo()
        {
            Dictionary<string, TTableColumn> columnInfo = base.ToColumnInfo();

            // TODO : Dictionary 객체 columnInfo 구현시 소스코드 예시 (2023.11.20 jbh)
            //columnInfo.Add("br_BrandCode", new TTableColumn()
            //{
            //    tc_orgin_name = "br_BrandCode",
            //    tc_trans_name = "브랜드코드"
            //});

            // TODO : Dictionary 객체 columnInfo에 추가할 Value(TTableColumn)에 속하는 프로퍼티 "tc_origin_name"에
            //        nameof 연산자 사용해서 속성 이름 자체를 문자열로 가져오기 (2023.11.23 jbh)
            // 참고 URL - https://hyokye0ng.tistory.com/86
            // nameof 연산자 장점 
            // 참고 URL - https://loveme-do.tistory.com/12
            // 공통 - 카테고리 레벨
            columnInfo.Add("ais_LevelCode", new TTableColumn()
            {
                // tc_origin_name = "ais_LevelCode",
                tc_origin_name = nameof(ais_LevelCode),
                // tc_trans_name = "AIS_분류코드"
                tc_trans_name  = AISParamsHelper.AIS_분류코드
            });

            // 개요 - 카테고리 면적
            columnInfo.Add("ais_AreaCode", new TTableColumn()
            {
                tc_origin_name = nameof(ais_AreaCode),
                // tc_trans_name = "AIS_개요분류코드"
                tc_trans_name  = AISParamsHelper.AIS_개요분류코드
            });
            columnInfo.Add("ais_IsExceptYn_TotalFloorArea", new TTableColumn()
            {
                tc_origin_name = nameof(ais_IsExceptYn_TotalFloorArea),
                tc_trans_name  = AISParamsHelper.AIS_연면적제외
            });
            columnInfo.Add("ais_IsExceptYn_FloorAreaRatio", new TTableColumn()
            {
                tc_origin_name = nameof(ais_IsExceptYn_FloorAreaRatio),
                tc_trans_name  = AISParamsHelper.AIS_용적률제외
            });
            columnInfo.Add("ais_IsExceptYn_FloorArea", new TTableColumn() 
            { 
                tc_origin_name = nameof(ais_IsExceptYn_FloorArea),
                tc_trans_name  = AISParamsHelper.AIS_바닥면적제외
            });

            // 건축
            // 카테고리 - 룸
            columnInfo.Add("ais_ArchCode", new TTableColumn()
            {
                tc_origin_name = nameof(ais_ArchCode),
                tc_trans_name  = AISParamsHelper.AIS_건축분류코드
            });
            columnInfo.Add("ais_IsEmergencyElevatorUseYn", new TTableColumn()
            {
                tc_origin_name = nameof(ais_IsEmergencyElevatorUseYn),
                tc_trans_name  = AISParamsHelper.AIS_비상용승강기
            });
            columnInfo.Add("ais_IsEvacuationElevatorUseYn", new TTableColumn()
            {
                tc_origin_name = nameof(ais_IsEvacuationElevatorUseYn),
                tc_trans_name  = AISParamsHelper.AIS_피난용승강기
            });
            columnInfo.Add("ais_IsSpecialEscStairsUseYn", new TTableColumn()
            {
                tc_origin_name = nameof(ais_IsSpecialEscStairsUseYn),
                tc_trans_name  = AISParamsHelper.AIS_특별피난계단
            });
            columnInfo.Add("ais_IsfireEscStairsUseYn", new TTableColumn()
            {
                tc_origin_name = nameof(ais_IsfireEscStairsUseYn),
                tc_trans_name  = AISParamsHelper.AIS_피난계단
            });
            columnInfo.Add("ais_FinishFloor", new TTableColumn()
            {
                tc_origin_name = nameof(ais_FinishFloor),
                tc_trans_name  = AISParamsHelper.AIS_바닥마감
            });
            columnInfo.Add("ais_NumberFloor", new TTableColumn()
            {
                tc_origin_name = nameof(ais_NumberFloor),
                tc_trans_name  = AISParamsHelper.AIS_바닥번호
            });
            columnInfo.Add("ais_BaseBoard", new TTableColumn()
            {
                tc_origin_name = nameof(ais_BaseBoard),
                tc_trans_name  = AISParamsHelper.AIS_걸레받이마감
            });
            columnInfo.Add("ais_NumberBaseBoard", new TTableColumn()
            {
                tc_origin_name = nameof(ais_NumberBaseBoard),
                tc_trans_name  = AISParamsHelper.AIS_걸레받이번호
            });
            columnInfo.Add("ais_FinishWall", new TTableColumn()
            {
                tc_origin_name = nameof(ais_FinishWall),
                tc_trans_name  = AISParamsHelper.AIS_벽마감
            });
            columnInfo.Add("ais_NumberWall", new TTableColumn()
            {
                tc_origin_name = nameof(ais_NumberWall),
                tc_trans_name  = AISParamsHelper.AIS_벽번호
            });
            columnInfo.Add("ais_FinishCeiling", new TTableColumn()
            {
                tc_origin_name = nameof(ais_FinishCeiling),
                tc_trans_name  = AISParamsHelper.AIS_천장마감
            });
            columnInfo.Add("ais_NumberCeiling", new TTableColumn()
            {
                tc_origin_name = nameof(ais_NumberCeiling),
                tc_trans_name  = AISParamsHelper.AIS_천장번호
            });

            // 건축
            // 카테고리 - 문
            columnInfo.Add("ais_FireGrade", new TTableColumn()
            {
                tc_origin_name = nameof(ais_FireGrade),
                tc_trans_name  = AISParamsHelper.AIS_방화등급
            });

            // 건축
            // 카테고리 - 창
            columnInfo.Add("ais_IsSmokeVentilatorUseYn", new TTableColumn()
            {
                tc_origin_name = nameof(ais_IsSmokeVentilatorUseYn),
                tc_trans_name  = AISParamsHelper.AIS_배연창
            });

            // 건축
            // 카테고리 - 주차(건물내부)
            columnInfo.Add("ais_InsideParkingArea", new TTableColumn()
            {
                tc_origin_name = nameof(ais_InsideParkingArea),
                tc_trans_name  = AISParamsHelper.AIS_건축주차구획
            });

            // 대지(배치)
            // 카테고리 - 면적
            columnInfo.Add("ais_SiteCode", new TTableColumn()
            {
                tc_origin_name = nameof(ais_SiteCode),
                tc_trans_name  = AISParamsHelper.AIS_대지분류코드
            });
            columnInfo.Add("ais_IsSiteUseYn", new TTableColumn()
            {
                tc_origin_name = nameof(ais_IsSiteUseYn),
                tc_trans_name  = AISParamsHelper.AIS_실사용
            });

            // 대지(배치)
            // 카테고리 - 프로젝트정보
            // TODO : AIS_공구상면적이 맞는지 아니면 AIS_공부상면적이 맞는지 차주에 "조원호 팀장님"과 소통 진행해서 확인하기(2023.11.17 jbh)
            // AIS_공구상면적 - 타입 실수(double) 
            columnInfo.Add("", new TTableColumn()
            {
                // tc_origin_name = nameof()"",
                tc_trans_name  = AISParamsHelper.AIS_공구상면적
            });

            // 대지(배치) 
            // 카테고리 - 주차
            columnInfo.Add("ais_OutsideParkingArea", new TTableColumn()
            {
                tc_origin_name = nameof(ais_OutsideParkingArea),
                tc_trans_name  = AISParamsHelper.AIS_대지주차구획
            });

            return columnInfo;
        }

        #endregion ToColumnInfo

        #region Clear

        // TODO : 메서드 "Clear" 추후 구현 예정 (2023.11.20 jbh)
        public override void Clear()
        {
            base.Clear();
            // this.TableCode = TableCodeType.AISParams;
            this.DataCode                      = DataCodeType.AISParams;   // 데이터 코드 

            // 공통 - 카테고리 레벨
            this.ais_LevelCode                 = string.Empty;             // AIS_분류코드 

            // 개요 - 카테고리 면적
            this.ais_AreaCode                  = string.Empty;             // AIS_개요분류코드 
            this.ais_IsExceptYn_TotalFloorArea = true;                     // AIS_연면적제외 
            this.ais_IsExceptYn_FloorAreaRatio = true;                     // AIS_용적률제외 
            this.ais_IsExceptYn_FloorArea      = true;                     // AIS_바닥면적제외 

            // 건축
            // 카테고리 - 룸
            this.ais_ArchCode                  = string.Empty;             // AIS_건축분류코드
            this.ais_IsEmergencyElevatorUseYn  = true;                     // AIS_비상용승강기 
            this.ais_IsEvacuationElevatorUseYn = true;                     // AIS_피난용승강기 
            this.ais_IsSpecialEscStairsUseYn   = true;                     // AIS_특별피난계단 
            this.ais_IsfireEscStairsUseYn      = true;                     // AIS_피난계단 
            this.ais_FinishFloor               = string.Empty;             // AIS_바닥마감 
            this.ais_NumberFloor               = string.Empty;             // AIS_바닥번호 
            this.ais_BaseBoard                 = string.Empty;             // AIS_걸레받이마감 
            this.ais_NumberBaseBoard           = string.Empty;             // AIS_걸레받이번호 
            this.ais_FinishWall                = string.Empty;             // AIS_벽마감
            this.ais_NumberWall                = string.Empty;             // AIS_벽번호
            this.ais_FinishCeiling             = string.Empty;             // AIS_천장마감
            this.ais_NumberCeiling             = string.Empty;             // AIS_천장번호 

            // 건축
            // 카테고리 - 문
            this.ais_FireGrade                 = string.Empty;             // AIS_방화등급

            // 건축
            // 카테고리 - 창
            this.ais_IsSmokeVentilatorUseYn    = true;                     // AIS_배연창

            // 건축
            // 카테고리 - 주차(건물내부)
            this.ais_InsideParkingArea         = string.Empty;             // AIS_건축주차구획(건물내부)

            // 대지(배치)
            // 카테고리 - 면적
            this.ais_SiteCode                  = string.Empty;             // AIS_대지분류코드
            this.ais_IsSiteUseYn               = true;                     // AIS_실사용(대지)

            // 대지(배치)
            // 카테고리 - 프로젝트정보
            // TODO : AIS_공구상면적이 맞는지 아니면 AIS_공부상면적이 맞는지 차주에 "조원호 팀장님"과 소통 진행해서 확인하기(2023.11.17 jbh)
            // AIS_공구상면적 - 타입 실수(double) 
            // this.공구상면적

            // 대지(배치) 
            // 카테고리 - 주차
            this.ais_OutsideParkingArea        = string.Empty;             // AIS_대지주차구획

        }

        #endregion Clear

        #region CreateClone

        // TODO : RbModelBase 클래스 객체 "CreateClone" 필요시 구현 예정 (2023.11.20 jbh)
        protected override RbModelBase CreateClone => (RbModelBase) new TAISParams();

        #endregion CreateClone

        #region Clone

        // TODO : 메서드 "Clone" 구현 예정 (2023.11.20 jbh)
        public override object Clone()
        {
            TAISParams tAISParams    = base.Clone() as TAISParams;

            // 공통 - 카테고리 레벨
            tAISParams.ais_LevelCode                 = this.ais_LevelCode;                   // AIS_분류코드 

            // 개요 - 카테고리 면적
            tAISParams.ais_AreaCode                  = this.ais_AreaCode;                    // AIS_개요분류코드 
            tAISParams.ais_IsExceptYn_TotalFloorArea = this.ais_IsExceptYn_TotalFloorArea;   // AIS_연면적제외 
            tAISParams.ais_IsExceptYn_FloorAreaRatio = this.ais_IsExceptYn_FloorAreaRatio;   // AIS_용적률제외
            tAISParams.ais_IsExceptYn_FloorArea      = this.ais_IsExceptYn_FloorArea;        // AIS_바닥면적제외

            // 건축
            // 카테고리 - 룸
            tAISParams.ais_ArchCode                  = this.ais_ArchCode;                    // AIS_건축분류코드
            tAISParams.ais_IsEmergencyElevatorUseYn  = this.ais_IsEmergencyElevatorUseYn;    // AIS_비상용승강기
            tAISParams.ais_IsEvacuationElevatorUseYn = this.ais_IsEvacuationElevatorUseYn;   // AIS_피난용승강기
            tAISParams.ais_IsSpecialEscStairsUseYn   = this.ais_IsSpecialEscStairsUseYn;     // AIS_특별피난계단 
            tAISParams.ais_IsfireEscStairsUseYn      = this.ais_IsfireEscStairsUseYn;        // AIS_피난계단
            tAISParams.ais_FinishFloor               = this.ais_FinishFloor;                 // AIS_바닥마감
            tAISParams.ais_NumberFloor               = this.ais_NumberFloor;                 // AIS_바닥번호 
            tAISParams.ais_BaseBoard                 = this.ais_BaseBoard;                   // AIS_걸레받이마감
            tAISParams.ais_NumberBaseBoard           = this.ais_NumberBaseBoard;             // AIS_걸레받이번호
            tAISParams.ais_FinishWall                = this.ais_FinishWall;                  // AIS_벽마감
            tAISParams.ais_NumberWall                = this.ais_NumberWall;                  // AIS_벽번호
            tAISParams.ais_FinishCeiling             = this.ais_FinishCeiling;               // AIS_천장마감
            tAISParams.ais_NumberCeiling             = this.ais_NumberCeiling;               // AIS_천장번호

            // 건축
            // 카테고리 - 문
            tAISParams.ais_FireGrade                 = this.ais_FireGrade;                   // AIS_방화등급

            // 건축
            // 카테고리 - 창
            tAISParams.ais_IsSmokeVentilatorUseYn    = this.ais_IsSmokeVentilatorUseYn;      // AIS_배연창

            // 건축
            // 카테고리 - 주차(건물내부)
            tAISParams.ais_InsideParkingArea         = this.ais_InsideParkingArea;           // AIS_건축주차구획(건물내부)

            // 대지(배치)
            // 카테고리 - 면적
            tAISParams.ais_SiteCode                  = this.ais_SiteCode;                    // AIS_대지분류코드
            tAISParams.ais_IsSiteUseYn               = this.ais_IsSiteUseYn;                 // AIS_실사용(대지)

            // 대지(배치)
            // 카테고리 - 프로젝트정보
            // TODO : AIS_공구상면적이 맞는지 아니면 AIS_공부상면적이 맞는지 차주에 "조원호 팀장님"과 소통 진행해서 확인하기(2023.11.27 jbh)
            // AIS_공구상면적 - 타입 실수(double) 
            // tAISParams.공구상면적                    = 

            // 대지(배치) 
            // 카테고리 - 주차
            tAISParams.ais_OutsideParkingArea        = this.ais_OutsideParkingArea;          // AIS_대지주차구획

            return (object) tAISParams;
        }

        #endregion Clone

        #region PutData

        // TODO : 메서드 "PutData" 구현 예정 (2023.11.20 jbh)
        public void PutData(TAISParams pSource) 
        {
            this.PutData((RbModelBase) pSource);
            this.ais_LevelCode = pSource.ais_LevelCode;
            this.ais_AreaCode  = pSource.ais_AreaCode;
            // . . . 
        }

        #endregion PutData

        #region GetFieldValues

        // TODO : 추후 DB 설계 필요시 메서드 "GetFieldValues" 구현 예정 (2023.11.20 jbh)
        //public override bool GetFieldValues(RevitOleDbRecordset p_rs)
        //{
        //    try
        //    {
        //        this.ais_LevelCode = p_rs.GetFieldInt("ais_LevelCode");
        //        return true;
        //    }
        //    catch (Exception ex) 
        //    {
        //        this.message = " " + this.TableCode.ToDescription() + " 실패\n--------------------------------------------------------------------------------------------------\n 테이블 : " + this.TableCode.ToDescription() + "\n 메소드 : " + MethodBase.GetCurrentMethod().ReflectedType.Name + "==>" + MethodBase.GetCurrentMethod().Name + "\n" + string.Format(" LINE : {0} 행\n", (object)new StackFrame(0, true).GetFileLineNumber()) + "--------------------------------------------------------------------------------------------------\n 내용 : " + ex.Message + "\n--------------------------------------------------------------------------------------------------\n";
        //        Log.Logger.ErrorColor(this.message);
        //    }
        //    return false;
        //}

        #endregion GetFieldValues

        #region InsertQuery

        // TODO : 추후 DB 설계 필요시 메서드 "InsertQuery" 구현 예정 (2023.11.20 jbh)
        // public override string InsertQuery() => string.Format(" INSERT INTO {0}{1} (", (object)DbQueryHelper.ToDBNameBridge(RbModelBase.REVIT_BASE), (object)this.TableCode) + " br_BrandCode,br_SiteID,br_BrandName,br_UseYn,br_Memo,br_AddProperty,br_InDate,br_InUser,br_ModDate,br_ModUser) VALUES ( " + string.Format(" {0}", (object)this.br_BrandCode) + string.Format(",{0}", (object)this.br_SiteID) + string.Format(",'{0}','{1}','{2}',{3}", (object)this.br_BrandName, (object)this.br_UseYn, (object)this.br_Memo, (object)this.br_AddProperty) + string.Format(",{0},{1}", (object)this.br_InDate.GetDateToStrInNull(), (object)this.br_InUser) + string.Format(",{0},{1}", (object)this.br_ModDate.GetDateToStrInNull(), (object)this.br_ModUser) + ")";

        #endregion InsertQuery

        #region Insert

        // TODO : 추후 DB 설계 필요시 메서드 "Insert" 구현 예정 (2023.11.20 jbh)
        //public override bool Insert()
        //{
        //    this.InsertChecked();
        //    if (this.OleDB.Execute(this.InsertQuery()))
        //        return true;
        //    this.message = " " + this.TableCode.ToDescription() + " 실패\n--------------------------------------------------------------------------------------------------\n 테이블 : " + this.TableCode.ToDescription() + "\n 메소드 : " + MethodBase.GetCurrentMethod().ReflectedType.Name + "==>" + MethodBase.GetCurrentMethod().Name + "\n" + string.Format(" LINE : {0} 행\n", (object)new StackFrame(0, true).GetFileLineNumber()) + "--------------------------------------------------------------------------------------------------\n" + string.Format(" 에러 : {0}\n", (object)this.OleDB.LastErrorID) + string.Format(" 코드 : ({0},{1})\n", (object)this.br_BrandCode, (object)this.br_SiteID) + " 내용 : " + this.OleDB.LastErrorMessage + "\n--------------------------------------------------------------------------------------------------\n";
        //    Log.Logger.DebugColor(this.message);
        //    return false;
        //}

        #endregion Insert

        #region InsertAsync

        // TODO : 추후 DB 설계 필요시 메서드 "InsertAsync" 구현 예정 (2023.11.20 jbh)
        //public override async Task<bool> InsertAsync()
        //{
        //    TAISParams tAISParams = this;
        //    // ISSUE: reference to a compiler-generated method
        //    tAISParams.\u003C\u003En__0();
        //    if (await tbrand.OleDB.ExecuteAsync(tAISParams.InsertQuery()))
        //        return true;
        //    tAISParams.message = " " + tAISParams.TableCode.ToDescription() + " 실패\n--------------------------------------------------------------------------------------------------\n 테이블 : " + tbrand.TableCode.ToDescription() + "\n 메소드 : " + MethodBase.GetCurrentMethod().ReflectedType.Name + "==>" + MethodBase.GetCurrentMethod().Name + "\n" + string.Format(" LINE : {0} 행\n", (object)new StackFrame(0, true).GetFileLineNumber()) + "--------------------------------------------------------------------------------------------------\n" + string.Format(" 에러 : {0}\n", (object)tAISParams.OleDB.LastErrorID) + string.Format(" 코드 : ({0},{1})\n", (object)tbrand.br_BrandCode, (object)tAISParams.br_SiteID) + " 내용 : " + tAISParams.OleDB.LastErrorMessage + "\n--------------------------------------------------------------------------------------------------\n";
        //    Log.Logger.DebugColor(tAISParams.message);
        //    return false;
        //}

        #endregion InsertAsync

        #region UpdateQuery

        // TODO : 추후 DB 설계 필요시 메서드 "UpdateQuery" 구현 예정 (2023.11.20 jbh)
        // public override string UpdateQuery() => string.Format(" UPDATE {0}{1} SET ", (object)DbQueryHelper.ToDBNameBridge(RbModelBase.REVIT_BASE), (object)this.TableCode) + " br_BrandName='" + this.br_BrandName + "',br_UseYn='" + this.br_UseYn + "',br_Memo='" + this.br_Memo + "'" + string.Format(",{0}={1}", (object)"br_AddProperty", (object)this.br_AddProperty) + string.Format(",{0}={1},{2}={3}", (object)"br_ModDate", (object)this.br_ModDate.GetDateToStrInNull(), (object)"br_ModUser", (object)this.br_ModUser) + string.Format(" WHERE {0}={1}", (object)"br_BrandCode", (object)this.br_BrandCode) + string.Format(" AND {0}={1}", (object)"br_SiteID", (object)this.br_SiteID);

        #endregion UpdateQuery

        #region Update

        // TODO : 추후 DB 설계 필요시 메서드 "Update" 구현 예정 (2023.11.20 jbh)
        //public override bool Update(RbModelBase p_old = null)
        //{
        //    this.UpdateChecked();
        //    if (this.OleDB.Execute(this.UpdateQuery()))
        //        return true;
        //    this.message = " " + this.TableCode.ToDescription() + " 실패\n--------------------------------------------------------------------------------------------------\n 테이블 : " + this.TableCode.ToDescription() + "\n 메소드 : " + MethodBase.GetCurrentMethod().ReflectedType.Name + "==>" + MethodBase.GetCurrentMethod().Name + "\n" + string.Format(" LINE : {0} 행\n", (object)new StackFrame(0, true).GetFileLineNumber()) + "--------------------------------------------------------------------------------------------------\n" + string.Format(" 에러 : {0}\n", (object)this.OleDB.LastErrorID) + string.Format(" 코드 : ({0},{1})\n", (object)this.br_BrandCode, (object)this.br_SiteID) + " 내용 : " + this.OleDB.LastErrorMessage + "\n--------------------------------------------------------------------------------------------------\n";
        //    Log.Logger.DebugColor(this.message);
        //    return false;
        //}

        #endregion Update

        #region UpdateAsync

        // TODO : 추후 DB 설계 필요시 메서드 "UpdateAsync" 구현 예정 (2023.11.20 jbh)
        //public override async Task<bool> UpdateAsync(RbModelBase p_old = null)
        //{
        //    TAISParams tAISParams = this;
        //    // ISSUE: reference to a compiler-generated method
        //    tAISParams.\u003C\u003En__1();
        //    if (await tAISParams.OleDB.ExecuteAsync(tAISParams.UpdateQuery()))
        //        return true;
        //    tAISParams.message = " " + tAISParams.TableCode.ToDescription() + " 실패\n--------------------------------------------------------------------------------------------------\n 테이블 : " + tAISParams.TableCode.ToDescription() + "\n 메소드 : " + MethodBase.GetCurrentMethod().ReflectedType.Name + "==>" + MethodBase.GetCurrentMethod().Name + "\n" + string.Format(" LINE : {0} 행\n", (object)new StackFrame(0, true).GetFileLineNumber()) + "--------------------------------------------------------------------------------------------------\n" + string.Format(" 에러 : {0}\n", (object)tAISParams.OleDB.LastErrorID) + string.Format(" 코드 : ({0},{1})\n", (object)tAISParams.br_BrandCode, (object)tAISParams.br_SiteID) + " 내용 : " + tAISParams.OleDB.LastErrorMessage + "\n--------------------------------------------------------------------------------------------------\n";
        //    Log.Logger.DebugColor(tAISParams.message);
        //    return false;
        //}

        #endregion UpdateAsync

        #region UpdateExInsertMySQLQuery

        // TODO : 추후 DB 설계 필요시 메서드 "UpdateExInsertMySQLQuery" 구현 예정 (2023.11.20 jbh)
        //public override string UpdateExInsertMySQLQuery()
        //{
        //    StringBuilder stringBuilder = new StringBuilder();
        //    stringBuilder.Append(this.InsertQuery());

        //    return stringBuilder.ToString();
        //}

        #endregion UpdateExInsertMySQLQuery

        #region UpdateExInsert

        // TODO : 추후 DB 설계 필요시 메서드 "UpdateExInsertMySQLQuery" 구현 예정 (2023.11.20 jbh) 
        //public override bool UpdateExInsert()
        //{
        //    //this.UpdateChecked();
        //    //if (this.OleDB.Execute(this.UpdateExInsertQuery()))
        //    //    return true;
        //    //this.message = " " + this.TableCode.ToDescription() + " 실패\n--------------------------------------------------------------------------------------------------\n 테이블 : " + this.TableCode.ToDescription() + "\n 메소드 : " + MethodBase.GetCurrentMethod().ReflectedType.Name + "==>" + MethodBase.GetCurrentMethod().Name + "\n" + string.Format(" LINE : {0} 행\n", (object)new StackFrame(0, true).GetFileLineNumber()) + "--------------------------------------------------------------------------------------------------\n" + string.Format(" 에러 : {0}\n", (object)this.OleDB.LastErrorID) + string.Format(" 코드 : ({0},{1})\n", (object)this.br_BrandCode, (object)this.br_SiteID) + " 내용 : " + this.OleDB.LastErrorMessage + "\n--------------------------------------------------------------------------------------------------\n";
        //    //Log.Logger.DebugColor(this.message);
        //    return false;
        //}

        #endregion UpdateExInsert

        #region UpdateExInsertAsync

        // TODO : 추후 DB 설계 필요시 메서드 "UpdateExInsertAsync" 구현 예정 (2023.11.20 jbh) 
        //public override async Task<bool> UpdateExInsertAsync()
        //{
        //    return false;
        //}

        #endregion UpdateExInsertAsync

        #region GetSelectWhereAnd

        // TODO : 추후 DB 설계 필요시 메서드 "GetSelectWhereAnd" 구현 예정 (2023.11.20 jbh) 
        //public override string GetSelectWhereAnd(object p_param, bool p_isKeyWord) 
        //{
        //    StringBuilder stringBuilder = new StringBuilder(" WHERE");
        //    // . . . 
        //    return !stringBuilder.ToString().Equals(" WHERE") ? stringBuilder.Replace("WHERE AND", "WHERE").ToString() : string.Empty;
        //}

        #endregion GetSelectWhereAnd

        #region GetSelectQuery

        // TODO : 추후 DB 설계 필요시 메서드 "GetSelectQuery" 구현 예정 (2023.11.20 jbh) 
        //public override string GetSelectQuery(object p_param)
        //{
        //    StringBuilder stringBuilder = new StringBuilder();

        //    try
        //    {
        //        // . . . 
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    return stringBuilder.ToString();
        //}

        #endregion GetSelectQuery

        #region Sample

        #endregion Sample
    }
}
