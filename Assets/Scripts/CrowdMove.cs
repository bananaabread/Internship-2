using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdMove : MonoBehaviour
{

    public float bobHeight = 0.2f;
    public float bobSpeed = 3f;
    public float swayAmount = 0.05f;
    public float squashAmount = 0.1f;
    public Vector2 speedRange = new Vector2(0.8f, 1.2f);

    Vector3 startPos;
    Vector3 startScale;
    float phase;
    float speed;

    void Start()
    {
        startPos = transform.localPosition;
        startScale = transform.localScale;
        phase = Random.value * Mathf.PI * 2f;
        speed = bobSpeed * Random.Range(speedRange.x, speedRange.y);
    }

    void Update()
    {
        float t = Time.time * speed + phase;
        float bob = Mathf.Abs(Mathf.Sin(t));

        transform.localPosition = startPos + new Vector3(Mathf.Sin(t * 0.5f) * swayAmount, bob * bobHeight, 0f);
        transform.localScale = new Vector3(
            startScale.x * (1f - bob * squashAmount * 0.5f),
            startScale.y * (1f + bob * squashAmount),
            startScale.z);
    }
}
