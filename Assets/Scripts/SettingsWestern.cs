using UnityEngine;

public class SettingsWestern : MonoBehaviour
{
    [SerializeField] Western.UI.ToggleSwitch soundsToggleWestern;
    [SerializeField] Western.UI.ToggleSwitch musicToggleWestern;

    void Start()
    {
        soundsToggleWestern.CurrentValue = PlayerPrefs.GetFloat("soundsWestern", 1f) == 1f;
        soundsToggleWestern.SetStateAndStartAnimation(soundsToggleWestern.CurrentValue);
        musicToggleWestern.CurrentValue = PlayerPrefs.GetFloat("musicWestern", 1f) == 1f;
        musicToggleWestern.SetStateAndStartAnimation(musicToggleWestern.CurrentValue);
    }


    public void soundsOnWestern()
    {
        PlayerPrefs.SetFloat("soundsWestern", 1f);
        PlayerPrefs.Save();
    }

    public void soundsOffWestern()
    {
        PlayerPrefs.SetFloat("soundsWestern", 0f);
        PlayerPrefs.Save();
    }

    public void musicOnWestern()
    {
        PlayerPrefs.SetFloat("musicWestern", 1f);
        PlayerPrefs.Save();
    }

    public void musicOffWestern()
    {
        PlayerPrefs.SetFloat("musicWestern", 0f);
        PlayerPrefs.Save();
    }

}
