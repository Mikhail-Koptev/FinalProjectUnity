using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    private UIController UIController;
    private Database database;

    private void Start()
    {
        UIController = GameObject.Find("UIController").GetComponent<UIController>();
        database = GameObject.Find("Database").GetComponent<Database>();
    }

    public void Continue()
    {
        UIController.Continue();
    }

    public void Restart()
    {
        database.SetRestart(true);
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        // Load main menu
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
