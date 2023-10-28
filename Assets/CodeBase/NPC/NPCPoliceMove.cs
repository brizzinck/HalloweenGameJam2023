using UnityEngine;

namespace CodeBase.NPC
{
  public class NPCPoliceMove : NPCMove
  {
    [SerializeField] private NPCScore _npcScore;
    [SerializeField] private float _timeToAddHappyScore;
    private float _currentTimeToHappyScore;
    protected override void Move()
    {
      base.Move();
      MoveToHero();
    }
    private void MoveToHero()
    {
      if (_npcAgroZone.IsAgro)
      {
        _currentMovePoint = (_npcAgroZone.Hero.transform.position - transform.position).normalized;
        _rigidbody2D.velocity = _currentMovePoint * _currentSpeed;
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