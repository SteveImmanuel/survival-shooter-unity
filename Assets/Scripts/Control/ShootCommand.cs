public class ShootCommand : BaseCommand
{

    PlayerShooting playerShooting;

    public ShootCommand(PlayerShooting playerShooting)
    {
        this.playerShooting = playerShooting;
    }

    public override void Execute()
    {
        playerShooting.Shoot();
    }

    public override void Unexecute()
    {
    }
}