using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;

/// <summary>
/// This script is used to control user interface of the game suach as game panel, animation, user input and warning
/// </summary>

public class UIManagerRoom3 : MonoBehaviour
{
    public GameData gameData;
    public SceneController sceneController;

    [Header("This area is for button clue")]
    public Button cluePuzzleBtn1;
    public Button cluePuzzleBtn2;
    public Button cluePuzzleBtn3;
    public Button clueBtn1;
    public Button clueBtn2;

    [Header("This area is for puzzle clue panel")]
    public RectTransform puzzleClue1;

    [Header("This area is for non puzzle clue panel")]
    public RectTransform clue1;
    public RectTransform clue2;

    [Header("This area is for all correct panel")]
    public RectTransform answerPanel1;
    
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
        cluePuzzleBtn2.onClick.AddListener(() => UseRock());
        cluePuzzleBtn3.onClick.AddListener(() => EnteringChurch());

        clueBtn1.onClick.AddListener(() => PickUpStone(clue1));
        clueBtn2.onClick.AddListener(() => NonPuzzleClue(clue2));

        OpenDoor();
    }

    void PickUpStone(RectTransform panel)
    {
        GameData.instance.room3PuzzleOpen[0] = true;
        cluePuzzleBtn2.interactable = true;
        ClosePanel();
        OpenPanel(panel);
    }
    
    // This is a function to activate a clue window 2
    public void UseRock()
    {
        GameData.instance.room3PuzzleOpen[1] = true;
        OpenDoor();
        sceneController.OpenScene("Inner Church");
    }

    public void EnteringChurch()
    {
        sceneController.OpenScene("Inner Church");
    }

    public void ClueWithPuzzle1(RectTransform panel)
    {
        ClosePanel();
        OpenPanel(panel);
    }

    // This is a fungtion to activate a clue window 3
    public void NonPuzzleClue(RectTransform panel)
    {
        ClosePanel();
        OpenPanel(panel);
    }
    
    public void OpenDoor()
    {
        var data = gameData.room3PuzzleOpen[1];

        if (data)
        {
            cluePuzzleBtn1.gameObject.SetActive(false);
            cluePuzzleBtn3.gameObject.SetActive(true);
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
