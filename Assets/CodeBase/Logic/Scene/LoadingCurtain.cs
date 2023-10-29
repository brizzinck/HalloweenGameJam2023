using System.Collections;
using UnityEngine;

namespace CodeBase.Logic.Scene
{
  public class LoadingCurtain : MonoBehaviour
  {
    [SerializeField] private float _duraction = 0.03f; 
    public CanvasGroup Curtain;
    
    private void Awake() => 
      DontDestroyOnLoad(this);

    private void Start() => 
      gameObject.SetActive(false);

    public void Show()
    {
      gameObject.SetActive(true);
      Curtain.alpha = 1;
    }
    
    public void Hide() => StartCoroutine(DoFadeIn());
    
    private IEnumerator DoFadeIn()
    {
      while (Curtain.alpha > 0)
      {
        Curtain.alpha -= 0.03f;
        yield return new WaitForSeconds(_duraction);
      }
      gameObject.SetActive(false);
    }
  }
}