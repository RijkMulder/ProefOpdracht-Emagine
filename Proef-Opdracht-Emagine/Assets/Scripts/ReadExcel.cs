using System;
using UnityEngine;
using System.Text.RegularExpressions;

[Serializable]
public class QuizQuestion
{
    public string question;
    public string[] options;
    public string answer;
    public string points;
    public QuizQuestion(string question, string[] options, string answer, string points)
    {
        this.question = question;
        this.options = options;
        this.answer = answer;
        this.points = points;
    }
}
public class ReadExcel : MonoBehaviour
{
    public static ReadExcel Instance { get; private set; }
    
    [SerializeField] private TextAsset excelFile;
    [SerializeField] private QuizQuestion[] questions;

    public QuizQuestion[] ReadCSV()
    {
        // set pattern to split the csv file
        string pattern = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
        string[] lines = excelFile.text.Split('\n');
    
        // make array of questions
        QuizQuestion[] quizQuestions = new QuizQuestion[lines.Length - 1];
    
        // loop through each line and split data
        for (int i = 1; i < lines.Length; i++)
        {
            string[] data = Regex.Split(lines[i], pattern);
        
            for (int j = 0; j < data.Length; j++)
            {
                data[j] = data[j].Trim('"');
            }
        
            quizQuestions[i - 1] = new QuizQuestion(
                data[0], 
                new string[] { data[1], data[2], data[3], data[4] },
                data[5], 
                data[6]
            );
        }

        questions = quizQuestions;
        return quizQuestions;
    }
}
