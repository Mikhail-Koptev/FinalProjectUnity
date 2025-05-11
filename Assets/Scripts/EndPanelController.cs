using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndPanelController : MonoBehaviour
{
    [SerializeField] private RectTransform endMessage;

    private bool isMessageShown = false;

    public void EndGame()
    {
        gameObject.SetActive(true);
        StartCoroutine(ShowEndMessage());
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && isMessageShown)
            SceneManager.LoadScene(1);
    }

    private IEnumerator ShowEndMessage()
    {
        while (endMessage.anchoredPosition.y < 170.0f)
        {
            endMessage.anchoredPosition += new Vector2(0, 5f);

            yield return new WaitForSeconds(0.005f);
        }

        isMessageShown = true;
    }
}
