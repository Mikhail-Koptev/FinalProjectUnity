using UnityEngine;

public class Scarecrow : MonoBehaviour
{
    [SerializeField] private GameObject droppedItem;

    private bool isPlayerDamaged = false;

    public void Kill()
    {
        if (droppedItem) {
            Instantiate(droppedItem, transform.position, Quaternion.identity);
        }
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
