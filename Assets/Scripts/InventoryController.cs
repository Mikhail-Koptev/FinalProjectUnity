using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    private int itemsCount;
    private const int maxItemsCount = 7;
    private List<GameObject> items;

    private Database database;

    private void Start()
    {
        Resources.UnloadUnusedAssets();

        items = new List<GameObject>(maxItemsCount);
        database = GameObject.Find("Database").GetComponent<Database>();

        var FetchedInventory = database.GetInventory();
        if (FetchedInventory != null) {
            foreach (var item in FetchedInventory) {
                GameObject obj = (GameObject) Resources.Load($"Prefabs/Items/{item}", typeof(GameObject));
                if (obj != null) {
                    AddItem(obj, true);
                }
            }
        }
    }

    private void Update()
    {
        int index = -1;

        if (Input.GetKeyDown(KeyCode.Alpha1)) index = 0;
        if (Input.GetKeyDown(KeyCode.Alpha2)) index = 1;
        if (Input.GetKeyDown(KeyCode.Alpha3)) index = 2;
        if (Input.GetKeyDown(KeyCode.Alpha4)) index = 3;
        if (Input.GetKeyDown(KeyCode.Alpha5)) index = 4;
        if (Input.GetKeyDown(KeyCode.Alpha6)) index = 5;
        if (Input.GetKeyDown(KeyCode.Alpha7)) index = 6;

        if (index <= itemsCount-1 && index != -1)
        {
            if (items[index].TryGetComponent<Bread>(out Bread bread)) {
                bread.Use();
            }
            
            else if (items[index].TryGetComponent<StaleBread>(out StaleBread staleBread)) {
                staleBread.Use();
            }

            else if (items[index].TryGetComponent<PoisonBottle>(out PoisonBottle poisonBottle)) {
                poisonBottle.Use();
            }

            else if (items[index].TryGetComponent<CraniumBasher>(out CraniumBasher craniumBasher)) {
                craniumBasher.Use();
            }

            itemsCount -= 1;
            index = -1;
        }
    }

    public void AddItem(GameObject item, bool isAsset = false)
    {
        if (itemsCount < maxItemsCount) {
            GameObject obj;

            if (itemsCount == 0) {
                obj = (GameObject) Instantiate(item, new Vector3(-300f, 0, 0), Quaternion.identity);
                
                Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
                Destroy(rb);

                BoxCollider2D collider = obj.GetComponent<BoxCollider2D>();
                Destroy(collider);
                
                obj.transform.SetParent(gameObject.transform, false);
                obj.transform.localScale = new Vector3(108f, 108f, 108f);
            }
            else {
                float x = items[itemsCount-1].transform.localPosition.x + 100f;
                obj = (GameObject) Instantiate(item, new Vector3(x, 0, 0), Quaternion.identity);
                
                Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
                Destroy(rb);

                BoxCollider2D collider = obj.GetComponent<BoxCollider2D>();
                Destroy(collider);
                
                obj.transform.SetParent(gameObject.transform, false);
                obj.transform.localScale = new Vector3(108f, 108f, 108f);
            }

            obj.name = item.name;
            items.Add(obj);
            itemsCount += 1;

            if (isAsset == false) {
                Destroy(item);
            }
        }
    }

    public void RemoveItem(GameObject item)
    {
        items.Remove(item);
        Destroy(item);
    }

    public void ClearInventory()
    {
        if (items.Count != 0) {
            items.Clear();
            for (int i = 0; i < transform.childCount; i++) {  
                GameObject.Destroy(transform.GetChild(i).gameObject);  
            }
        }
        SaveInventory();
    }

    public void SaveInventory()
    {
        database.ClearInventory();

        if (items.Count != 0) {
            foreach (var item in items) {
                database.AddToInventory(item);
            }
        }
    }
}
