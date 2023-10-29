using System;
using UnityEngine;

namespace CodeBase.NPC
{
  public class NPCAgroZone : MonoBehaviour
  {
    public Action<bool> ChangeAgro;
    [SerializeField] private NPCAnimator _npcAnimator;
    [SerializeField] private NPCScore _npcScore;
    [SerializeField] private float _distanceToAgro = 5;
    [SerializeField] private float _timeUntilCalm;
    private float _currentTimeUntilCalm;
    private bool _isAgro;
    private GameObject _hero;
    public bool IsAgro => _isAgro;
    public GameObject Hero => _hero;

    public void Construct(GameObject hero) => 
      _hero = hero;

    private void Update() => 
      CheckAgro();

    private void CheckAgro()
    {
      if (Vector3.Distance(_hero.transform.position, transform.position) < _distanceToAgro)
      {
        if (!_isAgro)
        {
          ChangeAgro?.Invoke(true);
          _isAgro = true;
          _npcScore.GameScoreService.MinusHappyScore(1);
          _currentTimeUntilCalm = 0;
        }
      }
      else if (_isAgro && Vector3.Distance(_hero.transform.position, transform.position) >= _distanceToAgro)
      {
        _currentTimeUntilCalm += Time.deltaTime;
        if (_currentTimeUntilCalm >= _timeUntilCalm)
        {
          _isAgro = false;
          ChangeAgro?.Invoke(false);
        }
      }
      _npcAnimator.SetAgro(_isAgro);
    }

    public void Refresh()
    {
      _isAgro = false;
      _currentTimeUntilCalm = 0;
      ChangeAgro?.Invoke(false);
    }
  }
}