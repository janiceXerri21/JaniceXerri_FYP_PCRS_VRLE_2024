using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoreInfoButtonScript : MonoBehaviour
{
    public GameObject[] otherButtons;
    public UIController uiController; // Assign this in the Inspector
    private bool isShowingMoreInfo = false; // Keep track of the button state

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
            // Change the button's color to indicate active/inactive state
            Renderer renderer = GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = isShowingMoreInfo ? Color.red : Color.green;
            }

            // Disable other buttons to avoid multiple inputs
            foreach (GameObject button in otherButtons)
            {
                button.GetComponent<BoxCollider>().enabled = false;
            }

            // Call the appropriate method from the UIController based on the current state
            if (isShowingMoreInfo)
            {
                uiController.ToggleInfoDisplay();
            }
            else
            {
                uiController.ToggleInfoDisplay();
            }

            // Toggle the state
            isShowingMoreInfo = !isShowingMoreInfo;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("HandPositionApproximation"))
        {
            // Optionally change the button color back, or handle it in OnTriggerEnter
            // based on the `isShowingMoreInfo` flag's state.

            // Re-enable all the buttons
            foreach (GameObject button in otherButtons)
            {
                button.GetComponent<BoxCollider>().enabled = true;
            }
        }
    }
}
