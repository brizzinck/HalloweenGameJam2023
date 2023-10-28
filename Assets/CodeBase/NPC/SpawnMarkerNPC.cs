using CodeBase.Infrastructure.Factory.GameFactory;
using UnityEngine;

namespace CodeBase.NPC
{
  public class SpawnMarkerNPC : MonoBehaviour
  {
    public NPCId NpcId;
    public string UniqueId;
    private IGameFactory _gameFactory;

    public void Construct(IGameFactory gameFactory, GameObject hero, NPCId npcId, string id)
    {
      _gameFactory = gameFactory;
      NpcId = npcId;
      UniqueId = id;
      Spawn(hero);
    }

    private async void Spawn(GameObject hero)
    {
      GameObject npc = await _gameFactory.CreateNPC(transform, hero, NpcId);
    }
  }
}