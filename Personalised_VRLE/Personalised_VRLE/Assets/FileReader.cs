using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileReader : MonoBehaviour
{
    //to store the information 
    public List<Recommendation> recommendations = new List<Recommendation>();

    //using this for initialisation
    void Awake()
    {
        TextAsset recommendationstext = Resources.Load<TextAsset>("final_recommendations_user_1001_course_recommendations");

        //text assest class enabling us to access the data in a string
        //creating an array - splitting based on the new line
        string[] data = recommendationstext.text.Split(new char[] { '\n' });

        //Debug.Log(data.Length); - was used to check if it reads the extra line in the csv file 

        //reading and parsing each line individually (note avoiding the first (of the fields) and the last line)
        for (int i = 1; i < data.Length - 1; i++)
        {
            // Using regular expressions to split the row by commas that are not within quotes.
            string[] row = System.Text.RegularExpressions.Regex.Split(data[i],
                                ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");

            // Trim each element to remove unwanted white spaces
            for (int j = 0; j < row.Length - 1; j++)
            {
                row[j] = row[j].Trim();
            }

            Recommendation r = new Recommendation();
            int.TryParse(row[0], out r.CourseCode);
            r.Degree = row[1].Replace("\"", ""); // Remove quotes
            r.DegreeSpecializations = row[2].Replace("\"", "");
            r.Campus = row[3].Replace("\"", "");
            r.KeySkills = row[4].Replace("\"", ""); // Remove quotes from the skills
            r.Filter = row[5].Replace("\"", "");

            float.TryParse(row[6].Replace("%", "").Replace("\"", ""), out r.Success_rate); //to convert the string to float, if there is data

            //adding the recommendation to the list
            recommendations.Add(r);
        }

 /*       //to see if it is working
        foreach (Recommendation r in recommendations)
        {
            Debug.Log(r.CourseCode + ", " + r.Degree + ", " + r.DegreeSpecializations + ", " + r.Campus + ", " + r.KeySkills + ", " + r.Filter + ", " + r.Success_rate);
        }*/
    }
    private void Start()
    {
        
    }
    void Update()
    {

    }
}