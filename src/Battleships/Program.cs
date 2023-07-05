using Battleships.Common;
using Battleships.Configuration;
using Battleships.Models.Builders;
using Battleships.UI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddScoped<IConsoleUserInterfaceOutput, ConsoleUserInterfaceOutput>();
builder.Services.AddScoped<IConsoleUserInterfaceInput, ConsoleUserInterfaceInput>();
builder.Services.AddSingleton<IRandomGenerator, RandomGenerator>();
builder.Services.AddScoped<IRandomizedGameBoardBuilder, RandomizedGameBoardBuilder>();
builder.Services.AddScoped<IRandomizedGameBoardDirector, RandomizedGameBoardDirector>();
builder.Services.AddScoped<IConsoleGameManager, ConsoleGameManager>();
builder.Services.Configure<GameSettings>(builder.Configuration.GetSection(nameof(GameSettings)));

using var host = builder.Build();
using IServiceScope gameScope = host.Services.CreateScope();
var gameManager = gameScope.ServiceProvider.GetRequiredService<IConsoleGameManager>();

gameManager.StartGame();