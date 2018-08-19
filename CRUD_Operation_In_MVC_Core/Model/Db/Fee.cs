using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRUD_Operation_In_MVC_Core.Model.Db
{
    public partial class Fee
    {
        [Display(Name="Voucher No")]
        public string Voucherno { get; set; }
        [Display(Name ="Date")]
        public DateTime? InputDate { get; set; }
        [Display(Name ="Student ID")]
        public int? StudentId { get; set; }
        [Display (Name ="Head Name")]
        public string Headname { get; set; }
        [Display(Name ="Amount")]
        public decimal? Amount { get; set; }
        public Student Student { get; set; }
    }
}
