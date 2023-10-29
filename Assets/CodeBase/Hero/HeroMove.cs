using System;
using CodeBase.Extensions;
using CodeBase.Services.Input;
using CodeBase.Services.StaticData;
using UnityEngine;

namespace CodeBase.Hero
{
  public class HeroMove : MonoBehaviour
  {
    [SerializeField] private float _movementSpeed;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    private IInputService _inputService;
    private IStaticDataService _staticData;
    private Camera _camera;
    private Vector3 _movementVector;

    public void Construct(IStaticDataService staticData, IInputService inputService)
    {
      _inputService = inputService;
      _staticData = staticData;
      _movementSpeed = _staticData.GameTempData.SpeedHero;
    }

    public void StopMove()
    {
      _rigidbody2D.velocity = Vector2.zero;
      enabled = false;
    }
    
    private void Start() =>
      _camera = Camera.main;

    private void Update() => 
      SetDirection();

    private void SetDirection()
    {
      if (_inputService != null)
      {
        _movementVector =
          _camera.transform.TransformDirection(new Vector3(_inputService.Axis.x, _inputService.Axis.y, 0));
      }
    }

    private void FixedUpdate()
    {
      Flip(_movementVector);
      Moving(_movementVector);
    }

    private void Moving(Vector3 movementVector)
    {
      movementVector.Normalize();
      _rigidbody2D.velocity = movementVector * _movementSpeed;
      transform.position = transform.position.WithToZ(-1);
    }

    private void Flip(Vector3 movementVector)
    {
      if (movementVector.x > 0)
        transform.localScale = transform.localScale.WithToX(Mathf.Abs(transform.localScale.z));
      if (movementVector.x < 0)
        transform.localScale = transform.localScale.WithToX(-transform.localScale.z);
    }
  }
}