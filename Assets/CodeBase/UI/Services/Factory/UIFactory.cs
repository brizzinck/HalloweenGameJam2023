using System.Threading.Tasks;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.States;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.StaticData;
using UnityEngine;

namespace CodeBase.UI.Services.Factory
{
  public class UIFactory : IUIFactory
  {
    private const string UIRootPath = "UIRoot";
    private const string MenuUIPath = "MenuUI";
    private readonly IAssetProvider _assets;
    private readonly IStaticDataService _staticData;
    private readonly IPersistentProgressService _progressService;
    private readonly IGameStateMachine _stateMachine;
    private Transform _uiRoot;

    public UIFactory(IGameStateMachine stateMachine, IAssetProvider assets, IStaticDataService staticData, IPersistentProgressService progressService)
    {
      _stateMachine = stateMachine;
      _assets = assets;
      _staticData = staticData;
      _progressService = progressService;
    }
    
    public async Task CreateUIRoot()
    {
      GameObject root = await _assets.Instantiate(UIRootPath);
      _uiRoot = root.transform;
    }
    public async Task CreateMenuUI()
    {
      GameObject menuUi = await _assets.Instantiate(MenuUIPath);
      menuUi.GetComponent<ActorUIMenu>().Construct(_stateMachine, _progressService);
    }
  }
}