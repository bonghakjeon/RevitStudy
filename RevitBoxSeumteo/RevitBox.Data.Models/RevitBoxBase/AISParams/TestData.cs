using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RevitBox.Data.Models.RevitBoxBase.AISParams.AISParamsView;
using RevitBoxSeumteoNet;

namespace RevitBox.Data.Models.RevitBoxBase.AISParams
{
    // TODO : 멀티 콤보박스(Multi ComboBox) 관련 테스트 클래스(Test) 구현 (필요 없을시 추후 삭제 예정) (2023.11.16 jbh)  
    // 참고 URL - https://m.blog.naver.com/goldrushing/221230210966  
    public class TestData : BindableBase
    {
        /// <summary>
        /// 테스트용 국가 데이터 리스트
        /// </summary>
        public IList<AISParams_Type> TestParamsTypeList { get => _TestParamsTypeList; set { _TestParamsTypeList = value; NotifyOfPropertyChange(); } }
        private IList<AISParams_Type> _TestParamsTypeList = new List<AISParams_Type>();

        /// <summary>
        /// 테스트용 도시 데이터 리스트 
        /// </summary>
        public IList<AISParams_Value> TestParamsValueList { get => _TestParamsValueList; set { _TestParamsValueList = value; NotifyOfPropertyChange(); } }
        private IList<AISParams_Value> _TestParamsValueList = new List<AISParams_Value>();

        public TestData()
        {
            AISParamsTypeCreate();   // 테스트용 국가 데이터 초기화
            AISParamsValueCreate();  // 테스트용 도시 데이터 초기화
        }

        /// <summary>
        /// 테스트용 국가 데이터 초기화
        /// </summary>
        private void AISParamsTypeCreate()
        {
            TestParamsTypeList.Add(new AISParams_Type() { Seq = 1, DivCode = "UK", DivName = "United Kingdom", OrderIdx = 1 });
            TestParamsTypeList.Add(new AISParams_Type() { Seq = 2, DivCode = "US", DivName = "USA", OrderIdx = 2 });
            TestParamsTypeList.Add(new AISParams_Type() { Seq = 3, DivCode = "SE", DivName = "Sweden", OrderIdx = 3 });
            TestParamsTypeList.Add(new AISParams_Type() { Seq = 4, DivCode = "KR", DivName = "Korea", OrderIdx = 4 });
            TestParamsTypeList.Add(new AISParams_Type() { Seq = 5, DivCode = "00", DivName = "=Country=", OrderIdx = 0 });
        }

        /// <summary>
        /// 테스트용 도시 데이터 초기화
        /// </summary>
        private void AISParamsValueCreate()
        {
            TestParamsValueList.Add(new AISParams_Value() { Seq = 1, DivCode = "UK", SubDivCode = "UK1", SubDivName = "London", OrderIdx = 1 });
            TestParamsValueList.Add(new AISParams_Value() { Seq = 2, DivCode = "UK", SubDivCode = "UK2", SubDivName = "Birmingham", OrderIdx = 2 });
            TestParamsValueList.Add(new AISParams_Value() { Seq = 3, DivCode = "UK", SubDivCode = "UK3", SubDivName = "Glasgow", OrderIdx = 3 });

            TestParamsValueList.Add(new AISParams_Value() { Seq = 4, DivCode = "US", SubDivCode = "US1", SubDivName = "Los Angeles", OrderIdx = 1 });
            TestParamsValueList.Add(new AISParams_Value() { Seq = 5, DivCode = "US", SubDivCode = "US2", SubDivName = "New York", OrderIdx = 2 });
            TestParamsValueList.Add(new AISParams_Value() { Seq = 6, DivCode = "US", SubDivCode = "US3", SubDivName = "Washington", OrderIdx = 3 });

            TestParamsValueList.Add(new AISParams_Value() { Seq = 7, DivCode = "SE", SubDivCode = "SE1", SubDivName = "Stockholm", OrderIdx = 1 });
            TestParamsValueList.Add(new AISParams_Value() { Seq = 8, DivCode = "SE", SubDivCode = "SE2", SubDivName = "Goteborg", OrderIdx = 2 });
            TestParamsValueList.Add(new AISParams_Value() { Seq = 9, DivCode = "SE", SubDivCode = "SE3", SubDivName = "Malmo", OrderIdx = 3 });

            TestParamsValueList.Add(new AISParams_Value() { Seq = 10, DivCode = "KR", SubDivCode = "KR1", SubDivName = "Seoul", OrderIdx = 1 });
            TestParamsValueList.Add(new AISParams_Value() { Seq = 11, DivCode = "KR", SubDivCode = "KR2", SubDivName = "Busan", OrderIdx = 2 });
            TestParamsValueList.Add(new AISParams_Value() { Seq = 12, DivCode = "KR", SubDivCode = "KR3", SubDivName = "Daegu", OrderIdx = 3 });
        }

        // 초기화된 데이터를 콤보박스에 사용할 수 있도록 메서드 "GetAllDivCom", "FindComCode" 추가
        /// <summary>
        /// 테스트용 국가 데이터 리스트(AISParams_Types)에 속한 데이터를 
        /// Enumerable 형식의 오름차순으로 정렬후 데이터 가져오기
        /// </summary>
        //public IEnumerable<AISParams_Type> GetAllDivTypes()
        //{
        //    // 오름차순 정렬 (내림차순은 OrderByDescending(o=>o.OrderIdx)
        //    return AISParams_Types.OrderBy(o => o.OrderIdx);
        //}

        public List<AISParams_Type> GetAllDivTypes()
        {
            // 오름차순 정렬 (내림차순은 OrderByDescending(o=>o.OrderIdx)
            return TestParamsTypeList.OrderBy(o => o.OrderIdx).ToList();
        }

        /// <summary>
        /// 메서드 파라미터 "pDivCode"에 해당하는 값들을
        /// 테스트용 도시 리스트(AISParams_Values)에서 선별해서 순서대로 가져오기
        /// </summary>
        //public IEnumerable<AISParams_Value> FindSubDivValues(string pDivCode)
        //{
        //    // (예) DivCode = "KR" 이면 KR에 해당하는 "Seoul","Busan","Daegu"의 세 값을 가져온다.
        //    return AISParams_Values.Where(nation => nation.DivCode.Equals(pDivCode)).OrderBy(o => o.OrderIdx);
        //}

        public List<AISParams_Value> FindSubDivValues(string pDivCode)
        {
            // (예) DivCode = "KR" 이면 KR에 해당하는 "Seoul","Busan","Daegu"의 세 값을 가져온다.
            return TestParamsValueList.Where(nation => nation.DivCode.Equals(pDivCode)).OrderBy(o => o.OrderIdx).ToList();
        }
    }
}
