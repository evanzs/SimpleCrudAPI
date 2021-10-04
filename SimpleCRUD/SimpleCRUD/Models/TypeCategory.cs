using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace SimpleCRUD.Models
{
    public enum TypeCategory
    {
        [Description("Fiction")]
        Fiction,

        [Description("Romance")]        
        Romance,

        [Description("Infantil")]      
        Infant,

    }
}
