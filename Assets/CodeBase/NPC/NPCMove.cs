using CodeBase.Hero;
using UnityEngine;
using Random = UnityEngine.Random;

public class NPCMove : MonoBehaviour
{
  [SerializeField] private float _defaultMovementSpeed;
  [SerializeField] private float _maxMovementSpeed;
  [SerializeField] private Rigidbody2D _rigidbody2D;
  private HeroMove _heroMove;
  private Vector3 currentDirection;

  private float directionChangeInterval = 2.0f;

  private float timeSinceDirectionChange = 0.0f;

  public void Construct(HeroMove heroMove)
  {
    _heroMove = heroMove;
  }

  private void Update()
  {
    timeSinceDirectionChange += Time.deltaTime;
    if (timeSinceDirectionChange > directionChangeInterval)
    {
      ChangeRandomVelocity();
      timeSinceDirectionChange = 0.0f;
    }
    _rigidbody2D.velocity = currentDirection * _defaultMovementSpeed;
  }

  private void ChangeRandomVelocity()
  {
    float angle = Random.Range(0f, 2f * Mathf.PI);
    currentDirection = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0);
  }
}
