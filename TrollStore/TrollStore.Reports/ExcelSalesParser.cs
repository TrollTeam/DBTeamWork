namespace TrollStore.Reports
{
    using System;
    using System.Text;
    using System.IO.Compression;
    using System.Data.OleDb;
    using System.Data.SqlClient;
    using TrollStore.Data;
    using TrollStore.Model;

    public class ExcelSalesParser
    {
        private string connectionString;
        private OleDbConnection dbConn;
        private int saleId;
        private int customerId;
        private decimal saleValue;
        private DateTime date;
        private TrollStoreData data;

        public ExcelSalesParser(string filePath, TrollStoreData data)
        {
            this.connectionString = GenerateConnectionString(filePath);
            this.dbConn = new OleDbConnection(this.connectionString);
            this.data = data;
        }

        public string GenerateConnectionString(string filePath)
        {
            return "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=.\\" + filePath + "; Extended Properties=\"Excel 12.0 Xml;HDR=YES\";";
        }

        public void InsertDataIntoSql()
        {
            var newSale = new Sale
            {
                SaleId = this.saleId,
                CustomerId = this.customerId,
                SaleValue = this.saleValue,
                Date = this.date
            };

            this.data.Sales.Add(newSale);
            data.SaveChanges();
        }

        public void GetDataFromExcel(string sheetName)
        {
            this.dbConn.Open();
            using (dbConn)
            {

                OleDbCommand cmd = new OleDbCommand(
                "select SaleId,SaleValue,Date,CustomerId from [" + sheetName + "$]", dbConn);

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    this.saleId = int.Parse(((double)reader[0]).ToString());
                    this.saleValue = decimal.Parse(reader[1].ToString());
                    this.date = ParseDate((string)reader[2]);
                    this.customerId = int.Parse(((double)reader[3]).ToString());
                    InsertDataIntoSql();
                }
            }
        }

        private DateTime ParseDate(string dataAsString)
        {
            string[] dateParts = dataAsString.Split('.');
            int day = int.Parse(dateParts[0]);
            int month = int.Parse(dateParts[1]);
            int year = int.Parse(dateParts[2]);

            return new DateTime(year, month, day);
        }

        public void ExtractToExcel(string sheetName)
        {
            OleDbCommand insertIntoExcel = new OleDbCommand(
                "insert into [" + sheetName + "$] (SaleId, CustomerId, SaleValue, Date) values(@saleId, @customerId, @saleValue, @date)", dbConn);

            insertIntoExcel.Parameters.AddWithValue("@saleId", this.saleId);
            insertIntoExcel.Parameters.AddWithValue("@customerId", this.customerId);
            insertIntoExcel.Parameters.AddWithValue("@saleValue", this.saleValue);
            insertIntoExcel.Parameters.AddWithValue("@date", this.date);
        }
    }
}
