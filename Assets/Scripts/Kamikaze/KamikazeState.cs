using UnityEngine;
using UnityEngine.AI;

public class KamikazeState
{
    // AGENTS
    protected GameObject owner;
    protected GameObject target;
    protected GameObject self;

    // ACTIONS
    public enum ACTION { ESPERAR, SEGUIR, ATACAR }

    // EVENTS
    public enum EVENT { ENTER, RUNNING, EXIT }

    // VARIABLES
    public ACTION currentAction;
    protected EVENT currentEvent;
    protected KamikazeState nextState;
    protected Material[] FSM_Materials;
    protected GameObject mesh;
    protected NavMeshAgent NMA;

    // CONSTRUCTOR
    public KamikazeState(GameObject owner, GameObject self, Material[] FSM_Material)
    {
        this.owner = owner;
        this.self = self;
        this.FSM_Materials = FSM_Material;
        mesh = self.transform.GetChild(0).gameObject;
        NMA = self.GetComponent<NavMeshAgent>();
        NMA.updateRotation = false;
    }

    // EVENTS
    protected virtual void UpdateToEnter() { currentEvent = EVENT.ENTER; }
    protected virtual void UpdateToRunning() { currentEvent = EVENT.RUNNING; }
    protected virtual void UpdateToExit() { currentEvent = EVENT.EXIT; }

    public KamikazeState UpdateEvent()
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
                Debug.LogError("Unexpected case in method KamikazeState.UpdateEvent();");
                break;
        }

        return (currentEvent.Equals(EVENT.EXIT)) ? nextState : this;
    }

    // TRANSITIONS
    protected int CompareDistance(GameObject target, float threshold)
    {
        Vector3 selfPosition = self.transform.position;
        Vector3 targetPosition = target.transform.position;

        float distance = Vector3.Distance(selfPosition, targetPosition);

        if (distance < threshold) { return -1; }
        else if (distance > threshold) { return 1; }
        else { return 0; }
    }
}
