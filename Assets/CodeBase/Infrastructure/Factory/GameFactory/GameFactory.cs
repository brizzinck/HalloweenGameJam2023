using System.Collections.Generic;
using System.Threading.Tasks;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Services.PersistentProgress;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory.GameFactory
{
	public class GameFactory : IGameFactory
	{
		public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
		public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();
		private readonly IAssetProvider _assets;
		public GameFactory(IAssetProvider assets)
		{
			_assets = assets;
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
			return hero;
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