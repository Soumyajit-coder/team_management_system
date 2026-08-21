using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using team_management_system.BAL.Interfaces;
using team_management_system.BAL.Services;
using team_management_system.DAL;
using team_management_system.DAL.Interface;
using team_management_system.DAL.Interfaces;
using team_management_system.DAL.Repositories;
using team_management_system.Helper;


AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
var builder = WebApplication.CreateBuilder(args);
// Database Connection
builder.Services.AddDbContext<teamManagementSystemDBContext>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("DBConnection"),
    options => options.EnableRetryOnFailure(10, TimeSpan.FromSeconds(5), null)
    ), ServiceLifetime.Transient
);


// JWT
builder.Services.AddAuthentication(option => {
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration.GetSection("Auth:Issuer").Value,
        ValidAudience = builder.Configuration.GetSection("Auth:Audience").Value,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Auth:TokenKey").Value))
    };
});

//Repositories
builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IRoleRepository, RoleRepository>();
builder.Services.AddTransient<IUserHasRoleRepository, UserHasRepository>();
builder.Services.AddTransient<IRoleHasPermissionRepository, RoleHasPermissionRepository>();
builder.Services.AddTransient<IPermissionRepository, PermissionRepository>();
builder.Services.AddTransient<IOrganizationMgmtRepository, OrganizationMgmtRepository>();

//Services
builder.Services.AddTransient<IUserDetailsService, UserDetailsService>();
builder.Services.AddTransient<IRoleService, RoleService>();
builder.Services.AddTransient<IUserHasRoleService, UserHasRoleService>();
builder.Services.AddTransient<IRoleHasPermissionService, RoleHasPermissionService>();
builder.Services.AddTransient<IPermissionService, PermissionService>();
builder.Services.AddTransient<IOrganizationMgmtService, OrganizationMgmtService>();

// Helper
builder.Services.AddTransient<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
builder.Services.AddTransient<IAuthorizationHandler, PermissionHandler>();

// AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Please enter a valid token(Bearer[space] token)",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            new string[] {}
        }
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
