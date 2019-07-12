using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CurrencyConverter.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CurrencyConverter.Controllers
{
    public class RatesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

		public List<string> GetCurrencies()
		{
			var optionsBuilder = new DbContextOptionsBuilder<CurrencyContext>();
			optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=CurrencyConverter.AspNetCore.Db;Trusted_Connection=True;ConnectRetryCount=0");

			var context = new CurrencyContext(optionsBuilder.Options);
			List<string> currencyNames = new List<string>();

			foreach (var currency in context.Currencies)
			{
				currencyNames.Add(currency.Name);
			}

			return currencyNames;
		}

		public IActionResult Get(Currency sourceCurrency, Currency destinationCurrency, DateTime rateDate)
		{
			return null;
		}
    }
}