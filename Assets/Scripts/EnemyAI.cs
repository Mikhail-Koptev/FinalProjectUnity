using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private enum States {
        Idle,
        LookingForPlayer,
        WalkingToPlayer,
        WalkingFromPlayer,
        Attack
    }

    private int currentState;

    private float idleTimer, attackTimer, walkingTimer;

    private float y;

    [SerializeField] private float health;
    [SerializeField] private float speed;
    [SerializeField] private float idleDelay;
    [SerializeField] private float attackDelay;
    [SerializeField] private float walkingDelay;

    private float __speed;

    private Animator animator;

    private GameObject targetPlayer;

    void Start()
    {
        __speed = speed;

        currentState = (int) States.Idle;
        idleTimer = 0f;
        attackTimer = attackDelay;

        animator = GetComponent<Animator>();
    }

    void Update()
    {
        idleTimer += Time.deltaTime;
        attackTimer += Time.deltaTime;
        walkingTimer += Time.deltaTime;

        Debug.Log(attackTimer);

        switch (currentState)
        {
            case (int) States.Idle:
                //transform.rotation = Quaternion.Euler(0, 0, 0);

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
                        if (Vector3.Distance(targetPlayer.transform.position, transform.position) > 4) {
                            transform.rotation = Quaternion.Euler(0, 180f, 0);
                            transform.position += direction * speed * Time.deltaTime;
                        }
                        else {
                            currentState = (int) States.Attack;
                            idleTimer = 0f;
                        }
                    }
                    else if (targetPlayer.transform.position.x < transform.position.x) {
                        if (Vector3.Distance(targetPlayer.transform.position, transform.position) > 4) {
                            transform.rotation = Quaternion.Euler(0, 0, 0);
                            transform.position += direction * speed * Time.deltaTime;
                        }
                        else {
                            currentState = (int) States.Attack;
                            idleTimer = 0f;
                        }
                    }
                }
                else {
                    currentState = (int) States.LookingForPlayer;
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
                                transform.position += direction * (speed-1f) * Time.deltaTime;
                            }
                            else {
                                transform.rotation = Quaternion.Euler(0, 180f, 0);
                                currentState = (int) States.Idle;
                                idleTimer = 0f;
                            }
                        }
                        else if (targetPlayer.transform.position.x < transform.position.x) {
                            if (Vector3.Distance(targetPlayer.transform.position, transform.position) < 5) {
                                transform.rotation = Quaternion.Euler(0, 180f, 0);
                                transform.position += direction * (speed-1f) * Time.deltaTime;
                            }
                            else {
                                transform.rotation = Quaternion.Euler(0, 0, 0);
                                currentState = (int) States.Idle;
                                idleTimer = 0f;
                            }
                        }
                    }
                    else {
                        currentState = (int) States.Attack;
                        walkingTimer = 0f;
                    }
                }
                break;
            
            case (int) States.Attack:
                if (attackTimer >= attackDelay) {
                    Attack();
                }
                break;
        }

        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("attack2") || !animator.GetCurrentAnimatorStateInfo(0).IsName("attack1")) {
            speed = __speed;
        }

        if (health <= 0)
            Destroy(gameObject);
    }

    private bool FindPlayer()
    {   
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, 10);

        foreach (Collider2D target in targets)
        {
            if (target.tag == "Player") {
                targetPlayer = target.gameObject;
                return true;
            }
        }

        return false;
    }

    // Hit enemy function
    public void Hit(float hp, GameObject? player)
    {
        health -= hp;
        if (player) targetPlayer = player;
        currentState = (int) States.WalkingFromPlayer;
        idleTimer = 0f;
    }

    private void Attack()
    {
        if (targetPlayer) {
            speed = 0f;

            // Turn left
            if (targetPlayer.transform.position.x < transform.position.x) {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }

            // Turn right
            else if (targetPlayer.transform.position.x > transform.position.x) {
                transform.rotation = Quaternion.Euler(0, 180f, 0);
            }

            if (Vector3.Distance(targetPlayer.transform.position, transform.position) > 2) {
                animator.SetTrigger("attack2");

                float damage = Random.Range(8.5f, 17.5f);
                targetPlayer.GetComponent<PlayerController>().Hit(damage);

                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("attack2")) {
                    speed = __speed;
                    currentState = (int) States.Idle;
                    attackTimer = 0f;
                }
            }
            else if (Vector3.Distance(targetPlayer.transform.position, transform.position) < 2) {
                animator.SetTrigger("attack1");

                float damage = Random.Range(2.5f, 6.5f);
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

    /*private void Attack()
    {
        if (targetPlayer) {
            speed = 0f;

            // Turn left
            if (targetPlayer.transform.position.x < transform.position.x) {
                transform.rotation = Quaternion.Euler(0, 0f, 0);
            }

            // Turn right
            else if (targetPlayer.transform.position.x > transform.position.x) {
                transform.rotation = Quaternion.Euler(0, 180f, 0);
            }

            animator.SetTrigger("attack1");

            float damage = Random.Range(2.5f, 6.5f);
            targetPlayer.GetComponent<PlayerController>().Hit(damage);

            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("attack1")) {
                speed = __speed;
                currentState = (int) States.Idle;
                attackTimer = 0f;
            }
        }
        else {
            currentState = (int) States.LookingForPlayer;
            idleTimer = 0f;
        }
    }*/

    private void ResetTimers()
    {
        idleTimer = 0f;
        attackTimer = 0f;
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
