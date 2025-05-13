using UnityEngine;

public class Grave : MonoBehaviour
{
    [SerializeField] private SceneController sceneController;

    public void Kill()
    {
        if (!GameObject.Find("Wizard")) {
            sceneController.ChangeScene();
            Destroy(gameObject);
        }
    }
}
