using System.Collections.Generic;
using System.Linq;
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
  public class StaticDataService : IStaticDataService
  {
    private const string LevelsDataPath = "StaticData/Levels";
    private const string StaticDataWindowPath = "StaticData/UI/WindowStaticData";
    private const string InteractiveObjects = "StaticData/InteractiveObjects";
    private const string NPCs = "StaticData/NPC";

    private Dictionary<InteractiveID, InteractiveStaticData> _interactiveObjects;
    private List<NPCStaticData> _npc = new List<NPCStaticData>();
    private Dictionary<string, LevelStaticData> _levels;
    private Dictionary<WindowId, WindowConfig> _windowConfigs;


    public void Load()
    {
      _levels = Resources
        .LoadAll<LevelStaticData>(LevelsDataPath)
        .ToDictionary(x => x.LevelKey, x => x);
      
      _interactiveObjects = Resources
        .LoadAll<InteractiveStaticData>(InteractiveObjects)
        .ToDictionary(x => x.InteractiveID, x => x);

      _npc = Resources
        .LoadAll<NPCStaticData>(NPCs)
        .ToList();

      _windowConfigs = Resources
        .Load<WindowStaticData>(StaticDataWindowPath)
        .Configs
        .ToDictionary(x => x.WindowId, x => x);
    }
    
    public LevelStaticData ForLevel(string sceneKey) =>
      _levels.TryGetValue(sceneKey, out LevelStaticData staticData)
        ? staticData
        : null;
    
    public InteractiveStaticData ForInteractiveObjects(InteractiveID id) =>
      _interactiveObjects.TryGetValue(id, out InteractiveStaticData staticData)
        ? staticData
        : null;

    public NPCStaticData ForRandomNPC() =>
      _npc[Random.Range(0, _npc.Count)];

    public NPCStaticData ForIdNPC(NPCId npcId)
    {
      foreach (NPCStaticData npc in _npc)
        if (npc.NpcId == npcId)
          return npc;
      return null;
    }

    public WindowConfig ForWindow(WindowId windowId) =>
      _windowConfigs.TryGetValue(windowId, out WindowConfig windowConfig)
        ? windowConfig
        : null;
  }
}