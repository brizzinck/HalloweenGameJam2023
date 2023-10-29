using UnityEngine;

namespace CodeBase.NPC
{
  public class NPCPoliceKill : MonoBehaviour
  {
    [SerializeField] private NPCAgroZone _npcAgroZone;
    [SerializeField] private NPCScore _npcScore;
    [SerializeField] private int _touch = 3;

    private void Update()
    {
      if (Input.GetMouseButtonDown(0) && _npcAgroZone.IsAgro)
      {
        Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D point = Physics2D.OverlapPoint(clickPosition);
        if (point != null && point.TryGetComponent(out NPCPoliceKill npcPoliceKill))
        {
          npcPoliceKill._touch--;
          _npcScore.GameScoreService.MinusHappyScore(1);
          if (_touch <= 0)
            Destroy(npcPoliceKill.gameObject);
        }
      }
    }
  }
}