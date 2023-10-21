using CodeBase.Data;
using CodeBase.Data.Player;

namespace CodeBase.Services.PersistentProgress
{
  public class PersistentProgressService : IPersistentProgressService
  {
    public PlayerProgress Progress { get; set; }
  }
}