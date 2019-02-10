using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace ERP.Utility
{
    public class UtilityClass
    {


        public static SelectList GetSelectListByDataTable(DataTable objDataTable, string pValueField, string pTextField)
        {
            List<SelectListItem> objSelectListItems = new List<SelectListItem>();

            objSelectListItems.Add(new SelectListItem() { Value = "", Text = "Please select one" });

            foreach (DataRow dataRow in objDataTable.Rows)
            {
                objSelectListItems.Add(new SelectListItem()
                {
                    Value = dataRow[pValueField].ToString(),
                    Text = dataRow[pTextField].ToString()
                });
            }

            return new SelectList(objSelectListItems, "Value", "Text");
        }

        public static SelectList GetSelectListByDataTable(DataTable objDataTable, string pValueField, string pTextField, string pSelectedValue)
        {
            List<SelectListItem> objSelectListItems = new List<SelectListItem>();

            objSelectListItems.Add(new SelectListItem() { Value = "", Text = "Please select one" });

            foreach (DataRow dataRow in objDataTable.Rows)
            {
                objSelectListItems.Add(new SelectListItem
                {
                    Value = dataRow[pValueField].ToString(),
                    Text = dataRow[pTextField].ToString(),
                    Selected = pSelectedValue == dataRow[pValueField].ToString()
                });
            }

            return new SelectList(objSelectListItems, "Value", "Text");
        }

        /*
        public static List<object> GetObjectListByDataTable(DataTable objDataTable, string pValueField, string pTextField)
        {
            List<object> objectList = new List<object>();

            if (objDataTable.Rows.Count > 0)
            {
                objectList.Add(new { pValueField = "", pTextField = "Please select one" });

                foreach (DataRow dataRow in objDataTable.Rows)
                {
                    objectList.Add(new { pValueField = dataRow[0], pTextField = dataRow[1] });
                }
            }

            return objectList;
        }
        */

        
        public static byte[] ConvertImageToByteArray(HttpPostedFileBase objHttpPostedFileBase)
        {
            if (objHttpPostedFileBase == null)
            {
                return null;
            }
            else
            {
                BinaryReader objBinaryReader = new BinaryReader(objHttpPostedFileBase.InputStream);
                //byte[] imageByteArray = objBinaryReader.ReadBytes(objHttpPostedFileBase.ContentLength);
                //return imageByteArray;
                return objBinaryReader.ReadBytes(objHttpPostedFileBase.ContentLength);
            }
        }

        public static MemoryStream GetSteamFromBase64String(string imageBase64)
        {
            if (imageBase64.IndexOf(',') > 0)
            {
                imageBase64 = imageBase64.Substring(imageBase64.IndexOf(',') + 1);
            }

            //byte[] bytes = Convert.FromBase64String(imageBase64);

            //MemoryStream objMemoryStream = new MemoryStream(bytes);

            //return objMemoryStream;
            return new MemoryStream(Convert.FromBase64String(imageBase64));
        }

        public static MemoryStream GetSteamFromByeArray(byte[] bytes)
        {
            //MemoryStream objMemoryStream = new MemoryStream(bytes);

            //return objMemoryStream;
            return new MemoryStream(bytes);
        }


    }
}