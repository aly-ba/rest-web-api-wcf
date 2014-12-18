using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLINQFeaturesApp
{
    class Program
    {
        static void Main(string[] args)
        {            
            // valid var statements
            var x = "1";
            var n = 0;
            string s = "string";
            var s2 = s;
            s2 = null;
            string s3 = null;
            var s4 = s3;
            /*
            string x = "1";
            int n = 0;
            string s2 = s;
            string s4 = s3;
            */

            // invalid var statements
            /*
            var v;
            var nu = null;
            var v2 = "12"; v2 = 3;
             */

            // old way to create and initialize an object
            /*
            Product p = new product(1, "first candy", 100.0);
            Product p = new Product();
            p.ProductID = 1;
            p.ProductName = "first candy";
            p.UnitPrice=100.0m;
            */

            //object initializer
            var product = new Product
            {
                ProductID = 1,
                ProductName = "first candy",
                UnitPrice = 100.0m
            };
            var arr = new[] { 1, 10, 20, 30 };

            // collection initializer
            var products = new List<Product> {
                new Product { 
                    ProductID = 1, 
                    ProductName = "first candy", 
                    UnitPrice = 10.0m },
                new Product { 
                    ProductID = 2, 
                    ProductName = "second candy", 
                    UnitPrice = 35.0m },
                new Product { 
                    ProductID = 3, 
                    ProductName = "first vegetable", 
                    UnitPrice = 6.0m },
                new Product { 
                    ProductID = 4, 
                    ProductName = "second vegetable", 
                    UnitPrice = 15.0m },
                new Product { 
                    ProductID = 5, 
                    ProductName = "another product", 
                    UnitPrice = 55.0m }
            };

            // anonymous types
            var a = new { Name = "name1", Address = "address1" };
            var b = new { Name = "name2", Address = "address2" };
            b = a;
            /*
            class __Anonymous1
            {
                private string name;
                private string address;
                public string Name {
                    get{
                        return name;
                    }
                    set {
                        name=value
                    }
                }
                public string Address {
                    get{
                        return address;
                    }
                    set{
                        address=value;
                    }
                }
            }
            */

            // extension methods
            if (product.IsCandy())
                Console.WriteLine("yes, it is a candy");
            else
                Console.WriteLine("no, it is not a candy");

            // lambda expression
            var veges1 = products.Get(IsVege);
            Console.WriteLine("\nThere are {0} vegetables:", veges1.Count());
            foreach (Product p in veges1)
            {
                Console.WriteLine("Product ID: {0}  Product name: {1}",
                    p.ProductID, p.ProductName);
            }

            Func<Product, bool> predicate = IsVege;
            var veges2 = products.Get(predicate);
            var veges3 = products.Get(
                delegate(Product p)
                {
                    return p.ProductName.Contains("vegetable");
                }
            );
            var veges4 = products.Get(p => p.ProductName.Contains("vegetable"));
            var veges5 = products.Get((Product p) => p.ProductName.Contains("vegetable"));

            var candies = products.Get(p => p.ProductName.Contains("candy"));

            // Linq extension method
            var veges6 = products.Where(p => p.ProductName.Contains("vegetable"));

            // query syntax
            // query expresion
            var veges7 = from p in products
                         where p.ProductName.Contains("vegetable")
                         select p;

            // LINQ + anonymous data type
            var candyOrVeges = from p in products
                               where p.ProductName.Contains("candy") || p.ProductName.Contains("vegetable")
                               orderby p.UnitPrice descending, p.ProductID
                               select new { p.ProductName, p.UnitPrice };

            var candyOrVeges2 = products.Where(p => p.ProductName.Contains("candy") || p.ProductName.Contains("vegetable")).OrderByDescending(p => p.UnitPrice).ThenBy(p => p.ProductID).Select(p => new { p.ProductName, p.UnitPrice });
            Console.WriteLine("\nCandy or vegetable in descending price order by query syntax:");
            foreach (var p in candyOrVeges)
            {
                Console.WriteLine("{0} {1}", p.ProductName, p.UnitPrice);
            }
            Console.WriteLine("\nCandy or vegetable in descending price order by method syntax:");
            foreach (var p in candyOrVeges2)
            {
                Console.WriteLine("{0} {1}", p.ProductName, p.UnitPrice);
            }

            // uncomment following line and type . to see all LINQ operators
            //System.Linq.Enumerable.

        }

        public static bool IsVege(Product p)
        {
            return p.ProductName.Contains("vegetable");
        }
    }

    public sealed class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
    }

    public static class MyExtensions
    {
        public static bool IsCandy(this Product p)
        {
            if (p.ProductName.IndexOf("candy") >= 0)
                return true;
            else
                return false;
        }

        public static IEnumerable<T> Get<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            foreach (T item in source)
            {
                if (predicate(item))
                    yield return item;
            }
        }
    }

}
