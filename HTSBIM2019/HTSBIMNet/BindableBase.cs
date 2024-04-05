using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

using Newtonsoft.Json;

using HTSBIMNet.Extensions;
using HTSBIMNet.Interface;

namespace HTSBIMNet
{
    public abstract class BindableBase : INotifyPropertyChangedExtend
    {
        private bool _isPropertyChanged;

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
        public bool _EnablePropertyChanged { get; set; } = true;

        [JsonIgnore]
        public bool _IsPropertyChanged
        {
            get => this._isPropertyChanged && !(this._isPropertyChanged = false);
            set => this._isPropertyChanged = value;
        }

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

        protected virtual void OnPropertyChanged(string name)
        {
            if (!this._EnablePropertyChanged)
                return;
            PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            // TODO : if 조건절에 != null 보다 빠른 is not null 연산자 사용 (2023.11.24 jbh)
            // 참고 URL - https://husk321.tistory.com/405
            if (propertyChanged is not null)
                propertyChanged((object)this, new PropertyChangedEventArgs(name));
            this._IsPropertyChanged = true;
        }

        public PropertyChangedDisableSection StartPropertyChangedDisableSection() => new PropertyChangedDisableSection(this);

        #region Sample

        #endregion Sample
    }
}
