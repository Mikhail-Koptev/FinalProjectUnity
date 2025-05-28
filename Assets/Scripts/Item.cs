using UnityEngine;

public class Item : MonoBehaviour
{
    private InventoryController invetory;

    protected virtual void Start()
    {
        invetory = GameObject.Find("Inventory").GetComponent<InventoryController>();
    }

    public virtual void Use()
    {
        invetory.RemoveItem(gameObject);
    }
}
