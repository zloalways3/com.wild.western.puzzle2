using UnityEngine;
using UnityEngine.SceneManagement;

public class LoaderWestern : MonoBehaviour
{
    [SerializeField] ProgressBarWestern progressBarWestern;
    float TimerWestern = 0;

    void Update()
    {
        if (progressBarWestern.BarValue == 100)
        {
            SceneManager.LoadScene(1);
        }
        if (Time.timeSinceLevelLoad >= TimerWestern)
        {
            TimerWestern  += 0.02f;
            progressBarWestern.BarValue += 1.0f;
        }
    }
}
