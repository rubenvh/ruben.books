using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ruben.Books.Repository;

namespace Ruben.Books.Web.Controllers
{
    public class AuthorController : Controller
    {
        private IAuthorRepository _authorRepository;

        public AuthorController(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        //
        // GET: /Author/Find/x

        public JsonResult Find(string term)
        {
            var found = _authorRepository.FindAuthorByName(term);
            return Json(found.Select(_ => new {Id = _.Id, Name = _.Name}).ToList(),
                JsonRequestBehavior.AllowGet);
        }

    }
}
