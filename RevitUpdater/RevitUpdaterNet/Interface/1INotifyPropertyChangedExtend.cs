﻿using System.ComponentModel;
using System.Runtime.CompilerServices;

using Newtonsoft.Json;

namespace RevitUpdaterNet.Interface
{
    public interface INotifyPropertyChangedExtend : INotifyPropertyChanged
    {
        [JsonIgnore]
        bool EnablePropertyChanged { get; set; }

        // TODO : 추후 필요시 프로퍼티 "IsPropertyChanged" 사용 예정 (2024.04.15 jbh)
        // [JsonIgnore]
        // bool IsPropertyChanged { get; set; }

        void Changed([CallerMemberName] string name = "");

        void NotifyOfPropertyChange([CallerMemberName] string name = "");

        bool SetAndNotify<T>(ref T field, T value, [CallerMemberName] string name = "");
    }

    #region Sample

    #endregion Sample
}
