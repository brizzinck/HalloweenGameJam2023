using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeBase.NPC
{
  public class NPCMoveInterrupter : MonoBehaviour
  {
    [SerializeField] private NPCMove _npcMove;
    private float _cooldownWalk;
    private float _currentCooldownWalk;

    private void Start() => 
      UpdateCurrentCooldown();

    private void Update()
    {
      CheckCooldownWalk();
    }

    private void UpdateCooldownWalk() =>
      _cooldownWalk = Random.Range(1.2f, 2.2f);

    private void CheckCooldownWalk()
    {
      if (!_npcMove.enabled)
      {
        _currentCooldownWalk += Time.deltaTime;
        if (_currentCooldownWalk >= _cooldownWalk)
        {
          _npcMove.enabled = true;
          UpdateCurrentCooldown();
        }
      }
      else
      {
        _currentCooldownWalk -= Time.deltaTime;
        if (_currentCooldownWalk <= 0)
          StartCoroutine(StopMove());
      }
    }

    private IEnumerator StopMove()
    {
      _npcMove.CurrentSpeed = 0;
      _npcMove.enabled = false;
      _currentCooldownWalk = 0;
      UpdateCooldownWalk();
      float velocityX = _npcMove.Rigidbody2D.velocity.x;
      while (_npcMove.Rigidbody2D.velocity.x > velocityX * 0.15f)
      {
        _npcMove.Rigidbody2D.velocity *= new Vector2(0.65f, 0.65f);
        yield return null;
      }
      _npcMove.Rigidbody2D.velocity = Vector2.zero;
    }

    private void UpdateCurrentCooldown() => 
      _currentCooldownWalk = Random.Range(5, 14);

    private void ResetCooldownWalk()
    {
      _npcMove.enabled = true;
      UpdateCurrentCooldown();
      UpdateCooldownWalk();
    }
  }
}