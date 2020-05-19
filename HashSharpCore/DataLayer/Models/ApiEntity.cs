using System;
using System.Collections.Generic;
using System.Text;
using HashSharpCore.Models.Contracts;

namespace HashSharpCore.DataLayer.Models
{
    public class ApiEntity : IApiEntity
    {
        public bool IsRemoved { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime RemoveTime { get; set; }
    }
}
