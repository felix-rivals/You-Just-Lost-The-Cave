using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamantEtPerdre : MonoBehaviour
{
    public PlayerScript ps;
    public AudioSource bruitage;
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
PickupText pickupText = GetComponent<PickupText>();
            pickupText.ShowPickupText("DiamantEtPerdre :\nTu peux maintenant dasher ! (shift + direction)");
            this.gameObject.SetActive(false);
            ps.canDash = true;
            bruitage.Play(0);
        }
    }
}
