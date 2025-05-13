using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float health = 100.0f;
    [SerializeField] private float attackDelay = 1;

    public bool _isAttack;
    private bool _isRight;
    private float time;

    private UIController UIController;
    [SerializeField] private GameObject endPanel;

    private GameObject sceneController;

    private PlayerMove playerMove;

    private Database database;

    private void Start()
    {
        UIController = GameObject.Find("UIController").GetComponent<UIController>();
        playerMove = gameObject.GetComponent<PlayerMove>();
        _isAttack = false;

        sceneController = GameObject.Find("SceneController");
        database = GameObject.Find("Database").GetComponent<Database>();

        if (database.GetHealth() != 0) {
            health = database.GetHealth();
            UIController.SetHP(health);
        }
    }

    private void Update()
    {
        if (time >= 0.1f)
            _isAttack = false;

        time += Time.deltaTime;

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
            Destroy(gameObject);
        }
    }

    // Hit player function
    public void Hit(float damage)
    {
        Debug.Log(damage);
        health -= damage;
        UIController.SetHP(health);
        gameObject.GetComponent<PlayerMove>().GetDamage();
    }

    public void GiveDamage()
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, 2);

        foreach (Collider2D target in targets)
        {
            if (target.tag == "Enemy") {
                EnemyAI enemy = target.GetComponent<EnemyAI>();
                enemy.Hit(Random.Range(5, 10), gameObject);
            }

            // Scarecrow collision handler
            else if (target.tag == "Scarecrow") {
                target.GetComponent<Scarecrow>().Kill();
            }
            
            // Grave collision handler
            else if (target.tag == "Grave")
                target.GetComponent<Grave>().Kill();
        }
    }

    public void Heal(float hp)
    {
        if ( (health + hp) < 100.0f) {
            health += hp;
            UIController.SetHP(health);
        }
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
            sceneController.GetComponent<SceneController>().ChangeScene();
        }
    }
}
