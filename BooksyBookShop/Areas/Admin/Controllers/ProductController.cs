using BooksyBookShopWeb.DataAccess.Repository.IRepository;
using BooksyBookShopWeb.Models;
using BooksyBookShopWeb.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BooksyBookShop.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = hostEnvironment; 
        }

        public IActionResult Index()
        {
            return View();
        }

        //GET - UPSERT
        //Used to find the right data in the database
        public IActionResult Upsert(int? id)
        {
            ProductViewModel productVM = new()
            {
                Product = new(),
                CategoryList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString(),
                }),
                CoverTypeList = _unitOfWork.CoverType.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString(),
                }),
            };

            if (id == null|| id == 0) 
            {
                //create product
                return View(productVM);
            } else
            {
                //update the product
                productVM.Product = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);
                return View(productVM);
            }         
        }

        //POST - UPSERT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductViewModel obj, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    //rename the file so no duplication of names
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"images\products");
                    var extension = Path.GetExtension(file.FileName);

                    //deletes the image if it already exists - this will happen when updating
                    if (obj.Product.ImageUrl != null)
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, obj.Product.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), 
                        FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    obj.Product.ImageUrl = @"\images\products\" + fileName + extension;
                }

                if (obj.Product.Id == 0)
                {
                    _unitOfWork.Product.Add(obj.Product);
                } else
                {
                    _unitOfWork.Product.Update(obj.Product);
                }
                _unitOfWork.Save();
                TempData["success"] = "Product Created!";

                return RedirectToAction("Index");
            }

            return View(obj);

        }


        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var productList = _unitOfWork.Product.GetAll(includeProperties: "Category,CoverType");
            return Json(new { data = productList });
        }

        //DELETE
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var obj = _unitOfWork.Product.GetFirstOrDefault(x => x.Id == id);
            if (obj == null)
            {
                return Json(new { success = false, message = "Error while deleting product." });
            }

            //deletes the existing image 
                var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, obj.ImageUrl.TrimStart('\\'));
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }

            _unitOfWork.Product.Remove(obj);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful!" });

        }
        #endregion
    }
}
