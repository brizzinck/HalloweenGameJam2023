using CodeBase.Infrastructure.Factory.GameFactory;
using UnityEngine;

namespace CodeBase.NPC
{
  public class SpawnMarkerNPC : MonoBehaviour
  {
    public string UniqueId;
    private IGameFactory _gameFactory;
    public void Construct(IGameFactory gameFactory, GameObject hero)
    {
      _gameFactory = gameFactory;
      Spawn(hero);
    }
    private async void Spawn(GameObject hero)
    {
      GameObject npc = await _gameFactory.CreateRandomNPC(transform, hero);
    }
  }
}