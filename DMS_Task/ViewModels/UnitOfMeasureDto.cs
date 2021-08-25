using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DMS_Task.ViewModels
{
    public class UnitOfMeasureDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class UnitOfMeasureCreateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
