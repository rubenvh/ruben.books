using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Antlr.Runtime;
using Ruben.Books.DataLayer;
using Ruben.Books.Domain;
using Ruben.Books.Repository;
using Ruben.Books.Web.Models;
using WebGrease.Css.Extensions;

namespace Ruben.Books.Web.Controllers
{
    public class BooksController : Controller
    {
        private IBooksRepository _repo;
        private IUnitOfWork<BooksContext> _unitOfWork;
        private ICategoryRepository _categoryRepository;

        public BooksController(IBooksRepository repo, IUnitOfWork<BooksContext> unitOfWork, ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
            _repo = repo;
        }

        //
        // GET: /Books/

        public ActionResult Index()
        {
            var books = _repo.AllIncluding(_ => _.Category, _ => _.Authors, _ => _.Readings).ToList();
            var viewModel = books.Select(_ => new BookVM(_)).ToList();
            ViewBag.Title = "All books";
            return View("BookVMIndex", viewModel);
        }

        //
        // GET: /Books/Details/5

        public ActionResult Details(int id)
        {
            var book = _repo.AllIncluding(_ => _.Category, _ => _.Authors, _ => _.Readings)
                .Single(_ => _.Id == id);
            
            return View(book);
        }

        //
        // GET: /Books/Create

        public ActionResult Create()
        {
            ViewData["Categories"] = new SelectList(_categoryRepository.All.ToList(), "Id", "Name", 1);
            return View();
        }

        //
        // POST: /Books/Create

        [HttpPost]
        public ActionResult Create(Book book)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    book.State = State.Added;
                    _repo.InsertOrUpdateGraph(book);
                    _unitOfWork.Save();
                    return RedirectToAction("Details", new { id=book.Id});
                }
            }
            catch
            {
                // TODO: write error on view
            }
            // TODO: make sure validation errors are passed to client
            ViewData["Categories"] = new SelectList(_categoryRepository.All.ToList(), "Id", "Name", 1);
            return View(book);
        }

        //
        // GET: /Books/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Books/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Books/Delete/5

        public ActionResult Delete(int id)
        {
            var book = _repo.AllIncluding(_ => _.Category, _ => _.Authors, _ => _.Readings)
                .Single(_ => _.Id == id);
            
            return View(book);
        }

        //
        // POST: /Books/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                _repo.Delete(id);
                _unitOfWork.Save();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
