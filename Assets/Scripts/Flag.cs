using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Flag : MonoBehaviour
{
    public static int maxAllowedCollisions = 5;
    
    private int collisionCount = 0;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("obstacle"))
        {
            Debug.Log("did not collide with obstacle");
            collision.collider.enabled = false;
            collision.rigidbody.gravityScale = 0;
            collision.gameObject.GetComponent<LemmingController>().Disable();
            collision.gameObject.GetComponent<Animator>().speed = 0;
            collisionCount++;
            if (collisionCount >= maxAllowedCollisions)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}
