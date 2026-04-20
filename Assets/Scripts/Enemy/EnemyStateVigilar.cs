using UnityEngine;

public class EnemyStateVigilar : EnemyState
{
    // CONSTRUCTOR
    public EnemyStateVigilar(GameObject target, GameObject self, Material[] FSM_Material) : base(target, self, FSM_Material)
    {
        self.GetComponent<MeshRenderer>().material = FSM_Material[0];
    }

    // ADD BEHAVIOUR TO EVENTS
    protected override void UpdateToEnter()
    {
        // step right there !!!
        NMA.isStopped = true;
        NMA.ResetPath();
        NMA.SetDestination(self.transform.position);
        NMA.velocity = Vector3.zero;

        base.UpdateToEnter();
    }

    protected override void UpdateToRunning()
    {
        base.UpdateToRunning();

        if (CompareDistance(10f) <= 0)
        {
            nextState = new EnemyStatePerseguir(target, self, FSM_Materials);
            currentEvent = EVENT.EXIT;
        }
    }

    protected override void UpdateToExit() { base.UpdateToExit(); }
}
