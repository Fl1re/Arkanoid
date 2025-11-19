using UnityEngine;

public interface IAudioSettings
{
    bool IsMuted { get; }
    void ToggleMute(bool isOn);
}