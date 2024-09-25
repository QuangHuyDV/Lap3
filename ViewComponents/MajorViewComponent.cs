using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lap3.Data;
using Lap3.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lap3.ViewComponents
{
    public class MajorViewComponent:ViewComponent
    {
        SchoolContext db;
        List<Major> majors;

        public MajorViewComponent(SchoolContext _context) {
            db = _context;
            majors = db.Majors.ToList();
        }

        public async Task<IViewComponentResult> InvokeAsync() {
            return View("RenderMajor", majors);
        }
    }
}