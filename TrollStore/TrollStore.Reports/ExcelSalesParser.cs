namespace TestReadZIP
{
    using System;
    using System.Text;
    using System.IO.Compression;
    using System.Data.OleDb;
    using System.Data.SqlClient;
    using TrollStore.Data;

    public class ExcelSalesParser
    {
        private string connectionString;
        private OleDbConnection dbConn;
        private double saleId;
        private double customerId;
        private double storeId;
        private double saleValue;
        private DateTime date;
        private TrollStoreData context;

        public ExcelSalesParser(string filePath, TrollStoreContext context, OleDbConnection dbConnection, string filePath)
        {
            this.connectionString = GenerateConnectionString(filePath);
            this.dbConn = dbConnection(connectionString);
            this.context = context;
        }

        public string GenerateConnectionString(string filePath)
        {
            return "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=.\\..\\..\\ExcelReportsForInput\\" + filePath + "; Extended Properties=\"Excel 12.0 Xml;HDR=YES\";";
        }

        public void GetDataFromExcel(string sheetName)
        {

            this.dbConn.Open();
            using (dbConn)
            {
                OleDbCommand cmd = new OleDbCommand(
                "select SaleId,CustomerName,CustomerId,StoreName, StoreId,SaleValue,Date from [" + sheetName + "$]", dbConn);

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    this.saleId = (double)reader[0];
                    this.customerId = (double)reader[2];
                    this.storeId = (double)reader[4];
                    this.saleValue = (double)reader[5];
                    this.date = ParseDate((string)reader[6]);
                }
            }
        }

        public void InsertDataIntoSql()
        {                   
            var newSales = new Sales
            {
                SaleId = this.saleId,
                CustomerId = this.customerId,
                StoreId = this.storeId,
                SaleValue = this.saleValue,
                DateTime = this.date
            };
            this.context.Sales.Add(newSales);
            context.SaveChanges();
        }

        private static DateTime ParseDate(string dataAsString)
        {
            string[] dateParts = dataAsString.Split('-');
            int day = int.Parse(dateParts[0]);
            int month = int.Parse(dateParts[1]);
            int year = int.Parse(dateParts[2]);

            return new DateTime(year, month, day);
        }

        public void ExtractToExcel(string sheetName)
        {         
            OleDbCommand insertIntoExcel = new OleDbCommand(
                "insert into [" + sheetName + "$] (SaleId, CustomerId, StoreId, SaleValu, Date) values(@saleId, @customerId, @storeId, @saleValue, @date)", dbConn);

            insertIntoExcel.Parameters.AddWithValue("@saleId", this.saleId);
            //insertData.Parameters.AddWithValue("@customerId", this.customerId);
            //insertData.Parameters.AddWithValue("@storeId", this.storeId);
            insertIntoExcel.Parameters.AddWithValue("@saleValue", this.saleValue);
            insertIntoExcel.Parameters.AddWithValue("@date", this.date);
        }
    }
}
