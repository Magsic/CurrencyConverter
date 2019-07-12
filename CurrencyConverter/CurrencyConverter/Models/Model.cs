using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CurrencyConverter.Models
{
	public class CurrencyContext : DbContext
	{
		public CurrencyContext(DbContextOptions<CurrencyContext> options) : base(options)
		{ }

		public DbSet<Currency> Currencies { get; set; }
		public DbSet<RateByDate> Rates { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Currency>().HasData(
				new Currency
				{
					Id = 1,
					Name = "AUD"
				},
				new Currency
				{
					Id = 2,
					Name = "CAD"
				},
				new Currency
				{
					Id = 3,
					Name = "CZK"
				},
				new Currency
				{
					Id = 4,
					Name = "DKK"
				},
				new Currency
				{
					Id = 5,
					Name = "HUF"
				},
				new Currency
				{
					Id = 6,
					Name = "JPY"
				},
				new Currency
				{
					Id = 7,
					Name = "NOK"
				},
				new Currency
				{
					Id = 8,
					Name = "SEK"
				},
				new Currency
				{
					Id = 9,
					Name = "CHF"
				},
				new Currency
				{
					Id = 10,
					Name = "GBP"
				},
				new Currency
				{
					Id = 11,
					Name = "USD"
				},
				new Currency
				{
					Id = 12,
					Name = "BAM"
				},
				new Currency
				{
					Id = 13,
					Name = "EUR"
				},
				new Currency
				{
					Id = 14,
					Name = "PLN"
				}
			);
		}
	}
}