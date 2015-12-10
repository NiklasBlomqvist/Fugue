using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

//  This script will be updated in Part 2 of this 2 part series.
public class EndPanel : MonoBehaviour
{

    public Text question;
    public GameObject endPanelObject;
    public Canvas canvas;
    private CanvasGroup cg;

    private IEnumerator fadeAlpha;

    private float displayTime;
    private float fadeTime;
    private float startTime;

    private static EndPanel endPanel;

    public static EndPanel Instance()
    {
        if (!endPanel)
        {
            endPanel = FindObjectOfType(typeof(EndPanel)) as EndPanel;
            if (!endPanel)
                Debug.LogError("There needs to be one active EndPanel script on a GameObject in your scene.");
        }

        return endPanel;
    }

    void Awake()
    {
        cg = canvas.GetComponent<CanvasGroup>();
    }

    // Yes/No/Cancel: A string, a Yes event, a No event and Cancel event
    public void ShowEndCard(string question, float stTime, float dispTime, float fdTime)
    {

        GameObject.Find("Camera").GetComponent<Crosshair>().crosshairTextureActive = null;
        GameObject.Find("Camera").GetComponent<Crosshair>().crosshairTextureNonActive = null;

        this.displayTime = dispTime;
        this.fadeTime = fdTime;
        this.startTime = stTime;

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
        yield return new WaitForSeconds(startTime);
        endPanelObject.SetActive(true);
        yield return new WaitForSeconds(displayTime);

        Application.LoadLevel(0);
        yield return null;
    }

    void ClosePanel()
    {
        endPanelObject.SetActive(false);
    }
}