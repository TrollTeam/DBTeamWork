namespace TrollStore.Reports
{
    using System;
    using System.Text;
    using System.IO.Compression;
    using System.Data.OleDb;
    using System.Data.SqlClient;
    using System.Linq;

    using TrollStore.Data;
    using TrollStore.Model;

    public class ExcelParser
    {
        private string connectionString;
        private OleDbConnection dbConn;
        private int saleId;
        private int customerId;
        private int storeId;
        private decimal saleValue;
        private DateTime date;
        private TrollStoreData data;

        private string fullName;
        private string address;
        private int countryId;


        public ExcelParser(string filePath, TrollStoreData data)
        {
            this.connectionString = GenerateConnectionString(filePath);
            this.dbConn = new OleDbConnection(this.connectionString);
            this.data = data;
        }

        public string GenerateConnectionString(string filePath)
        {
            return "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=.\\" + filePath + "; Extended Properties=\"Excel 12.0 Xml;HDR=YES\";";
        }

        public void GetSalesDataFromExcel(string sheetName)
        {
            this.dbConn.Open();
            using (dbConn)
            {

                OleDbCommand cmd = new OleDbCommand(
                "select SaleId,SaleValue,Date,CustomerId,StoreId from [" + sheetName + "$]", dbConn);

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    this.saleId = int.Parse(((double)reader[0]).ToString());
                    this.saleValue = decimal.Parse(reader[1].ToString());
                    this.date = ParseDate((string)reader[2]);
                    this.customerId = int.Parse(((double)reader[3]).ToString());
                    this.storeId = int.Parse(((double)reader[4]).ToString());
                    InsertSalesDataIntoSql();
                }
            }
        }

        public void GetCustomersDataFromExcel(string sheetName)
        {
            this.dbConn.Open();
            using (dbConn)
            {

                OleDbCommand cmd = new OleDbCommand(
                "select CustomerId,FullName,Address,CountryId from [" + sheetName + "$]", dbConn);

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    this.customerId = int.Parse(((double)reader[0]).ToString());
                    this.fullName = reader[1].ToString();
                    this.address = reader[2].ToString();
                    this.countryId = int.Parse(((double)reader[3]).ToString());
                    InsertCustomersDataIntoSql();
                }
            }
        }

        private void InsertCustomersDataIntoSql()
        {
            if (this.data.Customers.All().Any())
            {
                return;
            }

            var newCustomer = new Customer()
            {
                CustomerId = this.customerId,
                FullName = this.fullName,
                Address = this.address,
                CountryId = this.countryId
            };

            this.data.Customers.Add(newCustomer);

            this.data.SaveChanges();
        }

        private void InsertSalesDataIntoSql()
        {
            if (this.data.Sales.All().Any())
            {
                return;
            }

            var newSale = new Sale()
            {
                SaleId = this.saleId,
                CustomerId = this.customerId,
                SaleValue = this.saleValue,
                Date = this.date
            };

            this.data.Sales.Add(newSale);

            var storeForSale = this.data.Stores.All().First(s => s.StoreId == this.storeId);
            storeForSale.Sales.Add(newSale);

            Console.WriteLine();
            this.data.SaveChanges();
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
