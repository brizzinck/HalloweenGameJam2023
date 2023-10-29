using System;
using UnityEngine;

namespace CodeBase.Services.StaticData
{
  [Serializable]
  public class GameTempData
  {
    [Header("Game")] public float TimeToEnd;
    [Header("Hero")] public float SpeedHero;

    public GameTempData(float timeToEnd, float speedHero)
    {
      TimeToEnd = timeToEnd;
      SpeedHero = speedHero;
    }
  }
}