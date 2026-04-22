using System.Collections;
using UnityEngine;

public class EnemyStateDisparar : EnemyState
{
    // VARIABLES
    float timeLimit = 4.5f; /* Time.deltaTime returns time in seconds (0.015), not in miliseconds (15) */
    float timeCount = 5f;
    Vector3 dir;
    Quaternion rot;

    // CONSTRUCTOR
    public EnemyStateDisparar(GameObject target, GameObject self, Material[] FSM_Material) : base(target, self, FSM_Material)
    {
        self.GetComponent<MeshRenderer>().material = FSM_Material[2];
    }

    // ADD BEHAVIOUR TO EVENTS
    protected override void UpdateToEnter()
    {
        // step right there !!!
        NMA.isStopped = true;
        NMA.ResetPath();
        NMA.SetDestination(self.transform.position);
        NMA.velocity = Vector3.zero;
        // look at player (excluding Y axis)
        dir = target.transform.position - self.transform.position;
        dir.y = 0;
        rot = Quaternion.LookRotation(dir);
        self.transform.rotation = rot;

        base.UpdateToEnter();
    }

    protected override void UpdateToRunning()
    {
        // look at player (excluding Y axis)
        dir = target.transform.position - self.transform.position;
        dir.y = 0;
        rot = Quaternion.LookRotation(dir);
        self.transform.rotation = rot;
        // shoot when timeCount >= timeLimit
        if (timeCount >= timeLimit)
        {
            Shoot();
            timeCount = 0;
        }
        timeCount += Time.deltaTime;

        base.UpdateToRunning();

        if (CompareDistance(8f) >= 1)
        {
            currentEvent = EVENT.EXIT;
            nextState = new EnemyStatePerseguir(target, self, FSM_Materials);
        }
    }

    protected override void UpdateToExit()
    {
        base.UpdateToExit();
    }

    // SHOOT
    private void Shoot()
    {
        // throw bullet
        Vector3 cannonPosition = self.transform.position + new Vector3(0, 0.8f, 0);
        GameObject clone = GameObject.Instantiate(self.GetComponent<Enemy>().bullet, cannonPosition, Quaternion.identity);
        Rigidbody rb = clone.GetComponent<Rigidbody>();
        //clone.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        rb.AddForce((self.transform.forward * rb.mass * 10), ForceMode.Impulse);
    }
}
