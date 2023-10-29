using System.Threading.Tasks;
using CodeBase.Infrastructure.Factory.GameFactory;
using CodeBase.Services.GameLoopService;
using CodeBase.Services.StaticData;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Services.Audio
{
  public class AudioPlayer : IAudioPlayer
  {
    private float _volume = 0.25f; 
    private readonly IGameFactory _gameFactory;
    private readonly IStaticDataService _staticData;
    private readonly IGameTimer _gameTimer;
    private AudioSource _source;
    public float Volume => _volume;

    public AudioPlayer(IGameFactory gameFactory, IStaticDataService staticData, IGameTimer gameTimer)
    {
      _gameFactory = gameFactory;
      _staticData = staticData;
      _gameTimer = gameTimer;
      _gameTimer.OnEndGame += StopAudio;
    }

    ~AudioPlayer() =>
      _gameTimer.OnEndGame -= StopAudio;

    public void SetAudioSource(AudioSource audioSource) => 
      _source = audioSource;

    public async Task CreateAudio() => 
      _source = await _gameFactory.CreateAudioSource(this);

    public void StopAudio() =>
      _source.Stop();

    public void ChangeVolume(float volume)
    {
      _volume = volume;
      _source.volume = volume / 10;
    }

    public void PlayCurrentScene() => 
      _source.PlayOneShot(_staticData.ForLevel(SceneManager.GetActiveScene().name).SceneClip);
  }
}