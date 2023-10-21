using System.Collections.Generic;
using System.Threading.Tasks;
using CodeBase.Services;
using CodeBase.Services.PersistentProgress;

namespace CodeBase.Infrastructure.Factory
{
  public interface IFactory : IService
  {
    void Cleanup();
    Task WarmpUp();
  }
}