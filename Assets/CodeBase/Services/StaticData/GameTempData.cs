using System;
using System.Collections.Generic;
using CodeBase.Abilities;
using UnityEngine;

namespace CodeBase.Services.StaticData
{
  [Serializable]
  public class GameTempData
  {
    public event Action<int> OnChangeSoul;
    public event Action<int> OnChangeRating;
    [Header("Game")] public float TimeToEnd;
    private int _countSoul;
    private int _countRating;
    [Header("Hero")] public float SpeedHero;
    [Header("Ability")] public List<AbilityID> AvailableAbilityIds;
    public int CountSoul => _countSoul;
    public int CountRating => _countRating;

    public void ChangeSoul(int soul)
    {
      _countSoul += soul;
      OnChangeSoul?.Invoke(_countSoul);
    }
    public void ChangeRating(int rating)
    {
      _countRating += rating;
      OnChangeRating?.Invoke(_countRating);
    }
    public GameTempData(float timeToEnd, float speedHero, List<AbilityID> abilityIds)
    {
      TimeToEnd = timeToEnd;
      SpeedHero = speedHero;
      AvailableAbilityIds = new List<AbilityID>(abilityIds);
      _countSoul = 0;
      _countRating = 0;
    }
  }
}