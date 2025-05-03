using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class PuzzleRadio : MonoBehaviour {
    [SerializeField] List<Button> buttons = new List<Button>();
    float highlightDuration = 0.5f;
    float timeBetweenHighlights = 0.25f;
    [SerializeField] Color highlightedColor;
    [SerializeField] Image pantallaVictoria;
    [SerializeField] Image puzzle;
    [SerializeField] Color normalColor;
    int maxRounds = 3;
    [SerializeField] InteractableRadio interactableRadio;

    List<int> sequence = new List<int>();
    List<int> playerInput = new List<int>();
    int currentSequenceIndex = 0;
    int currentRound = 0;
    bool playerTurn = false;
    bool isCompleted = false;

    private void Start()
    {
        InitializeButtons();
    }


    void InitializeButtons()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            int index = i;
            buttons[i].onClick.AddListener(() => OnButtonPressed(index));
            buttons[i].image.color = normalColor;
        }
    }

    void StartNewRound()
    {
        if (currentRound >= maxRounds) return;

        playerTurn = false;
        GenerateNewSequence(currentRound + 1); 
        currentRound++;
        StartCoroutine(PlaySequence());
    }
    void GenerateNewSequence(int roundNumber)
    {
        sequence.Clear();
        int sequenceLength = roundNumber * 2; 

        for (int i = 0; i < sequenceLength; i++)
        {
            sequence.Add(Random.Range(0, buttons.Count));
        }
    }

    IEnumerator PlaySequence()
    {
        yield return new WaitForSecondsRealtime(1f);

        foreach (int index in sequence)
        {
            yield return HighlightButton(index);
            yield return new WaitForSecondsRealtime(timeBetweenHighlights);
        }

        EnablePlayerInput();
    }

    IEnumerator HighlightButton(int index)
    {
        buttons[index].image.color = highlightedColor;
        yield return new WaitForSecondsRealtime(highlightDuration);
        buttons[index].image.color = normalColor;
    }

    void EnablePlayerInput()
    {
        playerInput.Clear();
        currentSequenceIndex = 0;
        playerTurn = true;
    }

    void OnButtonPressed(int buttonIndex)
    {
        if (!playerTurn || isCompleted) return;

        StartCoroutine(HighlightButton(buttonIndex));
        playerInput.Add(buttonIndex);

        if (playerInput[currentSequenceIndex] != sequence[currentSequenceIndex])
        {
            PuzzleFailed();
            return;
        }

        currentSequenceIndex++;

        if (currentSequenceIndex >= sequence.Count)
        {
            PuzzleCompleted();
        }
    }
    void OnEnable()
    {
        Time.timeScale = 0f;
        playerTurn = false;
        currentRound = 0;
        sequence.Clear();
        playerInput.Clear();
        currentSequenceIndex = 0;
        foreach (Button button in buttons)
        {
            button.image.color = normalColor;
        }
        StartNewRound();
    }
    void OnDisable()
    {
        Time.timeScale = 1f;
    }

    void PuzzleCompleted()
    {
        playerTurn = false;

        if (currentRound >= maxRounds)
        {
            isCompleted = true;
            PantallaVictoria();
            return;
        }

        StartCoroutine(NextRound());
    }

    IEnumerator NextRound()
    {
        yield return new WaitForSecondsRealtime(1f);
        StartNewRound();
    }

    void PuzzleFailed()
    {
        playerTurn = false;
        currentRound = 0;
        sequence.Clear();
        StartCoroutine(RestartGame());
    }


    IEnumerator RestartGame()
    {
        yield return new WaitForSecondsRealtime(2f);
        StartNewRound();
    }

    void PantallaVictoria()
    {
        puzzle.gameObject.SetActive(false);
        pantallaVictoria.gameObject.SetActive(true);
    }
    public void TrapoPantallaVictoria()
    {
        StartCoroutine(TrapoPantallaVictoriaCR());
    }
    IEnumerator TrapoPantallaVictoriaCR()
    {
        interactableRadio.SetPuzzleCompleto(true);
        yield return new WaitForSecondsRealtime(1f);
        SalirPuzzle();
    }
    public void SalirPuzzle()
    {
        StopAllCoroutines();

        playerTurn = false;
        currentRound = 0;
        sequence.Clear();
        playerInput.Clear();
        currentSequenceIndex = 0;
        foreach (Button button in buttons)
        {
            button.image.color = normalColor;
        }
        this.gameObject.SetActive(false);

    }
}
