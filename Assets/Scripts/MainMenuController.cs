using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    private void Start()
    {
        GameObject musicPlayer = GameObject.Find("MusicPlayer");
        DontDestroyOnLoad(musicPlayer);
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void URL()
    {
        Application.OpenURL("http://github.com/Mikhail-Koptev/FinalProjectUnity");
    }
}
