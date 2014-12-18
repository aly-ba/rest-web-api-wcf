using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Transactions;

namespace TestLINQToEntitiesApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // CRUD operations on tables
            TestTables();

            // ToString
            ViewGeneratedSQL();

            // deferred execution
            TestDeferredExecution();

            // association (deferred/lazy loading)
            TestAssociation();

            // eager loading
            TestEagerLoading();

            // join two tables
            TestJoin();

            // query using a view
            TestView();

            // call a stored procedure
            TestStoredProcedure();

            // Table Per Hierarchy inheritance
            TestTPHInheritance();

            // Table Per Type inheritance
            TestTPTInheritance();

            // simultaneous changes
            TestSimultaneousChanges();

            // version control
            TestVersionControl();

            // transactions
            TestImplicitTransaction();

            // Wrap transactions in a transactin scope
            TestExplicitTransaction();

            Console.WriteLine("Press any key to continue ...");
            Console.ReadKey();
        }

        static void TestTables()
        {
            using (NorthwindEntities NWEntities = new NorthwindEntities())
            {
                // retrieve all Beverages products
                IEnumerable<Product> beverages =
                    from p in NWEntities.Products
                    where p.Category.CategoryName == "Beverages"
                    orderby p.ProductName
                    select p;
                Console.WriteLine("There are {0} Beverages",
                    beverages.Count());

                // update one product
                Product bev1 = beverages.ElementAtOrDefault(10);
                if (bev1 != null)
                {
                    decimal newPrice = (decimal)bev1.UnitPrice + 10.00m;
                    Console.WriteLine("The price of {0} is {1}. Update to {2}",
                        bev1.ProductName, bev1.UnitPrice, newPrice);
                    bev1.UnitPrice = newPrice;
                }

                // submit the change to database
                NWEntities.SaveChanges();

                // insert a product
                Product newProduct = new Product
                {
                    ProductName =
                        "new test product"
                };
                NWEntities.Products.Add(newProduct);
                NWEntities.SaveChanges();

                Console.WriteLine("Added a new product");

                // delete a product
                IQueryable<Product> productsToDelete =
                         from p in NWEntities.Products
                         where p.ProductName == "new test product"
                         select p;
                if (productsToDelete.Count() > 0)
                {
                    foreach (var p in productsToDelete)
                    {
                        NWEntities.Products.Remove(p);
                        Console.WriteLine("Deleted product {0}",
                               p.ProductID);
                    }
                    NWEntities.SaveChanges();
                }
            }
        }

        static void ViewGeneratedSQL()
        {
            using (NorthwindEntities NWEntities =
                new NorthwindEntities())
            {
                IQueryable<Product> beverages =
                    from p in NWEntities.Products
                    where p.Category.CategoryName == "Beverages"
                    orderby p.ProductName
                    select p;

                // view SQL using ToString method
                Console.WriteLine("The SQL statement is:\n" +
                    beverages.ToString());
            }
        }

        static void TestDeferredExecution()
        {
            using (NorthwindEntities NWEntities =
                new NorthwindEntities())
            {

                // SQL is not executed
                IQueryable<Product> beverages =
                    from p in NWEntities.Products
                    where p.Category.CategoryName == "Beverages"
                    orderby p.ProductName
                    select p;

                // SQL is executed on this statement
                Console.WriteLine("There are {0} Beverages",
                      beverages.Count());

                // SQL is executed on this statement
                decimal? averagePrice = (from p in NWEntities.Products
                                         select p.UnitPrice).Average();
                Console.WriteLine("The average price is {0}", averagePrice);

                // SQL is not executed even there is a singleton method
                var cheapestProductsByCategory =
                    from p in NWEntities.Products
                    group p by p.CategoryID into g
                    select new
                    {
                        CategoryID = g.Key,
                        CheapestProduct =
                            (from p2 in g
                             where p2.UnitPrice == g.Min(p3 => p3.UnitPrice)
                             select p2).FirstOrDefault()
                    };
                // SQL is executed on this statement
                Console.WriteLine("Cheapest products by category:");
                foreach (var p in cheapestProductsByCategory)
                {
                    if (p.CategoryID == null || p.CheapestProduct == null) continue;
                    Console.WriteLine(
                             "categery {0}: product name: {1} price: {2}",
                              p.CategoryID, p.CheapestProduct.ProductName,
                              p.CheapestProduct.UnitPrice);
                }
            }
        }

        static void TestAssociation()
        {
            using (NorthwindEntities NWEntities = new NorthwindEntities())
            {
                var categories = from c in NWEntities.Categories select c;
                foreach (var category in categories)
                {
                    Console.WriteLine("There are {0} products in category {1}",
                        category.Products.Count(), category.CategoryName);
                }
            }
        }

        static void TestEagerLoading()
        {
            using (NorthwindEntities NWEntities = new NorthwindEntities())
            {
                // eager loading products of categories
                var categories = from c
                                 in NWEntities.Categories.Include(c => c.Products)
                                 select c;
                foreach (var category in categories)
                {
                    Console.WriteLine("There are {0} products in category {1}",
                        category.Products.Count(), category.CategoryName);
                }
            }
        }

        static void TestJoin()
        {
            using (NorthwindEntities NWEntities = new NorthwindEntities())
            {
                var categoryProducts =
                    from c in NWEntities.Categories
                    join p in NWEntities.Products
                    on c.CategoryID equals p.CategoryID
                    into productsByCategory
                    select new
                    {
                        c.CategoryName,
                        productCount = productsByCategory.Count()
                    };

                foreach (var cp in categoryProducts)
                {
                    Console.WriteLine("There are {0} products in category {1}",
                          cp.productCount, cp.CategoryName);
                }
            }
        }

        static void TestView()
        {
            using (NorthwindEntities NWEntities = new NorthwindEntities())
            {
                var currentProducts = from p
                                   in NWEntities.Current_Product_Lists
                                      select p;
                foreach (var p in currentProducts)
                {
                    Console.WriteLine("Product ID: {0} Product Name: {1}",
                         p.ProductID, p.ProductName);
                }
            }
        }

        static void TestStoredProcedure()
        {
            using (NorthwindEntities NWEntities = new NorthwindEntities())
            {
                IEnumerable<Ten_Most_Expensive_Products_Result> tenProducts =
                            from p
                            in NWEntities.Ten_Most_Expensive_Products()
                            select p;
                Console.WriteLine("Ten Most Expensive Products:");
                foreach (Ten_Most_Expensive_Products_Result p in tenProducts)
                {
                    Console.WriteLine("Product Name: {0}, Price; {1}",
                               p.TenMostExpensiveProducts, p.UnitPrice);
                }

                // map a stored procedure to an entity class
                Product getProduct = NWEntities.GetProduct(1).FirstOrDefault();
                Console.WriteLine("\nProduct name for product 1:{0}",
                                                       getProduct.ProductName);
            }
        }

        static void TestTPHInheritance()
        {
            using (NorthwindEntities NWEntities = new NorthwindEntities())
            {
                var USACustomers1 = from c
                                 in NWEntities.BaseCustomers
                                    where c is USACustomer
                                    select c;

                var USACustomers2 = from c
                            in NWEntities.BaseCustomers.OfType<USACustomer>()
                                    select c;

                Console.WriteLine("Total number of USA customers: {0}",
                                USACustomers1.Count());
                Console.WriteLine("Total number of USA customers: {0}",
                                USACustomers2.Count());

                var USACustomers3 = from c
                                 in NWEntities.BaseCustomers
                                    select c as USACustomer;

                foreach (var c in USACustomers3)
                {
                    if (c != null)
                    {
                        Console.WriteLine("USA customer: {0}, Phone: {1}",
                            c.CompanyName, c.Phone);
                    }
                }

                var UKCustomers = from c
                            in NWEntities.BaseCustomers.OfType<UKCustomer>()
                                  select c;

                foreach (var c in UKCustomers)
                    Console.WriteLine("UK customer: {0}, Fax: {1}",
                        c.CompanyName, c.Fax);
            }
        }

        static void TestTPTInheritance()
        {
            using (NorthwindEntities NWEntities = new NorthwindEntities())
            {
                var usaCustomer1 = (from c
                    in NWEntities.BaseCustomers.OfType<USACustomer1>()
                                    select c).FirstOrDefault();
                if (usaCustomer1 != null)
                {
                    var phone1 = usaCustomer1.Phone;
                    Console.WriteLine("Phone for USA customer1:{0}",
                         phone1);
                }

                var ukCustomer1 = (from c
                    in NWEntities.BaseCustomers.OfType<UKCustomer1>()
                                   select c).FirstOrDefault();
                if (ukCustomer1 != null)
                {
                    var fax1 = ukCustomer1.Fax;
                    Console.WriteLine("Fax for UK customer1:{0}",
                         fax1);
                }

                //var usaCustomerx = (from c
                //    in NWEntities.BaseCustomers.OfType<USACustomer>()
                //                    where c.CustomerID == usaCustomer1.CustomerID
                //                    select c
                //    ).SingleOrDefault();

                var usaCustomer = (from c
                    in NWEntities.BaseCustomers.OfType<USACustomer>()
                                   where c.CustomerID == usaCustomer1.CustomerID
                                   select new { CustomerID = "new PK", c.Phone }
                    ).SingleOrDefault();

                if (usaCustomer != null)
                {
                    var phone = usaCustomer.Phone;
                    Console.WriteLine(
                     "Phone for USA customer from Customers table:{0}",
                          phone);
                }
            }
        }


        static void TestSimultaneousChanges()
        {
            using (NorthwindEntities NWEntities = new NorthwindEntities())
            {

                // first user
                Console.WriteLine("First User ...");
                Product product = (from p in NWEntities.Products
                                   where p.ProductID == 2
                                   select p).First();
                Console.WriteLine("Original price: {0}", product.UnitPrice);
                product.UnitPrice += 1.0m;
                Console.WriteLine("Current price to update: {0}",
                                   product.UnitPrice);
                // process more products

                // second user
                Console.WriteLine("\nSecond User ...");
                using (NorthwindEntities1 NWEntities1 = new NorthwindEntities1())
                {
                    Product1 product1 = (from p in NWEntities1.Product1s
                                         where p.ProductID == 2
                                         select p).First();
                    Console.WriteLine("Original price: {0}", product1.UnitPrice);
                    product1.UnitPrice += 2.0m;
                    Console.WriteLine("Current price to update: {0}",
                                      product1.UnitPrice);
                    NWEntities1.SaveChanges();
                    Console.WriteLine("Price update submitted to database");
                }

                // first user is ready to submit changes
                Console.WriteLine("\nFirst User ...");
                try
                {
                    NWEntities.SaveChanges();
                    Console.WriteLine("Price update submitted to database");
                }
                catch (DbUpdateConcurrencyException e)
                {
                    Console.WriteLine("Conflicts detected. Refreshing ...");
                    var entry = e.Entries.Single();
                    entry.OriginalValues.SetValues(entry.GetDatabaseValues());
                    NWEntities.SaveChanges();
                    Console.WriteLine("Price update submitted to database after refresh");
                }
            }
        }

        static void TestVersionControl()
        {
            using (NorthwindEntities NWEntities = new NorthwindEntities())
            {

                // first user
                Console.WriteLine("First User ...");
                Product product = (from p in NWEntities.Products
                                   where p.ProductID == 3
                                   select p).First();
                Console.WriteLine("Original unit in stock: {0}",
                                product.UnitsInStock);
                product.UnitsInStock += 1;
                Console.WriteLine("Current unit in stock to update: {0}",
                    product.UnitsInStock);
                // process more products

                // second user
                Console.WriteLine("\nSecond User ...");
                using (NorthwindEntities1 NWEntities1 = new NorthwindEntities1())
                {
                    Product1 product1 = (from p in NWEntities1.Product1s
                                         where p.ProductID == 3
                                         select p).First();
                    Console.WriteLine("Original unit in stock: {0}",
                        product1.UnitsInStock);
                    product1.UnitsInStock += 2;
                    Console.WriteLine("Current unit in stock to update: {0}",
                        product1.UnitsInStock);
                    NWEntities1.SaveChanges();
                    Console.WriteLine("update submitted to database");
                }

                // first user is ready to submit changes
                Console.WriteLine("\nFirst User ...");
                try
                {
                    NWEntities.SaveChanges();
                }
                catch (DbUpdateConcurrencyException e)
                {
                    Console.WriteLine("Conflicts detected. Refreshing ...");
                    var entry = e.Entries.Single();
                    entry.OriginalValues.SetValues(entry.GetDatabaseValues());
                    NWEntities.SaveChanges();
                    Console.WriteLine("update submitted to database after refresh");
                }
            }
        }

        static void TestImplicitTransaction()
        {
            using (NorthwindEntities NWEntities = new NorthwindEntities())
            {
                Product prod1 = (from p in NWEntities.Products
                                 where p.ProductID == 4
                                 select p).First();
                Product prod2 = (from p in NWEntities.Products
                                 where p.ProductID == 5
                                 select p).First();
                prod1.UnitPrice += 1;
                // update will be saved to database
                NWEntities.SaveChanges();
                Console.WriteLine("First update saved to database");

                prod2.UnitPrice = -5;
                // update will fail because UnitPrice can't be < 0
                // but previous update stays in database
                try
                {
                    NWEntities.SaveChanges();
                    Console.WriteLine("Second update saved to database");
                }
                catch (Exception)
                {
                    Console.WriteLine("Second update not saved to database");
                }
            }
        }

        static void TestExplicitTransaction()
        {
            using (NorthwindEntities NWEntities = new NorthwindEntities())
            {

                using (TransactionScope ts = new TransactionScope())
                {
                    try
                    {
                        Product prod1 = (from p in NWEntities.Products
                                         where p.ProductID == 4
                                         select p).First();
                        prod1.UnitPrice += 1;
                        NWEntities.SaveChanges();
                        Console.WriteLine("First update saved to database, but not commited.");

                        // now let's try to update another product
                        Product prod2 = (from p in NWEntities.Products
                                         where p.ProductID == 5
                                         select p).First();
                        // update will fail because UnitPrice can't be < 0
                        prod2.UnitPrice = -5;
                        NWEntities.SaveChanges();
                        ts.Complete();
                    }
                    catch (Exception e)
                    {
                        // both updates will fail because they are within one transaction
                        Console.WriteLine("Exception caught. Rollback the first update.");
                    }
                }
            }
        }
    }
}
