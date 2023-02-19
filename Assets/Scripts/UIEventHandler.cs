using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIEventHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Pause()
    {
        Debug.Log("hello");
        LemmingController.Pause();
        PlaceableObject.placingEnabled = true;
    }

    public void Play()
    {
        Debug.Log("hello");
        LemmingController.Resume();
        PlaceableObject.placingEnabled = false;
    }

    public void restart()
    {
        Debug.LogWarning("hello");
        SceneManager.LoadScene(SceneManager.GetActiveScene().ToString());
    }
}
