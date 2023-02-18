using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableObject : MonoBehaviour
{
    private bool dragging = false;
    private bool placed = false;
    private Vector3 offset;

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
            Debug.Log("clicked");
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == this.gameObject)
            {
                Debug.Log("hit square");
                dragging = true;
                offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }
    }

    private void OnMouseUp()
    {
        dragging = false;
        placed = true;
    }
}
