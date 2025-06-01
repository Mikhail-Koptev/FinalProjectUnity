using System.Collections.Generic;
using UnityEngine;

public class Database : MonoBehaviour
{
    private static Database instance;

    private bool isRestart;
    private float health;
    private float poisonedTimer;
    private bool isPlayerDead;
    private bool previousScene;
    private List<string> inventory;
    private List<string> destroyedObjects;

    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        if (Database.instance.destroyedObjects == null) {
            Database.instance.destroyedObjects = new List<string>();
        }

        if (Database.instance.inventory == null) {
            Database.instance.inventory = new List<string>();
        }
        DontDestroyOnLoad(gameObject);
    }

    public bool IsRestart()
    {
        return Database.instance.isRestart;
    }

    public void SetRestart(bool value)
    {
        Database.instance.isRestart = value;
    }

    public void Restart()
    {
        Database.instance.isRestart = false;
        Database.instance.health = 100f;
        Database.instance.poisonedTimer = 0f;
        Database.instance.isPlayerDead = false;
        Database.instance.previousScene = false;
        Database.instance.inventory.Clear();
        Database.instance.destroyedObjects.Clear();
    }

    public bool IsPreviousScene()
    {
        return Database.instance.previousScene;
    }

    public void SetPreviousScene(bool value)
    {
        Database.instance.previousScene = value;
    }

    public float GetHealth()
    {
        return Database.instance.health;
    }

    public void SetHealth(float hp)
    {
        Database.instance.health = hp;
    }

    public float GetPoisonedTimer()
    {
        return Database.instance.poisonedTimer;
    }

    public void SetPoisonedTimer(float time)
    {
        Database.instance.poisonedTimer = time;
    }

    public bool IsPlayerDead()
    {
        return Database.instance.isPlayerDead;
    }

    public void SetPlayerDead(bool value)
    {
        Database.instance.isPlayerDead = value;
    }

    public List<string> GetInventory()
    {
        return Database.instance.inventory;
    }

    public void AddToInventory(string obj)
    {
        Database.instance.inventory.Add(obj);
    }

    public void AddToInventory(GameObject obj)
    {
        Database.instance.inventory.Add(obj.name);
    }

    public void RemoveFromInventory(string obj)
    {
        Database.instance.inventory.Remove(obj);
    }

    public void RemoveFromInventory(GameObject obj)
    {
        Database.instance.inventory.Remove(obj.name);
    }

    public void ClearInventory()
    {
        Database.instance.inventory.Clear();
    }

    public bool IsObjectDestroyed(string obj)
    {
        return Database.instance.destroyedObjects.Contains(obj);
    }

    public bool IsObjectDestroyed(GameObject obj)
    {
        return Database.instance.destroyedObjects.Contains(obj.name);
    }

    public void AddDestroyedObject(string obj)
    {
        Database.instance.destroyedObjects.Add(obj);
    }

    public void AddDestroyedObject(GameObject obj)
    {
        Database.instance.destroyedObjects.Add(obj.name);
    }
}
