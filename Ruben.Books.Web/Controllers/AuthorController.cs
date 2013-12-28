using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ruben.Books.Repository;
using Ruben.Books.Domain;
using Ruben.Books.DataLayer;

namespace Ruben.Books.Web.Controllers
{
    public class AuthorController : Controller
    {
        private IAuthorRepository _authorRepository;
        private IUnitOfWork<BooksContext> _unitOfWork;

        public AuthorController(IAuthorRepository authorRepository, IUnitOfWork<BooksContext> unitOfWork)
        {
            _authorRepository = authorRepository;
            _unitOfWork = unitOfWork;
        }

        //
        // GET: /Author/Find/x

        public JsonResult Find(string term)
        {
            var found = _authorRepository.FindAuthorByName(term);
            return Json(found.Select(_ => new {Id = _.Id, Name = _.Name}).ToList(),
                JsonRequestBehavior.AllowGet);
        }

        public JsonResult Create(string name)
        {
            var found = _authorRepository.FindAuthorByName(name);

            int authorId = 0;
            if (found.Count() == 0)
            {
                var author = new Author()
                {
                    State = State.Added,
                    Name = name
                };

                _authorRepository.InsertOrUpdate(author);
                _unitOfWork.Save();
                authorId = author.Id;
            }
            else
            {
                authorId = found.First().Id;
            }
            return Json(new { Id = authorId, Name = name }, JsonRequestBehavior.AllowGet);
        }

    }
}
