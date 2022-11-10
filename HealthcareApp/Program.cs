var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<IPatientDataAccess<OpdPatient, int>, OpdPatientDataAccess>();
builder.Services.AddScoped<IPatientDataAccess<IpdPatient, int>, IpdPatientDataAccess>();
builder.Services.AddScoped<IPatientDataAccess<Patient, int>, PatientDataAccess>();
builder.Services.AddScoped<IStaffDataAccess<Doctor, int>, DoctorDataAccess>();
builder.Services.AddScoped<IStaffDataAccess<Nurse, int>, NurseDataAccess>();
builder.Services.AddScoped<IStaffDataAccess<WardBoy, int>, WardBoyDataAccess>();
builder.Services.AddScoped<IEntityDataAccess<Canteen, int>, CanteenDataAccess>();
builder.Services.AddScoped<IEntityDataAccess<Constant, int>, ConstantDataAccess>();
builder.Services.AddScoped<IEntityDataAccess<Medicine, int>, MedicineDataAccess>();
builder.Services.AddScoped<IReportDataAccess<Report, int>, ReportDataAccess>();

builder.Services.AddScoped<IServiceRepository<OpdPatient, int>, OpdPatientRepository>();
builder.Services.AddScoped<IServiceRepository<IpdPatient, int>, IpdPatientRepository>();
builder.Services.AddScoped<IServiceRepository<Patient, int>, PatientRepository>();
builder.Services.AddScoped<IServiceRepository<Doctor, int>, DoctorRepository>();
builder.Services.AddScoped<IServiceRepository<Nurse, int>, NurseRepository>();
builder.Services.AddScoped<IServiceRepository<WardBoy, int>, WardBoyRepository>();
builder.Services.AddScoped<IEntityRepository<Canteen, int>, CanteenRepository>();
builder.Services.AddScoped<IEntityRepository<Constant, int>, ConstantRepository>();
builder.Services.AddScoped<IEntityRepository<Medicine, int>, MedicineRepository>();
builder.Services.AddScoped<IReportRepository<Report, int>, ReportRepository>();



builder.Services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.PropertyNamingPolicy = null;
        });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors(option =>
{
    option.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
});

//app.UseHttpsRedirection();

app.UseAuthorization();



app.MapControllers();

app.Run();
