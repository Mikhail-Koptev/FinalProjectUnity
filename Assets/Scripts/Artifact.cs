using UnityEngine;

public class Artifact : MonoBehaviour
{
    protected PlayerController? owner;
    protected EnemyAI? enemy;
    protected bool isAttack;

    protected virtual void Update()
    {
        if (!owner) {
            if (transform.parent) {
                if (transform.parent.gameObject.name == "Inventory") {
                    owner = GameObject.Find("HeroKnight").GetComponent<PlayerController>();
                }
            }
        }
        else {
            isAttack = owner._isAttack;
            enemy = owner.lastAttackedEnemy;
        }
    }
}
