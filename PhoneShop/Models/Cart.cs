using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PhoneShop.Models.DataModel;
namespace PhoneShop.Models
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();
        public double TotalPriceWithDiscount = 0f;
        public void AddItem(Product product,int quantity)
        {
            CartLine line = lineCollection.Where(x => x.Product.Id == product.Id).FirstOrDefault();
            if (line == null)
            {
                lineCollection.Add(new CartLine
                {
                    Product = product,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }
        public void RemoveLine(Product product)
        {
            CartLine isProductInCart = lineCollection.Where(x => x.Product.Id == product.Id).FirstOrDefault();
            if (isProductInCart != null && isProductInCart.Quantity > 1) 
            {
                isProductInCart.Quantity--;
            }
            else
            {
                lineCollection.RemoveAll(x => x.Product.Id == product.Id);
            }
            TotalPriceWithDiscount = 0f;
        }

        public double ComputeTotalValue()
        {
            return lineCollection.Sum(x => x.Product.Price * x.Quantity);

        }
        public void Clear()
        {
            lineCollection.Clear();
        }

        public IEnumerable<CartLine> Lines
        {
            get { return lineCollection; }
        }
    }

    public class CartLine
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
