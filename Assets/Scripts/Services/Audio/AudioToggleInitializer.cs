using UnityEngine;
using Doozy.Runtime.UIManager.Components;

public class AudioToggleInitializer : MonoBehaviour
{
    [SerializeField] private UIToggle muteToggle;

    private void Start()
    {
        muteToggle.SetIsOn(!AudioSettings.Instance.IsMuted);
        
        muteToggle.OnValueChangedCallback.AddListener(OnToggleChanged);
    }

    private void OnToggleChanged(bool isOn)
    {
        Debug.Log(!isOn);
        AudioSettings.Instance.ToggleMute(!isOn);
    }
}
