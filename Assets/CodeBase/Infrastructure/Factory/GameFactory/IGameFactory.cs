using System.Collections.Generic;
using System.Threading.Tasks;
using CodeBase.Services.PersistentProgress;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory.GameFactory
{
	public interface IGameFactory : IFactory
	{
		List<ISavedProgressReader> ProgressReaders { get; }
		List<ISavedProgress> ProgressWriters { get; }
		Task<GameObject> CreateHud();
	}
}