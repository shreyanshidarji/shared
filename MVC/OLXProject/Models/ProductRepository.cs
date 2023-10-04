using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OLXProject.Models
{
    public class ProductRepository
    {
        private SqlConnection con;
        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            con = new SqlConnection(constr);

        }
        public List<MyAdvertiseModel> GetAdvertiseDetails()
        {
            connection();
            SqlCommand com = new SqlCommand("GetAdvertiseDetails", con);
            com.CommandType = CommandType.StoredProcedure;

            List<MyAdvertiseModel> productList = new List<MyAdvertiseModel>();

            con.Open();
            SqlDataReader reader = com.ExecuteReader();
            while (reader.Read())
            {
                MyAdvertiseModel product = new MyAdvertiseModel();
                product.advertiseId = Convert.ToInt32(reader["advertiseId"]);
                product.productSubCategoryName = Convert.ToString(reader["productSubCategoryName"]);
              //product.productSubCategoryId = Convert.ToInt32(reader["productSubCategoryId"]);
                product.advertiseTitle = Convert.ToString(reader["advertiseTitle"]);
                product.advertiseDescription = Convert.ToString(reader["advertiseDescription"]);
               // product.imageData = (byte[])reader["imageData"];
                product.imageData = reader["imageData"] != DBNull.Value ? (byte[])reader["imageData"] : null;
                product.areaName= Convert.ToString(reader["areaName"]);
                //product.areaId=Convert.ToInt32(reader["areaId"]);
                product.advertisePrice = reader.GetDecimal(reader.GetOrdinal("advertisePrice"));

                //product.advertiseStatus= Convert.ToBoolean(reader["advertiseStatus"]);
                product.userName= Convert.ToString(reader["userName"]);
               // product.userId=Convert.ToInt32(reader["userId"]);
                // product.advertiseapproved= Convert.ToBoolean(reader["advertiseapproved"]);
                product.createdOn=Convert.ToDateTime(reader["createdOn"]);
                product.updatedOn=Convert.ToDateTime(reader["updatedOn"]);
                productList.Add(product);
            }
            con.Close();
            return productList;
        }

        
        public List<AdvertiseImagesModel> GetImages()
        {
            connection();
            SqlCommand com = new SqlCommand("ps_GetImages", con); 
            com.CommandType = CommandType.StoredProcedure;

            List<AdvertiseImagesModel> images = new List<AdvertiseImagesModel>();

            con.Open();
            SqlDataReader reader = com.ExecuteReader();
            while (reader.Read())
            {
                images.Add(new AdvertiseImagesModel
                {
                   
                    advertiseId = Convert.ToInt32(reader["advertiseId"]),
                    imageData = (byte[])reader["imageData"],
                    createdOn = Convert.ToDateTime(reader["createdOn"]),
                    updatedOn = Convert.ToDateTime(reader["updatedOn"]),

            });
            }
            con.Close();

            return images;
        }
        //public list<advertiseimagesmodel> getimages()
        //{


        //    connection();
        //    sqlcommand com = new sqlcommand("ps_getimages", con);
        //    com.commandtype = commandtype.storedprocedure;

        //    list<advertiseimagesmodel> productimageslist = new list<advertiseimagesmodel>();

        //    con.open();
        //    sqldatareader reader = com.executereader();
        //    while (reader.read())
        //    {
        //        advertiseimagesmodel productimages = new advertiseimagesmodel();

        //        productimages.imagedata = (byte[])reader["imagedata"];
        //        productimageslist.add(productimages);
        //    }
        //    con.close();

        //    return productimageslist;
        //}


        public void AddProductDetails(MyAdvertiseModel productDetailsModel)
        {
            connection();
            SqlCommand com = new SqlCommand("AddNewAdvertise", con);
            com.CommandType = CommandType.StoredProcedure;

            com.Parameters.AddWithValue("@productSubCategoryId", productDetailsModel.productSubCategoryId);
            com.Parameters.AddWithValue("@advertiseTitle", productDetailsModel.advertiseTitle);
            com.Parameters.AddWithValue("@advertiseDescription", productDetailsModel.advertiseDescription);
            com.Parameters.AddWithValue("@advertisePrice", productDetailsModel.advertisePrice);

            com.Parameters.AddWithValue("@areaId", productDetailsModel.areaId);
            com.Parameters.AddWithValue("@userId", productDetailsModel.userId);

            con.Open();
            int i = com.ExecuteNonQuery();
            if (i == 1)
            {
                Console.WriteLine("success");
            }

            con.Close();

        }
        //PUBLIC VOID ADDIMAGES(INT ADVERTISEID, BYTE[] IMAGEDATA)
        //{

        //    CONNECTION();
        //    SQLCOMMAND COM = NEW SQLCOMMAND("PS_ADDIMAGEDATA", CON);
        //    COM.COMMANDTYPE = COMMANDTYPE.STOREDPROCEDURE;

        //        COM.PARAMETERS.ADD("@ADVERTISEID", SQLDBTYPE.INT).VALUE = ADVERTISEID;
        //        COM.PARAMETERS.ADD("@IMAGEDATA", SQLDBTYPE.VARBINARY, -1).VALUE = IMAGEDATA;

        //    CON.OPEN();
        //    INT I = COM.EXECUTENONQUERY();
        //    IF (I == 1)   

        //    {
        //        CONSOLE.WRITELINE("SUCCESS");
        //    }

        //    CON.CLOSE();

        //}
        public void AddImage(int advertiseId, byte[] imageData)
        {
            connection();
            SqlCommand com = new SqlCommand("ps_InsertAdvertiseImage", con);
            com.CommandType = CommandType.StoredProcedure;

            com.Parameters.Add("@advertiseId", SqlDbType.Int).Value = advertiseId;
            com.Parameters.Add("@imageData", SqlDbType.VarBinary, -1).Value = imageData;

            con.Open();
            int rowsAffected = com.ExecuteNonQuery();
            con.Close();

            if (rowsAffected == 1)
            {
                Console.WriteLine("successfully added image");
            }
            else
            {
                Console.WriteLine("image not uploaded");
            }
        }


    }
}

