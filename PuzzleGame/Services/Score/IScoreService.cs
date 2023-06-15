namespace PuzzleGame.Services.Score
{
    public interface IScoreService
    {
        Task AddScore(Score score);
        Task<List<Score>> GetUserScores(string user_name);
        Task<List<Score>> GetTopScores(int top);
    }
}