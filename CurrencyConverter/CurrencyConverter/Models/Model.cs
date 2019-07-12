using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CurrencyConverter.Models
{
	public class CurrencyContext : DbContext
	{
		public CurrencyContext()
		{
		}

		public CurrencyContext(DbContextOptions<CurrencyContext> options) : base(options)
		{}

		public DbSet<Currency> Currencies { get; set; }
		public DbSet<RateByDate> Rates { get; set; }
	}
}
