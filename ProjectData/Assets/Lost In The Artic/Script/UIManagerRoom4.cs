using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;

/// <summary>
/// This script is used to control user interface of the game suach as game panel, animation, user input and warning
/// </summary>

public class UIManagerRoom4 : MonoBehaviour
{
    public GameData gameData;
    public SceneController sceneController;
    
    [Header("This area is for button clue")]
    public Button cluePuzzleBtn1;
    public Button enterCaveBtn;

    [Header("This area is for puzzle clue panel")]
    public RectTransform puzzleClue1;
    
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
        enterCaveBtn.onClick.AddListener(() => CheckButton());
    }

    void CheckButton()
    {
        GameData.instance.room4PuzzleOpen[0] = true;
        sceneController.OpenScene("Inner Cave");

    }
    
    // This is a fungtion to activate a clue window 3
    public void ClueWithPuzzle1(RectTransform panel)
    {
        ClosePanel();
        OpenPanel(panel);
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
