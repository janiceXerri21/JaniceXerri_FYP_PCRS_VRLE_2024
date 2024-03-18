using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.IO;


public class Q_Learning_Agent : MonoBehaviour
{

    public float[][] q_table; //stores the values of the Q-Table
    public int stepsPerEpisode = 16; //Number of actions the agent takes within an episode (before a reset happens and another episode begins)
    public float learningRate = 0.5f; //The rate at which to update the value estimates given a reward.
    public float discountRate = 0.99f; //Discount rate used for getting the Q-Value
    public int stateSpaceSize = 256; //Number of possible states in the Environment (in this case number of possible classrooms)
    public int actionSpaceSize = 4; //Number of possilbe actions the Agent can take

    public float e = 1; // Initial epsilon value for random action selection.
    public float eMin = 0.1f; //Minimum epsilon value allowed
    public int annealingSteps = 100; //Number of steps to lower e to eMin
    public bool randomiseStartEveryEpisode = false;

    public EnviornmentCustomisationState CurrentCustomisationSettings;

    private int action = -1; //setting to -1 as a default so that we will know if no action has taken place yet //stores the last action that took place
    private int lastState; //Stores the previous state of the environment (the classroom id)

    private int currentStep = 0;
    private bool episodeIsDone = false; //Marks if the current episode is complete or still going
    private float reward; //Stores reward for the current step
    private float currentEpisodeReward = 0; //Stores total reward for the current episode

    //used for learner profile building
    private int profileNum = 0; //Overwritten in Start() to mark the number of profiles that exist //A new profile is created every execution
    private string fileName = ""; //Overwritten in Start()

    //ParticleEffects for when the agent takes an action
    public ParticleSystem[] envChangeVFX;



    // Start is called before the first frame update
    void Start()
    {
        //Mark that a new learner profile is created and get the current profile number
        profileNum = PlayerPrefs.GetInt("profileCount", 0);
        profileNum++;
        PlayerPrefs.SetInt("profileCount", profileNum);
        PlayerPrefs.Save();
        string subfolder = "LearnerProfiles";

        //If subfolder isn't there then make it
        if (!Directory.Exists(subfolder))
        {
            Directory.CreateDirectory(subfolder);
        }

        //Create the final filename
        fileName = Path.Combine(subfolder, "learner_Profile_" + profileNum.ToString() + ".txt");

        //Set last state to the starting state
        lastState = getCurrentStateNumber();

        //Intialise the Q-Table based on the current environment and possible actions
        QTableInit(stateSpaceSize, actionSpaceSize);

        //Send starting state to the Q-Table
        SendState(getCurrentStateNumber(), 0, episodeIsDone);
    }


    // Update is called once per frame
    void Update()
    {
        //score can be inputted either from keyboard numbers followed by the enter button, or by the voting buttons in the VR classroom
        //keyboard inputting is all handled here, whilst the VR buttons' functionality in mostly in the VoteButton.cs script
        //get the keyboard inputs to see the user's rating of the current classroom design (user can input numbers 1 to 5)
        if (Input.GetKeyDown(KeyCode.Alpha1))
            reward = -1f;
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            reward = -0.5f;
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            reward = 0;
        else if (Input.GetKeyDown(KeyCode.Alpha4))
            reward = 0.5f;
        else if (Input.GetKeyDown(KeyCode.Alpha5))
            reward = 1;

        //each step of the current episode needs the enter key to be pressed to confirm the score for the current classroom design (if keyboard is being used to input the users classroom design rating)
        if (Input.GetKeyDown(KeyCode.Return))
        {
            ContinueCurrentEpisode(reward);
        }
    }


    //Intialise the Q-Table based on the current environment and possible actions
    public void QTableInit(int stateSpaceSize, int actionSpaceSize)
    {
        q_table = new float[stateSpaceSize][];
        for (int i = 0; i < stateSpaceSize; i++)
        {
            q_table[i] = new float[actionSpaceSize];
            for (int j = 0; j < actionSpaceSize; j++)
            {
                q_table[i][j] = 0.0f;
            }
        }
    }


    //gets the current customised state that the VRLE environment is set to (format eg: 1241 with each number representing a different feature value)
    public int getCurrentState()
    {
        return CurrentCustomisationSettings.ConfigNumber;
    }


    //gets the current customised state number that the VRLE environment is set to (format eg: 27 which represents the 27th possible state)
    public int getCurrentStateNumber()
    {
        return (CurrentCustomisationSettings.getCurrentPosition()[0] + CurrentCustomisationSettings.getCurrentPosition()[1] * 16 - 16 -1);
    }


    // Uses the current state and the Q-table to decide what the next action should be
    public float[] GetAction()
    {
        lastState = getCurrentStateNumber();
        action = q_table[lastState].ToList().IndexOf(q_table[lastState].Max());

        //current chance of randomising the action
        if (Random.Range(0f, 1f) < e) 
        { 
            action = Random.Range(0, 3);
        }

        //Update the Epsilon Value if above the minimum to slowly reduce the randomness factor
        if (e > eMin) 
        { 
            e = e - ((1f - eMin) / (float)  annealingSteps);
        }

        //for debugging
        float currentQ = q_table[lastState][action];
        Debug.Log("Current Epsilon:" + e.ToString("F2"));
        Debug.Log("Current Q-Value:" + currentQ.ToString("F2"));

        return new float[1] { action };
    }

    //Returns a list of every states average Q-Value
	public float[] GetAverageQValuePerState()
    {
        float[] QValList = new float[q_table.Length];
        for (int i = 0; i < q_table.Length; i++)
        {
            QValList[i] = q_table[i].Average();
        }
        return QValList;
    }

    
    //updates the current customisation state along and associated reward to the Q-Table
    public void SendState(int state, float reward, bool episodeIsDone)
    {
        if (action > 0)
        {
            //If end of episode
            if (episodeIsDone == true)
            {
                q_table[lastState][action] += learningRate * (reward - q_table[lastState][action]);
            }
            //If end of a step
            else
            {
                q_table[lastState][action] += learningRate * (reward + discountRate * q_table[state].Max() - q_table[lastState][action]);
            }
        }
        lastState = state;
    }

    //runs a single step
    public void Step()
    {
        //run any paricle effects
        foreach(ParticleSystem p in envChangeVFX)
            p.Play();

        //iterate step count
        currentStep += 1;
        if (currentStep >= stepsPerEpisode)
        {
            episodeIsDone = true;
        }

        //Get the next action
        float[] actions = GetAction();
        int nextAction = Mathf.FloorToInt(actions[0]);

        //perform the action 
        //move up
        if (nextAction == 0)
        {
            //check if not at the top of the grid
            if (CurrentCustomisationSettings.getCurrentPosition()[1] > 1)
            {
                //then update the config number
                CurrentCustomisationSettings.ConfigNumber = CurrentCustomisationSettings.PositionToConfig(CurrentCustomisationSettings.getCurrentPosition()[0], CurrentCustomisationSettings.getCurrentPosition()[1]-1);
            }
        }
        //move down
        if (nextAction == 1)
        {
            //check if not at the bottom of the grid
            if (CurrentCustomisationSettings.getCurrentPosition()[1] < 16)
            {
                //then update the config number
                CurrentCustomisationSettings.ConfigNumber = CurrentCustomisationSettings.PositionToConfig(CurrentCustomisationSettings.getCurrentPosition()[0], CurrentCustomisationSettings.getCurrentPosition()[1] + 1);
            }
        }
        //move left
        if (nextAction == 2)
        {
            //check if not at the left end of the grid
            if (CurrentCustomisationSettings.getCurrentPosition()[0] > 1)
            {
                //then update the config number
                CurrentCustomisationSettings.ConfigNumber = CurrentCustomisationSettings.PositionToConfig(CurrentCustomisationSettings.getCurrentPosition()[0]-1, CurrentCustomisationSettings.getCurrentPosition()[1]);
            }
        }
        //move right
        if (nextAction == 3)
        {
            //check if not at the right end of the grid
            if (CurrentCustomisationSettings.getCurrentPosition()[0] < 16)
            {
                //then update the config number
                CurrentCustomisationSettings.ConfigNumber = CurrentCustomisationSettings.PositionToConfig(CurrentCustomisationSettings.getCurrentPosition()[0]+1, CurrentCustomisationSettings.getCurrentPosition()[1]);
            }
        }
    }



    //Pretty self explanatory function name
    public void StartNewEpisode()
    {
        episodeIsDone = false;
        currentStep = 0;
        currentEpisodeReward = 0;

        //If not randomiseStartEveryEpisode reset the classroom environment to initial starting state
        if (!randomiseStartEveryEpisode)
            CurrentCustomisationSettings.resetStateToStart();
        //If randomiseStartEveryEpisode is true then each episode will start at a different possible classroom design. This will increase variety but will also increase episodes needed for the agent to converge with the preferences of the user
        else
            CurrentCustomisationSettings.randomiseState(); 

    }


    //continues to the next step of the current episode and checks if the current episode has ended
    //is called in this script when keyboard reward amount is inputted, or from VoteButton.cs when a button is pressed in the VR Environment
    public void ContinueCurrentEpisode(float reward)
    {
        currentEpisodeReward += reward;
        //if not end of episode
        //Continue Step Loop
        if (!episodeIsDone)
        {
            SendState(getCurrentStateNumber(), reward, episodeIsDone);
            Step();

            //for debugging
            Debug.Log("Current State: " + getCurrentState());
            Debug.Log("Current Episode Reward: " + currentEpisodeReward);

            //Add the state and reward stats to the learner profile
            using (StreamWriter writer = new StreamWriter(fileName, true))
            {
                writer.WriteLine("Step: " + getCurrentState() + ", " + reward);
            }
        }
        //if episode is over
        else
        {
            SendState(getCurrentStateNumber(), currentEpisodeReward, episodeIsDone);

            //Add the state and reward stats to the learner profile
            using (StreamWriter writer = new StreamWriter(fileName, true))
            {
                writer.WriteLine("Episode End: " + getCurrentState() + ", Total Reward:" + currentEpisodeReward + ", Epsilon: " + e);
                writer.WriteLine("Avg. Q-Values: " + "[" + string.Join(", ", GetAverageQValuePerState()) + "]");
            }

            Debug.Log("Episode has ended");
            StartNewEpisode();
        }
    }
}
