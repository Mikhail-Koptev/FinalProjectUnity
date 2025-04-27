using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float hp = 100.0f;
    [SerializeField] private float attackDelay = 1;

    public bool _isAttack;
    private bool _isRight;
    private float time;

    private UIController UIController;

    private void Start()
    {
        UIController = GameObject.Find("UIController").GetComponent<UIController>();
        _isAttack = false;
    }

    private void Update()
    {
        if (time >= 0.1f)
            _isAttack = false;

        time += Time.deltaTime;

        if (time >= attackDelay)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                _isAttack = true;
                time = 0;
                GiveDamage();
            }
        }

        if (hp <= 0)
            Destroy(gameObject);
    }

    public void GetDamage(float damage)
    {
        hp -= damage;
        UIController.SetHP(hp);
    }

    public void GiveDamage()
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, 2);

        foreach (Collider2D target in targets)
        {
            if (target.tag == "Enemy") {
                if (target.TryGetComponent<PlayerController>(out PlayerController enemyController))
                {
                    enemyController.GetDamage(Random.Range(5, 10));
                }
            }
        }
    }
}
