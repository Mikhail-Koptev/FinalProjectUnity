using UnityEngine;

public class Stone : MonoBehaviour
{
    [SerializeField] private GameObject droppedItem;
    [SerializeField] private Vector3 dropPoint;

    private Database database;

    private void Start()
    {
        database = GameObject.Find("Database").GetComponent<Database>();

        if (database.IsObjectDestroyed(gameObject)) {
            Destroy(gameObject);
        }
    }

    public void Kill()
    {
        if (droppedItem) {
            GameObject obj = (GameObject) Instantiate(droppedItem, dropPoint, Quaternion.identity);
            obj.name = droppedItem.name;
        }
        database.AddDestroyedObject(gameObject);
        Destroy(gameObject);
    }
}
