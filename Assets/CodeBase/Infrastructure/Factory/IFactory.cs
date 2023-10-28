using System.Collections.Generic;
using System.Threading.Tasks;
using CodeBase.NPC;
using CodeBase.Services;
using CodeBase.Services.PersistentProgress;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
  public interface IFactory : IService
  {
    void Cleanup();
    Task WarmpUp();
  }
}