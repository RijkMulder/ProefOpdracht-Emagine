using UnityEngine;
using UnityEngine.UIElements;

public class MakeQuiz : MonoBehaviour
{
    [SerializeField] private VisualTreeAsset questionTemplate;
    [SerializeField] TrackQuiz trackQuiz;
    [SerializeField] ReadExcel readExcel;

    private void OnEnable()
    {
        // get root visual element
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        VisualElement rootBg = root.Q<VisualElement>("bg");
        VisualElement questionContainer = new VisualElement();
        
        // submit button
        Button submitButton = root.Q<Button>("submit");
        submitButton.RegisterCallback<ClickEvent>(evt => trackQuiz.CheckAnswers());
        // set element vertical
        questionContainer.style.flexDirection = FlexDirection.Column;
        rootBg.Add(questionContainer);
        
        // add all questions to conatainer
        PlaceQuizAssets(readExcel.ReadCSV(), questionContainer);
    }

    private void PlaceQuizAssets(QuizQuestion[] questions, VisualElement container)
    {
        foreach (QuizQuestion question in questions)
        {
            // clone template
            VisualElement questionElement = questionTemplate.CloneTree();
            
            // set question text
            questionElement.Q<Label>("question").text = question.question;
            
            // set options text
            for (int i = 0; i < question.options.Length; i++)
            {
                RadioButton button = questionElement.Q<RadioButton>($"option{i}");
                button.text = question.options[i];
                trackQuiz.quizAnswers.Add(button, question);
            }
            
            // add question to container
            container.Add(questionElement);
        }
    }
}
