using UnityEngine;

namespace CodeBase.NPC
{
  public class NPCPoliceKill : MonoBehaviour
  {
    [SerializeField] private int _touch = 3;
    private void Update()
    {
      if (Input.GetMouseButtonDown(0))
      {
        Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D point = Physics2D.OverlapPoint(clickPosition);
        if (point.gameObject != null && point.TryGetComponent(out NPCPoliceKill npcPoliceKill))
        {
          npcPoliceKill._touch--;
          if (_touch <= 0)
            Destroy(npcPoliceKill.gameObject);
        }
      }
    }
  }
}