using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

[System.Serializable]
public class ButtonSpritePair {
    public Sprite normalSprite;
    public Sprite highlightedSprite;
}

public class PuzzleRadio : MonoBehaviour {
    [SerializeField] CameraController cameraController;
    [SerializeField] List<ButtonSpritePair> buttonSprites = new List<ButtonSpritePair>();
    [SerializeField] List<Button> buttons = new List<Button>();
    float highlightDuration = 0.5f;
    float timeBetweenHighlights = 0.25f;
    AudioSource audioSource;
    [SerializeField] AudioClip sonidoLuz;
    [SerializeField] AudioClip sonidoFALLO;
    [SerializeField] Image pantallaVictoria;
    [SerializeField] Image puzzle;
    int maxRounds = 2;
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
        audioSource = GetComponent<AudioSource>();
    }


    void InitializeButtons()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            int index = i;
            buttons[i].onClick.AddListener(() => OnButtonPressed(index));
            buttons[i].image.sprite = buttonSprites[index].normalSprite; 
        }
    }

    void StartNewRound()
    {
        if (currentRound >= maxRounds) return;

        playerTurn = false;
        GenerateNewSequence(); 
        currentRound++;
        StartCoroutine(PlaySequence());
    }
    void GenerateNewSequence()
    {
        sequence.Clear();
        int sequenceLength = 4; 

        for (int i = 0; i < sequenceLength; i++)
        {
            sequence.Add(Random.Range(0, buttons.Count));
        }
    }

    IEnumerator PlaySequence()
    {
        yield return new WaitForSeconds(1f);

        foreach (int index in sequence)
        {
            yield return HighlightButton(index);
            
            yield return new WaitForSeconds(timeBetweenHighlights);
            
        }

        EnablePlayerInput();
    }

    IEnumerator HighlightButton(int index)
    {
        
        buttons[index].image.sprite = buttonSprites[index].highlightedSprite;
        audioSource.clip = sonidoLuz;
        audioSource.Play();
        yield return new WaitForSeconds(highlightDuration);
        
        buttons[index].image.sprite = buttonSprites[index].normalSprite;
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
        playerTurn = false;
        GameManager.Instance.GestionarCambioRaton(false);
        if (cameraController != null)
            cameraController.ToggleCameraControl(false);
        PlayerController.Instance.TogglePlayerControl(false);
        currentRound = 0;
        sequence.Clear();
        playerInput.Clear();
        currentSequenceIndex = 0;
        foreach (Button button in buttons)
        {
            int index = buttons.IndexOf(button);
            button.image.sprite = buttonSprites[index].normalSprite;
        }
        StartNewRound();
    }
    private void OnDisable()
    {
        if (cameraController != null)
            cameraController.ToggleCameraControl(true);
        PlayerController.Instance.TogglePlayerControl(true);
        GameManager.Instance.GestionarCambioRaton(true);
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
        yield return new WaitForSeconds(1f);
        StartNewRound();
    }

    void PuzzleFailed()
    {
        audioSource.clip = sonidoFALLO;
        audioSource.Play();
        playerTurn = false;
        currentRound = 0;
        sequence.Clear();
        StartCoroutine(RestartGame());
        
    }


    IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(2f);
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
        yield return new WaitForSeconds(1f);
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
            int index = buttons.IndexOf(button);
            button.image.sprite = buttonSprites[index].normalSprite;
        }
        this.gameObject.SetActive(false);

    }
}
