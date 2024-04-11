using System.ComponentModel;
using System.Runtime.CompilerServices;

using Newtonsoft.Json;

namespace RevitUpdaterNet.Interface
{
    public interface INotifyPropertyChangedExtend : INotifyPropertyChanged
    {
        [JsonIgnore]
        bool _EnablePropertyChanged { get; set; }

        [JsonIgnore]
        bool _IsPropertyChanged { get; set; }

        void Changed([CallerMemberName] string name = "");

        void NotifyOfPropertyChange([CallerMemberName] string name = "");

        bool SetAndNotify<T>(ref T field, T value, [CallerMemberName] string name = "");
    }

    #region Sample

    #endregion Sample
}
