using CodeBase.Data;
using CodeBase.Data.Player;

namespace CodeBase.Services.SaveLoad
{
  public interface ISaveLoadService : IService
  {
    void SaveProgress();
    PlayerProgress LoadProgress();
  }
}