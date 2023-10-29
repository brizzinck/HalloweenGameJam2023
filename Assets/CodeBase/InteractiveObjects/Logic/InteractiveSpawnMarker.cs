using CodeBase.Infrastructure.Factory.GameFactory;
using UnityEngine;

namespace CodeBase.InteractiveObjects.Logic
{
  public class InteractiveSpawnMarker : MonoBehaviour
  {
    public InteractiveID InteractiveID;
    public string UniqueId;
    private GameFactory _gameFactory;
    public void Construct(GameFactory gameFactory, InteractiveID interactiveID)
    {
      _gameFactory = gameFactory;
      InteractiveID = interactiveID;
      Spawn();
    }
    private async void Spawn()
    {
      GameObject interactive = await _gameFactory.CreateInteractiveObject(InteractiveID, transform);
    }
  }
}