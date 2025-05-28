using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private enum States {
        Idle,
        LookingForPlayer,
        WalkingToPlayer,
        WalkingFromPlayer,
        Attack,
        Stunned
    }

    [SerializeField] private int currentState;

    private float idleTimer, attackTimer, walkingTimer, stunningTimer;

    private float y;

    [SerializeField] private float health;
    [SerializeField] private float speed;
    [SerializeField] private float minDamage;
    [SerializeField] private float maxDamage;
    [SerializeField] private float idleDelay;
    [SerializeField] private float attackDelay;
    [SerializeField] private float walkingDelay;

    [SerializeField] private GameObject droppedItem;

    private Database database;

    private float __speed;
    private bool isStunned;

    private Animator animator;

    private GameObject targetPlayer;
    private bool isTargetPlayerDead;

    private void Start()
    {
        __speed = speed;

        currentState = (int) States.Idle;
        idleTimer = 0f;
        attackTimer = attackDelay;

        animator = GetComponent<Animator>();
        database = GameObject.Find("Database").GetComponent<Database>();

        if (database.IsObjectDestroyed(gameObject)) {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        idleTimer += Time.deltaTime;
        attackTimer += Time.deltaTime;
        walkingTimer += Time.deltaTime;
        if (stunningTimer > 0) stunningTimer -= Time.deltaTime;

        if (targetPlayer) {
            isTargetPlayerDead = targetPlayer.GetComponent<PlayerController>()._isDead;
        }

        if (isTargetPlayerDead) {
            currentState = (int) States.Idle;
        }

        switch (currentState)
        {
            case (int) States.Idle:
                if (animator.GetBool("idle") == false) {
                    animator.SetBool("idle", true);
                }

                if (!targetPlayer) {
                    if (idleTimer >= idleDelay) {
                        currentState = (int) States.LookingForPlayer;
                        idleTimer = 0f;
                    }
                }
                else {
                    if (Vector3.Distance(targetPlayer.transform.position, transform.position) > 4) {
                        if (attackTimer >= attackDelay) {
                            currentState = (int) States.Attack;
                            attackTimer = 0f;
                        }
                        else {
                            currentState = (int) States.WalkingFromPlayer;
                            idleTimer = 0f;
                        }
                    }

                    if (attackTimer >= attackDelay) {
                        currentState = (int) States.Attack;
                        idleTimer = 0f;
                    }
                    else {
                        currentState = (int) States.WalkingFromPlayer;
                        idleTimer = 0f;
                    }

                    if (idleTimer >= idleDelay) {
                        currentState = (int) States.WalkingToPlayer;
                        idleTimer = 0f;
                    }
                }
                break;
            
            case (int) States.LookingForPlayer:
                if (animator.GetBool("idle") == false) {
                    animator.SetBool("idle", true);
                }
                if (FindPlayer()) {
                    currentState = (int) States.WalkingToPlayer;
                    animator.SetBool("idle", false);
                    idleTimer = 0f;
                }
                break;
            
            case (int) States.WalkingToPlayer:
                if (targetPlayer) {
                    var direction = (targetPlayer.transform.position - transform.position).normalized;
                    direction.y = y + 1f;

                    if (targetPlayer.transform.position.x > transform.position.x) {
                        transform.rotation = Quaternion.Euler(0, 180f, 0);
                        if (Vector3.Distance(targetPlayer.transform.position, transform.position) > 4) {
                            animator.SetBool("idle", false);
                            transform.position += direction * speed * Time.deltaTime;
                        }
                        else {
                            animator.SetBool("idle", true);
                            currentState = (int) States.Attack;
                            idleTimer = 0f;
                        }
                    }
                    else if (targetPlayer.transform.position.x < transform.position.x) {
                        transform.rotation = Quaternion.Euler(0, 0, 0);
                        if (Vector3.Distance(targetPlayer.transform.position, transform.position) > 4) {
                            transform.position += direction * speed * Time.deltaTime;
                        }
                        else {
                            animator.SetBool("idle", true);
                            currentState = (int) States.Attack;
                            idleTimer = 0f;
                        }
                    }
                }
                else {
                    currentState = (int) States.LookingForPlayer;
                    animator.SetBool("idle", true);
                    idleTimer = 0f;
                }
                break;
            
            case (int) States.WalkingFromPlayer:
                if (targetPlayer) {
                    if (walkingTimer <= walkingDelay) {
                        var direction = -(targetPlayer.transform.position - transform.position).normalized;
                        direction.y = y + 1f;

                        if (targetPlayer.transform.position.x > transform.position.x) {
                            if (Vector3.Distance(targetPlayer.transform.position, transform.position) < 5) {
                                transform.rotation = Quaternion.Euler(0, 0, 0);
                                animator.SetBool("idle", false);
                                transform.position += direction * (speed-1f) * Time.deltaTime;
                            }
                            else {
                                transform.rotation = Quaternion.Euler(0, 180f, 0);
                                animator.SetBool("idle", true);
                                currentState = (int) States.Idle;
                                idleTimer = 0f;
                            }
                        }
                        else if (targetPlayer.transform.position.x < transform.position.x) {
                            if (Vector3.Distance(targetPlayer.transform.position, transform.position) < 5) {
                                transform.rotation = Quaternion.Euler(0, 180f, 0);
                                animator.SetBool("idle", false);
                                transform.position += direction * (speed-1f) * Time.deltaTime;
                            }
                            else {
                                transform.rotation = Quaternion.Euler(0, 0, 0);
                                animator.SetBool("idle", true);
                                currentState = (int) States.Idle;
                                idleTimer = 0f;
                            }
                        }
                    }
                    else {
                        animator.SetBool("idle", true);
                        currentState = (int) States.Attack;
                        walkingTimer = 0f;
                    }
                }
                else {
                    currentState = (int) States.LookingForPlayer;
                    animator.SetBool("idle", true);
                    walkingTimer = 0f;
                }
                break;
            
            case (int) States.Attack:
                if (attackTimer >= attackDelay) {
                    animator.SetBool("idle", true);
                    Attack();
                }
                break;
            
            case (int) States.Stunned:
                if (stunningTimer > 0) {
                    speed = 0;
                    currentState = (int) States.Stunned;
                }
                else {
                    speed = __speed;
                    currentState = (int) States.Idle;
                }
                break;
        }

        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("attack2") || !animator.GetCurrentAnimatorStateInfo(0).IsName("attack1")) {
            speed = __speed;
        }

        if (health <= 0) {
            if (droppedItem) {
                Instantiate(droppedItem, transform.position, Quaternion.identity);
            }

            if (database) {
                database.AddDestroyedObject(gameObject);
            }
            Destroy(gameObject);
        }
    }

    private bool FindPlayer()
    {   
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, 10);

        foreach (Collider2D target in targets)
        {
            if (target.tag == "Player") {
                targetPlayer = target.gameObject;
                isTargetPlayerDead = targetPlayer.GetComponent<PlayerController>()._isDead;
                return true;
            }
        }

        return false;
    }

    // Hit enemy function
    public void Hit(float hp, GameObject? player)
    {
        health -= hp;
        if (player) {
            targetPlayer = player;
            isTargetPlayerDead = targetPlayer.GetComponent<PlayerController>()._isDead;
        }
        currentState = (int) States.WalkingFromPlayer;
        animator.SetBool("idle", false);
        idleTimer = 0f;
    }

    public void Stun(float time)
    {
        animator.SetBool("idle", true);
        stunningTimer = time;
        currentState = (int) States.Stunned;
    }

    private void Attack()
    {
        if (targetPlayer && !isTargetPlayerDead) {
            speed = 0f;

            // Turn left
            if (targetPlayer.transform.position.x < transform.position.x) {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }

            // Turn right
            else if (targetPlayer.transform.position.x > transform.position.x) {
                transform.rotation = Quaternion.Euler(0, 180f, 0);
            }

            // Long-range attack
            if (Vector3.Distance(targetPlayer.transform.position, transform.position) > 2) {
                animator.SetTrigger("attack2");

                float damage = Random.Range(minDamage, maxDamage);
                targetPlayer.GetComponent<PlayerController>().Hit(damage);

                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("attack2")) {
                    speed = __speed;
                    currentState = (int) States.Idle;
                    attackTimer = 0f;
                }
            }
            // Close-range attack
            else if (Vector3.Distance(targetPlayer.transform.position, transform.position) < 2) {
                animator.SetTrigger("attack1");

                float damage = Random.Range(minDamage/1.5f, maxDamage/1.5f);
                targetPlayer.GetComponent<PlayerController>().Hit(damage);

                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("attack1")) {
                    speed = __speed;
                    currentState = (int) States.Idle;
                    attackTimer = 0f;
                }
            }
        }
        else {
            currentState = (int) States.LookingForPlayer;
            idleTimer = 0f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            y = transform.position.y;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            targetPlayer = collider.gameObject;
            isTargetPlayerDead = targetPlayer.GetComponent<PlayerController>()._isDead;
        }
    }
    
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            targetPlayer = null;
        }
    }
}
