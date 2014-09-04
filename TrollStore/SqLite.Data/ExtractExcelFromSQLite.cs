using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SQLite;
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
        private OleDbConnection dbConnToProducts;
        private SQLiteConnection connection = new SQLiteConnection(@"Data Source=.\..\..\..\SqLite.Data\SQLiteDatabase\Products.sqlite;Version=3;");
        public ExtractExcelFromSQLite(SqliteContext context, string filePath)
        {
            this.context = context;
            //this.dbConnToProducts = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=;Persist Security Info=False");
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

        public void GetProducts()
        {
            this.dbConnToProducts.Open();
        }

        public void ExctractToExcel(string filePath)
        {
            var package = CreateExcel(filePath);
            SQLiteCommand findCommand = new SQLiteCommand
                ("SELECT * FROM Products", connection);
            connection.Open();
            var reader = findCommand.ExecuteReader();
            var workSheet = package.Workbook.Worksheets[1];
            int counter = 1;
            while (reader.Read())
            {
                workSheet.Cells["A" + counter].Value = reader["ProductID"];
                workSheet.Cells["B" + counter].Value = reader["SoldPieces"];
                // workSheet.Cells["C" + counter].Value = reader["StartDate"];
                // workSheet.Cells["D" + counter].Value = reader["EndDate"];
                counter++;
            }
            package.Save();
            connection.Close();
            //var package = CreateExcel(filePath);

            //dbConn.Open();
            //using(dbConn)
            //{
            //    var query = "select * from Products";
            //    var dataAdapter = new OleDbDataAdapter(query, dbConn);
            //    var dataSet=new DataSet();
            //    dataAdapter.Fill(dataSet);
            //}
            //package.Save();
        }
    }
}