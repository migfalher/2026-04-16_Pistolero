using UnityEngine;

public class Enemy : MonoBehaviour
{
    // public components
    public Material[] FSM_Materials;

    // private attributes
    private EnemyState FSM;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
