using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CurrencyConverter.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CurrencyConverter.Controllers
{
	public class RatesController : Controller
	{
		private class CurrencyInfo
		{
			[JsonProperty(PropertyName = "srednji_tecaj")]
			public string RateString { get; set; }
		}

		static readonly HttpClient client = new HttpClient();

		private readonly CurrencyContext context;

		public RatesController(CurrencyContext currencyContext)
		{
			context = currencyContext;
		}

		public IActionResult Index()
		{
			return View();
		}

		public async Task<List<string>> GetCurrencies()
		{
			List<string> currencyNames = new List<string>();

			foreach (var currency in context.Currencies)
			{
				currencyNames.Add(currency.Name);
			}

			await populateDb("EUR", new DateTime(2019, 7, 9));

			return currencyNames;
		}

		public IActionResult Get(Currency sourceCurrency, Currency destinationCurrency, DateTime rateDate)
		{
			return null;
		}

		private async Task populateDb(string currency, DateTime rateDate)
		{
			var formatedDate = rateDate.ToString("yyyy-MM-dd");
			string responseBody = "";
			try
			{
				HttpResponseMessage response = await client.GetAsync($"http://api.hnb.hr/tecajn/v2?valuta={currency}&datum-primjene={formatedDate}");
				response.EnsureSuccessStatusCode();
				responseBody = await response.Content.ReadAsStringAsync();
				// Above three lines can be replaced with new helper method below
				// string responseBody = await client.GetStringAsync(uri);

				Debug.WriteLine(responseBody);

			}
			catch (HttpRequestException e)
			{
				Console.WriteLine("\nException Caught!");
				Console.WriteLine("Message :{0} ", e.Message);
			}

			var rateString = JsonConvert.DeserializeObject<CurrencyInfo[]>(responseBody).FirstOrDefault()?.RateString;

			double rate = double.Parse(rateString.Replace(',', '.'));

			Currency selectedCurrency = context.Currencies.Where(c => c.Name == currency).FirstOrDefault();

			context.Rates.Add(
				new RateByDate
				{
					Currency = selectedCurrency,
					Date = rateDate,
					Rate = rate
				});

			context.SaveChanges();
		}
	}
}