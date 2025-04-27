using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    private UIController UIController;

    private void Start()
    {
        UIController = GameObject.Find("UIController").GetComponent<UIController>();
    }

    public void Continue()
    {
        UIController.Continue();
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        // Load main menu
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
