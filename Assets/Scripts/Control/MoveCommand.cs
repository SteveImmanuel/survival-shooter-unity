using UnityEngine;

public class MoveCommand : BaseCommand
{
    private PlayerMovement playerMovement;
    private Vector3 direction;

    public MoveCommand(PlayerMovement playerMovement, Vector3 direction)
    {
        this.playerMovement = playerMovement;
        this.direction = direction;
    }

    public override void Execute()
    {
        playerMovement.Move(direction);
        playerMovement.Animating(direction);
    }

    public override void Unexecute()
    {
        playerMovement.Move(-direction);
        playerMovement.Animating(-direction);
    }
}