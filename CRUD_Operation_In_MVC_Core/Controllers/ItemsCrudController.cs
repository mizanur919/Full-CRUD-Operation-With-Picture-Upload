using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_Operation_In_MVC_Core.Controllers
{
    public class ItemsCrudController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}