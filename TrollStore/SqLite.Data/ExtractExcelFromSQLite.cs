using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
using TrollStore.Model;

namespace SqLite.Data
{
   public class ExtractExcelFromSQLite
    {
       private SqliteContext context;
       private OleDbConnection dbConn;

       public ExtractExcelFromSQLite(SqliteContext context, string filePath)
       {
           this.context=context;

            this.dbConn = new OleDbConnection(GenerateConnectionString(filePath));
       }
         public string GenerateConnectionString(string filePath)
        {
            return "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + "; Extended Properties=\"Excel 12.0 Xml;HDR=YES\";";
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

           package.Save();

           return package;
       }

       public void ExctractToExcel(string filePath)
       {
           var package = CreateExcel(filePath);
           
           dbConn.Open();
           using(dbConn)
           {
               var query = "select * from Products";
               var dataAdapter = new OleDbDataAdapter(query, dbConn);
               var dataSet=new DataSet();
               dataAdapter.Fill(dataSet);
           }
           package.Save();
       }
    }
}
