using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Import this namespace to work with UI
using UnityEngine.SceneManagement; // Import this to work with scenes
using TMPro; //for TextMeshPro


public class UIController : MonoBehaviour
{
    public TMP_Text courseCodeValueText;
    public TMP_Text campusValueText;
    public TMP_Text degreeValueText;
    public TMP_Text degreeSpecialisationValueText;
    public TMP_Text keySkillsValueText;
    public TMP_Text filterValueText;
    public TMP_Text successRateValueText;

    // Internal reference to FileReader
    private FileReader fileReader;
    private int currentIndex = 0;
    // Dictionary to hold the mapping of degree specializations to config numbers
    private Dictionary<string, int> degreeToConfigMapping;


    // Start is called before the first frame update
    void Awake()
    {
        // Find the FileReader in the scene and get its recommendations
        fileReader = FindObjectOfType<FileReader>();

        // Initialize the UI with the first recommendation if any are loaded
        if (fileReader != null && fileReader.recommendations.Count > 0)
        {
            UpdateUI();
        }
        else
        {
            Debug.LogError("No recommendations found. Ensure FileReader is loaded and has data.");
        }

        // Initialize the mapping dictionary
        InitializeDegreeToConfigMapping();

    }
    // Add a new boolean flag to keep track of whether secondary info is displayed
    private bool isSecondaryInfoDisplayed = false;
    public GameObject menubackgroundPanel; // Assigned this in Inspector
    public GameObject secondaryPanel; // Assigned this in Inspector

    // Add two public methods to handle displaying or hiding secondary information.
    // Call this method when the "More Info" button is pressed
    public void ToggleInfoDisplay()
    {
        isSecondaryInfoDisplayed = !isSecondaryInfoDisplayed; // Toggle the state

        // Show or hide panels based on the toggled state
        menubackgroundPanel.SetActive(!isSecondaryInfoDisplayed);
        secondaryPanel.SetActive(isSecondaryInfoDisplayed);

        // Update the UI text only if secondary info is to be shown
        if (isSecondaryInfoDisplayed)
        {
            Recommendation currentRec = fileReader.recommendations[currentIndex];
            courseCodeValueText.text = currentRec.CourseCode.ToString();
            keySkillsValueText.text = currentRec.KeySkills;
            filterValueText.text = currentRec.Filter;
        }
    }

    // Method to set the UI Text elements with the current course's data
    private void UpdateUI()
    {
        // Safety check in case the index is out of range
        if (currentIndex >= 0 && currentIndex < fileReader.recommendations.Count)
        {
            Recommendation currentRec = fileReader.recommendations[currentIndex];

            // Update primary information
            campusValueText.text = currentRec.Campus;
            degreeValueText.text = currentRec.Degree;
            degreeSpecialisationValueText.text = currentRec.DegreeSpecializations;
            successRateValueText.text = currentRec.Success_rate.ToString();

            // If secondary info is currently displayed, update its content too
            if (isSecondaryInfoDisplayed)
            {
                courseCodeValueText.text = currentRec.CourseCode.ToString();
                keySkillsValueText.text = currentRec.KeySkills;
                filterValueText.text = currentRec.Filter;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void NextCourse()
    {
        if (fileReader == null || fileReader.recommendations.Count == 0)
        {
            Debug.LogError("No recommendations to display.");
            return;
        }

        if (currentIndex < fileReader.recommendations.Count - 1)
        {
            currentIndex++;
            UpdateUI();
        }
    }

    public void PreviousCourse()
    {
        if (fileReader == null || fileReader.recommendations.Count == 0)
        {
            Debug.LogError("No recommendations to display.");
            return;
        }

        if (currentIndex > 0)
        {
            currentIndex--;
            UpdateUI();
        }
    }

    // Method to load a scene based on the current course
    public void LoadCourseScene()
    {
        // Here you would load the scene associated with the current course
        Recommendation currentRec = fileReader.recommendations[currentIndex];
        SceneManager.LoadScene("Scene_" + currentRec.CourseCode);
    }


    // Initialize the degree to config number mapping
    private void InitializeDegreeToConfigMapping()
    {
        degreeToConfigMapping = new Dictionary<string, int>()
        {
            { "Mechanical", 1111 },
            { "Computer Science Engineering", 2223 },
            { "Civil Engineering", 3332 },
            { "Electronics  Telecommunication Engineering", 4444 }
        };
    }

    // Method to be called when the "Select Course" button is pressed
    public void SelectCourseAndLoadVRLE()
    {
        string currentSpecialization = degreeSpecialisationValueText.text;

        if (degreeToConfigMapping.TryGetValue(currentSpecialization, out int configNumber))
        {
            // Config number found, now load the VRLE scene with this config number
            // Use PlayerPrefs, a singleton, or another method to pass the config number to the VRLE scene
            //PlayerPrefs is a simple way to save and load data between Unity scenes and sessions.Think of it as a small,
            //local database where you can set and get key - value pairs.It's often used for saving settings or preferences,
            //but you can also use it for simple communication between scenes, like in your case.
            PlayerPrefs.SetInt("StartConfigNumber", configNumber);
            PlayerPrefs.Save(); //to make sure the value is saved immediately

            // Load the VRLE scene
            SceneManager.LoadScene("VRLE");
        }
        else
        {
            // Handle the case where the specialization does not have a config number
            Debug.LogError("No config number found for the specialization: " + currentSpecialization);
        }
    }







}
