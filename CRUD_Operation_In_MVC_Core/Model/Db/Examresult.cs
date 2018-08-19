using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRUD_Operation_In_MVC_Core.Model.Db
{
    public partial class Examresult
    {
        [Display(Name="Serial No")]
        public int Examsl { get; set; }
        [Display(Name ="ID")]
        public int Studentid { get; set; }
        [Display(Name ="Exam Name")]
        public string Examname { get; set; }
        [Display (Name = "Institution")]
        public string Institution { get; set; }
        [Display(Name ="Board")]
        public string Board { get; set; }
        [Display(Name ="Result")]
        public string Result { get; set; }

        public Student Student { get; set; }
    }
}
