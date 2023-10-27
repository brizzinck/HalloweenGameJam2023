using CodeBase.Constants;
using CodeBase.Extensions;
using CodeBase.Services;
using CodeBase.Services.Input;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Hero
{
  public class HeroMove : MonoBehaviour
  { 
    [SerializeField] private float _movementSpeed;

    private IInputService _inputService;
    private Camera _camera;

    private void Awake() => 
      _inputService = AllServices.Container.Single<IInputService>();
    private void Start()
    {
      _camera = Camera.main;
    }

    private void Update()
    {
      Vector3 movementVector = Vector3.zero;

      if (_inputService.Axis.sqrMagnitude > ConstantsValue.Epsilon)
      {
        movementVector = _camera.transform.TransformDirection(_inputService.Axis);
        movementVector.z = 0;
        movementVector.Normalize();
        transform.up = movementVector;
      }
    
      transform.position += _movementSpeed * movementVector * Time.deltaTime;
    }
  
    private static string CurrentLevel() => 
      SceneManager.GetActiveScene().name;
  
  }
}