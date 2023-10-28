using System.Collections.Generic;
using System.Threading.Tasks;
using CodeBase.Abilities;
using CodeBase.InteractiveObjects.Logic;
using CodeBase.NPC;
using CodeBase.Services.GameScoreService;
using CodeBase.Services.PersistentProgress;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory.GameFactory
{
	public interface IGameFactory : IFactory
	{
		List<ISavedProgressReader> ProgressReaders { get; }
		List<ISavedProgress> ProgressWriters { get; }
		Task<GameObject> CreateHud();
		Task<GameObject> CreateHero();
		Task CreateInteractiveSpawner(string spawnerId, Vector3 at, InteractiveID interactiveId);
		Task<GameObject> CreateNPC(Transform parent, GameObject hero, NPCId npcId = NPCId.Random);
		Task CreateNPCSpawner(NPCId spawnerDataNpcId, string spawnerId, Vector3 at, GameObject hero);
		Task CreateAbility(AbilityID abilityID, IGameScoreService gameScoreService);
	}
}