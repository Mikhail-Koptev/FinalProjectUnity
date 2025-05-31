using UnityEngine;

public class CraniumBasher : Item
{
    private void Start()
    {
        base.Start();
    }

    public void Use()
    {
        EndSceneController endSceneController = GameObject.Find("EndSceneController").GetComponent<EndSceneController>();
        if (endSceneController) {
            endSceneController.EndGame();
        }

        base.Use();
    }
}
