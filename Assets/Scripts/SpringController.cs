using UnityEngine;

public class SpringController : MonoBehaviour
{
    private static float impulse = 7.5f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        var rotationz = transform.rotation.eulerAngles.z * Mathf.Deg2Rad;
        col.rigidbody.velocity = rotate(new Vector2(0, impulse), rotationz);

        if (col.gameObject.CompareTag("Lemming"))
        {
            col.gameObject.GetComponent<LemmingController>().rightLeft *= -1;
        }
    }

    
    public static Vector2 rotate(Vector2 v, float delta) {
        return new Vector2(
            v.x * Mathf.Cos(delta) - v.y * Mathf.Sin(delta),
            v.x * Mathf.Sin(delta) + v.y * Mathf.Cos(delta)
        );
    }

}
