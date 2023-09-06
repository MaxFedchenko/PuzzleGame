using Microsoft.EntityFrameworkCore;
using PuzzleGame.EF;

namespace PuzzleGame.Services.Score
{
    public class ScoreService : IScoreService
    {
        private readonly ScoreContext scoreContext;

        public ScoreService(ScoreContext scoreContext)
        {
            this.scoreContext = scoreContext;
        }

        public async Task<List<Score>> GetTopScores(int top)
        {
            return await scoreContext.Scores
                .Include(s => s.User)
                .OrderBy(s => s.TimeSeconds)
                .Take(top)
                .Select(s => new Score
                {
                    Name = s.User.Name,
                    TimeSeconds = s.TimeSeconds,
                    MovesAmount = s.MovesAmount
                })
                .ToListAsync();
        }

        public async Task<List<Score>> GetUserScores(string user_name)
        {
            return await scoreContext.Scores
                .Include(s => s.User)
                .Where(s => s.User.Name == user_name)
                .OrderBy(s => s.TimeSeconds)
                .Select(s => new Score
                {
                    Name = s.User.Name,
                    TimeSeconds = s.TimeSeconds,
                    MovesAmount = s.MovesAmount
                })
                .ToListAsync();
        }

        public async Task AddScore(Score score)
        {
            var user = await scoreContext.Users.FirstOrDefaultAsync(u => u.Name == score.Name);

            if (user == null)
            {
                scoreContext.Users.Add(user = new Entities.User { Name = score.Name });
                await scoreContext.SaveChangesAsync();
            }

            scoreContext.Scores.Add(new Entities.Score
            {
                User = user,
                TimeSeconds = score.TimeSeconds,
                MovesAmount = score.MovesAmount
            });
            await scoreContext.SaveChangesAsync();
        }
    }
}
