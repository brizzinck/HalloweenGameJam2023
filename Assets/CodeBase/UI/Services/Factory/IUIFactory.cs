using System.Threading.Tasks;
using CodeBase.Services;

namespace CodeBase.UI.Services.Factory
{
  public interface IUIFactory: IService
  {
    Task CreateUIRoot();
    Task CreateMenuUI();
    Task CreateGameHud();
    Task CreateAbilityUI();
    Task CreateEndUI();
  }
}