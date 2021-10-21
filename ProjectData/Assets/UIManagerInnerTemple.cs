using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;

public class UIManagerInnerTemple : MonoBehaviour
{
    public GameData gameData;
    public SceneController sceneController;

    public Image backgroundImg;
    public Sprite visionSprite;

    [Header("This area is for puzzle clue panel")]
    public Button cluePuzzleBtn1;
    public Button afterVisionPuzzleBtn;

    [Header("This area is for button clue")]
    public Button clueBtn1;
    public Button answerBtn;
    

    [Header("This area is for puzzle clue panel")]
    public RectTransform puzzleClue1;

    [Header("This area is for non puzzle clue panel")]
    public RectTransform seal;
    public RectTransform vision;
    public RectTransform NewLocation;

    [Header("This area is for the puzzle input")]
    public InputField puzzleInput1;
    public Text notificationDisplay;

    [Header("This part is for the puzzle answer, make sure you add the answer for your puzzle")]
    public string[] puzzleAnswer1;

    [Header("Check this variable if this is the last puzzle room in the game")]
    public bool lastRoom = false;

    private bool clue1On = false;
    private bool clue2On = false;
    private bool clue3On = false;
    private bool clue4On = false;
    private bool clue5On = false;

    //new system here
    public float tweenDelay = 1f;
    public List<RectTransform> openWindow = new List<RectTransform>();

    // Start is called before the first frame update
    void Start()
    {
        // Find game data object in the begining of the scene
        gameData = GameObject.FindGameObjectWithTag("data").GetComponent<GameData>();

        cluePuzzleBtn1.onClick.AddListener(() => ClueWithPuzzle1(puzzleClue1));
        afterVisionPuzzleBtn.onClick.AddListener(() => ClueWithPuzzle1(puzzleClue1));

        clueBtn1.onClick.AddListener(() => BreakingSeal(vision));
        answerBtn.onClick.AddListener(() => Answer(NewLocation));
    }

    // This is a fungtion to check puzzle 1 answer, if correct a new clue will be open and player can advance to the new map
    public void CheckingAnswer1(bool canAdvance)
    {
        var a = 0;

        for (int i = 0; i < puzzleAnswer1.Length; i++)
        {
            if (puzzleInput1.text.Equals(puzzleAnswer1[i], StringComparison.OrdinalIgnoreCase))
            {
                a++;
                break;
            }
        }

        if (a > 0)
        {
            SwitchCorrect1(true);
            GameData.instance.innerTemplePuzzleOpen[0] = true;
        }
        else
        {
            StartCoroutine(AnswerFalse1());
        }

    }

    // This is a fungtion to activate a clue window 1
    public void ClueWithPuzzle1(RectTransform panel)
    {
        bool isComplete = GameData.instance.innerTemplePuzzleOpen[0];

        if (isComplete)
        {
            SwitchCorrect1(true);
        }
        else
        {
            ClosePanel();
            OpenPanel(panel);
        }
    }

    // This is a fungtion to activate a clue window 3
    public void Answer(RectTransform panel)
    {
        var breaking = GameData.instance.innerTemplePuzzleOpen[1];

        if (!breaking)
        {
            AnswerCorrect(true);
            GameData.instance.innerTemplePuzzleOpen[1] = true;
        }

        ClosePanel();
        OpenPanel(panel);
    }

    public void BreakingSeal(RectTransform panel)
    {
        backgroundImg.sprite = visionSprite;
        cluePuzzleBtn1.gameObject.SetActive(false);
        afterVisionPuzzleBtn.gameObject.SetActive(true);
        ClosePanel();
        OpenPanel(panel);
    }

    //This function is used to increase the game progression data and close all clue window the is open.
    //It is also serve to check if the last puzzle already solve or not, if yes then the game finish time will be calculated
    public void AnswerCorrect(bool continueMap)
    {
        if (continueMap && !lastRoom)
        {
            gameData.gamePhase++;
        }

        if (clue1On)
        {
            clue1On = false;
        }
        else if (clue2On)
        {
            clue2On = false;
        }
    }

    //This coroutine is used to add a message for the player when player put a wrong answer in puzzle 1 input
    IEnumerator AnswerFalse1()
    {
        string prev = notificationDisplay.text;

        notificationDisplay.text = "Nothing happened";
        notificationDisplay.color = Color.red;

        yield return new WaitForSeconds(1);
        notificationDisplay.text = prev;
        notificationDisplay.color = Color.black;
    }

    // This function is used to activate a clue when puzzle 1 answer is correct
    public void SwitchCorrect1(bool isActive)
    {
        if (isActive)
        {
            ClosePanel();
            OpenPanel(seal);
            clue1On = true;
        }
        else
        {
            ClosePanel();
        }
    }

    void OpenPanel(RectTransform rt)
    {
        rt.DOAnchorPos(Vector2.zero, tweenDelay);
        openWindow.Add(rt);
    }

    public void ClosePanel()
    {
        for (int i = 0; i < openWindow.Count; i++)
        {
            openWindow[i].DOAnchorPos(new Vector2(0, 2000), tweenDelay);
        }

        openWindow.Clear();
    }

}
