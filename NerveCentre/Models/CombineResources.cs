using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NerveCentre.Models
{
    public class CombineResources
    {
        public int CombineResourcesFactoryId { get; set; }
        public int CombineResourcesTVId { get; set; }
        public string CombineResourcesResourcePath { get; set; }
        public int CombineResourcesResourceId { get; set; }
        public decimal CombineResourcesDuration { get; set; }
        public bool CombineResourcesIsActive { get; set; }
        public bool CombineResourcesIsDeleted { get; set; }
        public int CombineResourcesSortOrder { get; set; }
    }
}