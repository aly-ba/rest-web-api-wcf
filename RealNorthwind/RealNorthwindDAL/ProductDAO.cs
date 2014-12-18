using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MyWCFServices.RealNorthwindBDO;
using System.Data.SqlClient;
using System.Configuration;

namespace MyWCFServices.RealNorthwindDAL
{
    public class ProductDAO
    {
        string connectionString = ConfigurationManager.ConnectionStrings["NorthwindConnectionString"].ConnectionString;

        public ProductBDO GetProduct(int id)
        {
            /*
            // TODO: connect to DB to retrieve product
            ProductBDO p = new ProductBDO();
            p.ProductID = id;
            p.ProductName =
                "fake product name from data access layer";
            p.UnitPrice = 30.00m;
            p.QuantityPerUnit = "fake QPU";
            return p;
             */
            ProductBDO p = null;
            using (SqlConnection conn =
                    new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText =
                        "select * from Products where ProductID=@id";
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            p = new ProductBDO();
                            p.ProductID = id;
                            p.ProductName =
                                (string)reader["ProductName"];
                            p.QuantityPerUnit =
                                (string)reader["QuantityPerUnit"];
                            p.UnitPrice =
                                (decimal)reader["UnitPrice"];
                            p.UnitsInStock =
                                (short)reader["UnitsInStock"];
                            p.UnitsOnOrder =
                                (short)reader["UnitsOnOrder"];
                            p.ReorderLevel =
                                (short)reader["ReorderLevel"];
                            p.Discontinued =
                                (bool)reader["Discontinued"];
                        }
                    }
                }
            }
            return p;
        }
        public bool UpdateProduct(ProductBDO product, ref string message)
        {
            /*
            // TODO: connect to DB to update product
            message = "product updated successfully";
            return true;
             */
            message = "product updated successfully";
            bool ret = true;
            using (SqlConnection conn =
                new SqlConnection(connectionString))
            {
                string cmdStr = @"UPDATE products 
                    SET ProductName=@name,
                    QuantityPerUnit=@unit,
                    UnitPrice=@price,
                    Discontinued=@discontinued
                    WHERE ProductID=@id";
                using (SqlCommand cmd = new SqlCommand(cmdStr, conn))
                {
                    cmd.Parameters.AddWithValue("@name",
                        product.ProductName);
                    cmd.Parameters.AddWithValue("@unit",
                        product.QuantityPerUnit);
                    cmd.Parameters.AddWithValue("@price",
                        product.UnitPrice);
                    cmd.Parameters.AddWithValue("@discontinued",
                        product.Discontinued);
                    cmd.Parameters.AddWithValue("@id",
                        product.ProductID);
                    conn.Open();
                    if (cmd.ExecuteNonQuery() != 1)
                    {
                        message = "no product is updated";
                        ret = false;
                    }
                }
            }
            return ret;
        }
    }
}
