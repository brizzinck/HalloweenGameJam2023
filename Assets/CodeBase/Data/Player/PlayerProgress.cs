using System;
using CodeBase.Data.World;

namespace CodeBase.Data.Player
{
  [Serializable]
  public class PlayerProgress
  {
    public State HeroState;
    public WorldData WorldData;
    public Stats HeroStats;

    public PlayerProgress(string initialLevel)
    {
      WorldData = new WorldData(initialLevel);
      HeroState = new State();
      HeroStats = new Stats();
    }
  }
}