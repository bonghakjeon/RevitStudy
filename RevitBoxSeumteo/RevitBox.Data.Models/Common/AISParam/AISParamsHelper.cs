using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitBox.Data.Models.Common.AISParam
{
    public class AISParamsHelper
    {
        #region 메소드 타입

        /// <summary>
        /// 반환형 void 메소드 타입
        /// </summary>
        public const string MethodType = "void";

        /// <summary>
        /// 비동기 메소드 타입
        /// </summary>
        public const string AsyncMethodType = "async";

        #endregion 메소드 타입



        #region AISParamsCreateBoardV

        #region 공통 

        // Title, Button 공통 
        public const string 매개변수생성 = "AIS 매개변수 생성";

        #endregion 공통

        #region Title

        #endregion Title

        #region Text

        public const string 매개변수생성클릭 = "버튼 AIS 매개변수 생성을 클릭하세요.";

        public const string 매개변수생성중 = "AIS 매개변수 생성 중...";

        public const string 매개변수생성완료 = "AIS 매개변수 생성 완료!";

        #endregion Text

        #region Button



        #endregion Button

        #region Message

        #endregion Message

        #endregion AISParamsCreateBoardV



        #region 공통 - 속성값

        /// <summary>
        /// 분야 - 개요, 건축, 대지(배치)
        /// 매개변수
        /// 개요 - AIS_연면적제외, AIS_용적률제외, AIS_바닥면적제외
        /// 건축 - AIS_비상용승강기, AIS_피난용승강기, AIS_특별피난계단, AIS_피난계단, AIS_배연창
        /// 대지(배치) - AIS_실사용
        /// 속성값 - Yes (BOOL)
        /// </summary>
        public const string Yes = "Yes";

        /// <summary>
        /// 분야 - 개요, 건축, 대지(배치)
        /// 매개변수
        /// 개요 - AIS_연면적제외, AIS_용적률제외, AIS_바닥면적제외
        /// 건축 - AIS_비상용승강기, AIS_피난용승강기, AIS_특별피난계단, AIS_피난계단, AIS_배연창
        /// 대지(배치) - AIS_실사용
        /// 속성값 - No (BOOL)
        /// </summary>
        public const string No = "No";


        /// <summary>
        /// 건축, 대지(배치)
        /// 매개변수 AIS_건축주차구획, AIS_대지주차구획 - 속성값 "일반"
        /// </summary>
        public const string 일반 = "일반";

        /// <summary>
        /// 건축, 대지(배치)
        /// 매개변수 AIS_건축주차구획, AIS_대지주차구획 - 속성값 "장애인"
        /// </summary>
        public const string 장애인 = "장애인";

        /// <summary>
        /// 건축, 대지(배치)
        /// 매개변수 AIS_건축주차구획, AIS_대지주차구획 - 속성값 "경차"
        /// </summary>
        public const string 경차 = "경차";

        /// <summary>
        /// 건축, 대지(배치)
        /// 매개변수 AIS_건축주차구획, AIS_대지주차구획 - 속성값 "여성"
        /// </summary>
        public const string 여성 = "여성";

        #endregion 공통 - 속성값

        #region 공통 - 카테고리 레벨

        #region 매개변수 

        /// <summary>
        /// AIS_분류코드 - 타입 코드(string)
        /// </summary>
        public const string AIS_분류코드 = "AIS_분류코드";

        #endregion 매개변수 

        #region 속성값

        /// <summary>
        /// 매개변수 AIS_분류코드 속성값 - L지상
        /// </summary>
        public const string L지상 = "L지상";

        /// <summary>
        /// 매개변수 AIS_분류코드 속성값 - L지하
        /// </summary>
        public const string L지하 = "L지하";

        /// <summary>
        /// 매개변수 AIS_분류코드 속성값 - L옥탑
        /// </summary>
        public const string L옥탑 = "L옥탑";

        /// <summary>
        /// 매개변수 AIS_분류코드 속성값 - L피트
        /// </summary>
        public const string L피트 = "L피트";

        #endregion 속성값

        #endregion 공통 - 카테고리 레벨

        #region 개요 - 카테고리 면적

        #region 매개변수 

        /// <summary>
        /// AIS_개요분류코드 - 타입 코드(string)
        /// </summary>
        public const string AIS_개요분류코드 = "AIS_개요분류코드";

        /// <summary>
        /// AIS_연면적제외 - 타입(bool)
        /// </summary>
        public const string AIS_연면적제외 = "AIS_연면적제외";

        /// <summary>
        /// AIS_용적률제외 - 타입(bool)
        /// </summary>
        public const string AIS_용적률제외 = "AIS_용적률제외";

        /// <summary>
        /// AIS_바닥면적제외 - 타입(bool)
        /// </summary>
        public const string AIS_바닥면적제외 = "AIS_바닥면적제외";

        #endregion 매개변수 

        #region 속성값

        /// <summary>
        /// 매개변수 AIS_개요분류코드 속성값 - B
        /// </summary>
        public const string B = "B";

        /// <summary>
        /// 매개변수 AIS_개요분류코드 속성값 - A+시설(용도)분류코드 (예: A14202)
        /// </summary>
        public const string A = "A";

        /// <summary>
        /// 매개변수 AIS_개요분류코드 속성값 - F
        /// </summary>
        public const string F = "F";

        #endregion 속성값

        #endregion 개요 - 카테고리 면적

        #region 건축 

        #region 카테고리 - 룸

        #region 매개변수 

        /// <summary>
        /// AIS_건축분류코드 - 타입 코드(string)
        /// </summary>
        public const string AIS_건축분류코드 = "AIS_건축분류코드";

        /// <summary>
        /// AIS_비상용승강기 - 타입 bool
        /// </summary>
        public const string AIS_비상용승강기 = "AIS_비상용승강기";

        /// <summary>
        /// AIS_피난용승강기 - 타입 bool
        /// </summary>
        public const string AIS_피난용승강기 = "AIS_피난용승강기";

        /// <summary>
        /// AIS_특별피난계단 - 타입 bool
        /// </summary>
        public const string AIS_특별피난계단 = "AIS_특별피난계단";

        /// <summary>
        /// AIS_피난계단 - 타입 bool
        /// </summary>
        public const string AIS_피난계단 = "AIS_피난계단";

        /// <summary>
        /// AIS_바닥마감 - 타입 문자(char)
        /// </summary>
        public const string AIS_바닥마감 = "AIS_바닥마감";

        /// <summary>
        /// AIS_바닥번호 - 타입 문자(char)
        /// </summary>
        public const string AIS_바닥번호 = "AIS_바닥번호";

        /// <summary>
        /// AIS_걸레받이마감 - 타입 문자(char)
        /// </summary>
        public const string AIS_걸레받이마감 = "AIS_걸레받이마감";

        /// <summary>
        /// AIS_걸레받이번호 - 타입 문자(char)
        /// </summary>
        public const string AIS_걸레받이번호 = "AIS_걸레받이번호";

        /// <summary>
        /// AIS_벽마감 - 타입 문자(char)
        /// </summary>
        public const string AIS_벽마감 = "AIS_벽마감";

        /// <summary>
        /// AIS_벽번호 - 타입 문자(char)
        /// </summary>
        public const string AIS_벽번호 = "AIS_벽번호";

        /// <summary>
        /// AIS_천장마감 - 타입 문자(char)
        /// </summary>
        public const string AIS_천장마감 = "AIS_천장마감";

        /// <summary>
        /// AIS_천장번호 - 타입 문자(char)
        /// </summary>
        public const string AIS_천장번호 = "AIS_천장번호";

        #endregion 매개변수

        #region 속성값

        /// <summary>
        /// 매개변수 AIS_건축분류코드 속성값 - 기능분류체계
        /// </summary>
        public const string 기능분류체계 = "기능분류체계";


        /// <summary>
        /// 매개변수 AIS_바닥마감 속성값 - 바닥마감재
        /// </summary>
        public const string 바닥마감재 = "바닥마감재";

        /// <summary>
        /// 매개변수 AIS_바닥번호 속성값 - 바닥마감상세번호
        /// </summary>
        public const string 바닥마감상세번호 = "바닥마감상세번호";


        /// <summary>
        /// 매개변수 AIS_걸레받이마감 속성값 - 걸레받이마감재
        /// </summary>
        public const string 걸레받이마감재 = "걸레받이마감재";

        /// <summary>
        /// 매개변수 AIS_걸레받이번호 속성값 - 걸레받이마감상세번호
        /// </summary>
        public const string 걸레받이마감상세번호 = "걸레받이마감상세번호";

        /// <summary>
        /// 매개변수 AIS_벽마감 속성값 - 벽마감재
        /// </summary>
        public const string 벽마감재 = "벽마감재";

        /// <summary>
        /// 매개변수 AIS_벽번호 속성값 - 벽마감상세번호
        /// </summary>
        public const string 벽마감상세번호 = "벽마감상세번호";

        /// <summary>
        /// 매개변수 AIS_천장마감 속성값 - 천장마감재
        /// </summary>
        public const string 천장마감재 = "천장마감재";

        /// <summary>
        /// 매개변수 AIS_천장번호 속성값 - 천장마감상세번호
        /// </summary>
        public const string 천장마감상세번호 = "천장마감상세번호";

        #endregion 속성값

        #endregion 카테고리 - 룸

        #region 카테고리 - 문

        #region 매개변수 

        /// <summary>
        /// AIS_방화등급 - 타입 문자(char)
        /// </summary>
        public const string AIS_방화등급 = "AIS_방화등급";

        #endregion 매개변수 

        #region 속성값

        /// <summary>
        /// 매개변수 AIS_방화등급 - 속성값
        /// </summary>
        public const string 갑 = "갑";

        /// <summary>
        /// 매개변수 AIS_방화등급 - 속성값
        /// </summary>
        public const string 을 = "을";

        #endregion 속성값

        #endregion 카테고리 - 문

        #region 카테고리 - 창

        #region 매개변수

        /// <summary>
        /// AIS_배연창 - 타입 bool
        /// </summary>
        public const string AIS_배연창 = "AIS_배연창";

        #endregion 매개변수

        #endregion 카테고리 - 창

        #region 카테고리 - 주차

        /// <summary>
        /// AIS_건축주차구획 - 타입 문자(char)
        /// </summary>
        public const string AIS_건축주차구획 = "AIS_건축주차구획";

        #endregion 카테고리 - 주차

        #endregion 건축

        #region 대지(배치)

        #region 카테고리 - 면적

        #region 매개변수 

        /// <summary>
        /// AIS_대지분류코드 - 타입 코드(string)
        /// </summary>
        public const string AIS_대지분류코드 = "AIS_대지분류코드";

        /// <summary>
        /// AIS_실사용 - 타입 bool
        /// </summary>
        public const string AIS_실사용 = "AIS_실사용";

        #endregion 매개변수 

        #region 속성값

        /// <summary>
        /// 매개변수 AIS_대지분류코드 - 속성값
        /// </summary>
        public const string S = "S";

        #endregion 속성값

        #endregion 카테고리 - 면적

        #region 카테고리 - 프로젝트정보

        #region 매개변수 

        /// <summary>
        /// AIS_공구상면적 - 타입 실수(double)
        /// </summary>
        public const string AIS_공구상면적 = "AIS_공구상면적";

        #endregion 매개변수 

        #endregion 카테고리 - 프로젝트정보

        #region 카테고리 - 주차

        #region 매개변수 

        /// <summary>
        /// AIS_대지주차구획 - 타입 문자(char)
        /// </summary>
        public const string AIS_대지주차구획 = "AIS_대지주차구획";

        #endregion 매개변수

        #endregion 카테고리 - 주차

        #endregion 대지(배치)
    }
}
