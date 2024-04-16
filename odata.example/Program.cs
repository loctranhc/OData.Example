using Microsoft.AspNetCore.OData;
using odata.example.DbContext;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers().AddOData(option => option.Select().Filter());
builder.Services.AddDbContext<OdataExampleDbContext>();

var app = builder.Build();
app.MapControllers();
app.Run();
