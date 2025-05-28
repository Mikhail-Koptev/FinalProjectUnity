using UnityEngine;

public class Straw : MonoBehaviour
{
    [SerializeField] private GameObject hiddenEnemy;

    private Database database;

    private void Start()
    {
        database = GameObject.Find("Database").GetComponent<Database>();

        if (database.IsObjectDestroyed(gameObject)) {
            if (database.IsObjectDestroyed("WizardWithBasher") == false) {
                if (hiddenEnemy.name == "WizardWithBasher") {
                    Instantiate(hiddenEnemy, transform.position, Quaternion.identity);
                }
            }
            Destroy(gameObject);
        }
    }

    public void Kill()
    {
        if (hiddenEnemy) {
            GameObject obj = (GameObject) Instantiate(hiddenEnemy, transform.position, Quaternion.identity);
            obj.name = hiddenEnemy.name;
        }
        database.AddDestroyedObject(gameObject);
        Destroy(gameObject);
    }
}
