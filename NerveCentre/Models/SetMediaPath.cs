using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NerveCentre.Models
{
    public class SetMediaPath
    {
        public int SetMediaPathFactoryId { get; set; }
        public int SetMediaPathTVId { get; set; }
        public string SetMediaPathResourcePath { get; set; }
        public int SetMediaPathResourceId { get; set; }
        public decimal SetMediaPathDuration { get; set; }
        public bool SetMediaPathIsActive { get; set; }
        public bool SetMediaPathIsDeleted { get; set; }
        public int SetMediaPathSortOrder { get; set; }
    }
}