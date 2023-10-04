using OLXProject.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;



namespace OLXProject.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult MyAds()
        {
               
            ProductRepository product = new ProductRepository();
            List<MyAdvertiseModel> prc = product.GetAdvertiseDetails();
            return View(prc);
            
        }
        public ActionResult Sell()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Sell(MyAdvertiseModel productDetailsModel)
        {

             ProductRepository product = new ProductRepository();
             product.AddProductDetails(productDetailsModel);
            return RedirectToAction("About");

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Images()
        {
            ProductRepository productRepository = new ProductRepository();
            var images = productRepository.GetImages();
            return View(images);
        }
        public ActionResult UploadImage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadImage(HttpPostedFileBase file, int advertiseId)
        {
            if (file != null && file.ContentLength > 0)
            {
                try
                {
                    byte[] imageData;
                    using (var binaryReader = new BinaryReader(file.InputStream))
                    {
                        imageData = binaryReader.ReadBytes(file.ContentLength);
                    }

                    ProductRepository productRepository = new ProductRepository();
                    productRepository.AddImage(advertiseId, imageData);

                    TempData["SuccessMessage"] = "Image uploaded successfully!";
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = "Error uploading image: " + ex.Message;
                }
            }
            else
            {
                TempData["ErrorMessage"] = "No file selected for upload.";
            }

            return RedirectToAction("Images");
        }




    }
}