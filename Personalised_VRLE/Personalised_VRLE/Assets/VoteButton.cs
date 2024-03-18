using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoteButton : MonoBehaviour
{

    public float voteScore = 0;
    public Q_Learning_Agent AI_agent_Brain;
    public GameObject[] otherButtons;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    //when button is initially pressed
    private void OnTriggerEnter(Collider other)
    {
        //only activate if pressed by hand
        if (other.CompareTag("HandPositionApproximation"))
        {
            //Change this button's colour to green
            Renderer renderer = GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = Color.green;
            }

            //disable the other buttons so only one can be pressed at a time
            foreach (GameObject button in otherButtons)
            {
                button.GetComponent<BoxCollider>().enabled = false;
            }
        }
    }

    //when button is let go
    private void OnTriggerExit(Collider other)
    {
        //only activate if pressed by hand
        if (other.CompareTag("HandPositionApproximation"))
        {
            //Change this button's colour back to white
            Renderer renderer = GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = Color.white;
            }

            //Submit this button's score and trigger the episode to continue
            AI_agent_Brain.ContinueCurrentEpisode(voteScore);

            //renable all the buttons
            foreach (GameObject button in otherButtons)
            {
                button.GetComponent<BoxCollider>().enabled = true;
            }
        }

    }
}
