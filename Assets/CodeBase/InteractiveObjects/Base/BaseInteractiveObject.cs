using System;
using CodeBase.InteractiveObjects.Logic;
using CodeBase.Services.GameScoreService;
using CodeBase.Services.Input;
using UnityEngine;

namespace CodeBase.InteractiveObjects.Base
{
  public abstract class BaseInteractiveObject : MonoBehaviour
  {
    [SerializeField] private InteractiveID _interactiveID;
    [SerializeField] private InteractiveDetector _interactiveDetector;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite _destroySprite;
    [SerializeField] private int _costHappyScore;
    private IGameScoreService _gameScoreService;
    private bool _isDestroy;
    public bool IsDestroy => _isDestroy;

    private void Awake() =>
      OnAwake();
    private void OnDestroy() =>
      OnDestroyAction();

    public virtual void Constructor(IInputService inputService, IGameScoreService gameScoreService,
      IDisplayInputService displayInputService)
    {
      _interactiveDetector.Constructor(inputService, displayInputService);
      _gameScoreService = gameScoreService;
    }

    protected virtual void OnAwake() => 
      _interactiveDetector.HeroPress += ChangeToDestroySprite;
    
    protected virtual void OnDestroyAction() => 
      _interactiveDetector.HeroPress -= ChangeToDestroySprite;

    protected virtual void ChangeToDestroySprite()
    {
      if (_isDestroy)
        return;
      _gameScoreService.MinusHappyScore(_costHappyScore);
      _spriteRenderer.sprite = _destroySprite;
      _isDestroy = true;
    }
  }
}