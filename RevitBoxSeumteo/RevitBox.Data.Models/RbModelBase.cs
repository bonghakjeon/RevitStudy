using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RevitBox.Data.Models.Interface;
using RevitBoxSeumteoNet;

namespace RevitBox.Data.Models
{
    // TODO : 추후 필요시 "RbModelBase" 클래스 구현 예정 (2023.11.17 jbh)
    public class RbModelBase<T> : RbModelBase where T : RbModelBase<T> { }


    // TODO : 추후 필요시 "RbClass" 클래스, "ICloneable" 인터페이스 구현 예정 (2023.11.17 jbh)
    public class RbModelBase : BindableBase // RbModelBase // RbClass, ICloneable
    {
    }
}
