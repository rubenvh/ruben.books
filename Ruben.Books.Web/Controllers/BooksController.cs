using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ruben.Books.DataLayer;
using Ruben.Books.Repository;

namespace Ruben.Books.Web.Controllers
{
    public class BooksController : Controller
    {
        private IBooksRepository _repo;
        private IUnitOfWork<BooksContext> _unitOfWork;

        public BooksController(IBooksRepository repo, IUnitOfWork<BooksContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repo = repo;
        }

        //
        // GET: /Books/

        public ActionResult Index()
        {
            var books = _repo.AllIncluding(_ => _.Category, _ => _.Readings);
            return View(books);
        }

        //
        // GET: /Books/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Books/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Books/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
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
            return View();
        }

        //
        // POST: /Books/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
