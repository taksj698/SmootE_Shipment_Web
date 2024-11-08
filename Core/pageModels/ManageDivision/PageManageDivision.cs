﻿using SmootE_Shipment_Web.Core.dbModels;
using System.ComponentModel.DataAnnotations;

namespace SmootE_Shipment_Web.Core.pageModels.ManageDivision
{
    public class PageManageDivision
    {
        public List<DataDivision>? tbDivision { get; set; }
        public PageManageDivisionSave? pageManageDivisionSave { get; set; }
    }
    public class DataDivision //: TbDivision 
    {
        public bool IsCanDelete { get; set; }
    }


    public class PageManageDivisionSave
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "ระบุชื่อแผนก")]
        public string DivisionName { get; set; }
    }
}
