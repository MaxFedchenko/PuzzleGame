using PuzzleGame.DTOs;
using PuzzleGame.EF;
using PuzzleGame.Services.Score;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors();
builder.Services.AddTransient<ScoreContext>();
builder.Services.AddTransient<IScoreService, ScoreService>();

var app = builder.Build();

app.UseCors(builder => builder.WithOrigins("http://localhost:8080")
                          .AllowAnyHeader()
                          .AllowAnyMethod());

app.UseStaticFiles();

app.MapGet("api/score/top/{top}", async (int top) =>
{
    var score_service = app.Services.GetRequiredService<IScoreService>();
    return await score_service!.GetTopScores(top);
});

app.MapGet("api/score/{name}", async (string name) =>
{
    var score_service = app.Services.GetRequiredService<IScoreService>();
    return await score_service!.GetUserScores(name);
});

app.MapPost("api/score", async (ScoreDTO score) =>
{
    var score_service = app.Services.GetRequiredService<IScoreService>();

    await score_service!.AddScore(new Score
    {
        Name = score.Name,
        TimeSeconds = score.TimeSeconds,
        MovesAmount = score.MovesAmount,
    });
});

app.MapGet("/", context =>
{
    context.Response.Redirect("/index.html");
    return Task.FromResult(0);
});

app.Run();
