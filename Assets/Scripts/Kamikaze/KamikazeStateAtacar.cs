using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class KamikazeStateAtacar : KamikazeState
{
    // VARIABLES
    Vector3 dir;
    Quaternion rot;

    // CONSTRUCTOR
    public KamikazeStateAtacar(GameObject owner, GameObject self, Material[] FSM_Material, GameObject target) : base(owner, self, FSM_Material)
    {
        this.target = target;
        mesh.GetComponent<MeshRenderer>().material = FSM_Material[2];
        self.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        mesh.transform.Translate(new Vector3(0, -0.2f, 0), Space.Self);
        NMA.speed *= 5;
        NMA.stoppingDistance = 0;
    }

    // ADD BEHAVIOUR TO EVENTS
    protected override void UpdateToEnter()
    {
        // go to owner
        NMA.SetDestination(target.transform.position);
        // look at owner (excluding Y axis)
        dir = target.transform.position - self.transform.position;
        dir.y = 0;
        rot = Quaternion.LookRotation(dir);
        self.transform.rotation = rot;

        base.UpdateToEnter();
    }

    protected override void UpdateToRunning()
    {

        base.UpdateToRunning();

        if (target != null && !target.IsDestroyed())
        {
            // go to owner
            NMA.SetDestination(target.transform.position);
            // look at owner (excluding Y axis)
            dir = target.transform.position - self.transform.position;
            dir.y = 0;
            rot = Quaternion.LookRotation(dir);
            self.transform.rotation = rot;
        }
        else
        {
            nextState = new KamikazeStateSeguir(owner, self, FSM_Materials);
            currentEvent = EVENT.EXIT;
        }
    }

    protected override void UpdateToExit()
    {
        base.UpdateToExit();
    }
}
