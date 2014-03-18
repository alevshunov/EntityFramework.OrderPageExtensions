using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace KudesniK.EntityFramework.OrderPageExtensions.Demo.Models
{
    public enum CountryOrder
    {
        Rank,
        Name,
        Population,
        RelevanceTop,
        Date,
        Percent,
        Source
    }

    public class Country
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Rank { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public Int64 Population { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public float Percent { get; set; }

        public string Source { get; set; }
    }

    public class Db : DbContext
    {
        public DbSet<Country> Countries { get; set; }
    }
}