using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextButtonScript : MonoBehaviour
{
    public GameObject[] otherButtons;
    public UIController uiController; // Assign this in the Inspector

    private void Awake()
    {
        // Find and assign the UIController if not set in the Inspector
        if (uiController == null)
        {
            uiController = FindObjectOfType<UIController>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HandPositionApproximation"))
        {
            // Change this button's colour to indicate it's pressed
            Renderer renderer = GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = Color.green;
            }

            // Disable the other buttons
            foreach (GameObject button in otherButtons)
            {
                button.GetComponent<BoxCollider>().enabled = false;
            }

            // Call the NextCourse method from the UIController
            uiController.NextCourse();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("HandPositionApproximation"))
        {
            // Change this button's colour back to its default state
            Renderer renderer = GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = Color.red;
            }

            // Re-enable the other buttons
            foreach (GameObject button in otherButtons)
            {
                button.GetComponent<BoxCollider>().enabled = true;
            }
        }
    }
}
