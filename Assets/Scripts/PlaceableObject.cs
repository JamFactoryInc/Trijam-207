using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlaceableObject : MonoBehaviour
{
    public static bool placingEnabled = true;
    
    public TilemapCollider2D tileCollider;
    public LayerMask tileLayer;

    public enum dirs
    {
        up, down, left, right
    };
    public dirs castDir = dirs.down;

    private bool dragging = false;
    private bool placed = false;
    private Vector3 offset;
    private Vector3 startPos;
    private Vector2 castDirVect;

    private void Start()
    {
        startPos = transform.position;

        switch (castDir)
        {
            case dirs.up: 
                castDirVect = Vector2.up;
                break;
            case dirs.down:
                castDirVect = Vector2.down;
                break;
            case dirs.left:
                castDirVect = Vector2.left;
                break;
            case dirs.right:
                castDirVect = Vector2.right;
                break;
        }

        if (tileCollider == null)
        {
            tileCollider = FindObjectOfType<TilemapCollider2D>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!placed && dragging && placingEnabled)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
        }
    }

    private void OnMouseDown()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == this.gameObject)
            {
                dragging = true;
                offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }
    }

    private void OnMouseUp()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, castDirVect, .75f, tileLayer);

        if (hit.collider == tileCollider)
        {
            Debug.Log("tilemap detected below");
            placed = true;
        }
        else
        {
            transform.position = startPos;
        }

        dragging = false;
    }
}
