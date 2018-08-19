using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRUD_Operation_In_MVC_Core.Model.Db
{
    public partial class Student
    {
        public Student()
        {
            Examresult = new HashSet<Examresult>();
            FeeNavigation = new HashSet<Fee>();
        }
        [Display(Name = "ID")]
        public int Id { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Fee")]
        public decimal? Fee { get; set; }
        [Display(Name = "Picture")]
        public string Picture { get; set; }

        public ICollection<Examresult> Examresult { get; set; }
        public ICollection<Fee> FeeNavigation { get; set; }
    }
}
