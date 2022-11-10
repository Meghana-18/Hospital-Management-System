using System;
using Application.Entities.Base;

namespace Application.Entities
{
	public class Doctor : Staff
	{
		public string Specialization { get; set; }
		public bool VisitingDoc { get; set; }
		public decimal OpdFees { get; set; }
		public decimal VisitCharges { get; set; }
	}
}

