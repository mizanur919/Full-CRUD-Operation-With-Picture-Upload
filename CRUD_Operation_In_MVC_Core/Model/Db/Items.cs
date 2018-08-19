using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRUD_Operation_In_MVC_Core.Model.Db
{
    public partial class Items
    {
        [Display(Name="Item No")]
        public string Itemcode { get; set; }
        [Display(Name ="Name")]
        public string Itemname { get; set; }
        [Display(Name ="Picture")]
        public string Picture { get; set; }
    }
}
