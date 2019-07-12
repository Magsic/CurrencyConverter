using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyConverter.Models
{
	public class RateByDate
	{
		public int Id { get; set; }
		public Currency Currency { get; set; }
		public DateTime Date { get; set; }
		public double Rate { get; set; }
	}
}
