using UnityEngine;

public class EndSceneController : MonoBehaviour
{
    [SerializeField] private GameObject endGamePanel;

    public void EndGame()
    {
        endGamePanel.SetActive(true);
    }
}
