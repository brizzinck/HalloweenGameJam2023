using System.Collections.Generic;
using System.Threading.Tasks;
using CodeBase.Hero;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.InteractiveObjects.Base;
using CodeBase.InteractiveObjects.Logic;
using CodeBase.NPC;
using CodeBase.Services.Input;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.StaticData;
using CodeBase.Services.StaticData.Interactive;
using CodeBase.Services.StaticData.NPC;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure.Factory.GameFactory
{
	public class GameFactory : IGameFactory
	{
		public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
		public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();
		private readonly IAssetProvider _assets;
		private readonly IStaticDataService _staticData;
		private readonly IInputService _inputService;
		
		public GameFactory(IAssetProvider assets, IStaticDataService staticData, IInputService inputService)
		{
			_assets = assets;
			_staticData = staticData;
			_inputService = inputService;
		}
		public void Cleanup()
		{
			ProgressReaders.Clear();
			ProgressWriters.Clear();
		}
		public Task WarmpUp()
		{
			return null;
		}
		public async Task<GameObject> CreateHud()
		{
			GameObject hud = await InstantiateRegisteredAsync(AssetAddress.Hud);
			return hud;
		}
		public async Task<GameObject> CreateHero()
		{
			GameObject hero = await InstantiateRegisteredAsync(AssetAddress.Hero);
			hero.transform.position = _staticData.ForLevel(SceneManager.GetActiveScene().name).HeroSpawnPoint;
			return hero;
		}

		public async Task CreateInteractiveSpawner(string spawnerId, Vector3 at, InteractiveID interactiveId)
		{
			GameObject prefab = await _assets.Load<GameObject>(AssetAddress.InteractiveSpawner);
			InteractiveSpawnMarker spawner = InstantiateRegistered(prefab, at)
				.GetComponent<InteractiveSpawnMarker>();
			spawner.Construct(this);
			spawner.InteractiveID = interactiveId;
			spawner.UniqueId = spawnerId;		
		}
		public async Task CreateNPCSpawner(string spawnerId, Vector3 at, GameObject hero)
		{
			GameObject prefab = await _assets.Load<GameObject>(AssetAddress.NPCSpawner);
			SpawnMarkerNPC spawner = InstantiateRegistered(prefab, at)
				.GetComponent<SpawnMarkerNPC>();
			spawner.Construct(this, hero);
			spawner.UniqueId = spawnerId;	
		}

		public async Task<GameObject> CreateInteractiveObject(InteractiveID id, Transform parent)
		{
			InteractiveStaticData interactiveStaticData = _staticData.ForInteractiveObjects(id);
			GameObject prefab = await _assets.Load<GameObject>(interactiveStaticData.PrefabReference);
			GameObject interactive = Object.Instantiate(prefab, parent.position, Quaternion.identity, parent);
			interactive.GetComponent<BaseInteractiveObject>().Constructor(_inputService);
			return interactive;
		}

		public async Task<GameObject> CreateRandomNPC(Transform parent, GameObject hero)
		{
			NPCStaticData npcStaticData = _staticData.ForRandomNPC();
			GameObject prefab = await _assets.Load<GameObject>(npcStaticData.PrefabReference);
			GameObject npc = Object.Instantiate(prefab, parent.position, Quaternion.identity, parent);
			npc.GetComponent<NPCAgroZone>().Construct(hero);
			return npc;
		}

		private GameObject InstantiateRegistered(GameObject prefab, Vector3 at)
		{
			GameObject gameObject = Object.Instantiate(prefab, at, Quaternion.identity);
			RegisterProgressWatchers(gameObject);
			return gameObject;
		}

		private GameObject InstantiateRegistered(GameObject prefab)
		{
			GameObject gameObject = Object.Instantiate(prefab);
			RegisterProgressWatchers(gameObject);

			return gameObject;
		}

		private async Task<GameObject> InstantiateRegisteredAsync(string prefabPath)
		{
			GameObject gameObject = await _assets.Instantiate(address: prefabPath);
			RegisterProgressWatchers(gameObject);

			return gameObject;
		}

		private async Task<GameObject> InstantiateRegisteredAsync(string prefabPath, Vector3 at)
		{
			GameObject gameObject = await _assets.Instantiate(address: prefabPath, at: at);
			RegisterProgressWatchers(gameObject);
			return gameObject;
		}

		private void RegisterProgressWatchers(GameObject gameObject)
		{
			foreach (ISavedProgressReader progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
				Register(progressReader);
		}

		private void Register(ISavedProgressReader progressReader)
		{
			if (progressReader is ISavedProgress progressWriter)
				ProgressWriters.Add(progressWriter);
			ProgressReaders.Add(progressReader);
		}
	}
}