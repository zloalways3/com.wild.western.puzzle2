using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuWestern : MonoBehaviour
{
    private float menuWesternIndex = 0.01f;

    public void QuitWestern()
    {
        menuWesternIndex += 0.1f;
        Application.Quit();
    }

    public void openLevelsWestern()
    {
        menuWesternIndex += 1f;
        SceneManager.LoadScene(3);
    }
}
