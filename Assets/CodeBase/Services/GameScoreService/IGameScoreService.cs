using System;

namespace CodeBase.Services.GameScoreService
{
  public interface IGameScoreService : IService
  {
    public event Action<int> ChangeHappyScore;
    int HappyScore { get; }
    public void AddHappyScore(int score);
    public void MinusHappyScore(int score);
  }
}