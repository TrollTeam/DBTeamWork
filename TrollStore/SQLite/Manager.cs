using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Manager manager = new Manager(@"Data Source=..\\..\\ProductsDatabase\SQLiteDatabase.sqlite; Version=3;");
//manager.AddProduct(1, 13, new DateTime(2014, 1, 1), new DateTime(2014, 2, 2),conn);

namespace SQLiteDatabase
{
    public class Manager
    {
        private SQLiteConnection conn;
        private OleDbConnection dbConn;

        public Manager(string connectionString)
        {
            this.conn = new SQLiteConnection(connectionString);
        }

        public void AddProduct(int productId, int externalId, int soldPieces, DateTime startDate, DateTime endDate, SQLiteConnection conn)
        {
            conn.Open();
            SQLiteCommand addProduct = new SQLiteCommand(
                "insert into Products(ProductId,SoldPieces, ExternalProductId,SoldPieces) values(@ProductId, @ExternalProductId, @SoldPieces)", conn);

            addProduct.Parameters.AddWithValue("@ProductId", productId);
            addProduct.Parameters.AddWithValue("@ExternalProductId", externalId);
            addProduct.Parameters.AddWithValue("@SoldPieces", soldPieces);
            addProduct.Parameters.AddWithValue("@StarDate", startDate);
            addProduct.Parameters.AddWithValue("@EndDate", endDate);

            addProduct.ExecuteNonQuery();
            conn.Close();
        }

        public void ExtractToExcel(string sheetName)
        {
            OleDbCommand getData = new OleDbCommand(
                "select ProductId, ExternalProductId, SoldPieces, StarDate, EndDate from Products", dbConn);
            int productId=0;
            int externalProductId=0;
            int soldPieces=0;
            DateTime startDate=new DateTime();
            DateTime endDate=new DateTime();

            conn.Open();
            var reader = getData.ExecuteReader();
            while (reader.Read())
            {
                productId = int.Parse(reader["ProductId"].ToString());
                externalProductId = int.Parse(reader["ExternalProductId"].ToString());
                soldPieces = int.Parse(reader["SoldPieces"].ToString());
                startDate = (DateTime)reader["StarDate"];
                endDate = (DateTime)reader["EndDate"];
            }

            OleDbCommand insertIntoExcel = new OleDbCommand(
                "insert into [" + sheetName + "$] (ProductId, ExternalProductId, SoldPieces, StarDate, EndDate) values(@ProductId, @SoldPieces, @StarDate, @EndDate)", dbConn);

            insertIntoExcel.Parameters.AddWithValue("@ProductId", productId);
            insertIntoExcel.Parameters.AddWithValue("@ExternalProductId", externalProductId);
            insertIntoExcel.Parameters.AddWithValue("@SoldPieces", soldPieces);
            insertIntoExcel.Parameters.AddWithValue("@StarDate",startDate);
            insertIntoExcel.Parameters.AddWithValue("@EndDate", endDate);
        }
    }
}
