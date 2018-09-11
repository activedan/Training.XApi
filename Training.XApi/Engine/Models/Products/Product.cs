using System;
using System.Collections.Generic;
using Training.XApi.Engine.Enums;

namespace Training.XApi.Engine.Models.Products
{
    public class Product
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ProductType Type { get; set; }
        public decimal Price { get; set; }
        public List<ProductProperty> Properties { get; set; }

        public IList<Product> BundledProducts { get; set; }

        public List<Guid> Upsells { get; set; }

        public Product()
        {
            this.Properties = new List<ProductProperty>();
        }
    }

    public class ProductProperty
    {
        public string Key { get; set; }
        public string Value { get; set; }

        public string ToString()
        {
            var val = Value != null ? $"\"{Value}\"" : "null";
            return $"{Key}={val}";
        }
    }
}
