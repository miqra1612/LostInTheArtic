using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class IntroDialogManager : MonoBehaviour
{
    public SceneController sceneController;
    public Button continueBtn;

    public TextMeshProUGUI dialogScreen;
    public float dialogSpeed = 0.01f;
    private int id = 0;
    public List<DialogData> introDialogue;
    private bool firstTime = true;
    private bool advanceDialogue = false;

    // Start is called before the first frame update
    void Start()
    {
        continueBtn.onClick.AddListener(() => StartDialogue());
    }

    public void StartDialogue()
    {
        StartCoroutine(Dialog(id));
    }

    // Update is called once per frame
    public void UpdateDialog()
    {
        if (advanceDialogue)
        {
            if (id < introDialogue.Count-1)
            {
                id++;
                StartCoroutine(Dialog(id));
            }
            else
            {
                sceneController.OpenScene("Map");
            }
        }
    }

    IEnumerator Dialog(int id)
    {
        var dialog = introDialogue[id].dialog;

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
}
