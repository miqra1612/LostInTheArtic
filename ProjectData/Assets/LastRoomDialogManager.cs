using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class LastRoomDialogManager : MonoBehaviour
{
    public GameData gameData;
    public SceneController sceneController;

    public RectTransform dialogPanel;
    public TextMeshProUGUI dialogScreen;
    public float dialogSpeed = 0.01f;

    public Button continueBtn;
    public Button nextDialogBtn;

    public string sceneName = "Winner Room";
    public float tweenDelay = 1f;
    public bool lastRoom = false;

    public List<DialogData> dialogDatas;
    private bool firstTime = true;
    private bool advanceDialogue = false;
    private int id = 0;
    
    // Start is called before the first frame update

    private void Start()
    {
        gameData = GameObject.FindGameObjectWithTag("data").GetComponent<GameData>();

        continueBtn.onClick.AddListener(() => StartDialog());
        nextDialogBtn.onClick.AddListener(() => UpdateDialog());
    }

    public void StartDialog()
    {
        dialogPanel.DOAnchorPos(Vector2.zero, tweenDelay);
        StartCoroutine(Dialog(id));
    }
    
    // Update is called once per frame
    public void UpdateDialog()
    {
        //if (advanceDialogue)
        //{

        //}

        StopAllCoroutines();

        if (id < dialogDatas.Count - 1)
        {
            id++;
            StartCoroutine(Dialog(id));
        }
        else
        {
            StartCoroutine(CalculateFinishTime());
        }
    }

    IEnumerator Dialog(int id)
    {
        var dialog = dialogDatas[id].dialog;

        dialogScreen.text = string.Empty;

        advanceDialogue = false;

        if (firstTime)
        {
            firstTime = false;
            yield return new WaitForSeconds(2);
        }

        foreach (char a in dialog)
        {
            dialogScreen.text += a;
            yield return new WaitForSeconds(dialogSpeed);
        }

        advanceDialogue = true;
    }

    IEnumerator CalculateFinishTime()
    {
        if (lastRoom)
        {
            gameData.CalculateFinishTime();
            yield return new WaitForSeconds(0.5f);
            sceneController.OpenScene(sceneName);
        }
    }
}
