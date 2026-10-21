using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class CalculatorValidator : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_Text screenText;
    [SerializeField] private int maxDigits = 4;

    [Header("Difficulty")]
    [SerializeField] private int maxNumber = 12;
    [SerializeField] private float feedbackSeconds = 1f;

    public event Action Solved;   // the manager listens to this

    private string currentProblem;
    private int correctAnswer;
    private string playerInput = "";
    private bool acceptingInput = true;

    // Runs every time this calculator is shown, so it always starts with a fresh problem
    private void OnEnable()
    {
        if (screenText == null)
        {
            Debug.LogError("Screen Text is not assigned in the Inspector.", this);
            return;
        }
        GenerateNewProblem();
    }

    private void GenerateNewProblem()
    {
        string previous = currentProblem;

        do
        {
            int a = UnityEngine.Random.Range(1, maxNumber + 1);
            int b = UnityEngine.Random.Range(1, maxNumber + 1);

            switch (UnityEngine.Random.Range(0, 4))
            {
                case 0:
                    currentProblem = $"{a}+{b}";
                    correctAnswer = a + b;
                    break;
                case 1:
                    if (b > a) (a, b) = (b, a);
                    currentProblem = $"{a}-{b}";
                    correctAnswer = a - b;
                    break;
                case 2:
                    currentProblem = $"{a}x{b}";
                    correctAnswer = a * b;
                    break;
                default:
                    currentProblem = $"{a * b}÷{b}";
                    correctAnswer = a;
                    break;
            }
        } while (currentProblem == previous);

        playerInput = "";
        acceptingInput = true;
        RefreshScreen();
    }

    private void RefreshScreen()
    {
        screenText.text = $"{currentProblem}={playerInput}";
    }

    public void OnNumberButtonClicked(int digit)
    {
        if (!acceptingInput || playerInput.Length >= maxDigits) return;
        playerInput += digit.ToString();
        RefreshScreen();
    }

    public void OnClearButtonClicked()
    {
        if (!acceptingInput) return;
        playerInput = "";
        RefreshScreen();
    }

    public void OnEnterButtonClicked()
    {
        if (!acceptingInput) return;

        if (!int.TryParse(playerInput, out int guess))
        {
            Debug.LogWarning("Nothing entered.");
            return;
        }

        if (guess == correctAnswer)
        {
            StartCoroutine(ShowFeedbackThen("CORRECT", () =>
            {
                if (Solved != null) Solved();      // manager moves to the next calculator
                else GenerateNewProblem();         // no manager: just roll a new problem
            }));
        }
        else
        {
            StartCoroutine(ShowFeedbackThen("TRY AGAIN", () =>
            {
                playerInput = "";
                acceptingInput = true;
                RefreshScreen();
            }));
        }
    }

    private IEnumerator ShowFeedbackThen(string message, Action next)
    {
        acceptingInput = false;
        screenText.text = message;
        yield return new WaitForSeconds(feedbackSeconds);
        next();
    }
}