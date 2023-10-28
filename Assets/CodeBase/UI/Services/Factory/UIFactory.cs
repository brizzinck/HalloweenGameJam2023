using System.Threading.Tasks;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Factory.GameFactory;
using CodeBase.Infrastructure.States;
using CodeBase.Services.GameScoreService;
using CodeBase.Services.Input;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.StaticData;
using CodeBase.UI.Elements;
using UnityEngine;

namespace CodeBase.UI.Services.Factory
{
  public class UIFactory : IUIFactory
  {
    private const string UIRootPath = "UIRoot";
    private const string MenuUIPath = "MenuUI";
    private const string GameHud = "GameHud";
    private const string AbilityUI = "AbilityUI";
    private readonly IAssetProvider _assets;
    private readonly IStaticDataService _staticData;
    private readonly IPersistentProgressService _progressService;
    private readonly IGameStateMachine _stateMachine;
    private readonly IGameScoreService _gameScoreService;
    private readonly IGameFactory _gameFactory;
    private readonly IInputService _inputService;
    private Transform _uiRoot;

    public UIFactory(IGameStateMachine stateMachine, IAssetProvider assets, IStaticDataService staticData,
      IPersistentProgressService progressService, IGameScoreService gameScoreService, IGameFactory gameFactory,
      IInputService inputService)
    {
      _stateMachine = stateMachine;
      _assets = assets;
      _staticData = staticData;
      _progressService = progressService;
      _gameScoreService = gameScoreService;
      _gameFactory = gameFactory;
      _inputService = inputService;
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

    public async Task CreateGameHud()
    {
      GameObject hud = await _assets.Instantiate(GameHud);
      hud.GetComponent<ActorGameHud>().Construct(_stateMachine, _progressService, _gameScoreService);
    }

    public async Task CreateAbilityUI()
    {
      GameObject hud = await _assets.Instantiate(AbilityUI);
      hud.GetComponent<ActorAbilityUI>()
        .Construct(_stateMachine, _progressService, _gameScoreService, _gameFactory, _inputService);
    }
  }
}