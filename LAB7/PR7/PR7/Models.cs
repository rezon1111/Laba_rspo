using System;

namespace PublishingApp.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string FullName => $"{Surname} {Name}";
    }

    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public int ReleaseYear { get; set; }
        public int Pages { get; set; }
        public int Circulation { get; set; }
        public string Type { get; set; }
        public decimal Price => CalculatePrice();

        private decimal CalculatePrice()
        {
            decimal basePrice = Pages * 0.5m + Circulation * 0.01m;
            return Math.Round(basePrice, 2);
        }

        public override string ToString()
        {
            return $"{Title} ({AuthorName}, {ReleaseYear}) - {Pages} стр., тираж: {Circulation}";
        }
    }

    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Type { get; set; } = 1;
        public string Address { get; set; }
        public string Phone { get; set; }

        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(Name) &&
                   !string.IsNullOrWhiteSpace(Phone);
        }
    }


    public class Office
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }

        public override string ToString()
        {
            return $"{Name} ({Address})";
        }
    }

    public class Order
    {
        public int Id { get; set; }
        public string OrderName { get; set; }
        public int BookId { get; set; }
        public string BookTitle { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int OfficeId { get; set; }
        public string OfficeName { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public DateTime? CompletionDate { get; set; }
        public decimal Price { get; set; }

        public static string GenerateOrderName()
        {
            return $"Предзаказ-{DateTime.Now:yyyyMMdd-HHmmss}";
        }
    }
}
