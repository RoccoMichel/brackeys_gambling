using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ListElement : MonoBehaviour
{
    [SerializeField] Color standardColor;
    [SerializeField] Color highlightColor;
    [SerializeField] Image background;
    [SerializeField] TMP_Text rankDisplay;
    [SerializeField] TMP_Text scoreDisplay;
    [SerializeField] TMP_Text dateDisplay;

    public void SetValues(SaveData.GameData data, bool highlight, int rank)
    {
        background.color = highlight ? highlightColor : standardColor;
        rankDisplay.text = '#' + rank.ToString();
        scoreDisplay.text = data.score.ToString();
        dateDisplay.text = data.date;
    }
}
