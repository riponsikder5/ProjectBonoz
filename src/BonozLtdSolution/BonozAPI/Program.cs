using BonozAPI.Hubs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = TokenService.GetTokenValidationParameters(builder.Configuration);

    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = (context) =>
        {
            if (context.Request.Path.StartsWithSegments("/hubs/bonoz-chat"))
            {
                var jwt = context.Request.Query["access_token"];
                if (!string.IsNullOrWhiteSpace(jwt))
                {
                    context.Token = jwt;
                }
            }
            return Task.CompletedTask;
        }
    };
});

builder.Services.AddDbContext<BanazDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("BONOZDB")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
    // Add any other Swagger configuration options you need
});

builder.Services.AddScoped<IShop, ShopManager>();
builder.Services.AddScoped<IProduct, ProductManager>();
builder.Services.AddScoped<IUser, UsersManager>();
builder.Services.AddScoped<IShoppingCart, ShoppingCartManager>();
builder.Services.AddScoped<IAddress, AddressManager>();
builder.Services.AddScoped<IOrder, OrderManager>();
builder.Services.AddScoped<IAccount, AccountManager>();
builder.Services.AddTransient<TokenService>();

builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(policy =>
        policy.WithOrigins("https://localhost:7296", "http://localhost:7296")
        .AllowAnyMethod()
        .WithHeaders(HeaderNames.ContentType));

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHub<BonozChatHub>("/hubs/bonoz-chat");

app.Run();
