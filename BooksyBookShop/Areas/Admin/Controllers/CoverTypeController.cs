using BooksyBookShopWeb.DataAccess.Repository.IRepository;
using BooksyBookShopWeb.Models;
using BooksyWeb.Data_Context;
using Microsoft.AspNetCore.Mvc;

namespace BooksyBookShop.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CoverTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            //gets all the Cover Types from DB and converts it to a list
            IEnumerable<CoverType> objCoverTypeList = _unitOfWork.CoverType.GetAll();
            return View(objCoverTypeList);
        }

        //GET
        public IActionResult Create()
        {
                        
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CoverType obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.CoverType.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Cover Type Created!";

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

            var coverTypeFromDB = _unitOfWork.CoverType.GetFirstOrDefault(x => x.Id == id);

            if (coverTypeFromDB == null)
            {
                return NotFound();
            }

            return View(coverTypeFromDB);
        }

        //POST - UPDATE
        //Actually performs the update in the DB
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(CoverType obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.CoverType.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Cover Type Updated!";

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

            var coverTypeFromDB = _unitOfWork.CoverType.GetFirstOrDefault(x => x.Id == id);

            if (coverTypeFromDB == null)
            {
                return NotFound();
            }

            return View(coverTypeFromDB);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCoverType(int? id)
        {
            var obj = _unitOfWork.CoverType.GetFirstOrDefault(x => x.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            
            _unitOfWork.CoverType.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Cover Type Deleted!";

            return RedirectToAction("Index");
            
        }
    }
}
