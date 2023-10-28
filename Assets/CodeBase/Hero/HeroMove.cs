using System;
using CodeBase.Constants;
using CodeBase.Extensions;
using CodeBase.Services;
using CodeBase.Services.Input;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Hero
{
  public class HeroMove : MonoBehaviour
  { 
    [SerializeField] private float _movementSpeed;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    private IInputService _inputService;
    private Camera _camera;
    private Vector3 _movementVector;
    private void Awake() => 
      _inputService = AllServices.Container.Single<IInputService>();
    private void Start() => 
      _camera = Camera.main;
    private void Update()
    {
      _movementVector = Vector3.zero;
      if (_inputService.Axis.sqrMagnitude > ConstantsValue.Epsilon)
        _movementVector = _camera.transform.TransformDirection(_inputService.Axis);
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
        transform.localScale = transform.localScale.WithToX(1);
      if (movementVector.x < 0)
        transform.localScale = transform.localScale.WithToX(-1);
    }
  }
}