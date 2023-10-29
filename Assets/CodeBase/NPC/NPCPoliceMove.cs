using UnityEngine;

namespace CodeBase.NPC
{
  public class NPCPoliceMove : NPCMove
  {
    [SerializeField] private NPCScore _npcScore;
    [SerializeField] private float _timeToAddHappyScore;
    [SerializeField] private NPCAgroZone _npcAgroZone;
    private float _currentTimeToHappyScore;
    protected override void Move()
    {
      base.Move();
      MoveToHero();
    }

    protected override void MoveToPoint(Vector3 to)
    {
      base.MoveToPoint(to);
      _npcAgroZone.Refresh();
    }

    private void MoveToHero()
    {
      if (_npcAgroZone.IsAgro)
      {
        Vector3 directionToHero = (_npcAgroZone.Hero.transform.position - transform.position).normalized;
        float distanceToHero = Vector3.Distance(transform.position, _npcAgroZone.Hero.transform.position);
        float stoppingDistance = 2.0f;

        if (distanceToHero > stoppingDistance)
        {
          _currentMovePoint = directionToHero;
          _rigidbody2D.velocity = _currentMovePoint * _currentSpeed;
        }
        else
        {
          _rigidbody2D.velocity = Vector2.zero;
          _npcAnimator.SetIdle();
        }

        _currentTimeToHappyScore += Time.deltaTime;
        if (_currentTimeToHappyScore >= _timeToAddHappyScore)
        {
          _currentTimeToHappyScore = 0;
          _npcScore.GameScoreService.AddHappyScore(1);
        }
      }
      else
        _currentTimeToHappyScore = 0;
    }
  }
}