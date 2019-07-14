using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyConverter.Models
{
	public class Statistics
	{
		public int Id { get; set; }
		public Currency Currency { get; set; }
		public DateTime Date { get; set; }
		public int ConvertCount { get; set; }
	}
}