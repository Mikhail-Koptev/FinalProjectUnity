using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private RectTransform hpBar;
    [SerializeField] private float maxWidth;

    private bool isPaused;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused) {
                Pause();
            }
            else {
                Continue();
            }
        }
    }

    public void SetHP(float hp)
    {
        float percent = hp / 100;

        hpBar.sizeDelta = new Vector2(maxWidth * percent, hpBar.sizeDelta.y);
    }

    public void Continue()
    {
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
        isPaused = false;
    }

    private void Pause()
    {
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
        isPaused = true;
    }
}
