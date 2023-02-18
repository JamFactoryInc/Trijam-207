using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Flag : MonoBehaviour
{
    public int maxAllowedCollisions = 5;

    private int collisionCount = 0;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("obstacle"))
        {
            Debug.Log("did not collide with obstacle");
            collisionCount++;
            if (collisionCount >= maxAllowedCollisions)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().ToString());
            }
        }
    }
}
