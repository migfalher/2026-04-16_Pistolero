using UnityEngine;

public class KamikazeStateEsperar : KamikazeState
{
    // VARIABLES


    // CONSTRUCTOR
    public KamikazeStateEsperar(GameObject owner, GameObject self, Material[] FSM_Material) : base(owner, self, FSM_Material)
    {
        mesh.GetComponent<MeshRenderer>().material = FSM_Material[0];
    }

    // ADD BEHAVIOUR TO EVENTS
    protected override void UpdateToEnter()
    {
        // just in case
        NMA.isStopped = true;
        NMA.ResetPath();
        NMA.SetDestination(self.transform.position);
        NMA.velocity = Vector3.zero;

        base.UpdateToEnter();
    }

    protected override void UpdateToRunning()
    {
        base.UpdateToRunning();

        if (CompareDistance(owner, 3f) <= 0)
        {
            nextState = new KamikazeStateSeguir(owner, self, FSM_Materials);
            currentEvent = EVENT.EXIT;
        }
    }

    protected override void UpdateToExit() { base.UpdateToExit(); }
}
