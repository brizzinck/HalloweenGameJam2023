using System.Collections;
using CodeBase.Extensions;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeBase.NPC
{
  public class NPCMove : MonoBehaviour
  {
    [SerializeField] protected NPCAnimator _npcAnimator;
    [SerializeField] protected float _defaultMovementSpeed;
    [SerializeField] protected float _maxMovementSpeed;
    [SerializeField] protected Rigidbody2D _rigidbody2D;
    [SerializeField] protected NPCBorderDetector _detector;
    [SerializeField] protected NPCAgroZone _npcAgroZone;
    [SerializeField] protected Transform _spriteFlip;
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
        _npcAnimator.SetWalk(true);
      }
      else
      {
        _rigidbody2D.velocity = Vector2.zero;
        _npcAnimator.SetWalk(false);
      }
    }
    
    protected void ChangeRandomVelocity()
    {
      _directionChangeInterval = Random.Range(1.25f, 2f);
      float angle = Random.Range(0f, 2f * Mathf.PI);
      _currentMovePoint = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0);
      Flip(_currentMovePoint);
    }
    private IEnumerator CheckStay()
    {
      _isStay = true;
      yield return new WaitForSeconds(0.55f);
      _isStay = false;
    }
    protected virtual void MoveToPoint(Vector3 to)
    {
      _currentMovePoint = (to - transform.position).normalized;
      _timeSinceDirectionChange = 0.0f;
      Flip(_currentMovePoint);
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
    private void Flip(Vector3 targetPoint)
    {
      Vector3 toTarget = (targetPoint - transform.position).normalized;
      Vector3 forward = transform.right;

      // Кросс-продукт
      float crossProduct = forward.x * toTarget.y - forward.y * toTarget.x;

      if (crossProduct > 0)
        _spriteFlip.localScale = _spriteFlip.localScale.WithToX(-Mathf.Abs(_spriteFlip.localScale.x));
      else
        _spriteFlip.localScale = _spriteFlip.localScale.WithToX(Mathf.Abs(_spriteFlip.localScale.x));
    }


    public void ResetVelocity()
    {
      _rigidbody2D.velocity = Vector2.zero;
      _currentSpeed = 0;
    }
  }
}