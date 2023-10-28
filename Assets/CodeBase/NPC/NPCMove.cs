using System;
using CodeBase.Hero;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeBase.NPC
{
  public class NPCMove : MonoBehaviour
  {
    [SerializeField] private float _defaultMovementSpeed;
    [SerializeField] private float _maxMovementSpeed;
    [SerializeField] private Rigidbody2D _rigidbody2D;

    private GameObject _hero;
    private Vector3 _currentMovePoint;

    private float _currentSpeed;
    private float _directionChangeInterval = 2.0f;
    private float _timeSinceDirectionChange = 0.0f;

    private void Start() => 
      ChangeRandomVelocity();

    private void Update() => 
      Move();

    public void Constructor(GameObject heroMove) => 
      _hero = heroMove;

    private void Move()
    {
      _timeSinceDirectionChange += Time.deltaTime;
      if (_timeSinceDirectionChange > _directionChangeInterval)
      {
        ChangeRandomVelocity();
        _timeSinceDirectionChange = 0.0f;
      }
      SpeedCorrector();
      _rigidbody2D.velocity = _currentMovePoint * _currentSpeed;
    }

    private void ChangeRandomVelocity()
    {
      float angle = Random.Range(0f, 2f * Mathf.PI);
      _currentMovePoint = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0);
    }

    private void SpeedCorrector()
    {
      if (Vector3.Distance(_hero.transform.position, transform.position) < 5 && _currentSpeed != _maxMovementSpeed)
      {
        _currentSpeed += Time.deltaTime * Mathf.PI;
        if (_currentSpeed > _maxMovementSpeed)
          _currentSpeed = _maxMovementSpeed;
      }
      else if (Vector3.Distance(_hero.transform.position, transform.position) >= 5 && _currentSpeed != _defaultMovementSpeed)
      {
        _currentSpeed -= Time.deltaTime * Mathf.PI;
        if (_currentSpeed < _defaultMovementSpeed)
          _currentSpeed = _defaultMovementSpeed;
      }
    }
  }
}