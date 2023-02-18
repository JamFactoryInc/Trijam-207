using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LemmingController : MonoBehaviour
{

    public static readonly float speed = 2;

    private Rigidbody2D _rigidbody2D;
    private int rightLeft = 1;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.velocity = new Vector2(speed, 0);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (!col.gameObject.CompareTag("Spring") && !col.gameObject.CompareTag("SpikeKillBox"))
        {
            
            if (
                col.contacts.ToList().TrueForAll(p => p.point.x < transform.position.x) ||
                col.contacts.ToList().TrueForAll(p => p.point.x > transform.position.x)
            )
            {
                rightLeft *= -1;
            }
            
            _rigidbody2D.velocity = new Vector2(speed * rightLeft, _rigidbody2D.velocity.y);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (Mathf.Abs(_rigidbody2D.velocity.y) < 0.1f && collision.contacts.ToList().TrueForAll(p => p.point.y < transform.position.y))
        {
            _rigidbody2D.velocity = new Vector2(speed * rightLeft, _rigidbody2D.velocity.y);
        }
    }

    public void Kill()
    {
        Destroy(gameObject);
    }
}
