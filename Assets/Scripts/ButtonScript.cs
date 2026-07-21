using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonScript : Selectable
{
    BaseEventData m_BaseEvent;
    public Transform in_targ_transform;
    public Transform out_targ_transform;

    void Update()
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
