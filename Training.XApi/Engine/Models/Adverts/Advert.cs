using Training.XApi.Engine.Enums;
using Training.XApi.Engine.Models.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Training.XApi.Engine.Models.Adverts
{
    public class Advert : IValidatableObject
    {
        public Guid AdvertId { get; set; }

        public Guid MemberId { get; set; }

        public string AdvertReference { get; set; }
        public string HeroImage { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }

        public Vertical Vertical { get; set; }
        public AdvertStatus Status { get; set; }
        public WorkflowStatus WorkflowStatus { get; set; }

        public List<AdvertProduct> Products { get; set; }
        public Service Service { get; set; }

        public AdvertData EditingData { get; set; }

        public AdvertData LiveData { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime? DateLastUpdated { get; set; }
        public DateTime? DateFirstApproved { get; set; }
        public DateTime? DateLastApproved { get; set; }
        public DateTime? DateCancelled { get; set; }
        public DateTime? DateToExpire { get; set; }
        public DateTime? DateLastPaused { get; set; }

        public Advert()
        {
            Products = new List<AdvertProduct>();
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext = null)
        {
            var validationResults = new List<ValidationResult>();

            if (AdvertId == null || AdvertId == Guid.Empty)
            {
                validationResults.Add(new ValidationResult("AdvertId cannot be null or empty.", new List<string> { nameof(AdvertId) }));
            }

            if (MemberId == null || MemberId == Guid.Empty)
            {
                validationResults.Add(new ValidationResult("MemberId cannot be null or empty.", new List<string> { nameof(MemberId) }));
            }

            return validationResults;
        }
    }

    public class AdvertData
    {
        public AdvertData()
        {
            Properties = new List<AdvertProperty>();
            Photos = new List<Photo>();
            Features = new List<Feature>();
        }

        public Vertical Vertical { get; set; }
        public decimal? Price { get; set; }
        public string Currency { get; set; }
        public string SpecId { get; set; }
        public string Comments { get; set; }
        public Seller Seller { get; set; }
        public Location Location { get; set; }
        public List<AdvertProperty> Properties { get; set; }
        public List<Feature> Features { get; set; }
        public List<Photo> Photos { get; set; }
    }

    public class AdvertProduct
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid? ParentProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public ProductType Type { get; set; }
        public List<ProductProperty> Properties { get; set; }

        public PaymentStatus PaymentStatus { get; set; }

        public AdvertProduct() { }

        public AdvertProduct(Product product, Guid? parentProductId)
        {
            Id = Guid.NewGuid();
            ProductId = product.ProductId;
            ParentProductId = parentProductId;
            Name = product.Name;
            Description = product.Description;
            Price = product.Price;
            Type = product.Type;
            Properties = product.Properties;
            PaymentStatus = PaymentStatus.AwaitingPayment;
        }
    }

    public class AdvertProperty
    {
        public string Key { get; set; }
        public string Value { get; set; }

        public string ToString()
        {
            var val = Value != null ? $"\"{Value}\"" : "null";
            return $"{Key}={val}";
        }

        public bool AreSame(AdvertProperty advertProperty)
        {
            return string.Compare(Key, advertProperty.Key, StringComparison.InvariantCultureIgnoreCase) == 0 &&
                   string.Compare(Value, advertProperty.Value, StringComparison.InvariantCultureIgnoreCase) == 0;
        }

    }

    public class Feature : IEquatable<Feature>
    {
        public string FeatureId { get; set; }
        public string Availability { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }

        public Feature() { }

        public Feature(string id, string availability, string category, string description)
        {
            FeatureId = id;
            Availability = availability;
            Category = category;
            Description = description;
        }

        public bool Equals(Feature other)
        {
            return FeatureId == other.FeatureId;
        }

        public override int GetHashCode()
        {
            return FeatureId.GetHashCode();
        }

        public bool AreSame(Feature feature)
        {
            return string.Compare(FeatureId, feature.FeatureId, StringComparison.InvariantCultureIgnoreCase) == 0 &&
                   string.Compare(Availability, feature.Availability, StringComparison.InvariantCultureIgnoreCase) == 0 &&
                   string.Compare(Category, feature.Category, StringComparison.InvariantCultureIgnoreCase) == 0 &&
                   string.Compare(Description, feature.Description, StringComparison.InvariantCultureIgnoreCase) == 0;
        }
    }

    public class Photo
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string SecureUrl { get; set; }
        public bool AreSame(Photo photo)
        {
            return string.Compare(SecureUrl, photo.SecureUrl, StringComparison.InvariantCultureIgnoreCase) == 0;
        }
    }

    public class Seller
    {
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string AlternatePhoneNumber { get; set; }
    }

    public class Location
    {
        public string Postcode { get; set; }
        public string Region { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Suburb { get; set; }
        public Country Country { get; set; }
    }
}
