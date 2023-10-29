using System.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Services.Audio
{
  public interface IAudioPlayer : IService
  {
    void SetAudioSource(AudioSource audioSource);
    Task CreateAudio();
    void PlayCurrentScene();
    void StopAudio();
    void ChangeVolume(float volume);
    float Volume { get; }
  }
}