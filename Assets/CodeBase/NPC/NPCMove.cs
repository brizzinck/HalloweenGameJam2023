using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeBase.NPC
{
  public class NPCMove : MonoBehaviour
  {
    [SerializeField] private float _defaultMovementSpeed;
    [SerializeField] private float _maxMovementSpeed;
    [SerializeField] private Rigidbody2D _rigidbody2D;

    [SerializeField] private NPCBorderDetector _detector;
    private GameObject _hero;
    private Vector3 _currentMovePoint;
    private float _currentSpeed;
    private float _directionChangeInterval = 1.25f;
    private float _timeSinceDirectionChange = 0.0f;
    public float CurrentSpeed
    {
      get => _currentSpeed;
      set => _currentSpeed = value;
    }

    public Rigidbody2D Rigidbody2D => _rigidbody2D;

    private void Awake() => 
      _detector.EntryObstacle += MoveToPoint;

    private void Start() => 
      ChangeRandomVelocity();

    private void Update() => 
      Move();

    private void OnDestroy() => 
      _detector.EntryObstacle -= MoveToPoint;

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
    
    private void MoveToPoint(Vector3 to)
    {
      _currentMovePoint = (to - transform.position).normalized;
      _timeSinceDirectionChange = 0.0f;
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