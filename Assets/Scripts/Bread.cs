using UnityEngine;

public class Bread : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().Heal(10);
            Destroy(gameObject);
        }
    }
}
