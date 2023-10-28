using System.Collections.Generic;
using CodeBase.NPC;
using UnityEngine;

namespace CodeBase.Services.GameScoreService
{
  public class GameScoreService : IGameScoreService
  {
    public List<NPCScore> NpcScores { get; set; } = new List<NPCScore>();
    private int _happyScore = 100;
    public void AddNpc(NPCScore npcScore)
    {
      NpcScores.Add(npcScore);
    }

    public void AddHappyScore(int score)
    {
      _happyScore += score;
      if (_happyScore > 100)
        _happyScore = 100;
      Debug.Log(_happyScore);
    }

    public void MinusHappyScore(int score)
    {
      _happyScore -= score;
      if (_happyScore < 0)
        _happyScore = 0;
      Debug.Log(_happyScore);
    }
  }
}