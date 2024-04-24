using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Importing this to work with scenes

public class ChangeInCourse : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HandPositionApproximation"))
        {
            // Change this button's color to green as a visual cue (optional)
            Renderer renderer = GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = Color.green;
            }

            // Load the IntroScene
            SceneManager.LoadScene("IntroScene"); 
        }
    }

    // OnTriggerExit may not be necessary if the scene is changing immediately upon button press
    // If you want to include a delay before changing the scene, you can implement it here
    private void OnTriggerExit(Collider other)
    {
        
    }
}
