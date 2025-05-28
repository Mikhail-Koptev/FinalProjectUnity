using UnityEngine;

public class StaleBread : Item
{
    private PlayerController player;

    private void Start()
    {
        base.Start();
        player = GameObject.Find("HeroKnight").GetComponent<PlayerController>();
    }

    public override void Use()
    {
        player.PoisonPlayer(15);
        base.Use();
    }
}
