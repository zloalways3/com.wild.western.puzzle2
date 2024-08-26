using UnityEngine;
using UnityEngine.SceneManagement;

public class UtilsWestern : MonoBehaviour
{


    public void openSettingsWestern()
    {
        int indexWestern = int.Parse(PlayerPrefs.GetString("settingsWestern", "2"));
        SceneManager.LoadScene(indexWestern);
    }


    public void openMenuWestern()
    {
        int indexWestern1 = int.Parse(PlayerPrefs.GetString("menuWestern", "1"));
        SceneManager.LoadScene(indexWestern1);
    }
}
