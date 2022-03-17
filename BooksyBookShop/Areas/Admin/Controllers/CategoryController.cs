using BooksyBookShopWeb.DataAccess.Repository.IRepository;
using BooksyBookShopWeb.Models;
using BooksyWeb.Data_Context;
using Microsoft.AspNetCore.Mvc;

namespace BooksyBookShop.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            //gets all the categories from DB and converts it to a list
            IEnumerable<Category> objCategoryList = _unitOfWork.Category.GetAll();
            return View(objCategoryList);
        }

        //GET
        public IActionResult Create()
        {
                        
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The Display Order cannot exactly match the Name.");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category created!";

                return RedirectToAction("Index");
            }

            return View(obj);
            
        }

        //GET - UPDATE
        //Used to find the right data in the database
        public IActionResult Update(int? id)
        {
            if (id == null|| id == 0) 
            {
                return NotFound();
            }

            var catergoryFromDB = _unitOfWork.Category.GetFirstOrDefault(x => x.Id == id);

            if (catergoryFromDB == null)
            {
                return NotFound();
            }

            return View(catergoryFromDB);
        }

        //POST - UPDATE
        //Actually performs the update in the DB
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The Display Order cannot exactly match the Name.");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category updated!";

                return RedirectToAction("Index");
            }

            return View(obj);

        }

        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var catergoryFromDB = _unitOfWork.Category.GetFirstOrDefault(x => x.Id == id);

            if (catergoryFromDB == null)
            {
                return NotFound();
            }

            return View(catergoryFromDB);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCategory(int? id)
        {
            var obj = _unitOfWork.Category.GetFirstOrDefault(x => x.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            
            _unitOfWork.Category.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Category deleted!";

            return RedirectToAction("Index");
            
        }
    }
}
