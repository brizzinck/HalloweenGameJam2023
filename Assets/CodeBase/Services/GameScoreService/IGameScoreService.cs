using System.Collections.Generic;
using CodeBase.NPC;

namespace CodeBase.Services.GameScoreService
{
  public interface IGameScoreService : IService
  {
    public List<NPCScore> NpcScores { get; set; }
    public void AddHappyScore(int score);
    public void MinusHappyScore(int score);
    void AddNpc(NPCScore npcScore);
  }
}