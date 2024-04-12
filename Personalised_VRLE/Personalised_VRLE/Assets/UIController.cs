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

    }

    // Method to set the UI Text elements with the current course's data
    private void UpdateUI()
    {
        // Safety check in case the index is out of range
        if (currentIndex >= 0 && currentIndex < fileReader.recommendations.Count)
        {
            Recommendation currentRec = fileReader.recommendations[currentIndex];
            courseCodeValueText.text = currentRec.CourseCode.ToString();
            campusValueText.text = currentRec.Campus;
            degreeValueText.text = currentRec.Degree;
            degreeSpecialisationValueText.text = currentRec.DegreeSpecializations;
            keySkillsValueText.text = currentRec.KeySkills;
            filterValueText.text = currentRec.Filter;
            successRateValueText.text = currentRec.Success_rate.ToString();

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

}
