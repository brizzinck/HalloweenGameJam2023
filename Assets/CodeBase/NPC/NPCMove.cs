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
    [SerializeField] private NPCAgroZone _npcAgroZone;
    private Vector3 _currentMovePoint;
    private float _currentSpeed;
    private float _directionChangeInterval = 1.25f;
    private float _timeSinceDirectionChange = 0.0f;
    private bool _isStay;
    public float CurrentSpeed
    {
      get => _currentSpeed;
      set => _currentSpeed = value;
    }
    public Rigidbody2D Rigidbody2D => _rigidbody2D;
    private void Awake()
    {
      _detector.EntryObstacle += MoveToPoint;
    }

    private void Start() => 
      ChangeRandomVelocity();

    private void Update() => 
      Move();

    private void OnDestroy() => 
      _detector.EntryObstacle -= MoveToPoint;

    private void Move()
    {
      if (!_isStay)
      {
        _timeSinceDirectionChange += Time.deltaTime;
        if (_timeSinceDirectionChange > _directionChangeInterval)
        {
          ChangeRandomVelocity();
          _timeSinceDirectionChange = 0.0f;
          if (!_npcAgroZone.IsAgro)
            StartCoroutine(CheckStay());
        }
        SpeedCorrector(_npcAgroZone.IsAgro);
        _rigidbody2D.velocity = _currentMovePoint * _currentSpeed;
      }
      else
        _rigidbody2D.velocity = Vector2.zero;
    }
    
    private void ChangeRandomVelocity()
    {
      _directionChangeInterval = Random.Range(1.25f, 2f);
      float angle = Random.Range(0f, 2f * Mathf.PI);
      _currentMovePoint = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0);
    }

    private IEnumerator CheckStay()
    {
      _isStay = true;
      yield return new WaitForSeconds(0.55f);
      _isStay = false;
    }
    private void MoveToPoint(Vector3 to)
    {
      _currentMovePoint = (to - transform.position).normalized;
      _timeSinceDirectionChange = 0.0f;
    }
    
    private void SpeedCorrector(bool agro)
    {
      if (agro)
      {
        _currentSpeed += Time.deltaTime * Mathf.PI;
        if (_currentSpeed > _maxMovementSpeed)
          _currentSpeed = _maxMovementSpeed;
      }
      else
      {
        _currentSpeed -= Time.deltaTime * Mathf.PI;
        if (_currentSpeed < _defaultMovementSpeed)
          _currentSpeed = _defaultMovementSpeed;
      }
    }
  }
}