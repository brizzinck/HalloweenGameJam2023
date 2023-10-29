using CodeBase.Services.StaticData;
using CodeBase.StaticData;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.NPC
{
  public class NPCPoliceKill : MonoBehaviour
  {
    [SerializeField] private NPCAgroZone _npcAgroZone;
    [SerializeField] private NPCScore _npcScore;
    [SerializeField] private int _touch = 3;
    private int _currentTouch;
    private IStaticDataService _staticData;
    private LevelGameStaticData _levelGameStaticData;

    public void Construct(IStaticDataService staticData)
    {
      _staticData = staticData;
      _levelGameStaticData = _staticData.ForGameLevel(SceneManager.GetActiveScene().name);
      _currentTouch = _touch;
    }

    private void Update()
    {
      if (Input.GetMouseButtonDown(0) && _npcAgroZone.IsAgro)
      {
        Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D point = Physics2D.OverlapPoint(clickPosition);
        if (point != null && point.TryGetComponent(out NPCPoliceKill npcPoliceKill))
        {
          npcPoliceKill._currentTouch--;
          _npcScore.GameScoreService.MinusHappyScore(1);
          if (_currentTouch <= 0)
          {
            transform.position = _levelGameStaticData
              .InteractiveSpawnMarker[Random.Range(0, _levelGameStaticData.InteractiveSpawnMarker.Count)].Position;
            _currentTouch = _touch;
          }
        }
      }
    }
  }
}