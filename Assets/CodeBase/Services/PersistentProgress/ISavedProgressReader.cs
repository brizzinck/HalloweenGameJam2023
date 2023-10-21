using CodeBase.Data;
using CodeBase.Data.Player;

namespace CodeBase.Services.PersistentProgress
{
  public interface ISavedProgressReader
  {
    void LoadProgress(PlayerProgress progress);
  }
}