using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class PopText : MonoBehaviour
{
    private TMP_Text textDisplay;

    public void SetValues(string message)
    {
        textDisplay = GetComponent<TMP_Text>();
        textDisplay.text = message;
    }

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
