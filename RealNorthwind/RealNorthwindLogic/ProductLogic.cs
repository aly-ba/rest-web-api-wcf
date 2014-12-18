using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MyWCFServices.RealNorthwindBDO;
using MyWCFServices.RealNorthwindDAL;

namespace MyWCFServices.RealNorthwindLogic
{
    public class ProductLogic
    {
        ProductDAO productDAO = new ProductDAO();
        public ProductBDO GetProduct(int id)
        {
            /*
            // TODO: call data access layer to retrieve product
            ProductBDO p = new ProductBDO();
            p.ProductID = id;
            p.ProductName =
                "fake product name from business logic layer";
            p.UnitPrice = 20.00m;
            p.QuantityPerUnit = "fake QPU";
            if (id > 50) p.UnitsOnOrder = 30;
            return p;
             */
            return productDAO.GetProduct(id);
        }

        public bool UpdateProduct(ProductBDO product, 
            ref string message)
        {
            ProductBDO productInDB =
                GetProduct(product.ProductID);
            // invalid product to update
            if (productInDB == null)
            {
                message = "cannot get product for this ID";
                return false;
            }
            // a product can't be discontinued 
            // if there are non-fulfilled orders
            if (product.Discontinued == true
                && productInDB.UnitsOnOrder > 0)
            {
                message = "cannot discontinue this product";
                return false;
            }
            else
            {
                /*
                // TODO: call data access layer to update product
                message = "Product updated successfully";
                return true;
                 */
                return productDAO.UpdateProduct(product, ref message);
            }
        }
    }
}
