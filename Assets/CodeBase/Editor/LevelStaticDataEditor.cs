using System.Linq;
using CodeBase.InteractiveObjects;
using CodeBase.InteractiveObjects.Logic;
using CodeBase.Logic.Id;
using CodeBase.StaticData;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Editor
{
  [CustomEditor(typeof(LevelStaticData))]
  public class LevelStaticDataEditor : UnityEditor.Editor
  {
    private const string InitialPointTag = "InitialPoint";

    public override void OnInspectorGUI()
    {
      base.OnInspectorGUI();

      LevelStaticData levelData = (LevelStaticData) target;

      if (GUILayout.Button("Collect"))
      {
        levelData.InteractiveSpawnMarker = FindObjectsOfType<InteractiveSpawnMarker>()
          .Select(x => 
            new InteractiveSpawnStaticData(x.GetComponent<UniqueId>().Id, x.InteractiveID, x.transform.position))
          .ToList();

        levelData.LevelKey = SceneManager.GetActiveScene().name;
      }
      
      EditorUtility.SetDirty(target);
    }
  }
}