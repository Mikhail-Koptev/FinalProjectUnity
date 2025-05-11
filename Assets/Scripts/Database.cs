using UnityEngine;

public class Database : MonoBehaviour
{
    private static Database instance;

    public float health;

    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;        
        DontDestroyOnLoad(gameObject);
    }

    public void SetHealth(float hp)
    {
        Database.instance.health = hp;
    }

    public float GetHealth()
    {
        return Database.instance.health;
    }
}
