using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CurrencyConverter.Models;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyConverter.Controllers
{
    public class RatesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

		public List<Currency> GetCurrencies()
		{
			var context = new CurrencyContext();
			List<Currency> currencies = new List<Currency>();

			foreach (var currency in context.Currencies)
			{
				currencies.Add(currency);
			}

			return currencies;
		}

		public IActionResult Get(Currency sourceCurrency, Currency destinationCurrency, DateTime rateDate)
		{
			return null;
		}
    }
}