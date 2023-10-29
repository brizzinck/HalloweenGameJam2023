using CodeBase.Abilities;
using CodeBase.InteractiveObjects.Logic;
using CodeBase.NPC;
using CodeBase.Services.StaticData.Interactive;
using CodeBase.Services.StaticData.NPC;
using CodeBase.StaticData;
using CodeBase.StaticData.Windows;
using CodeBase.UI.Services.Windows;
using UnityEngine;

namespace CodeBase.Services.StaticData
{
  public interface IStaticDataService : IService
  {
    void Load();
    LevelStaticData ForLevel(string sceneKey);
    WindowConfig ForWindow(WindowId shop);
    InteractiveStaticData ForInteractiveObjects(InteractiveID id);
    NPCStaticData ForRandomNPC();
    NPCStaticData ForIdNPC(NPCId npcId);
    AbilityStaticData ForAbilities(AbilityID abilityID);
    public GameTempData GameTempData { get; }
    bool ForAvailableAbilities(AbilityID abilityID);
  }
}