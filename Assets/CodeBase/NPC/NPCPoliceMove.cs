namespace CodeBase.NPC
{
  public class NPCPoliceMove : NPCMove
  { 
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
      }
    }
  }
}