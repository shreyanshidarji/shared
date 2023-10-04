using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OLXProject.Models
{
   

    public class ProductCategoryModel
    {
        public int productCategoryId { get; set; }
        public string productCategoryName { get; set; }
        public DateTime createdOn { get; set; }
        public DateTime updatedOn { get; set; }
    }
      
    public class ProductSubCategoryModel:ProductCategoryModel
    {
        public int productSubCategoryId { get; set; }
        public int productCategoryId { get; set; }
        public string productSubCategoryName { get; set; }
        public DateTime createdOn { get; set; }
        public DateTime updatedOn { get; set; }
    }



      

    public class MyAdvertiseModel : AdvertiseImagesModel
    {
        public int advertiseId { get; set; }
        public string productSubCategoryName { get; set; }
         public int productSubCategoryId { get; set; }
        public string advertiseTitle { get; set; }
        public string advertiseDescription { get; set; }
        public decimal advertisePrice { get; set; }
        
        public int areaId { get; set; }
        public string areaName { get; set; }
        public bool advertiseStatus { get; set; } 
        public string userName { get; set; }
        public int userId { get; set; }
        public bool advertiseapproved { get; set; }
        
        public DateTime createdOn { get; set; }
        public DateTime updatedOn { get; set; }

    }
       
    public class AdvertiseImagesModel
    {
        public int advertiseImageId { get; set; }
        public int advertiseId { get; set; }
        public byte[] imageData { get; set; }
        public byte[] ImageData1 { get; set; }
        public byte[] ImageData2 { get; set; }
        public byte[] ImageData3 { get; set; }
        public DateTime createdOn { get; set; }
        public DateTime updatedOn { get; set; }

    }
       
    public class StateModel: CityModel
    {
        public int stateId { get; set; }
        public string stateName { get; set; }

    }
    public class CityModel: AreaModel
    {
        public int cityId { get; set; }
        public int stateId { get; set; }
        public string cityName { get; set; }

    }
    public class AreaModel:MyAdvertiseModel
    {
        public  int areaId { get; set; }
        public int cityId { get; set; }
        public string areaName { get; set; }

    }

}