using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LemmingController : MonoBehaviour
{
    public AudioSource lemAudioDeath;
    public AudioSource lemAudioWin;

    public static List<LemmingController> ctrls = new();
    public static void Pause()
    {
        ctrls.ForEach(c => c.Disable());
    }

    public static void Resume()
    {
        ctrls.ForEach(c => c.Enable());
        Flag.maxAllowedCollisions = ctrls.Count();
    }

    public static readonly float speed = 2;
    private bool isDisabled = false;

    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private Collider2D _collider2D;
    private SpriteRenderer _renderer;
    public int rightLeft = 1;
    // Start is called before the first frame update
    void Start()
    {
        ctrls.Add(this);
        _collider2D = GetComponent<BoxCollider2D>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();
        _rigidbody2D.velocity = new Vector2(speed, 0);
        Disable();
    }

    public void Disable()
    {
        
        _rigidbody2D.gravityScale = 0;
        _animator.speed = 0;
        isDisabled = true;
        _rigidbody2D.velocity = Vector2.zero;
        _collider2D.enabled = false;
    }
    
    public void Enable()
    {
        _rigidbody2D.gravityScale = 1;
        _animator.speed = 1;
        isDisabled = false;
        _rigidbody2D.velocity = Vector2.right * rightLeft * speed;
        _collider2D.enabled = true;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Lemming"))
        {
            Physics2D.IgnoreCollision(col.gameObject.GetComponent<Collider2D>(), _collider2D);
            return;
        }

        if (!isDisabled && !col.gameObject.CompareTag("Spring") && !col.gameObject.CompareTag("SpikeKillBox"))
        {
            
            if (
                col.contacts.ToList().TrueForAll(p => p.point.x < transform.position.x) ||
                col.contacts.ToList().TrueForAll(p => p.point.x > transform.position.x)
            )
            {
                _renderer.flipX = !_renderer.flipX;
                rightLeft *= -1;
            }
            
            _rigidbody2D.velocity = new Vector2(speed * rightLeft, _rigidbody2D.velocity.y);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Lemming"))
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), _collider2D);
            return;
        }

        if (!isDisabled && Mathf.Abs(_rigidbody2D.velocity.y) < 0.1f && collision.contacts.ToList().TrueForAll(p => p.point.y < transform.position.y))
        {
            _rigidbody2D.velocity = new Vector2(speed * rightLeft, _rigidbody2D.velocity.y);
        }
    }

    public void Kill()
    {
        lemAudioDeath.Play();

        Destroy(gameObject);
    }

    public void Victory()
    {
        lemAudioWin.Play();

        Destroy(gameObject);
    }
}
