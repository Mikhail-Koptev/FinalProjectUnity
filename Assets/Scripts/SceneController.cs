using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private Image FadePanel;
    [SerializeField] private Transform _camera;
    [SerializeField] private float cameraLeft;
    [SerializeField] private float cameraRight;

    private void Start()
    {
        StartCoroutine(FadeOut());
    }

    private void Update()
    {
        if (_camera.position.x < cameraLeft)
            _camera.position = new Vector3(cameraLeft, _camera.position.y, _camera.position.z);
        
        if (_camera.position.x > cameraRight)
            _camera.position = new Vector3(cameraRight, _camera.position.y, _camera.position.z);
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

            yield return new WaitForSeconds(0.01f);
        }
    }

    private IEnumerator FadeInAndChangeScene()
    {
        while (FadePanel.color.a < 1)
        {
            float a = FadePanel.color.a;
            FadePanel.color = new Color(FadePanel.color.r, FadePanel.color.g, FadePanel.color.b, a += 0.01f);

            yield return new WaitForSeconds(0.01f);
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private IEnumerator FadeOut()
    {
        while (FadePanel.color.a > 0)
        {
            float a = FadePanel.color.a;
            FadePanel.color = new Color(FadePanel.color.r, FadePanel.color.g, FadePanel.color.b, a -= 0.01f);

            yield return new WaitForSeconds(0.01f);
        }
    }
}