using Coop.Interview.Api.Core.Games;
using Coop.Interview.Api.Core.Games.Rules;
using Coop.Interview.Api.Http;
using Coop.Interview.Api.Http.Games;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddSingleton<IMoveGenerator, RandomMoveGenerator>();
builder.Services.AddSingleton<IProvideGameRules, RockPaperScissors>();
// builder.Services.AddSingleton<IProvideGameRules, FireWoodWaterEarthMetal>();
builder.Services.AddScoped<GameEngine>();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseExceptionHandler();

app.MapGameEndpoints();

app.Run();
