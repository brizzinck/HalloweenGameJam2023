using System;
using System.Collections.Generic;
using CodeBase.NPC;
using CodeBase.Services.GameScoreService;
using UnityEngine;

namespace CodeBase.Abilities
{
  public class BaseAbility : MonoBehaviour
  {
    [SerializeField] private AbilityID _abilityID;
    [SerializeField] protected float timeToComplete;
    [SerializeField] protected int countSouls;
    [SerializeField] protected int removeHappyBySoul;
    [SerializeField] protected List<NPCWithSoul> npcWithSouls = new List<NPCWithSoul>();
    [SerializeField] protected Transform _pointTorture;
    protected IGameScoreService gameScoreService;
    protected float currentTimeToComplete;
    protected bool processTorture;

    public void Construct(IGameScoreService gameScoreService) =>
      this.gameScoreService = gameScoreService;

    private void Update() =>
      UpdateTorture();

    private void OnTriggerEnter2D(Collider2D other)
    {
      if (other.TryGetComponent(out NPCWithSoul npc))
        EnterTorture(npc);
    }
    protected virtual void UpdateTorture()
    {
      if (processTorture)
      {
        currentTimeToComplete += Time.deltaTime;
        if (currentTimeToComplete >= timeToComplete)
          ExitTorture();
      }
    }

    protected virtual void EnterTorture(NPCWithSoul npcWithSoul)
    {
      if (npcWithSouls.Count >= countSouls)
        return;
      gameScoreService.MinusHappyScore(removeHappyBySoul);
      processTorture = true;
      currentTimeToComplete = 0;
      AddNPCWithSoul(npcWithSoul);
    }

    protected virtual void ExitTorture()
    {
      RemoveNPCWithSoul();
      processTorture = false;
    }

    protected virtual void AddNPCWithSoul(NPCWithSoul soul)
    {
      soul.transform.parent = _pointTorture;
      soul.transform.position = _pointTorture.position;
      if (soul.TryGetComponent(out NPCAgroZone npcAgroZone))
        npcAgroZone.enabled = false;
      if (soul.TryGetComponent(out NPCMove npcMove))
      {
        npcMove.ResetVelocity();
        npcMove.enabled = false;
      }
      if (soul.GetComponentInChildren<NPCAnimator>().TryGetComponent(out NPCAnimator npcAnimator))
        npcAnimator.PlayTorture(_abilityID);
      npcWithSouls.Add(soul);
    }

    protected virtual void RemoveNPCWithSoul()
    {
      foreach (NPCWithSoul soul in npcWithSouls)
      {
        soul.transform.parent = null;
        if (soul.TryGetComponent(out NPCAgroZone npcAgroZone))
          npcAgroZone.enabled = true;
        if (soul.TryGetComponent(out NPCMove npcMove))
          npcMove.enabled = true;
        if (soul.GetComponentInChildren<NPCAnimator>().TryGetComponent(out NPCAnimator npcAnimator))
          npcAnimator.PlayIdeal();
      }

      npcWithSouls = new List<NPCWithSoul>();
    }
  }
}