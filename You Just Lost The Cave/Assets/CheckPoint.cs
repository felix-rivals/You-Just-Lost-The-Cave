using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public DeathScript refScript;
    public List<GameObject> listCP = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            //if (!listCP.Contains(this.gameObject))
            //{
                refScript.startPoint = this.gameObject;
                //listCP.Add(this.gameObject);
            //}
        }
    }
}
