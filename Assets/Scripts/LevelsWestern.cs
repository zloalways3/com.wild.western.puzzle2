using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsWestern : MonoBehaviour
{
    private string keyWestern = "keyWestern";

    public void openPuzzleWestern1()
    {
        keyWestern = "keyWestern1";
        PlayerPrefs.SetFloat("puzzleWestern", 0f);
        PlayerPrefs.Save();
        SceneManager.LoadScene(4);
    }
    public void openPuzzleWestern2()
    {
        keyWestern += "keyWestern1";
        PlayerPrefs.SetFloat("puzzleWestern", 1f);
        PlayerPrefs.Save();
        SceneManager.LoadScene(4);
    }
    public void openPuzzleWestern3()
    {
        keyWestern = "keyWestern2";
        PlayerPrefs.SetFloat("puzzleWestern", 2f);
        PlayerPrefs.Save();
        SceneManager.LoadScene(4);
    }
}
