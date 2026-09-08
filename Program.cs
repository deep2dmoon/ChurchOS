using System.Text;
using System.Text.Json.Serialization;
using System.Threading.RateLimiting;
using htmos.branch;
using htmos.contract;
using htmos.data;
using htmos.services;
using htmos.services.repository;
using htmos.services.workers;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder();

builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IBranchRepository, BranchRepository>();
builder.Services.AddHostedService<FollowUp>();
builder.Services.AddScoped<SmsService>();
builder.Services.AddScoped<HttpClient>();
builder.Services.AddScoped<IMemberRepository, MemberRepository>();
builder.Services.AddScoped<AnnouncementService>();
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<AppointmentService>();




builder.Services.AddControllers().AddJsonOptions(option =>
{
    option.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

string ConnString = builder.Configuration.GetValue<string>("Postgre:ConnString")!;



builder.Services.AddDbContextPool<DatabaseContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("ConnString")));

builder.Services.AddRateLimiter((option) =>
{
    option.AddPolicy("member/new", (ctx) =>
{
    return RateLimitPartition.GetFixedWindowLimiter(ctx.Connection.RemoteIpAddress, (_) => new FixedWindowRateLimiterOptions
    {
        PermitLimit = 3,
        Window = TimeSpan.FromSeconds(60),
        QueueProcessingOrder = QueueProcessingOrder.OldestFirst
    });
});

    option.AddPolicy("member/login", (ctx) =>
    {
        return RateLimitPartition.GetFixedWindowLimiter(ctx.Connection.RemoteIpAddress, (_) => new FixedWindowRateLimiterOptions
        {
            PermitLimit = 5,

            Window = TimeSpan.FromSeconds(60),
            QueueProcessingOrder = QueueProcessingOrder.OldestFirst
        });
    });
});

// add authentication & authorization services
builder.Services.AddAuthentication("Bearer").AddJwtBearer("Bearer", (option) => option.TokenValidationParameters =
 new TokenValidationParameters
 {
     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"]!)),
     ValidIssuer = builder.Configuration["Valid:iss"],
     ValidateIssuer = true,
     ValidateAudience = true,
     ValidAudience = builder.Configuration["Valid:aud"],
     ValidateLifetime = true,

 });
builder.Services.AddAuthorizationBuilder()
    .AddPolicy("CanWrite", policy => policy.RequireClaim("Permission", "Write"))
    .AddPolicy("CanWriteBranch", policy => policy.RequireClaim("Permission", "Branch.Write"))
    .AddPolicy("CanRead", policy => policy.RequireClaim("Permission", "Read"));




var app = builder.Build();

Console.WriteLine(ConnString);
// use registered services
app.ContextMigrator();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseRateLimiter();

app.Run();