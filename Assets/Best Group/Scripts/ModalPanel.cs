using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

//  This script will be updated in Part 2 of this 2 part series.
public class ModalPanel : MonoBehaviour
{

    public Text question;
    public GameObject modalPanelObject;
    public Canvas canvas;
    private CanvasGroup cg;

    private IEnumerator fadeAlpha;

    private float displayTime;
    private float fadeTime;

    private static ModalPanel modalPanel;

    public static ModalPanel Instance()
    {
        if (!modalPanel)
        {
            modalPanel = FindObjectOfType(typeof(ModalPanel)) as ModalPanel;
            if (!modalPanel)
                Debug.LogError("There needs to be one active ModalPanel script on a GameObject in your scene.");
        }

        return modalPanel;
    }

    void Awake()
    {
        cg = canvas.GetComponent<CanvasGroup>();
    }

    // Yes/No/Cancel: A string, a Yes event, a No event and Cancel event
    public void Choice(string question, float dispTime, float fdTime)
    {
        modalPanelObject.SetActive(true);

        this.displayTime = dispTime;
        this.fadeTime = fdTime;

        this.question.text = question;

        SetAlpha();

    }

    void SetAlpha()
    {
        if (fadeAlpha != null)
        {
            StopCoroutine(fadeAlpha);
        }
        fadeAlpha = FadeAlpha();
        StartCoroutine(fadeAlpha);
    }

    IEnumerator FadeAlpha()
    {
        cg.alpha = 1;

        yield return new WaitForSeconds(displayTime);

        while (cg.alpha > 0)
        {
            cg.alpha -= Time.deltaTime / fadeTime;

            yield return null;
        }
        yield return null;
    }

    void ClosePanel()
    {
        modalPanelObject.SetActive(false);
    }
}