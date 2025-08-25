using System.Collections;
using TMPro;
using UnityEngine;

public class PopText : MonoBehaviour
{
    TMP_Text textDisplay;

    public void SetValues(string message, float lifetime)
    {
        textDisplay = GetComponent<TMP_Text>();
        textDisplay.text = message;
        StartCoroutine(DelayedFade(lifetime / 2));
        Destroy(gameObject, lifetime);
    }

    IEnumerator DelayedFade(float delay)
    {
        yield return new WaitForSeconds(delay);

        textDisplay.CrossFadeAlpha(0, delay, true);

        yield return null;
    }
}
