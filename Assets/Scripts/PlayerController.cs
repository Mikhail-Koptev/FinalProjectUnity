using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float health = 100.0f;
    [SerializeField] private float attackDelay = 1;

    public bool _isAttack;
    private bool _isRight;
    private bool _isPoisoned;
    public bool _isDead;

    private float time;
    private float poisonedTimer;

    private UIController UIController;
    private InventoryController inventory;
    [SerializeField] private GameObject endPanel;

    private GameObject sceneController;

    private PlayerMove playerMove;

    private Database database;

    public EnemyAI lastAttackedEnemy;

    private void Start()
    {
        UIController = GameObject.Find("UIController").GetComponent<UIController>();
        inventory = GameObject.Find("Inventory").GetComponent<InventoryController>();

        playerMove = gameObject.GetComponent<PlayerMove>();
        _isAttack = false;

        sceneController = GameObject.Find("SceneController");
        database = GameObject.Find("Database").GetComponent<Database>();

        if (!database.IsPlayerDead())
        {
            if (database.GetHealth() != 0) {
                health = database.GetHealth();
                UIController.SetHP(health);
            }

            if (database.GetPoisonedTimer() != 0) {
                _isPoisoned = true;
                poisonedTimer = database.GetPoisonedTimer();
                UIController.SetPoisoned(true);
            }
        }
        else {
            health = 100f;
            poisonedTimer = 0f;
            _isDead = false;
            database.SetPlayerDead(false);
        }
    }

    private void Update()
    {
        if (time >= 0.1f) {
            _isAttack = false;
        }

        time += Time.deltaTime;

        if (_isPoisoned) {
            poisonedTimer += Time.deltaTime;
            Hit(0.005f, true);
        }

        if (poisonedTimer >= 5.0f) {
            _isPoisoned = false;
            poisonedTimer = 0f;
            UIController.SetPoisoned(false);
        }

        if (time >= attackDelay)
        {
            if (Input.GetKeyDown(KeyCode.F) && !playerMove._isDamaged)
            {
                _isAttack = true;
                time = 0;
                GiveDamage();
            }
        }

        if (health <= 0) {
            EndGame();
            inventory.ClearInventory();
            _isDead = true;
            database.SetPlayerDead(true);
        }
    }

    // Hit player function
    public void Hit(float damage, bool byPoison = false)
    {
        health -= damage;
        UIController.SetHP(health);
        if (!byPoison) {
            gameObject.GetComponent<PlayerMove>().GetDamage();
        }
    }

    public void GiveDamage()
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, 2);

        foreach (Collider2D target in targets)
        {
            // Enemy collision handler
            if (target.tag == "Enemy") {
                EnemyAI enemy = target.GetComponent<EnemyAI>();
                enemy.Hit(Random.Range(5, 10), gameObject);
                lastAttackedEnemy = enemy;
            }

            // Scarecrow collision handler
            else if (target.tag == "Scarecrow") {
                target.GetComponent<Scarecrow>().Kill();
            }
            
            // Grave collision handler
            else if (target.tag == "Grave") {
                target.GetComponent<Grave>().Kill();
            }

            // Straw collision handler
            else if (target.tag == "Straw") {
                target.GetComponent<Straw>().Kill();
            }
        }
    }

    // Heal player function
    public void Heal(float hp)
    {
        if ( (health + hp) < 100.0f) {
            health += hp;
            UIController.SetHP(health);
        }
    }

    // Poison player function
    public void PoisonPlayer(float damage)
    {
        _isPoisoned = true;
        UIController.SetPoisoned(true);
        health -= damage;
        UIController.SetHP(health);
    }

    private void EndGame()
    {
        endPanel.gameObject.GetComponent<EndPanelController>().EndGame();
    }

    // Change scene
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "ChangeScene") {
            database.SetHealth(health);
            database.SetPoisonedTimer(poisonedTimer);
            inventory.SaveInventory();
            sceneController.GetComponent<SceneController>().ChangeScene();
        }

        else if (collider.tag == "PreviousScene") {
            database.SetHealth(health);
            database.SetPoisonedTimer(poisonedTimer);
            inventory.SaveInventory();
            database.SetPreviousScene(true);
            sceneController.GetComponent<SceneController>().PreviousScene();
        } 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Item") {
            inventory.AddItem(collision.gameObject);
        }
    }
}