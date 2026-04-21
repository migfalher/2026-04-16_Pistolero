using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class KamikazeStateAtacar : KamikazeState
{
    // VARIABLES
    Vector3 dir;
    Quaternion rot;

    // CONSTRUCTOR
    public KamikazeStateAtacar(GameObject owner, GameObject self, Material[] FSM_Material) : base(owner, self, FSM_Material)
    {
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
        NMA.SetDestination(owner.transform.position);
        // look at owner (excluding Y axis)
        dir = owner.transform.position - self.transform.position;
        dir.y = 0;
        rot = Quaternion.LookRotation(dir);
        self.transform.rotation = rot;

        base.UpdateToEnter();
    }

    protected override void UpdateToRunning()
    {
        // go to owner
        NMA.SetDestination(owner.transform.position);
        // look at owner (excluding Y axis)
        dir = owner.transform.position - self.transform.position;
        dir.y = 0;
        rot = Quaternion.LookRotation(dir);
        self.transform.rotation = rot;

        base.UpdateToRunning();
    }

    protected override void UpdateToExit()
    {
        base.UpdateToExit();
    }
}
