using System.Collections;
using UnityEngine;
using TMPro;

public class PopupManager : MonoBehaviour
{
    public GameObject popupPanel;
    public TMP_Text popupText;
    public Transform moveTarget;

    public void ShowPopup(string message)
    {
        popupText.text = message;
        popupPanel.SetActive(true);
        StartCoroutine(PopupRoutine());
    }

    private IEnumerator PopupRoutine()
    {
        yield return new WaitForSeconds(0.3f);

        float t = 0f;
        Vector3 startPos = popupPanel.transform.position;
        Vector3 endPos = moveTarget.position;

        while (t < 1f)
        {
            t += Time.deltaTime * 2f;
            popupPanel.transform.position = Vector3.Lerp(startPos, endPos, t);
            yield return null;
        }

        popupPanel.SetActive(false);
        popupPanel.transform.position = Vector3.Lerp(endPos, startPos, t);
    }
}
