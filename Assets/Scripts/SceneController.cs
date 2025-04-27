using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private Image FadePanel;

    private void Start()
    {
        StartCoroutine(FadeOut());
    }

    public void ChangeScene()
    {
        StartCoroutine(FadeInAndChangeScene());
    }

    private IEnumerator FadeIn()
    {
        while (FadePanel.color.a < 1)
        {
            float a = FadePanel.color.a;
            FadePanel.color = new Color(FadePanel.color.r, FadePanel.color.g, FadePanel.color.b, a += 0.01f);

            yield return new WaitForSeconds(0.001f);
        }
    }

    private IEnumerator FadeInAndChangeScene()
    {
        while (FadePanel.color.a < 1)
        {
            float a = FadePanel.color.a;
            FadePanel.color = new Color(FadePanel.color.r, FadePanel.color.g, FadePanel.color.b, a += 0.01f);

            yield return new WaitForSeconds(0.001f);
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private IEnumerator FadeOut()
    {
        while (FadePanel.color.a > 0)
        {
            float a = FadePanel.color.a;
            FadePanel.color = new Color(FadePanel.color.r, FadePanel.color.g, FadePanel.color.b, a -= 0.01f);

            yield return new WaitForSeconds(0.001f);
        }
    }
}