using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject musicPlayerPrefab;

    private void Start()
    {
        GameObject musicPlayer = GameObject.Find("MusicPlayer(Clone)");
        if (!musicPlayer) {
            musicPlayer = (GameObject) Instantiate(musicPlayerPrefab, Vector3.zero, Quaternion.identity);
            DontDestroyOnLoad(musicPlayer);
        }

        GameObject database = GameObject.Find("Database");
        if (database) {
            Destroy(database);
        }
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
