using UnityEngine;

public class Scarecrow : MonoBehaviour
{
    [SerializeField] private GameObject droppedItem;

    private bool isPlayerDamaged = false;

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
            GameObject obj = (GameObject) Instantiate(droppedItem, transform.position, Quaternion.identity);
            obj.name = droppedItem.name;
        }
        database.AddDestroyedObject(gameObject);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player") {
            if (!isPlayerDamaged) {
                collider.gameObject.GetComponent<PlayerController>().Hit(20f);
                isPlayerDamaged = true;
            }
        }
    }
}
