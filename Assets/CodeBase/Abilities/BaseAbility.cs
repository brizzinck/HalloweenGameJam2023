using CodeBase.Services.GameScoreService;
using UnityEngine;

namespace CodeBase.Abilities
{
  public class BaseAbility : MonoBehaviour
  {
    [SerializeField] private AbilityID _abilityID;
    protected IGameScoreService gameScoreService;
    public void Construct(IGameScoreService gameScoreService) => 
      this.gameScoreService = gameScoreService;
  }
}