using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Lap3.Views.Shared.Components.Major
{
    public class RenderMajor : PageModel
    {
        private readonly ILogger<RenderMajor> _logger;

        public RenderMajor(ILogger<RenderMajor> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}