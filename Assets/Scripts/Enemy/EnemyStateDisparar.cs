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
        // give player some personal space
        NMA.stoppingDistance = dispararDistance * 0.75f;
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
        // shoot when timeCount >= timeLimit
        if (timeCount >= timeLimit)
        {
            Shoot();
            timeCount = 0;
        }
        timeCount += Time.deltaTime;

        base.UpdateToRunning();

        if (CompareDistance(perseguirDistance) >= 1)
        {
            currentEvent = EVENT.EXIT;
            nextState = new EnemyStatePerseguir(target, self, FSM_Materials);
        }
    }

    protected override void UpdateToExit()
    {
        NMA.stoppingDistance = 0;
        base.UpdateToExit();
    }

    // SHOOT
    private void Shoot()
    {
        // throw bullet
        GameObject clone = GameObject.Instantiate(self.GetComponent<Enemy>().bullet, origin.position, Quaternion.identity);
        Rigidbody rb = clone.GetComponent<Rigidbody>();
        rb.AddForce((self.transform.forward * rb.mass * 10), ForceMode.Impulse);
    }
}
