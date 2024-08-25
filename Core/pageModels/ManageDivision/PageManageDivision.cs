﻿using Document_Control.Core.dbModels;
using System.ComponentModel.DataAnnotations;

namespace Document_Control.Core.pageModels.ManageDivision
{
    public class PageManageDivision
    {
        public List<DataDivision>? tbDivision { get; set; }
        public PageManageDivisionSave? pageManageDivisionSave { get; set; }
    }
    public class DataDivision : TbDivision 
    {
        [Required(ErrorMessage = "ระบุชื่อแผนก")]
        public string DivisionName { get; set; }
        public bool IsCanDelete { get; set; }
    }


    public class PageManageDivisionSave : TbDivision
    {

    }
}