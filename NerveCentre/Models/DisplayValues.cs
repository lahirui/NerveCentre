using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NerveCentre.Models
{
    public class DisplayValues
    {
        public int? ModelFactoryId { get; set; }
        public int? ModelTelevisionId { get; set; }
        public int? ModelResourceId { get; set; }
        public string ModelResourcePath { get; set; }
        public Nullable<decimal> ModelDuration { get; set; }
        public bool? ModelIsActive { get; set; }
        public bool? ModelIsDeleted { get; set; }
        public int? ModelSortOrder { get; set; }

    }
}