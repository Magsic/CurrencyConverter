using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CurrencyConverter.Models;
using Microsoft.AspNetCore.Mvc;
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

		public List<string> GetCurrencies()
		{
			return context.Currencies.Select(c => c.Name).ToList();
		}

		public async Task<double> GetRate(string sourceCurrency, string destinationCurrency, DateTime rateDate, double amount)
		{
			if (sourceCurrency == "---")
				return 0;

			if (sourceCurrency == destinationCurrency)
				return amount;

			if (destinationCurrency == "HRK")
			{
				double rate = await getRate(sourceCurrency, rateDate);
				return amount * rate;
			}

			if(sourceCurrency == "HRK")
			{
				double rate = await getRate(destinationCurrency, rateDate);
				return amount / rate;
			}

			double sourceRate = await getRate(sourceCurrency, rateDate);
			double destinationRate = await getRate(destinationCurrency, rateDate);

			return (amount * sourceRate) / destinationRate;
		}

		private async Task<double> getRate(string currency, DateTime date)
		{
			RateByDate rateByDate = context.Rates.Where(r => r.Date == date && r.Currency.Name == currency).FirstOrDefault();
			if (rateByDate == null)
				return await populateDb(currency, date);
			else
				return rateByDate.Rate;
		}

		private async Task<double> populateDb(string currency, DateTime rateDate)
		{
			var formatedDate = rateDate.ToString("yyyy-MM-dd");
			string responseBody = "";
			try
			{
				HttpResponseMessage response = await client.GetAsync($"http://api.hnb.hr/tecajn/v2?valuta={currency}&datum-primjene={formatedDate}");
				response.EnsureSuccessStatusCode();
				responseBody = await response.Content.ReadAsStringAsync();
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
			return rate;
		}
	}
}