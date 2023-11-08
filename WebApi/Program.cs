using BusinessObjects;
using BusinessObjects.Models;
using BusinessObjects.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Odata
builder.Services.AddControllers().AddOData(options =>
options.AddRouteComponents(routePrefix: "odata", model: GetEdmModel())
.Select()
.Expand()
.OrderBy()
.SetMaxTop(100)
.Count()
.Filter());

// JWT set up
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "PE_SE160413", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Please Enter The Token To Authenticate The Role",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});
// Token
var serect = builder.Configuration["AppSettings:SecretKey"];
var key = Encoding.ASCII.GetBytes(serect);
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

// DI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<RoseTattooShop2023DBContext>(option =>
    option.UseSqlServer(builder.Configuration.GetConnectionString("DBSQL")));
builder.Services.AddScoped<ITattooStickerRepo, TattooStickerRepo>();
builder.Services.AddScoped<IMemberAccountRepo, MemberAccountRepo>();
builder.Services.AddScoped<IRoseTattooRepo, RoseTattooRepo>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// Odata
app.UseODataBatching();

// Authen
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

 static IEdmModel GetEdmModel()
{
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    //builder.EntitySet<Artworks>("Artworks");
    //builder.EntitySet<Users>("Users");
    //builder.EntitySet<Museums>("Museums");
    return builder.GetEdmModel();
}