using System.Linq;
using CodeBase.InteractiveObjects.Logic;
using CodeBase.Logic.Id;
using CodeBase.NPC;
using CodeBase.StaticData;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Editor
{
  [CustomEditor(typeof(LevelGameStaticData))]
  public class LevelGameStaticDataEditor : UnityEditor.Editor
  {
    private const string InitialPointTag = "HeroInitialPoint";

    public override void OnInspectorGUI()
    {
      base.OnInspectorGUI();

      LevelGameStaticData levelGameData = (LevelGameStaticData)target;

      if (GUILayout.Button("Collect"))
      {
        levelGameData.InteractiveSpawnMarker = FindObjectsOfType<InteractiveSpawnMarker>()
          .Select(x => 
            new InteractiveSpawnStaticData(x.GetComponent<UniqueId>().Id, x.InteractiveID, x.transform.position))
          .ToList();
        
        levelGameData.NPCSpawnMarker = FindObjectsOfType<SpawnMarkerNPC>()
          .Select(x => 
            new SpawnMarkerNPCStaticData(x.NpcId, x.GetComponent<UniqueId>().Id, x.transform.position))
          .ToList();

        levelGameData.HeroSpawnPoint = GameObject.FindWithTag(InitialPointTag).transform.position;
        
        levelGameData.LevelKey = SceneManager.GetActiveScene().name;
      }
      
      EditorUtility.SetDirty(target);
    }
  }
}