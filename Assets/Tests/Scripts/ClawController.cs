using UnityEngine;

public class ClawController : MonoBehaviour
{
    public int score = 0;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Fish") && Input.GetKeyDown(KeyCode.Space))
        {
            Destroy(collision.gameObject);
        }
    }
}
