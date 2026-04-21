using UnityEngine;

public class Kamikaze : MonoBehaviour
{
    // public components
    public Material[] FSM_Materials;

    // private attributes
    private KamikazeState FSM;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject target = GameObject.Find("Player");
        GameObject self = this.gameObject;
        FSM = new KamikazeStateEsperar(target, self, FSM_Materials);
    }

    // Update is called once per frame
    void Update()
    {
        FSM = FSM.UpdateEvent();
    }

    private void OnCollisionEnter(Collision coll)
    {
        GameObject go = coll.gameObject;
        string tag = go.tag;
        if (tag.Equals("Enemy"))
        {
            Destroy(go);
            Destroy(this.gameObject);
        }
    }
}
