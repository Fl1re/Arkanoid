using UnityEngine;
public class AudioSettings : MonoBehaviour, IAudioSettings
{
    public static AudioSettings Instance { get; private set; }
    private bool _isMuted;
    private const string MutePrefKey = "AudioMuted";

    private void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadMuteState();
    }

    private void LoadMuteState() { _isMuted = PlayerPrefs.GetInt(MutePrefKey, 0) == 1; }
    private void SaveMuteState() { PlayerPrefs.SetInt(MutePrefKey, _isMuted ? 1 : 0); PlayerPrefs.Save(); }

    public bool IsMuted => _isMuted;

    public void ToggleMute(bool isOn)
    {
        _isMuted = isOn;
        SaveMuteState();
    }
}