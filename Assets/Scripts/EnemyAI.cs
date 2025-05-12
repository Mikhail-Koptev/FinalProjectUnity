using System;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private enum States {
        Idle,
        LookingForPlayer,
        WalkingToPlayer,
        WalkingFromPlayer
    }

    private int currentState;

    private float timer;

    private float y;

    [SerializeField] private float health;
    [SerializeField] private float speed;

    private Animator animator;

    private GameObject targetPlayer;

    void Start()
    {
        currentState = (int) States.Idle;
        //animator.SetTrigger("idle");
        timer = 0f;

        animator = GetComponent<Animator>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        switch (currentState)
        {
            case (int) States.Idle:
                if (animator.GetBool("idle") == false) {
                    animator.SetBool("idle", true);
                }
                if (timer >= 5.0f) {
                    currentState = (int) States.LookingForPlayer;
                    timer = 0f;
                }
                break;
            
            case (int) States.LookingForPlayer:
                if (animator.GetBool("idle") == false) {
                    animator.SetBool("idle", true);
                }
                if (FindPlayer()) {
                    currentState = (int) States.WalkingToPlayer;
                    animator.SetBool("idle", false);
                    timer = 0f;
                }
                break;
            
            case (int) States.WalkingToPlayer:
                if (targetPlayer) {
                    var direction = (targetPlayer.transform.position - transform.position).normalized;
                    direction.y = y + 1f;
                    if (Math.Abs(Vector3.Distance(targetPlayer.transform.position, transform.position)) > 2) {

                        transform.position += direction * speed * Time.deltaTime;
                    }
                }
                else {
                    currentState = (int) States.LookingForPlayer;
                    //animator.SetBool("idle", true);
                    timer = 0f;
                }
                break;
            
            case (int) States.WalkingFromPlayer:
                if (targetPlayer) {
                    var direction = -(targetPlayer.transform.position - transform.position).normalized;
                    direction.y = y + 1f;
                    if (Math.Abs(Vector3.Distance(targetPlayer.transform.position, transform.position)) < 5) {
                        transform.rotation = Quaternion.Euler(0, 180f, 0);
                        transform.position += direction * (speed-1f) * Time.deltaTime;
                    }
                    else {
                        transform.rotation = Quaternion.Euler(0, 0, 0);
                        currentState = (int) States.Idle;
                        //animator.SetBool("idle", true);
                        timer = 0f;
                    }
                }
                break;
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

    private void HitPlayer(PlayerController playerController)
    {
        playerController.GetDamage(20f);
        currentState = (int) States.WalkingFromPlayer;
        animator.SetBool("idle", false);
        timer = 0f;
    }

    public void Hit(float hp, GameObject? player)
    {
        health -= hp;
        if (player) targetPlayer = player;
        currentState = (int) States.WalkingFromPlayer;
        animator.SetBool("idle", false);
        timer = 0f;
    }

    private void Attack()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            y = transform.position.y;
        }

        else if (collision.gameObject.tag == "Player")
        {
            HitPlayer(collision.gameObject.GetComponent<PlayerController>());
        }
    }
}
