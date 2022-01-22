using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemmePaPerdre : MonoBehaviour
{
    public PlayerScript ps;
    public AudioSource bruitage;
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            PickupText pickupText = GetComponent<PickupText>();
            pickupText.ShowPickupText("GemmePasPerdre : \n Tu peux maintenant grimper aux murs ! (D");
            this.gameObject.SetActive(false);
            ps.canClimbing = true;
            bruitage.Play(0);
        }
    }
}
