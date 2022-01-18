using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderScript : MonoBehaviour
{

    public GameObject pl;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name == "VoidObject")
        {
            Debug.Log("salut");
            pl.GetComponent<PlayerScript>().isGrounded = true;
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.name == "VoidObject")
        {
            Debug.Log("au revoir");
            pl.GetComponent<PlayerScript>().isGrounded = false;
        }
    }
}
