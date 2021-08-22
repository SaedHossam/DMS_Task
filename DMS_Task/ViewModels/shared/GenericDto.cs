using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DMS_Task.ViewModels.shared
{
    public abstract class GenericDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
