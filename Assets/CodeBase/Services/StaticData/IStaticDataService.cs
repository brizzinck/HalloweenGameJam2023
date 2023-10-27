using CodeBase.InteractiveObjects.Logic;
using CodeBase.Services.StaticData.Interactive;
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
  }
}