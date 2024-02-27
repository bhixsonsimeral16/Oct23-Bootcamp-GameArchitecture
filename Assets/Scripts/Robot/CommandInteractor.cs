using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CommandInteractor : Interactor
{
    Queue<Command> commands = new Queue<Command>();

    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private GameObject pointerPrefab;
    [SerializeField] private Camera cam;

    private Command currentCommand;
    private Queue<GameObject> pointers = new Queue<GameObject>();

    public override void Interact()
    {
        if(playerInput.commandPressed && agent != null)
        {
            Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0));

            if(Physics.Raycast(ray, out RaycastHit hit))
            {
                if(hit.transform.CompareTag("Ground"))
                {
                    // Remove the old pointer when it is reached
                    pointers.Enqueue(Instantiate(pointerPrefab, hit.point, Quaternion.identity));

                    commands.Enqueue(new MoveCommand(agent, hit.point));
                }
                else if(hit.transform.CompareTag("Builder"))
                {
                    Builder builder = hit.transform.GetComponent<Builder>();

                    if(builder != null)
                    {
                        commands.Enqueue(new BuildCommand(agent, builder));
                    }
                }
            }
        }

        ProcessCommand();
    }

    public void ClearCommands()
    {
        if (commands.Count > 0)
        {
            currentCommand.ClearCommand();
            while(commands.Count > 0)
            {
                if(currentCommand is MoveCommand)
                {
                    Destroy(pointers.Dequeue());
                }
                commands.Dequeue();
            }
        }
    }

    void ProcessCommand()
    {
        if((currentCommand != null && !currentCommand.isComplete) || commands.Count == 0)
        {
            return;
        }
        else if(currentCommand != null && currentCommand.isComplete)
        {
            Destroy(pointers.Dequeue());
        }

        currentCommand = commands.Dequeue();
        currentCommand.Execute();
    }

    public void SetAgent(NavMeshAgent agent)
    {
        this.agent = agent;
    }

    public void ClearAgent()
    {
        agent = null;
    }
}
