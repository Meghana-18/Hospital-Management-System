using System;
namespace Application.Entities.Base
{
    public class Patient
    {
        public int PatientId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string MobileNo { get; set; }
        public string EmailID { get; set; }
        public string HouseNo { get; set; }
        public string Society { get; set; }
        public string Area { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string DOB { get; set; }
        public string Gender { get; set; }
        public string AgeType { get; set; }
        public bool Admit { get; set; }

    }
}

