using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;

public class UIManagerInnerHut : MonoBehaviour
{
    public GameData gameData;
    public SceneController sceneController;
    
    [Header("This area is for button clue")]
    public Button clueBtn1;
    public Button clueBtn2;
    public Button clueBtn3;
    public Button clueBtn4;
    public Button clueBtn5;
    public Button clueBtn6;

    [Header("This area is for non puzzle clue panel")]
    public RectTransform clue1;
    public RectTransform clue2;
    public RectTransform clue3;
    
    [Header("This area is for other setting")]
    public Button sleepBtn;
    public GameObject bedBeforeKeyPanel;
    public GameObject bedAfterPanel;
    public GameObject btnBeforeSleep;
    public GameObject btnAfterSleep;
    public Image backgroundImg;
    public Sprite afterWakeUpSprite;
    
    //this is to mark the pauzzle that already solve so whenever player reopen the puzzle the answer menu is open directly
    private bool clue1On = false;
    private bool clue2On = false;
    private bool clue3On = false;
    private bool clue4On = false;
    private bool clue5On = false;

    //new system here
    public float tweenDelay = 1f;
    public List<RectTransform> openWindow = new List<RectTransform>();

    private void Awake()
    {
        // Find game data object in the begining of the scene
        gameData = GameObject.FindGameObjectWithTag("data").GetComponent<GameData>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
        clueBtn1.onClick.AddListener(() => NonPuzzleClue(clue1));
        clueBtn2.onClick.AddListener(() => NonPuzzleClue(clue2));
        clueBtn3.onClick.AddListener(() => NonPuzzleClue(clue3));
        clueBtn4.onClick.AddListener(() => NonPuzzleClue(clue1));
        clueBtn5.onClick.AddListener(() => NonPuzzleClue(clue2));
        clueBtn6.onClick.AddListener(() => NonPuzzleClue(clue3));
        sleepBtn.onClick.AddListener(() => AfterWakeUp());

        CheckVision();
    }

    
    public void NonPuzzleClue(RectTransform panel)
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

    public void AfterWakeUp()
    {
        gameData.innerHutPuzzleOpen[2] = true;
        
        bedAfterPanel.SetActive(true);
        bedBeforeKeyPanel.SetActive(false);
        backgroundImg.sprite = afterWakeUpSprite;
        btnBeforeSleep.SetActive(false);
        btnAfterSleep.SetActive(true);
    }

    void CheckVision()
    {
        var visionOpen = gameData.innerHutPuzzleOpen[2];

        if (visionOpen)
        {
            bedAfterPanel.SetActive(true);
            bedBeforeKeyPanel.SetActive(false);
            backgroundImg.sprite = afterWakeUpSprite;
            btnBeforeSleep.SetActive(false);
            btnAfterSleep.SetActive(true);
        }
    }
    
}
