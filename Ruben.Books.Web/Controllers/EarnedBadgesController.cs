using Ruben.Books.DataLayer;
using Ruben.Books.Repository;
using Ruben.Books.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ruben.Books.Web.Controllers
{
    public class EarnedBadgesController : Controller
    {
        private readonly IBookBadgeRepository _repo;
        private readonly IUnitOfWork<BooksContext> _unitOfWork;

        public EarnedBadgesController(IUnitOfWork<BooksContext> unitOfWork, IBookBadgeRepository repo)
        {
            _repo = repo;
            _unitOfWork = unitOfWork;
        }
        //
        // GET: /EarnedBadges/

        public ActionResult Index()
        {
            var badges = _repo.AllIncluding(_ => _.Reading.Book)
                .Where(_ => !_.IsSpent)
                .ToList();

            return View(badges.Select(_=>new BookBadgeVM
                {
                    EarnedDate = _.Reading.Date,
                    Book = _.Reading.Book.Title,
                    Id = _.Id,
                    BookId = _.Reading.Book.Id,
                }));
        }

        public ActionResult Spend(int id)
        {
            _repo.SpendBadge(id);            
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }

    }
}
