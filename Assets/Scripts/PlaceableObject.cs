using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlaceableObject : MonoBehaviour
{
    public TilemapCollider2D tileCollider;
    public LayerMask tileLayer;

    private bool dragging = false;
    private bool placed = false;
    private Vector3 offset;
    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!placed && dragging)
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
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, .75f, tileLayer);

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
