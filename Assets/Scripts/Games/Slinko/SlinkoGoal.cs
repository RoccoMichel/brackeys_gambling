using UnityEngine;

public class SlinkoGoal : MonoBehaviour
{
    public int index;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return; // the slinko has the player tag!

        GetComponentInParent<SlinkoManager>().GameFinish(index);
    }
}
