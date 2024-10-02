using UnityEngine;

public class ClawScript : MonoBehaviour
{
    [SerializeField]
    LobsterController lobster;

    //If fish collides with lobster claws and player presses space, gain points
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Fish") && Input.GetKeyDown(KeyCode.Space))
        {
            ++lobster.score;
            Destroy(collision.gameObject);
            Debug.Log("CScore: " + lobster.score);
        }
    }
}
