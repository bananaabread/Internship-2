using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PineappleScript : MonoBehaviour
{
    private int blinkNum = 0;
    public int maxBlinks = 4;

    void Update()
    {
        if (blinkNum == maxBlinks)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            StartCoroutine(WaitToBlink());
        }
    }
    private IEnumerator WaitToBlink()
    {
        yield return new WaitForSeconds(1f);
        StartCoroutine(Blink());
    }
    private IEnumerator Blink()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(UnBlink());
    }
    private IEnumerator UnBlink()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.1f);
        blinkNum++;
        StartCoroutine(Blink());
    }
}
