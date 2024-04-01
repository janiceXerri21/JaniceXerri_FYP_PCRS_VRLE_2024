using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviousButtonScript : MonoBehaviour
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
            // Change this button's colour to green
            Renderer renderer = GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = Color.green;
            }

            // Disable the other buttons so only one can be pressed at a time
            foreach (GameObject button in otherButtons)
            {
                button.GetComponent<BoxCollider>().enabled = false;
            }

            // Call the PreviousCourse method from the UIController
            uiController.PreviousCourse();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("HandPositionApproximation"))
        {
            // Change this button's colour back to white
            Renderer renderer = GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = Color.red;
            }

            // Re-enable all the buttons
            foreach (GameObject button in otherButtons)
            {
                button.GetComponent<BoxCollider>().enabled = true;
            }
        }
    }
}
