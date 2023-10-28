using CodeBase.Services.GameScoreService;
using UnityEngine;

namespace CodeBase.NPC
{
  public class NPCScore : MonoBehaviour
  {
    private IGameScoreService _gameScoreService;

    public IGameScoreService GameScoreService
      => _gameScoreService;

    public void Construct(IGameScoreService gameScoreService) =>
      _gameScoreService = gameScoreService;
  }
}