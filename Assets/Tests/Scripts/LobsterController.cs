using UnityEngine;

public class LobsterController : MonoBehaviour
{
    public int score = 0;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Fish") && Input.GetKeyDown(KeyCode.Space))
        {
            score++;
            Destroy(collision.gameObject);
            Debug.Log("CScore: " + score);
        }
    }
}
