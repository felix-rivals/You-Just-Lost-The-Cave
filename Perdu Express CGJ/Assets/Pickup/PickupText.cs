using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupText : MonoBehaviour
{

    public float ShowDuration = 1f;
    public float FadeSpeed = 0.05f;
    public float MoveStep = 0.1f;
    public Collider2D HideCollider;
    public SpriteRenderer HideSpriteRenderer;

    GameObject Go;

    public void ShowPickupText(string txt)
    {
        Go = Instantiate(Resources.Load("PickupText") as GameObject,
            transform.position,
            Quaternion.identity);
        Go.GetComponent<TextMesh>().text = txt;

        HideCollider.enabled = false;
        HideSpriteRenderer.enabled = false;

        StartCoroutine(Show());
    }

    IEnumerator Show()
    {
        yield return new WaitForSeconds(ShowDuration);

        Color alphaColor = Go.GetComponent<TextMesh>().color;

        while (alphaColor.a >= 0)
        {
            alphaColor = new Color(alphaColor.r,
                alphaColor.g,
                alphaColor.b,
                alphaColor.a - 0.05f);
            Go.GetComponent<TextMesh>().color = alphaColor;
            yield return new WaitForSeconds(FadeSpeed);

            Go.transform.Translate(Vector3.up * MoveStep);
        }

        Destroy(Go.gameObject);
        Destroy(gameObject);
    }
}
