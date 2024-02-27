using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveCommand : Command
{
    private NavMeshAgent agent;
    private Vector3 destination;
    public override bool isComplete => ReachedDestination();

    public MoveCommand(NavMeshAgent agent, Vector3 destination)
    {
        this.agent = agent;
        this.destination = destination;
    }

    public override void Execute()
    {
        agent.SetDestination(destination);
    }

    public override void ClearCommand()
    {
        agent.ResetPath();
    }

    bool ReachedDestination()
    {
        if(agent.remainingDistance > 0.1f)
        {
            return false;
        }

        return true;
    }
}
