using UnityEngine;

public class Grave : MonoBehaviour
{
    [SerializeField] private GameObject graveOwner;
    [SerializeField] private GameObject droppedItem;

    public void Kill()
    {
        if (!graveOwner) {
            if (droppedItem) {
                GameObject obj = (GameObject) Instantiate(droppedItem, transform.position, Quaternion.identity);
                obj.name = droppedItem.name;
            }
            Destroy(gameObject);
        }
    }
}
