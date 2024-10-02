using UnityEngine;

public class LobsterController : MonoBehaviour
{
    public int score = 0;

    //If fish collides with lobster body, lose point/health
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Fish"))
        {
            score--;
            Destroy(collision.gameObject);
            Debug.Log("LScore: " + score);
        }
    }
}
