using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[Serializable]
enum DialogState
{
    Empty,
    Advancing,
    Done,
}

public class DialogSystem : MonoBehaviour
{
    public static DialogSystem Instance { get; private set; }

    public TMPro.TMP_Text Text;
    public GameObject XButtonImage;
    public GameObject Panel;
    public AudioSource AudioSource;

    public int NthSound = 2;
    public float CharacterDelay = 0.05f;

    private Coroutine currentShowText;
    private string currentText;

    private Queue<string> textQueue = new Queue<string>();

    private DialogState state;

    private void Awake()
    {
        Instance = this;
        state = DialogState.Empty;
    }

    private void Update()
    {
        var gamepad = Gamepad.current;
        if (gamepad != null)
        {
            if (gamepad.circleButton.wasPressedThisFrame)
            {
                if (currentShowText != null)
                    StopCoroutine(currentShowText);
                state = DialogState.Empty;
                textQueue.Clear();
            }
            else if (gamepad.crossButton.wasPressedThisFrame)
            {
                AdvanceDialog();
            }
        }

        XButtonImage.SetActive(state == DialogState.Done);
        switch (state) {
            case DialogState.Advancing:
                Panel.SetActive(true);
                break;

            case DialogState.Done:
                Panel.SetActive(true);
                break;

            case DialogState.Empty when textQueue.Count > 0:
                Panel.SetActive(true);
                currentShowText = StartCoroutine(ShowText(textQueue.Dequeue()));
                break;

            case DialogState.Empty:
                Panel.SetActive(false);
                break;
        }
    }

    public void QueueText(string text)
    {
        textQueue.Enqueue(text);
    }

    private IEnumerator ShowText(string text)
    {
        state = DialogState.Advancing;
        currentText = text;
        for (int i = 0; i < text.Length; i++)
        {
            Text.text = text.Substring(0, i + 1);

            if (i % NthSound == 0)
                AudioSource.Play();
            yield return new WaitForSeconds(CharacterDelay);
        }

        state = DialogState.Done;
        currentShowText = null;
        currentText = null;
    }

    public void AdvanceDialog()
    {
        switch (state) {
            case DialogState.Advancing:
                StopCoroutine(currentShowText);
                Text.text = currentText;
                state = DialogState.Done;
                break;

            case DialogState.Done:
                Text.text = "";
                state = DialogState.Empty;
                break;

            case DialogState.Empty:
                break;
        }
    }
}
