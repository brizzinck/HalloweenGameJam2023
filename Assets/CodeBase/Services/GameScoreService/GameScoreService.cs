using System;

namespace CodeBase.Services.GameScoreService
{
  public class GameScoreService : IGameScoreService
  {
    private int _happyScore = 100;
    public event Action<int> ChangeHappyScore;
    public int HappyScore => _happyScore;

    public void AddHappyScore(int score)
    {
      _happyScore += score;
      if (_happyScore > 100)
        _happyScore = 100;
      ChangeHappyScore?.Invoke(_happyScore);
    }

    public void MinusHappyScore(int score)
    {
      _happyScore -= score;
      if (_happyScore < 0)
        _happyScore = 0;
      ChangeHappyScore?.Invoke(_happyScore);
    }
  }
}