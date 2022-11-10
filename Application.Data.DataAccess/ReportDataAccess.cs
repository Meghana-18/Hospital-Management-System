using System;
using Application.Dal.Contract;
using Application.Entities.Base;
using System.Data;
using System.Data.SqlClient;
using Application.Entities;

namespace Application.Data.DataAccess
{
    public class ReportDataAccess : IReportDataAccess<Report, int>
    {
        SqlConnection connection;
        SqlCommand command;

        public ReportDataAccess()
        {
            connection = new SqlConnection("Data Source = 127.0.0.1; Initial Catalog = HealthcareSystem_DB; User Id = sa; Password = Docker@SQL123");
        }

        decimal IReportDataAccess<Report, int>.calcOpdBill(int id)
        {
            decimal billAmt = 0, doctorCharges = 0;
            bool dressing = false, bloodTest = false, ecg = false;
            int DID = 0;
            try
            {
                connection.Open();
                command = connection.CreateCommand();
                command.CommandText = $"SELECT OPDFees + ODiagnoseFees + OMedicineCharges AS TempBillAmt FROM OPD_Patient WHERE PID = {id}";
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    billAmt = Convert.ToDecimal(reader["TempBillAmt"]);
                }
                reader.Close();

                connection.Open();
                command = connection.CreateCommand();
                command.CommandText = $"SELECT ODressing, OBloodTest, OECG, DID FROM OPD_Patient WHERE PID = {id}";
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    dressing = Convert.ToBoolean(reader["ODressing"]);
                    bloodTest = Convert.ToBoolean(reader["OBloodTest"]);
                    ecg = Convert.ToBoolean(reader["OECG"]);
                    DID = Convert.ToInt32(reader["DID"]);
                }
                reader.Close();

                if (dressing)
                {
                    connection.Open();
                    command = connection.CreateCommand();
                    command.CommandText = $"SELECT CPrice FROM Constants WHERE CName = 'DRESSING FEES'";
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        billAmt += Convert.ToDecimal(reader["CPrice"]);
                    }
                    reader.Close();
                }
                if (bloodTest)
                {
                    connection.Open();
                    command = connection.CreateCommand();
                    command.CommandText = $"SELECT CPrice FROM Constants WHERE CName = 'BLOODTEST COST'";
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        billAmt += Convert.ToDecimal(reader["CPrice"]);
                    }
                    reader.Close();
                }
                if (ecg)
                {
                    connection.Open();
                    command = connection.CreateCommand();
                    command.CommandText = $"SELECT CPrice FROM Constants WHERE CName = 'ECG COST'";
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        billAmt += Convert.ToDecimal(reader["CPrice"]);
                    }
                    reader.Close();
                }

                ////Doctor charges
                ////Get DCharges from Doctor table by matching DID retrieved from OPD_Patient table
                //connection.Open();
                //command = connection.CreateCommand();
                //command.CommandText = $"SELECT DCharges FROM Doctor WHERE DID = {DID}";
                //reader = command.ExecuteReader();
                //while (reader.Read())
                //{
                //    doctorCharges = Convert.ToDecimal(reader["DCharges"]);
                //}
                //reader.Close();

                ////Add doctor charges to billAmt
                //billAmt += doctorCharges;

                connection.Open();
                command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = $"UPDATE OPD_Patient SET OBillAmount={billAmt} where PID={id}";
                int result = command.ExecuteNonQuery();

            }
            catch (SqlException sqlEx)
            {
                throw sqlEx;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
            return billAmt;
        }

        decimal IReportDataAccess<Report, int>.calcIpdBill(int id)
        {
            decimal billAmt = 0;
            try
            {
                DateTime admitDate = new DateTime(2001, 06, 18), dischargeDate = new DateTime(2001, 06, 18);
                int docVisitCount = 0, DID = 0, NID = 0, RID = 0;
                bool bloodCheck = false, radiology = false, injection = false;
                decimal laundryPerDay = 0, laundryCharges = 0, foodChargesPerMeal = 0, foodCharges = 0, doctorFeesPerVisit = 0, doctorFees = 0,
                    roomChargesPerDay = 0, roomCharges = 0, days = 0, nurseFees = 0;

                connection.Open();
                command = connection.CreateCommand();
                command.CommandText = $"SELECT IAdmitDate, IDischargeDate WHERE PID = {id}";
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    admitDate = Convert.ToDateTime(reader["IAdmitDate"]);
                    dischargeDate = Convert.ToDateTime(reader["IDischargeDate"]);
                }
                reader.Close();

                days = Convert.ToDecimal((dischargeDate - admitDate).TotalDays);

                //Get laundryperday from constants
                connection.Open();
                command = connection.CreateCommand();
                command.CommandText = $"SELECT CPrice FROM Constants WHERE CName = 'LAUNDRY PER DAY'";
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    laundryPerDay = Convert.ToDecimal(reader["CPrice"]);
                }
                reader.Close();

                //Calculate laundry charges
                laundryCharges = days * laundryPerDay;

                //Set ILaundry
                connection.Open();
                command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = $"UPDATE IPD_Patient SET ILaundry={laundryCharges} where PID={id}";
                int result = command.ExecuteNonQuery();

                //Get foodcharges from constants
                connection.Open();
                command = connection.CreateCommand();
                command.CommandText = $"SELECT CPrice FROM Constants WHERE CName = 'FOOD CHARGES PER MEAL'";
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    foodChargesPerMeal = Convert.ToDecimal(reader["CPrice"]);
                }
                reader.Close();

                //Calculate food charges
                foodCharges = days * 2 * foodChargesPerMeal;

                //Set IFoodCharges
                connection.Open();
                command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = $"UPDATE IPD_Patient SET IFoodCharges={foodCharges} where PID={id}";
                result = command.ExecuteNonQuery();

                //Get required values from IPD_Patient table
                connection.Open();
                command = connection.CreateCommand();
                command.CommandText = $"SELECT DID, NID, RID, IDocVisitCount, IMedicineCharges + ILaundry + IFoodCharges AS TempBillAmt, " +
                    $"IBloodCheck, IRadiology, IInjection FROM IPD_Patient WHERE PID = {id}";
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    DID = Convert.ToInt32(reader["DID"]);
                    NID = Convert.ToInt32(reader["NID"]);
                    RID = Convert.ToInt32(reader["RID"]);
                    docVisitCount = Convert.ToInt32(reader["IDocVisitCount"]);
                    billAmt += Convert.ToDecimal(reader["TempBillAmt"]);
                    bloodCheck = Convert.ToBoolean(reader["IBloodCheck"]);
                    radiology = Convert.ToBoolean(reader["IRadiology"]);
                    injection = Convert.ToBoolean(reader["IInjection"]);
                }
                reader.Close();

                //Add doctorFees based on doctor visits
                //Get doctorFeesPerVisit from Doctor table by matching DID retrieved from IPD_Patient table
                connection.Open();
                command = connection.CreateCommand();
                command.CommandText = $"SELECT DFees FROM Doctor WHERE DID = {DID}";
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    doctorFeesPerVisit = Convert.ToDecimal(reader["DFees"]);
                }
                reader.Close();

                //Calculate doctor fees --> fees * noOfDoctorVisits
                doctorFees = doctorFeesPerVisit * docVisitCount;

                //Add doctor fees to billAmt
                billAmt += doctorFees;

                //connection.Open();
                //command = connection.CreateCommand();
                //command.CommandText = $"SELECT IMedicineCharges + ILaundry + IFoodCharges AS TempBillAmt FROM IPD_Patient WHERE PID = {id}";
                //reader = command.ExecuteReader();
                //while (reader.Read())
                //{
                //    billAmt = Convert.ToDecimal(reader["TempBillAmt"]);
                //}
                //reader.Close();

                //connection.Open();
                //command = connection.CreateCommand();
                //command.CommandText = $"SELECT IBloodCheck, IRadiology, IInjection FROM IPD_Patient WHERE PID = {id}";
                //reader = command.ExecuteReader();
                //while (reader.Read())
                //{
                //    bloodCheck = Convert.ToBoolean(reader["IBloodCheck"]);
                //    radiology = Convert.ToBoolean(reader["IRadiology"]);
                //    injection = Convert.ToBoolean(reader["IInjection"]);
                //}
                //reader.Close();

                //Room Charges (per day)
                //Get RID from IPD_Patient
                //Get RCharge from Room table by using retrieved RID
                connection.Open();
                command = connection.CreateCommand();
                command.CommandText = $"SELECT RCharge FROM Room WHERE RID = {RID}";
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    roomChargesPerDay = Convert.ToDecimal(reader["RCharge"]);
                }
                reader.Close();
                //Calculate roomCharges --> roomChargesPerDay * days
                roomCharges = roomChargesPerDay * days;
                //Add roomCharges to billAmt
                billAmt += roomCharges;

                //Nurse Charges
                //Get NID from IPD_Patient
                //Get NFees from Nurse
                connection.Open();
                command = connection.CreateCommand();
                command.CommandText = $"SELECT NFees FROM Nurse WHERE NID = {NID}";
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    nurseFees = Convert.ToDecimal(reader["NFees"]);
                }
                reader.Close();
                //Add NFees to billAmt
                billAmt += nurseFees;

                //TODO: Canteen Charges

                if (bloodCheck)
                {
                    connection.Open();
                    command = connection.CreateCommand();
                    command.CommandText = $"SELECT CPrice FROM Constants WHERE CName = 'BLOODTEST COST'";
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        billAmt += Convert.ToDecimal(reader["CPrice"]);
                    }
                    reader.Close();
                }
                if (radiology)
                {
                    connection.Open();
                    command = connection.CreateCommand();
                    command.CommandText = $"SELECT CPrice FROM Constants WHERE CName = 'RADIOLOGY'";
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        billAmt += Convert.ToDecimal(reader["CPrice"]);
                    }
                    reader.Close();
                }
                if (injection)
                {
                    connection.Open();
                    command = connection.CreateCommand();
                    command.CommandText = $"SELECT CPrice FROM Constants WHERE CName = 'INJECTION'";
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        billAmt += Convert.ToDecimal(reader["CPrice"]);
                    }
                    reader.Close();
                }

                connection.Open();
                command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = $"UPDATE IPD_Patient SET IBillAmount={billAmt} where PID={id}";
                result = command.ExecuteNonQuery();

            }
            catch (SqlException sqlEx)
            {
                throw sqlEx;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
            return billAmt;
        }

        IEnumerable<Doctor> IReportDataAccess<Report, int>.GetbySpec(string spec)
        {
            List<Doctor> Doctors = new List<Doctor>();
            try
            {
                connection.Open();
                command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = CommandType.Text;
                command.CommandText = $"SELECT * FROM Doctor WHERE DSpecialization='{spec}'";
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Doctors.Add(
                          new Doctor()
                          {
                              StaffId = Convert.ToInt32(reader["DID"]),
                              FirstName = Convert.ToString(reader["DFirstName"]),
                              MiddleName = Convert.ToString(reader["DMiddleName"]),
                              LastName = Convert.ToString(reader["DLastName"]),
                              MobileNo = Convert.ToString(reader["DMobile"]),
                              EmailID = Convert.ToString(reader["DEmailID"]),
                              HouseNo = Convert.ToString(reader["DHouseNo"]),
                              Society = Convert.ToString(reader["DSociety"]),
                              Area = Convert.ToString(reader["DArea"]),
                              City = Convert.ToString(reader["DCity"]),
                              State = Convert.ToString(reader["DState"]),
                              DOB = Convert.ToString(reader["DDOB"]),
                              Gender = Convert.ToString(reader["DGender"]),
                              Specialization = Convert.ToString(reader["DSpecialization"]),
                              Salary = Convert.ToDecimal(reader["DSalary"]),
                              VisitingDoc = Convert.ToBoolean(reader["DVisiting"]),
                              OpdFees = Convert.ToDecimal(reader["DFees"]),
                              VisitCharges = Convert.ToDecimal(reader["DCharges"])
                          }
                        );
                }
                reader.Close();
            }
            catch (SqlException sqlEx)
            {
                throw sqlEx;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
            return Doctors;
        }

        IEnumerable<Doctor> IReportDataAccess<Report, int>.GetEmpDoctors()
        {
            List<Doctor> Doctors = new List<Doctor>();
            try
            {
                connection.Open();
                command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = CommandType.Text;
                command.CommandText = $"SELECT * FROM Doctor WHERE DVisiting='false'";
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Doctors.Add(
                          new Doctor()
                          {
                              StaffId = Convert.ToInt32(reader["DID"]),
                              FirstName = Convert.ToString(reader["DFirstName"]),
                              MiddleName = Convert.ToString(reader["DMiddleName"]),
                              LastName = Convert.ToString(reader["DLastName"]),
                              MobileNo = Convert.ToString(reader["DMobile"]),
                              EmailID = Convert.ToString(reader["DEmailID"]),
                              HouseNo = Convert.ToString(reader["DHouseNo"]),
                              Society = Convert.ToString(reader["DSociety"]),
                              Area = Convert.ToString(reader["DArea"]),
                              City = Convert.ToString(reader["DCity"]),
                              State = Convert.ToString(reader["DState"]),
                              DOB = Convert.ToString(reader["DDOB"]),
                              Gender = Convert.ToString(reader["DGender"]),
                              Specialization = Convert.ToString(reader["DSpecialization"]),
                              Salary = Convert.ToDecimal(reader["DSalary"]),
                              VisitingDoc = Convert.ToBoolean(reader["DVisiting"]),
                              OpdFees = Convert.ToDecimal(reader["DFees"]),
                              VisitCharges = Convert.ToDecimal(reader["DCharges"])
                          }
                        );
                }
                reader.Close();
            }
            catch (SqlException sqlEx)
            {
                throw sqlEx;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
            return Doctors;
        }

        IEnumerable<Doctor> IReportDataAccess<Report, int>.GetVisitingDoctors()
        {
            List<Doctor> Doctors = new List<Doctor>();
            try
            {
                connection.Open();
                command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = CommandType.Text;
                command.CommandText = $"SELECT * FROM Doctor WHERE DVisiting='true'";
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Doctors.Add(
                          new Doctor()
                          {
                              StaffId = Convert.ToInt32(reader["DID"]),
                              FirstName = Convert.ToString(reader["DFirstName"]),
                              MiddleName = Convert.ToString(reader["DMiddleName"]),
                              LastName = Convert.ToString(reader["DLastName"]),
                              MobileNo = Convert.ToString(reader["DMobile"]),
                              EmailID = Convert.ToString(reader["DEmailID"]),
                              HouseNo = Convert.ToString(reader["DHouseNo"]),
                              Society = Convert.ToString(reader["DSociety"]),
                              Area = Convert.ToString(reader["DArea"]),
                              City = Convert.ToString(reader["DCity"]),
                              State = Convert.ToString(reader["DState"]),
                              DOB = Convert.ToString(reader["DDOB"]),
                              Gender = Convert.ToString(reader["DGender"]),
                              Specialization = Convert.ToString(reader["DSpecialization"]),
                              Salary = Convert.ToDecimal(reader["DSalary"]),
                              VisitingDoc = Convert.ToBoolean(reader["DVisiting"]),
                              OpdFees = Convert.ToDecimal(reader["DFees"]),
                              VisitCharges = Convert.ToDecimal(reader["DCharges"])
                          }
                        );
                }
                reader.Close();
            }
            catch (SqlException sqlEx)
            {
                throw sqlEx;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
            return Doctors;
        }

        IEnumerable<IpdPatient> IReportDataAccess<Report, int>.GetbyDoctor(int DID)
        {
            List<IpdPatient> IpdPatients = new List<IpdPatient>();
            try
            {
                connection.Open();
                command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = CommandType.Text;
                command.CommandText = $"SELECT * FROM IPD_Patient INNER JOIN Patient ON IPD_Patient.PID = Patient.PID WHERE IPD_Patient.DID={DID}";
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    IpdPatients.Add(
                          new IpdPatient()
                          {
                              FirstName = Convert.ToString(reader["PFirstName"]),
                              MiddleName = Convert.ToString(reader["PMiddleName"]),
                              LastName = Convert.ToString(reader["PLastName"]),
                              MobileNo = Convert.ToString(reader["PMobile"]),
                              EmailID = Convert.ToString(reader["PEmailID"]),
                              HouseNo = Convert.ToString(reader["PHouseNo"]),
                              Society = Convert.ToString(reader["PSociety"]),
                              Area = Convert.ToString(reader["PArea"]),
                              City = Convert.ToString(reader["PCity"]),
                              State = Convert.ToString(reader["PState"]),
                              DOB = Convert.ToString(reader["PDOB"]),
                              Gender = Convert.ToString(reader["PGender"]),
                              AgeType = Convert.ToString(reader["PAgeType"]),
                              Admit = Convert.ToBoolean(reader["PAdmit"]),
                              PatientId = Convert.ToInt32(reader["PID"]),
                              DoctorId = Convert.ToInt32(reader["DID"]),
                              NurseId = Convert.ToInt32(reader["NID"]),
                              RoomTypeId = Convert.ToInt32(reader["RID"]),
                              Ward = Convert.ToString(reader["IWard"]),
                              RoomNo = Convert.ToInt32(reader["IRoomNo"]),
                              AdvancePaid = Convert.ToBoolean(reader["IAdvance"]),
                              AdmittedDate = Convert.ToString(reader["IAdmitDate"]),
                              DischargeDate = Convert.ToString(reader["IDischargeDate"]),
                              NoOfDoctorVisits = Convert.ToInt32(reader["IDocVisitCount"]),
                              MedicineCharges = Convert.ToDecimal(reader["IMedicineCharges"]),
                              BloodCheck = Convert.ToBoolean(reader["IBloodCheck"]),
                              Radiology = Convert.ToBoolean(reader["IRadiology"]),
                              Injection = Convert.ToBoolean(reader["IInjection"]),
                              LaundryCharges = Convert.ToDecimal(reader["ILaundry"]),
                              FoodCharges = Convert.ToDecimal(reader["IFoodCharges"]),
                              BillAmount = Convert.ToDecimal(reader["IBillAmount"]),
                              BillPaid = Convert.ToBoolean(reader["IBillPaid"])
                          }
                        );
                }
                reader.Close();
            }
            catch (SqlException sqlEx)
            {
                throw sqlEx;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
            return IpdPatients;
        }

        IEnumerable<IpdPatient> IReportDataAccess<Report, int>.GetbyNurse(int NID)
        {
            List<IpdPatient> IpdPatients = new List<IpdPatient>();
            try
            {
                connection.Open();
                command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = CommandType.Text;
                command.CommandText = $"SELECT * FROM IPD_Patient INNER JOIN Patient ON IPD_Patient.PID = Patient.PID WHERE NID={NID}";
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    IpdPatients.Add(
                          new IpdPatient()
                          {
                              PatientId = Convert.ToInt32(reader["PID"]),
                              DoctorId = Convert.ToInt32(reader["DID"]),
                              NurseId = Convert.ToInt32(reader["NID"]),
                              RoomTypeId = Convert.ToInt32(reader["RID"]),
                              Ward = Convert.ToString(reader["IWard"]),
                              RoomNo = Convert.ToInt32(reader["IRoomNo"]),
                              AdvancePaid = Convert.ToBoolean(reader["IAdvance"]),
                              AdmittedDate = Convert.ToString(reader["IAdmitDate"]),
                              DischargeDate = Convert.ToString(reader["IDischargeDate"]),
                              NoOfDoctorVisits = Convert.ToInt32(reader["IDocVisitCount"]),
                              MedicineCharges = Convert.ToDecimal(reader["IMedicineCharges"]),
                              BloodCheck = Convert.ToBoolean(reader["IBloodCheck"]),
                              Radiology = Convert.ToBoolean(reader["IRadiology"]),
                              Injection = Convert.ToBoolean(reader["IInjection"]),
                              LaundryCharges = Convert.ToDecimal(reader["ILaundry"]),
                              FoodCharges = Convert.ToDecimal(reader["IFoodCharges"]),
                              BillAmount = Convert.ToDecimal(reader["IBillAmount"]),
                              BillPaid = Convert.ToBoolean(reader["IBillPaid"])
                          }
                        );
                }
                reader.Close();
            }
            catch (SqlException sqlEx)
            {
                throw sqlEx;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
            return IpdPatients;
        }

        IEnumerable<IpdPatient> IReportDataAccess<Report, int>.GetbyWard(string ward)
        {
            List<IpdPatient> IpdPatients = new List<IpdPatient>();
            try
            {
                connection.Open();
                command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = CommandType.Text;
                command.CommandText = $"SELECT * FROM IPD_Patient INNER JOIN Patient ON IPD_Patient.PID = Patient.PID WHERE IWard='{ward}'";
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    IpdPatients.Add(
                          new IpdPatient()
                          {
                              PatientId = Convert.ToInt32(reader["PID"]),
                              DoctorId = Convert.ToInt32(reader["DID"]),
                              NurseId = Convert.ToInt32(reader["NID"]),
                              RoomTypeId = Convert.ToInt32(reader["RID"]),
                              Ward = Convert.ToString(reader["IWard"]),
                              RoomNo = Convert.ToInt32(reader["IRoomNo"]),
                              AdvancePaid = Convert.ToBoolean(reader["IAdvance"]),
                              AdmittedDate = Convert.ToString(reader["IAdmitDate"]),
                              DischargeDate = Convert.ToString(reader["IDischargeDate"]),
                              NoOfDoctorVisits = Convert.ToInt32(reader["IDocVisitCount"]),
                              MedicineCharges = Convert.ToDecimal(reader["IMedicineCharges"]),
                              BloodCheck = Convert.ToBoolean(reader["IBloodCheck"]),
                              Radiology = Convert.ToBoolean(reader["IRadiology"]),
                              Injection = Convert.ToBoolean(reader["IInjection"]),
                              LaundryCharges = Convert.ToDecimal(reader["ILaundry"]),
                              FoodCharges = Convert.ToDecimal(reader["IFoodCharges"]),
                              BillAmount = Convert.ToDecimal(reader["IBillAmount"]),
                              BillPaid = Convert.ToBoolean(reader["IBillPaid"])
                          }
                        );
                }
                reader.Close();
            }
            catch (SqlException sqlEx)
            {
                throw sqlEx;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
            return IpdPatients;
        }

        decimal IReportDataAccess<Report, int>.CalcTotalCollection()
        {
            decimal totalCollection = 0;
            try
            {
                connection.Open();
                command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = CommandType.Text;
                command.CommandText = $"SELECT((SELECT COALESCE(SUM(OBillAmount), 0) FROM OPD_Patient) + (SELECT COALESCE(SUM(IBillAmount), 0) FROM IPD_Patient)) AS TOTAL_COLLECTION";
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    totalCollection += Convert.ToDecimal(reader["TOTAL_COLLECTION"]);
                }
                reader.Close();
            }
            catch (SqlException sqlEx)
            {
                throw sqlEx;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
            return totalCollection;
        }


    }
}

