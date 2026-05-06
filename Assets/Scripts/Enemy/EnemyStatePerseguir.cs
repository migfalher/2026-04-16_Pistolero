using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStatePerseguir : EnemyState
{
    // VARIABLES
    Vector3 dir;
    Quaternion rot;

    // CONSTRUCTOR
    public EnemyStatePerseguir(GameObject target, GameObject self, Material[] FSM_Material) : base(target, self, FSM_Material)
    {
        self.GetComponent<MeshRenderer>().material = FSM_Material[1];
    }

    // ADD BEHAVIOUR TO EVENTS
    protected override void UpdateToEnter()
    {
        // go to player
        NMA.SetDestination(target.transform.position);
        // look at player (excluding Y axis)
        dir = target.transform.position - self.transform.position;
        dir.y = 0;
        rot = Quaternion.LookRotation(dir);
        self.transform.rotation = rot;

        base.UpdateToEnter();
    }

    protected override void UpdateToRunning()
    {
        // go to player
        NMA.SetDestination(target.transform.position);
        // look at player (excluding Y axis)
        dir = target.transform.position - self.transform.position;
        dir.y = 0;
        rot = Quaternion.LookRotation(dir);
        self.transform.rotation = rot;

        base.UpdateToRunning();

        if (CompareDistance(dispararDistance) <= 0)
        {
            currentEvent = EVENT.EXIT;

            nextState = new EnemyStateDisparar(target, self, FSM_Materials);
        }
        else if(CompareDistance(vigilarDistance) >= 1)
        {
            currentEvent = EVENT.EXIT;

            nextState = new EnemyStateVigilar(target, self, FSM_Materials);
        }
    }

    protected override void UpdateToExit() { base.UpdateToExit(); }
}
