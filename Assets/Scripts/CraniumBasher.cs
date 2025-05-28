using UnityEngine;

public class CraniumBasher : Artifact
{
    protected override void Update()
    {
        base.Update();
        if (isAttack && enemy != null) {
            Use(enemy);
        }
    }

    public void Use(EnemyAI enemy)
    {
        int number = Random.Range(1, 5);
        if (number == 4) {
            enemy.Stun(0.5f);
        }
    }
}
