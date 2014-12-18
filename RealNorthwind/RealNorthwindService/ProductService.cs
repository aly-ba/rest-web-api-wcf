using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using MyWCFServices.RealNorthwindBDO;
using MyWCFServices.RealNorthwindLogic;

namespace MyWCFServices.RealNorthwindService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class ProductService : IProductService
    {
        ProductLogic productLogic = new ProductLogic();
        public Product GetProduct(int id)
        {
            /*
            // TODO: call business logic layer to retrieve product
            Product product = new Product();
            product.ProductID = id;
            product.ProductName = 
                "fake product name from service layer";
            product.UnitPrice = 10.0m;
            product.QuantityPerUnit = "fake QPU";
            return product;
            */

            ProductBDO productBDO = null;
            try
            {
                productBDO = productLogic.GetProduct(id);
            }
            catch (Exception e)
            {
                string msg = e.Message;
                string reason = "GetProduct Exception";
                throw new FaultException<ProductFault>
                    (new ProductFault(msg), reason);
            }

            if (productBDO == null)
            {
                string msg =
                    string.Format("No product found for id {0}",
                    id);
                string reason = "GetProduct Empty Product";
                if (id == 999)
                {
                    throw new Exception(msg);
                }
                else
                {
                    throw new FaultException<ProductFault>
                        (new ProductFault(msg), reason);
                }
            }
            Product product = new Product();
            TranslateProductBDOToProductDTO(productBDO, product);
            return product;
        }

        public bool UpdateProduct(Product product,
            ref string message)
        {
            bool result = true;

            // first check to see if it is a valid price
            if (product.UnitPrice <= 0)
            {
                message = "Price cannot be <= 0";
                result = false;
            }
            // ProductName can't be empty
            else if (string.IsNullOrEmpty(product.ProductName))
            {
                message = "Product name cannot be empty";
                result = false;
            }
            // QuantityPerUnit can't be empty
            else if (string.IsNullOrEmpty(product.QuantityPerUnit))
            {
                message = "Quantity cannot be empty";
                result = false;
            }
            else
            {
                ProductBDO productBDO = new ProductBDO();
                TranslateProductDTOToProductBDO(product, productBDO);

                try
                {
                    result = productLogic.UpdateProduct(
                        productBDO, ref message);
                }
                catch (Exception e)
                {
                    string msg = e.Message;
                    string reason = "UpdateProduct Exception";
                    throw new FaultException<ProductFault>
                        (new ProductFault(msg), reason);
                }
            }
            return result;
        }

        private void TranslateProductBDOToProductDTO(
            ProductBDO productBDO,
            Product product)
        {
            product.ProductID = productBDO.ProductID;
            product.ProductName = productBDO.ProductName;
            product.QuantityPerUnit = productBDO.QuantityPerUnit;
            product.UnitPrice = productBDO.UnitPrice;
            product.Discontinued = productBDO.Discontinued;
        }

        private void TranslateProductDTOToProductBDO(
            Product product,
            ProductBDO productBDO)
        {
            productBDO.ProductID = product.ProductID;
            productBDO.ProductName = product.ProductName;
            productBDO.QuantityPerUnit = product.QuantityPerUnit;
            productBDO.UnitPrice = product.UnitPrice;
            productBDO.Discontinued = product.Discontinued;
        }
    }
}
