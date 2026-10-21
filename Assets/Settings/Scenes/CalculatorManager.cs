using UnityEngine;

public class CalculatorManager : MonoBehaviour
{
    [SerializeField] private CalculatorValidator[] calculators;
    [SerializeField] private bool randomOrder = false;

    private int current = -1;

    private void Awake()
    {
        foreach (var calc in calculators)
        {
            calc.Solved += ShowNext;
            calc.gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        ShowNext();
    }

    private void OnDestroy()
    {
        foreach (var calc in calculators)
            if (calc != null) calc.Solved -= ShowNext;
    }

    private void ShowNext()
    {
        if (calculators.Length == 0) return;

        // Hide the one currently showing
        if (current >= 0) calculators[current].gameObject.SetActive(false);

        if (randomOrder && calculators.Length > 1)
        {
            int next;
            do { next = Random.Range(0, calculators.Length); } while (next == current);
            current = next;
        }
        else
        {
            current = (current + 1) % calculators.Length;
        }

        calculators[current].gameObject.SetActive(true);
    }
}