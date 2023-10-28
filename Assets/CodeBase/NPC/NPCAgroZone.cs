using System;
using UnityEngine;

namespace CodeBase.NPC
{
  public class NPCAgroZone : MonoBehaviour
  {
    public Action<bool> ChangeAgro;
    [SerializeField] private float _distanceToAgro = 5;
    [SerializeField] private float _timeUntilCalm;
    private float _currentTimeUntilCalm;
    private bool _isAgro;

    public bool IsAgro => _isAgro;

    private GameObject _hero;

    public void Construct(GameObject hero)
    {
      _hero = hero;
    }
    
    private void Update()
    {
      CheckAgro();
    }
    private void CheckAgro()
    {
      if (Vector3.Distance(_hero.transform.position, transform.position) < 5)
      {
        if (!_isAgro)
        {
          ChangeAgro?.Invoke(true);
          _isAgro = true;
          _currentTimeUntilCalm = 0;
        }
      }
      else if (_isAgro && Vector3.Distance(_hero.transform.position, transform.position) >= 5)
      {
        _currentTimeUntilCalm += Time.deltaTime;
        if (_currentTimeUntilCalm >= _timeUntilCalm)
        {
          _isAgro = false;
          ChangeAgro?.Invoke(false);
        }
      }
    }
  }
}