using System.Runtime.CompilerServices;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // public components
    public GameObject bullet;
    public Material[] FSM_Materials;

    // private attributes
    private EnemyState FSM;
    protected bool tbk;

    // GETTERS AND SETTERS
    public bool targetedByKamikaze
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        get { return tbk; }
        [MethodImpl(MethodImplOptions.Synchronized)]
        set { tbk = value; }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tbk = false;
        GameObject target = GameObject.Find("Player");
        GameObject self = this.gameObject;
        FSM = new EnemyStateVigilar(target, self, FSM_Materials);
    }

    // Update is called once per frame
    void Update()
    {
        FSM = FSM.UpdateEvent();
    }
}
