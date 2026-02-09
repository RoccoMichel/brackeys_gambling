using UnityEngine;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] private Transform listParent;
    [SerializeField] private GameObject listElement;
    private GameObject[] listElementsReference;

    public void ClearList()
    {
        if (listElementsReference == null) return;
        foreach (GameObject go in listElementsReference) Destroy(go);
    }
    public void InstantiateList(SaveData.GameData[] data)
    {
        ClearList();

        listElementsReference = new GameObject[data.Length];
        for (int i = 0; i < data.Length; i++)
        {
            data[i].rank = i + 1;
            listElementsReference[i] = Instantiate(listElement, listParent);
            
            listElementsReference[i].GetComponent<ListElement>().SetValues(data[i], false, i + 1);
        }
    }
    public void InstantiateList(SaveData.GameData[] data, int highlightIndex, int playerRank)
    {
        ClearList();

        listElementsReference = new GameObject[data.Length];
        for (int i = 0; i < data.Length; i++)
        {
            data[i].rank = i + 1;
            listElementsReference[i] = Instantiate(listElement, listParent);

            if (i != highlightIndex) listElementsReference[i].GetComponent<ListElement>().SetValues(data[i], i == highlightIndex, i + 1);
            else listElementsReference[i].GetComponent<ListElement>().SetValues(data[i], i == highlightIndex, playerRank + 1);
        }
    }
}
