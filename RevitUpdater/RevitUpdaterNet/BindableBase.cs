using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

using Newtonsoft.Json;

using RevitUpdaterNet.Extensions;
using RevitUpdaterNet.Interface;

namespace RevitUpdaterNet
{
    public abstract class BindableBase : INotifyPropertyChangedExtend
    {
        public static event PropertyChangedEventHandler StaticPropertyChanged;

        public static void StaticChanged([CallerMemberName] string name = null)
        {
            PropertyChangedEventHandler staticPropertyChanged = BindableBase.StaticPropertyChanged;
            // TODO : if 조건절에 == null 보다 빠른 is null 연산자 사용 (2023.11.24 jbh)
            // 참고 URL - https://husk321.tistory.com/405
            if (staticPropertyChanged is null)
                return;
            staticPropertyChanged((object)null, new PropertyChangedEventArgs(name));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [JsonIgnore]
        public bool EnablePropertyChanged { get; set; } = true;

        //[JsonIgnore]
        //public bool IsPropertyChanged
        //{
        //    get => this._IsPropertyChanged && !(this._IsPropertyChanged = false);
        //    set => this._IsPropertyChanged = value;
        //}
        //private bool _IsPropertyChanged;

        public void Changed<TProperty>(Expression<Func<TProperty>> property) => this.OnPropertyChanged(property.GetMemberInfo().Name);

        public void Changed([CallerMemberName] string name = "") => this.OnPropertyChanged(name);

        public void Changed(params string[] names)
        {
            // TODO : if 조건절에 != null 보다 빠른 is not null 연산자 사용 (2023.11.24 jbh)
            // 참고 URL - https://husk321.tistory.com/405
            if ((names is not null ? names.Length : 0) <= 0)
                return;
            foreach (string name in names)
                this.OnPropertyChanged(name);
        }

        public void NotifyOfPropertyChange<TProperty>(Expression<Func<TProperty>> property) => this.OnPropertyChanged(property.GetMemberInfo().Name);

        public void NotifyOfPropertyChange([CallerMemberName] string name = "") => this.OnPropertyChanged(name);

        public void NotifyOfPropertyChange(params string[] names)
        {
            // TODO : if 조건절에 != null 보다 빠른 is not null 연산자 사용 (2023.11.24 jbh)
            // 참고 URL - https://husk321.tistory.com/405
            if ((names is not null ? names.Length : 0) <= 0)
                return;
            foreach (string name in names)
                this.OnPropertyChanged(name);
        }

        public bool SetAndNotify<T>(ref T field, T value, [CallerMemberName] string name = "")
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
                return false;
            field = value;
            this.OnPropertyChanged(name);
            return true;
        }

        // TODO : 이벤트 메서드 OnPropertyChanged 구현 (2024.04.15 jbh)
        // 참고 URL - https://nomadcoder.tistory.com/entry/WPF-%EC%B4%88%EA%B0%84%EB%8B%A8-INotifyPropertyChanged-%EA%B5%AC%ED%98%84%ED%95%98%EA%B8%B0#google_vignette

        protected virtual void OnPropertyChanged(string propertyName)
        {
            // if (!this.EnablePropertyChanged)
            if (false == this.EnablePropertyChanged)
                return;
            PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            // TODO : if 조건절에 != null 보다 빠른 is not null 연산자 사용 (2024.04.11 jbh)
            // 참고 URL - https://husk321.tistory.com/405
            //if (propertyChanged is not null)
            //    // PropertyChangedEventArgs 클래스의 새 인스턴스(이벤트 핸들러 - propertyChanged) 초기화
            //    // 참고 URL - https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.propertychangedeventargs?view=net-8.0
            //    // 참고 2 URL - https://fusionit.tistory.com/26
            //    propertyChanged((object)this, new PropertyChangedEventArgs(name));

            // PropertyChanged 가 null이 아니면 Invoke를 호출한다
            // 참고 URL - https://m.cafe.daum.net/aspdotnet/6TQG/3149
            // 참고 2 URL - https://hwigyeom.ntils.com/3
            // 참고 3 URL - https://cafe.daum.net/aspdotnet/WJSu/65
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            // TODO : 추후 필요시 프로퍼티 "IsPropertyChanged" 사용 예정 (2024.04.15 jbh)
            // this.IsPropertyChanged = true;
        }

        public PropertyChangedDisableSection StartPropertyChangedDisableSection() => new PropertyChangedDisableSection(this);

        #region Sample

        #endregion Sample
    }
}
