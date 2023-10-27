using System;
using CodeBase.Infrastructure.Factory.GameFactory;
using UnityEngine;

namespace CodeBase.InteractiveObjects.Logic
{
  public class InteractiveSpawnMarker : MonoBehaviour
  {
    public InteractiveID InteractiveID;
    public string UniqueId;
    private GameFactory _gameFactory;
    public void Construct(GameFactory gameFactory)
    {
      _gameFactory = gameFactory;
      Spawn();
    }
    private async void Spawn()
    {
      GameObject interactive = await _gameFactory.CreateInteractiveObject(InteractiveID, transform);
    }
  }
}