using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;

namespace SqLite.Data
{
   public class ExtractExcelFromSQLite
    {
       private SqliteContext context;

       public ExtractExcelFromSQLite(SqliteContext context)
       {
           this.context=context;
       }

       private ExcelPackage CreateExcel(string filePath)
       {
           FileInfo file = new FileInfo(filePath);
           ExcelPackage package = new ExcelPackage(file);
           if (package.Workbook.Worksheets.Count != 0)
           {
               return package;
           }

           var workSheet = package.Workbook.Worksheets.Add("Products");

           workSheet.Cells["A1"].Value = "ProductId";
           workSheet.Cells["B1"].Value = "SoldPieces";
           workSheet.Cells["C1"].Value = "StartData";
           workSheet.Cells["D1"].Value = "EndDate";

           package.Save();

           return package;
       }

       public void ExctractToExcel(string filePath)
       {
           var package = CreateExcel(filePath);
           var workSheet = package.Workbook.Worksheets[1];

           var sqliteProducts =
               from products in context.Products
               select new
               {
                   ProductId = products.ProductID,
                   SoldPieces = products.SoldPieces,
                   StartDate = products.StartDate,
                   EndDate = products.EndDate
               };

           int count = 2;

           foreach (var product in sqliteProducts)
           {
               workSheet.Cells["A" + count].Value = product.ProductId;
               workSheet.Cells["B" + count].Value = product.SoldPieces;
               workSheet.Cells["C" + count].Value = product.StartDate;
               workSheet.Cells["D" + count].Value = product.EndDate;
               count++;

           }

           package.Save();

       }
    }
}
