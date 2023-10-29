using System.Collections.Generic;
using CodeBase.NPC;
using CodeBase.Services.GameScoreService;
using CodeBase.Services.StaticData;
using CodeBase.StaticData;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace CodeBase.Abilities
{
  public class BaseAbility : MonoBehaviour
  {
    [SerializeField] private AbilityID _abilityID;
    [SerializeField] protected float timeToComplete;
    [SerializeField] protected int countToDestroy = 1;
    [SerializeField] protected int removeHappyBySoul;
    [SerializeField] protected List<NPCWithSoul> npcWithSouls = new List<NPCWithSoul>();
    [SerializeField] protected Transform _pointTorture;
    protected IGameScoreService gameScoreService;
    protected IStaticDataService staticData;
    protected float currentTimeToComplete;
    protected bool processTorture;
    protected LevelGameStaticData LevelGameStaticData;

    public void Construct(IGameScoreService gameScoreService, IStaticDataService staticData)
    {
      this.gameScoreService = gameScoreService;
      this.staticData = staticData; 
      LevelGameStaticData = staticData.ForGameLevel(SceneManager.GetActiveScene().name);
    }

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
      if (processTorture || npcWithSoul.IsTorture) return;
      npcWithSoul.IsTorture = true;
      gameScoreService.MinusHappyScore(removeHappyBySoul);
      processTorture = true;
      currentTimeToComplete = 0;
      AddNPCWithSoul(npcWithSoul);
    }

    protected virtual void ExitTorture()
    {
      RemoveNPCWithSoul();
      processTorture = false;
      CheckToDestroy();
    }

    protected virtual void AddNPCWithSoul(NPCWithSoul soul)
    {
      soul.transform.parent = _pointTorture;
      soul.transform.position = _pointTorture.position;
      soul.IsTorture = false;
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
          npcAnimator.SetIdle();
        soul.transform.position = LevelGameStaticData.NPCSpawnMarker[Random.Range(0, LevelGameStaticData.NPCSpawnMarker.Count)].Position;
      }
      npcWithSouls = new List<NPCWithSoul>();
    }

    protected void CheckToDestroy()
    {
      countToDestroy--;
      if (countToDestroy <= 0)
        Destroy(gameObject);
    }
  }
}