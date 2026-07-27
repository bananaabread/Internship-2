using System.Net;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonScript : Selectable
{
    public Transform in_targ_transform;
    public Transform out_targ_transform;

    public float Dis;
    public float speed = 5f;

    public bool hasEntered = false;

    void Update()
    {
        Dis = Vector2.Distance(transform.position, in_targ_transform.position);
        if (!hasEntered)
        {
            if (Dis > 0)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(in_targ_transform.position.x, transform.position.y, 1), speed * Time.deltaTime);
            }
            if (transform.position.x > in_targ_transform.position.x -0.1f && transform.position.x < in_targ_transform.position.x + 0.1f)
            {
                hasEntered = true;
            }
        }
        if (hasEntered)
        {
            if (IsHighlighted())
            {
                transform.position = new Vector3(out_targ_transform.position.x, transform.position.y);
            }
            else
            {
                transform.position = new Vector3(in_targ_transform.position.x, transform.position.y);
            }
        }
    }
}
