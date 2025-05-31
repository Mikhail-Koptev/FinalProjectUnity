using UnityEngine;

public class PoisonBottle : Item
{
    private PlayerController player;

    private void Start()
    {
        base.Start();
        player = GameObject.Find("HeroKnight").GetComponent<PlayerController>();
    }

    public override void Use()
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, 50);

        foreach (Collider2D target in targets)
        {
            if (target.tag == "Enemy") {
                EnemyAI enemy = target.GetComponent<EnemyAI>();
                enemy.Hit(1000, null);
            }
        }

        player.PoisonPlayer(15);

        base.Use();
    }
}
