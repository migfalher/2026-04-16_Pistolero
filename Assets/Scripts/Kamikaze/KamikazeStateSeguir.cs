using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class KamikazeStateSeguir : KamikazeState
{
    // VARIABLES
    Vector3 dir;
    Quaternion rot;
    GameObject[] enemiesList;

    // CONSTRUCTOR
    public KamikazeStateSeguir(GameObject owner, GameObject self, Material[] FSM_Material) : base(owner, self, FSM_Material)
    {
        mesh.GetComponent<MeshRenderer>().material = FSM_Material[1];
        mesh.transform.Translate(new Vector3(0, 0.4f, 0), Space.Self);
    }

    // ADD BEHAVIOUR TO EVENTS
    protected override void UpdateToEnter()
    {
        enemiesList = GameObject.FindGameObjectsWithTag("Enemy");

        NMA.SetDestination(owner.transform.position);

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

        foreach (GameObject enemy in enemiesList)
        {
            if (CompareDistance(enemy, 10f) <= 0)   // is it near enough?
            {
                Enemy enemyScript = enemy.GetComponent<Enemy>();
                if (enemyScript.targetedByKamikaze) { Debug.Log(enemy.name + " already targeted"); } // is it occupied?
                else
                {
                    enemyScript.targetedByKamikaze = true;
                    owner = enemy;
                    nextState = new KamikazeStateAtacar(owner, self, FSM_Materials);
                    currentEvent = EVENT.EXIT;
                    return;
                }
            }
        }
    }

    protected override void UpdateToExit() { base.UpdateToExit(); }
}
