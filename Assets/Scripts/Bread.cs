using UnityEngine;

public class Bread : Item
{
    private PlayerController player;

    protected override void Start()
    {
        base.Start();
        player = GameObject.Find("HeroKnight").GetComponent<PlayerController>();
    }

    public override void Use()
    {
        player.Heal(10);
        base.Use();
    }
}
