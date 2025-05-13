using UnityEngine;

public class Database : MonoBehaviour
{
    private static Database instance;

    private float health;
    private bool isWizardDefeated = false;

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

    public float GetHealth()
    {
        return Database.instance.health;
    }

    public void SetHealth(float hp)
    {
        Database.instance.health = hp;
    }

    public void SetWizardDefeated(bool value)
    {
        Database.instance.isWizardDefeated = value;
    }

    public bool GetWizardDefeated()
    {
        return Database.instance.isWizardDefeated;
    }
}
