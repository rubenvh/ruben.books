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
        private IBookBadgeRepository _badgeRepo;
        private IAuthorRepository _authorRepo;

        public BooksController(IUnitOfWork<BooksContext> unitOfWork, 
            IBooksRepository repo, 
            ICategoryRepository categoryRepository, 
            IBookBadgeRepository badgeRepo,
            IAuthorRepository authorRepo)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
            _repo = repo;
            _badgeRepo = badgeRepo;
            _authorRepo = authorRepo;
        }

        //
        // GET: /Books/

        public ActionResult Index(BookFilterVM filter)
        {           
            ViewBag.Title = "All books";
            var viewModel = new BookAndFilterVM
            {
                Books = GetBooks(filter),
                Filter = filter
            };
            var categories = new List<Category>() { new Category {Name="_none", Id=-1}};
            categories.AddRange( _categoryRepository.All.ToList());
            viewModel.CategoryIds = new SelectList(categories, "Id", "Name", -1);

            return View(viewModel);
        }

        public ActionResult IndexContent(BookFilterVM filter)
        {               
            return View("BookVMIndex", GetBooks(filter));
        }

        private List<BookVM> GetBooks(BookFilterVM filter)
        {
            var booksQuery = _repo.AllIncluding(_ => _.Category, _ => _.Authors, _ => _.Readings);

            // TODO: convert to filter domain model and pass to repository
            if (filter != default(BookFilterVM))
            {
                if (filter.IsRead.HasValue)
                {
                    booksQuery = filter.IsRead.Value ?
                        booksQuery.Where(_ => _.Readings.Any()) :
                        booksQuery.Where(_ => !_.Readings.Any());
                }
                if (filter.CategoryId.HasValue && filter.CategoryId.Value > 0)
                {
                    booksQuery = booksQuery.Where(_ => _.CategoryId == filter.CategoryId.Value);
                }
            }   

            var books = booksQuery.OrderByDescending(_=>_.Id).ToList()
                .Select(_ => new BookVM(_));

            return (filter != null && filter.IsRead.HasValue && filter.IsRead.Value) ?
                books.OrderByDescending(_ => _.LastRead).ToList() :
                books.OrderByDescending(_ => _.Id).ToList();

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
            return View(new CreateOrUpdateBookVM());
        }

        //
        // POST: /Books/Create

        [HttpPost]
        public ActionResult Create(CreateOrUpdateBookVM book)
        {
            if (this.ModelState.IsValid)
            {
                var bookToAdd = new Book()
                {
                    State = State.Added,
                    CategoryId = book.CategoryId,
                    FirstPublished = new DateTime(book.YearFirstPublished, 1, 1),
                    Published = new DateTime(book.YearPublished, 1, 1),
                    Isbn = book.Isbn,
                    Pages = book.Pages,
                    Tags = book.Tags,
                    Title = book.Title,
                    Authors = book.AuthorIds.Select(_ => new Author() { Id = _ }).ToList(),
                    Owned = book.Owned
                };

                _repo.InsertOrUpdateGraph(bookToAdd);
                _unitOfWork.Save();
               
                return RedirectToAction("Details", new { id = bookToAdd.Id });
            }

            ViewData["Categories"] = new SelectList(_categoryRepository.All.ToList(), "Id", "Name", 1);
            return View(book);
        }

        //
        // GET: /Books/Edit/5

        public ActionResult Edit(int id)
        {
            var book = _repo.AllIncluding(_ => _.Category, _ => _.Authors, _ => _.Readings)
                .Single(_ => _.Id == id);
            ViewData["Categories"] = new SelectList(_categoryRepository.All.ToList(), "Id", "Name", 1);
            return View(new CreateOrUpdateBookVM(book));
        }

        //
        // POST: /Books/Edit/5

        [HttpPost]
        public ActionResult Edit(CreateOrUpdateBookVM book)
        {
            if (this.ModelState.IsValid)
            {
                var bookToUpdate = _repo.AllIncluding(_ => _.Category, _ => _.Authors, _ => _.Readings)
                    .Single(_ => _.Id == book.Id);
                                
                bookToUpdate.State = State.Modified;
                bookToUpdate.CategoryId = book.CategoryId;
                bookToUpdate.FirstPublished = new DateTime(book.YearFirstPublished, 1, 1);
                bookToUpdate.Published = new DateTime(book.YearPublished, 1, 1);
                bookToUpdate.Isbn = book.Isbn;
                bookToUpdate.Pages = book.Pages;
                bookToUpdate.Tags = book.Tags;
                bookToUpdate.Title = book.Title;                
                bookToUpdate.Owned = book.Owned;

                bookToUpdate.Authors = _authorRepo.All.Where(_ => book.AuthorIds.Contains(_.Id)).ToList();
                
                _repo.InsertOrUpdate(bookToUpdate);
                _unitOfWork.Save();

                return RedirectToAction("Details", new { id = bookToUpdate.Id });
            }

            ViewData["Categories"] = new SelectList(_categoryRepository.All.ToList(), "Id", "Name", 1);
            return View(book);
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
