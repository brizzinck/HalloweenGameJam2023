using CodeBase.Services.StaticData;
using CodeBase.StaticData;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.NPC
{
  public class NPCPoliceKill : MonoBehaviour
  {
    [SerializeField] private NPCAgroZone _npcAgroZone;
    [SerializeField] private NPCScore _npcScore;
    [SerializeField] private NPCAnimator _npcAnimator;
    [SerializeField] private int _touch = 3;
    [SerializeField] private NPCMove _npcMove;
    private const string LayerMask = "NPCPolice";
    private int _currentTouch;
    private IStaticDataService _staticData;
    private LevelGameStaticData _levelGameStaticData;
    private Vector3 _defaultScale;
    private bool _isTouch = true;

    public void Construct(IStaticDataService staticData)
    {
      _staticData = staticData;
      _levelGameStaticData = _staticData.ForGameLevel(SceneManager.GetActiveScene().name);
      _currentTouch = _touch;
      _defaultScale = transform.localScale;
    }
    private void Update()
    {
      if (_isTouch && Input.GetMouseButtonDown(0) && _npcAgroZone.IsAgro)
      {
        int layerMask = 1 << UnityEngine.LayerMask.NameToLayer(LayerMask);
        Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(clickPosition, Vector2.zero, 0, layerMask);
        if (hit.collider != null && hit.collider.TryGetComponent(out NPCPoliceKill npcPoliceKill))
        {
          npcPoliceKill._currentTouch--;
          _npcScore.GameScoreService.MinusHappyScore(1);
          _isTouch = false;
          _npcMove.ResetVelocity();
          _npcMove.enabled = false;
          _npcAnimator.SetIdle();
          Sequence seq = DOTween.Sequence();
          seq.Append(transform.DOScale(_defaultScale * 1.1f, 0.2f));
          seq.Append(transform.DOScale(_defaultScale, 0.2f));
          seq.OnComplete(() =>
          {
            if (_currentTouch <= 0)
            {
              transform.position = _levelGameStaticData
                .NPCSpawnMarker[Random.Range(0, _levelGameStaticData.NPCSpawnMarker.Count)].Position;
              _currentTouch = _touch;
              _npcAgroZone.Refresh();
            }
            _npcMove.enabled = true;
            _isTouch = true;
          });
        }
      }
    }
    
  }
}