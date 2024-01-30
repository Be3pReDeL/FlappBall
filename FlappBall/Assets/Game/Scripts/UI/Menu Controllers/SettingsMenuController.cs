using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuController : UIController
{
    [SerializeField] private Button _soundButtonOn, _soundButtonOff, _vibrationButtonOn, _vibrationButtonOff;

    public override void Start() {
        base.Start();
        
        UpdateSoundButtonsState(PlayerPrefsManager.GetMusicEnabled());
        UpdateVibrationButtonsState(PlayerPrefsManager.GetVibrationEnabled());
    }

    private void UpdateSoundButtonsState(bool toggle) {
        _soundButtonOn.interactable = !toggle;
        _soundButtonOff.interactable = toggle;
    }

    private void UpdateVibrationButtonsState(bool toggle) {
        _vibrationButtonOn.interactable = !toggle;
        _vibrationButtonOff.interactable = toggle;
    }

    public void ToggleSound(bool toggle) {
        PlayerPrefsManager.SetMusicEnabled(Convert.ToInt32(toggle));
        AudioListener.volume = toggle ? 1 : 0;
        UpdateSoundButtonsState(toggle);
    }

    public void ToggleVibration(bool toggle) {
        PlayerPrefsManager.SetVibrationEnabled(Convert.ToInt32(toggle));
        UpdateVibrationButtonsState(toggle);
    }
}
