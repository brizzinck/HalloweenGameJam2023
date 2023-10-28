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
  [CustomEditor(typeof(LevelStaticData))]
  public class LevelStaticDataEditor : UnityEditor.Editor
  {
    private const string InitialPointTag = "HeroInitialPoint";

    public override void OnInspectorGUI()
    {
      base.OnInspectorGUI();

      LevelStaticData levelData = (LevelStaticData)target;

      if (GUILayout.Button("Collect"))
      {
        levelData.InteractiveSpawnMarker = FindObjectsOfType<InteractiveSpawnMarker>()
          .Select(x => 
            new InteractiveSpawnStaticData(x.GetComponent<UniqueId>().Id, x.InteractiveID, x.transform.position))
          .ToList();
        
        levelData.NPCSpawnMarker = FindObjectsOfType<SpawnMarkerNPC>()
          .Select(x => 
            new SpawnMarkerNPCStaticData(x.NpcId, x.GetComponent<UniqueId>().Id, x.transform.position))
          .ToList();

        levelData.HeroSpawnPoint = GameObject.FindWithTag(InitialPointTag).transform.position;
        
        levelData.LevelKey = SceneManager.GetActiveScene().name;
      }
      
      EditorUtility.SetDirty(target);
    }
  }
}