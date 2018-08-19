using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRUD_Operation_In_MVC_Core.Model.Db
{
    public partial class Mystuinfo
    {
        [Display(Name = "ID")]
        public int Id { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
