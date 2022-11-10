using System;
using Application.Entities.Base;

namespace Application.Entities
{
	public class IpdPatient : Patient
	{
		public int DoctorId { get; set; }
		public int NurseId { get; set; }
		public int RoomTypeId { get; set; }
		public string Ward { get; set; }
		public int RoomNo { get; set; }
		public bool AdvancePaid { get; set; }
		public string AdmittedDate { get; set; }
		public string DischargeDate { get; set; }
		public int NoOfDoctorVisits { get; set; }
		public decimal MedicineCharges { get; set; }
		public bool BloodCheck { get; set; }
		public bool Radiology { get; set; }
		public bool Injection { get; set; }
		public decimal LaundryCharges { get; set; }
		public decimal FoodCharges { get; set; }
		public decimal BillAmount { get; set; }
		public bool BillPaid { get; set; }
	}
}

