using AuthTutorial.Resource.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthTutorial.Resource.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookStore store;

        public BooksController(BookStore store)
        {
            this.store = store;
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetAvailbleBooks()
        {
            return Ok(store.Books);
        }
    }
}
