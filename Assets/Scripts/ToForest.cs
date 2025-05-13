using UnityEngine;
using UnityEngine.SceneManagement;

public class ToForest : MonoBehaviour
{
    private Database database;

    private void Start()
    {
        database = GameObject.Find("Database").GetComponent<Database>();
        database.SetWizardDefeated(true);
        SceneManager.LoadScene(1);
    }
}
