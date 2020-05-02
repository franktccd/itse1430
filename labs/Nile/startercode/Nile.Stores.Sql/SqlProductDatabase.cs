/*
 * ITSE 1430
 * Frank Rygiewicz
 * 4/21/20
 */
 using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nile.Stores.Sql
{
    public class SqlProductDatabase : ProductDatabase
    {
        public SqlProductDatabase(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override Product AddCore ( Product product )
        {
            using (var conn = OpenConnection())
            {
                var cmd = new SqlCommand("AddProduct", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@name", product.Name);
                cmd.Parameters.AddWithValue("@price", product.Price);
                cmd.Parameters.AddWithValue("@description", product.Description);
                cmd.Parameters.AddWithValue("@isDiscontinued", product.IsDiscontinued);

                var result = cmd.ExecuteScalar();

                var id = Convert.ToInt32(result);
                product.Id = id;


                return product;
            }
        }

        protected override void RemoveCore ( int id )
        {

            using (var conn = OpenConnection())
            {
                var cmd = conn.CreateCommand();
                cmd.CommandText = "RemoveProduct";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);


                cmd.ExecuteNonQuery();
            };

        }

        protected override IEnumerable<Product> GetAllCore ()
        {
            var items = new List<Product>();
            var ds = new DataSet();

            //Create a connection and open
            using (var conn = OpenConnection())
            {
                var cmd = conn.CreateCommand();
                cmd.CommandText = "GetAllProducts";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                var da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
            }

            var table = ds.Tables.OfType<DataTable>().FirstOrDefault();
            if (table != null)
                foreach (var row in table.Rows.OfType<DataRow>())
                {
                    var product = new Product() {
                        Id = row.Field<int>("id"),
                        Name = row["name"]?.ToString(),
                        Price = row.Field<decimal>("price"),
                        Description = row["description"]?.ToString(),
                        IsDiscontinued = row.Field<bool>("isDiscontinued")
                    };

                    items.Add(product);
                };

            List<Product> sorted = items.OrderBy(p => p.Name).ToList();
            return sorted;
        }

        protected override Product GetCore ( int id )
        {
            var items = GetAllCore();

            return items.FirstOrDefault(p => p.Id == id);

        }

        protected override Product UpdateCore ( Product existing, Product newItem )
        {
            using (var conn = OpenConnection())
            {
                var cmd = new SqlCommand("UpdateProduct", conn);

                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id", newItem.Id);
                cmd.Parameters.AddWithValue("@name", newItem.Name);
                cmd.Parameters.AddWithValue("@price", newItem.Price);
                cmd.Parameters.AddWithValue("@description", newItem.Description);
                cmd.Parameters.AddWithValue("@isDiscontinued", newItem.IsDiscontinued);

                cmd.ExecuteNonQuery();

                return newItem;
            }


        }

        private SqlConnection OpenConnection ()
        {
            var conn = new SqlConnection(_connectionString);
            conn.Open();

            return conn;
        }

        private Product FindByName ( string name )
        {
            var items = GetAllCore();

            return items.FirstOrDefault(p => String.Compare(p.Name, name, true) == 0);
        }

        private readonly string _connectionString;
    }
}