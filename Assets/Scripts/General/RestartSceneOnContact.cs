using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartSceneOnContact : MonoBehaviour
{
    public bool usePlayerTag = true;
    public LayerMask interactions;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (usePlayerTag)
        {
            if (collision.CompareTag("Player"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                return;
            }
        }
        else
        {
            if (GetComponent<Collider2D>().IsTouchingLayers(interactions))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}
