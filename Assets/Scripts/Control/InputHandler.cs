using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerShooting))]
public class InputHandler : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private PlayerShooting playerShooting;
    private Queue<BaseCommand> commands;

    private void Start()
    {
        commands = new Queue<BaseCommand>();
        playerMovement = GetComponent<PlayerMovement>();
        playerShooting = GetComponent<PlayerShooting>();
    }

    private void Update()
    {
        BaseCommand moveCommand = InputMovementHandling();
        BaseCommand shootCommand = InputShootHandling();

        if (moveCommand != null)
        {
            commands.Enqueue(moveCommand);
            moveCommand.Execute();
        }

        if (shootCommand != null)
        {
            shootCommand.Execute();
        }
    }

    private BaseCommand InputMovementHandling()
    {
        if (Input.GetKey(KeyCode.D))
        {
            return new MoveCommand(playerMovement, new Vector3(1, 0, 0));
        }
        else if (Input.GetKey(KeyCode.A))
        {
            return new MoveCommand(playerMovement, new Vector3(-1, 0, 0));
        }
        else if (Input.GetKey(KeyCode.W))
        {
            return new MoveCommand(playerMovement, new Vector3(0, 0, 1));
        }
        else if (Input.GetKey(KeyCode.S))
        {
            return new MoveCommand(playerMovement, new Vector3(0, 0, -1));
        }
        else if (Input.GetKey(KeyCode.Z))
        {
            Undo();
            return null;
        }
        else
        {
            return new MoveCommand(playerMovement, new Vector3(0, 0, 0));
        }
    }

    private void Undo()
    {
        if (commands.Count > 0)
        {
            BaseCommand undoCommand = commands.Dequeue();
            undoCommand.Unexecute();
        }
    }

    private BaseCommand InputShootHandling()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            return new ShootCommand(playerShooting);
        }
        else
        {
            return null;
        }
    }
}