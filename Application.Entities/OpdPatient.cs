using System;
using Application.Entities.Base;

namespace Application.Entities
{
	public class OpdPatient : Patient
	{
		//PatientId is already there in the parent class so not included here
		public int DoctorId { get; set; }
		public decimal OpdFees { get; set; }
		public decimal DiagnoseFees { get; set; }
		public bool Dressing { get; set; }
		public bool BloodTest { get; set; }
		public bool Ecg { get; set; }
		public decimal MedicineCharges { get; set; }
		public decimal BillAmount { get; set; }
		public bool BillPaid { get; set; }
	}
}

