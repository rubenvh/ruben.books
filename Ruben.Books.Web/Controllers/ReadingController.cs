﻿using Ruben.Books.DataLayer;
using Ruben.Books.Domain;
using Ruben.Books.Repository;
using Ruben.Books.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Ruben.Books.Web.Controllers
{
    public class ReadingController : Controller
    {
        private IReadingsRepository _repo;
        private IBooksRepository _bookRepo;
        private IBookBadgeRepository _badgeRepo;
        private IUnitOfWork<BooksContext> _unitOfWork;

        public ReadingController(
            IUnitOfWork<BooksContext> unitOfWork,
            IReadingsRepository repo,  
            IBooksRepository bookRepo,
            IBookBadgeRepository badgeRepo)
        {
            _unitOfWork = unitOfWork;
            _repo = repo;
            _bookRepo = bookRepo;
            _badgeRepo = badgeRepo;
        }

        //
        // GET: /Books/

        public ActionResult Index()
        {
            // TODO: pass paging?
            var readings = _repo.GetTimeline();

            var viewModel = readings.Select(_ => new BookVM(_)).ToList();
            
            return View(viewModel);
        }

        public ActionResult MarkRead(int bookId)
        {
            var book = _bookRepo.Find(bookId);
            return View(new PrefillMarkReadVM() {
                BookId = bookId,
                Pages = book.Pages
            });
        }

        [HttpPost]
        public ActionResult MarkRead(Reading reading)
        {
            if(ModelState.IsValid)
            {
                var book = _bookRepo.Find(reading.BookId);
                reading.Book = book;
                _badgeRepo.CreateBadgesForBookRead(reading);
                reading.State = State.Added;
                _repo.InsertOrUpdateGraph(reading);
                _unitOfWork.Save();
                return Json(new {Success=true}, JsonRequestBehavior.AllowGet);
            }

            var errors = ModelState
                .Where(_ => _.Value.Errors.Any())
                .Select(_=>new {Field=_.Key, Error=_.Value.Errors.First().ErrorMessage})
                .ToList();
            
            //this.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return Json(new { Success=false,Errors = errors }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int id)
        {
            var reading = _repo.Find(id);
            _repo.Delete(id);
            _unitOfWork.Save();
            return RedirectToAction("Details", "Books", new { id=reading.BookId });
        }
    }
}