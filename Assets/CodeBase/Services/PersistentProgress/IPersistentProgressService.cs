using CodeBase.Data;
using CodeBase.Data.Player;

namespace CodeBase.Services.PersistentProgress
{
  public interface IPersistentProgressService : IService
  {
    PlayerProgress Progress { get; set; }
  }
}