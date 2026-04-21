using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class EnemyState
{
    // AGENTS
    protected GameObject target;
    protected GameObject self;

    // ACTIONS
    public enum ACTION { VIGILAR, PERSEGUIR, DISPARAR }

    // EVENTS
    public enum EVENT { ENTER, RUNNING, EXIT }

    // VARIABLES
    public ACTION currentAction;
    protected EVENT currentEvent;
    protected EnemyState nextState;
    protected Material[] FSM_Materials;
    protected NavMeshAgent NMA;

    // CONSTRUCTOR
    public EnemyState(GameObject target, GameObject self, Material[] FSM_Material)
    {
        this.target = target;
        this.self = self;
        this.FSM_Materials = FSM_Material;
        NMA = self.GetComponent<NavMeshAgent>();
        NMA.updateRotation = false;
    }

    // EVENTS
    protected virtual void UpdateToEnter() { currentEvent = EVENT.ENTER; }
    protected virtual void UpdateToRunning() { currentEvent = EVENT.RUNNING; }
    protected virtual void UpdateToExit() { currentEvent = EVENT.EXIT; }

    public EnemyState UpdateEvent()
    {
        switch (currentEvent)
        {
            case EVENT.ENTER:
                UpdateToEnter();
                UpdateToRunning();
                break;
            case EVENT.RUNNING:
                UpdateToRunning();
                break;
            case EVENT.EXIT:
                UpdateToExit();
                break;
            default:
                Debug.LogError("Unexpected case in method EnemyState.UpdateEvent();");
                break;
        }

        return (currentEvent.Equals(EVENT.EXIT)) ? nextState : this;
    }

    // TRANSITIONS
    protected int CompareDistance(float threshold)
    {
        Vector3 selfPosition = self.transform.position;
        Vector3 targetPosition = target.transform.position;

        float distance = Vector3.Distance(selfPosition, targetPosition);

        if (distance < threshold) { return -1; }
        else if (distance > threshold) { return 1; }
        else { return 0; }
    }
}
