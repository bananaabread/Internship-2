using UnityEngine;

public class PromptScript : MonoBehaviour
{
    private Vector3 startScale;
    private Vector3 bigScale;

    private bool isGrowing = true;

    public float changeSpeed = 1f;

    private RectTransform rT;

    // Start is called before the first frame update
    void Start()
    {
        rT = GetComponent<RectTransform>();
        startScale = rT.transform.localScale;
        bigScale = new Vector3(rT.transform.localScale.x + 1, transform.localScale.y + 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (rT.transform.localScale != startScale && rT.transform.localScale != bigScale)
        {
            if (isGrowing)
            {
                    rT.transform.localScale = new Vector3(rT.transform.localScale.x + (changeSpeed * Time.deltaTime), rT.transform.localScale.y + (changeSpeed * Time.deltaTime));
            }
            else
            {
                rT.transform.localScale = new Vector3(rT.transform.localScale.x - (changeSpeed * Time.deltaTime), rT.transform.localScale.y - (changeSpeed * Time.deltaTime));
            }
        }
        if (rT.transform.localScale == bigScale)
        {
            isGrowing = false;
        }
        if (rT.transform.localScale == startScale)
        {
            isGrowing = true;
        }
    }
}
