using System;
using UnityEngine;

public class SpikeKillBoxController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        // I know this is slow shut up
        col.gameObject.GetComponent<LemmingController>().Kill();
    }
}
