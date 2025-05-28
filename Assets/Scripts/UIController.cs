using UnityEngine;
using UnityEngine.UI;

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

    public void SetPoisoned(bool value)
    {
        if (value == true) {
            hpBar.gameObject.GetComponent<Image>().color = new Color(0.4745098f, 0.7254902f, 0.05882353f);
        }

        else if (value == false) {
            hpBar.gameObject.GetComponent<Image>().color = new Color(0.8867924f, 0, 0);
        }
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
