using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeBase.NPC
{
  public class NPCMove : MonoBehaviour
  {
    [SerializeField] protected float _defaultMovementSpeed;
    [SerializeField] protected float _maxMovementSpeed;
    [SerializeField] protected Rigidbody2D _rigidbody2D;
    [SerializeField] protected NPCBorderDetector _detector;
    [SerializeField] protected NPCAgroZone _npcAgroZone;
    protected Vector3 _currentMovePoint;
    protected float _currentSpeed;
    protected float _directionChangeInterval = 1.25f;
    protected float _timeSinceDirectionChange = 0.0f;
    protected bool _isStay;
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

    protected virtual void Move()
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
    
    protected void ChangeRandomVelocity()
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
    protected void MoveToPoint(Vector3 to)
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

    public void ResetVelocity()
    {
      _rigidbody2D.velocity = Vector2.zero;
      _currentSpeed = 0;
    }
  }
}