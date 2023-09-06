using PuzzleGame.DTOs;
using PuzzleGame.EF;
using PuzzleGame.Services.Score;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors();
builder.Services.AddTransient<ScoreContext>();
builder.Services.AddTransient<IScoreService, ScoreService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseCors(builder => builder.WithOrigins("http://localhost:8080")
                                  .AllowAnyHeader()
                                  .AllowAnyMethod());
}

app.UseFileServer();

var apiGroup = app.MapGroup("api");
var scoreGroup = apiGroup.MapGroup("score");

scoreGroup.MapGet("top/{top}", async (int top, IScoreService score_service) =>
    await score_service.GetTopScores(top));

scoreGroup.MapGet("{name}", async (string name, IScoreService score_service) =>
    await score_service.GetUserScores(name));

scoreGroup.MapPost("", async (ScoreDTO score, IScoreService score_service) =>
{
    await score_service.AddScore(new Score
    {
        Name = score.Name,
        TimeSeconds = score.TimeSeconds,
        MovesAmount = score.MovesAmount,
    });

    return Results.StatusCode(201);
});

app.Run();
