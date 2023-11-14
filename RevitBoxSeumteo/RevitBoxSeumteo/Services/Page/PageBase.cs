using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RevitBoxSeumteoNet;

namespace RevitBoxSeumteo.Services.Page
{
    [HiddenVm]
    public abstract class PageBase<TData> : PageBase
    {
        #region 프로퍼티 

        // TODO : 데이터 타입 프로퍼티 "DataType" 구현시 오류 발생
        //        오류 코드 "CS8370" - 오류 메시지 "'병합 할당' 기능(??= 연산)은 C# 7.3에서 사용할 수 없습니다. 8.0 이상의 언어 버전을 사용하세요."
        //        해당 오류 해결하기 위해 '병합 할당' 기능 (??=) 연산 대신 싱글톤 패턴으로 구현 (20023.11.10 jbh)
        // '병합 할당' 기능 (??=) 연산 
        // 참고 URL - https://developstudy.tistory.com/69
        // 참고 2 URL - https://velog.io/@jinuku/C-%EB%B0%8F-.-%EC%97%B0%EC%82%B0%EC%9E%90
        //public Type DataType => dataType ??= typeof(TData);
        //private Type dataType;
        // public Type DataType => dataType ??= typeof(TData);
        // public Type DataType => dataType ?? typeof(TData);
        // private Type dataType;

        // TODO : 데이터 타입 프로퍼티 "DataType" 필요시 사용 예정
        /// <summary>
        /// 데이터 타입
        /// </summary>
        public Type DataType
        { 
            get
            {
                // 싱글톤 패턴 
                if (_DataType == null)
                {
                    _DataType = typeof(TData);
                }
                return _DataType;
            }
            set => _DataType = value;
        }
        private Type _DataType;


        public ObservableCollection<TData> Datas { get; } = new ObservableCollection<TData>();

        public TData SelectedData { get => _SelectedData; set { SetAndNotify(ref _SelectedData, value); } }
        private TData _SelectedData;

        public ObservableCollection<TData> SelectedDatas { get; } = new ObservableCollection<TData>();

        #endregion 프로퍼티

        #region 생성자

        public PageBase()
        {

        }

        #endregion 생성자

        #region OnQueryDataHeaders

        // TODO : 메서드 "OnQueryDataHeaders" 필요시 구현 예정 (2023.11.10 jbh)
        //public override IReadOnlyDictionary<string, TableColumnInfo> OnQueryDataHeaders()
        //{
        //    return App.Sys?.TableColumns?.GetDictionary<TData>();
        //}

        #endregion OnQueryDataHeaders
    }

    // TODO : 아래 abstract 클래스 PageBase 구현 예정 (2023.11.10 jbh)
    [HiddenVm]
    public abstract class PageBase  : BindableBase, IPage // BindableBase, Screen
    {
       
    }

}
