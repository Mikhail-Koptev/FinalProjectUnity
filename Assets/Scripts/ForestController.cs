using UnityEngine;

public class ForestController : MonoBehaviour
{
    [SerializeField] private GameObject endMenu;

    private Database database;

    void Start()
    {
        database = GameObject.Find("Database").GetComponent<Database>();

        if (database.GetWizardDefeated()) {
            endMenu.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
