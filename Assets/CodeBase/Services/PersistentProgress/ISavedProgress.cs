using CodeBase.Data;
using CodeBase.Data.Player;

namespace CodeBase.Services.PersistentProgress
{
  public interface ISavedProgress : ISavedProgressReader
  {
    void UpdateProgress(PlayerProgress progress);
  }
}