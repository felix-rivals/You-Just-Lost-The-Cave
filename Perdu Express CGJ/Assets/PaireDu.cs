using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaireDu : MonoBehaviour
{
    public PlayerScript ps;
    public AudioSource bruitage;
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            this.gameObject.SetActive(false);
            ps.canDoubleJump = true;
            bruitage.Play(0);
        }
    }
}
