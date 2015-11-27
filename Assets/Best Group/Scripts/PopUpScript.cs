using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PopUpScript : MonoBehaviour {

    public GameObject window;
    public Text messageField;

    void Start()
    {
        //window = this.gameObject;
    }

    public void Show(string message)
    {
        messageField.text = message;
        window.SetActive(true);
    }

    public void Hide()
    {
        window.SetActive(false);
    }
}
