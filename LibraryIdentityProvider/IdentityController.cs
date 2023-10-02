﻿using Microsoft.AspNetCore.Mvc;

namespace LibraryIdentityProvider
{
    [ApiController]
    [Route("[controller]")]
    public class IdentityController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
