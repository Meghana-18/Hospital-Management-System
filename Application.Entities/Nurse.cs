using System;
using Application.Entities.Base;

namespace Application.Entities
{
	public class Nurse : Staff
	{
		public decimal FeesPerday { get; set; }
	}
}

