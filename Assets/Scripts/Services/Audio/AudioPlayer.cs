using UnityEngine;

public class AudioPlayer : MonoBehaviour, IAudioPlayer
{
    [SerializeField] private AudioClip ballHitClip;
    [SerializeField] private AudioClip brickDestroyClip;
    private AudioSource _audioSource;
    private IAudioSettings _settings;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>() ?? gameObject.AddComponent<AudioSource>();
        _settings = AudioSettings.Instance;
    }

    private void PlaySound(AudioClip clip)
    {
        if (_settings.IsMuted || clip == null) return;
        _audioSource.PlayOneShot(clip);
    }

    public void PlayBallHit() { PlaySound(ballHitClip); }
    public void PlayBrickDestroy() { PlaySound(brickDestroyClip); }
}