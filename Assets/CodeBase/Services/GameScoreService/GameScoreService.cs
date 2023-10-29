using System;
using CodeBase.Services.GameLoopService;
using CodeBase.Services.StaticData;

namespace CodeBase.Services.GameScoreService
{
  public class GameScoreService : IGameScoreService
  {
    private int _happyScore = 100;
    public event Action<int> ChangeHappyScore;
    public int HappyScore => _happyScore;

    private readonly IStaticDataService _staticData;
    private IGameTimer _gameTimer;

    public GameScoreService(IStaticDataService staticData, IGameTimer gameTimer)
    {
      _staticData = staticData;
      _gameTimer = gameTimer;
      _gameTimer.OnEndGame += CalculateEndScore;
    }

    ~GameScoreService()
    {
      _gameTimer.OnEndGame -= CalculateEndScore;
    }
    
    public void CalculateEndScore()
    {
      _staticData.GameTempData.ChangeRating((100 - _happyScore) / 2);
      _staticData.GameTempData.ChangeSoul((int)(_happyScore / (_staticData.GameTempData.TimeToEnd * 0.5f)));
    }
    
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

    public void RefreshScore()
    {
      _happyScore = 100;
      ChangeHappyScore?.Invoke(_happyScore);
    }
  }
}