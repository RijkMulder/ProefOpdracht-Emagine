using System;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;

public class TrackQuiz : MonoBehaviour
{
    public static TrackQuiz Instance { get; private set; }
    public Dictionary<RadioButton, QuizQuestion> quizAnswers = new();

    private void Awake()
    {
        Instance = this;
    }
    
    public void CheckAnswers()
    {
        int score = 0;
        int maxScore = 0;
        
        foreach (KeyValuePair<RadioButton, QuizQuestion> answer in quizAnswers)
        {
            if (!answer.Key.value) continue;
            
            string correctAnswer = answer.Value.answer;
            int points = int.Parse(answer.Value.points);
            maxScore += points;
            if (answer.Key.label == correctAnswer)
            {
                score += points;
            }
        }
        Debug.Log($"Score: {score} / {maxScore}");
    }
}
