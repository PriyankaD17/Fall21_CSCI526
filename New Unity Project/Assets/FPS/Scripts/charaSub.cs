using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class charaSub : MonoBehaviour
{
    public GameObject textBox;

    void Start()
    {
        StartCoroutine(theSequence());
    }

    IEnumerator theSequence(){
        //yield return new WaitForSeconds(1);
        textBox.GetComponent<TMPro.TextMeshProUGUI>().text = "Where am I?";
        yield return new WaitForSeconds(2);
        textBox.GetComponent<TMPro.TextMeshProUGUI>().text = "Who am I?";
        yield return new WaitForSeconds(1);
        textBox.GetComponent<TMPro.TextMeshProUGUI>().text = "";
        yield return new WaitForSeconds(11);
        textBox.GetComponent<TMPro.TextMeshProUGUI>().text = "...... What the hell is that?";
        yield return new WaitForSeconds(2);
        textBox.GetComponent<TMPro.TextMeshProUGUI>().text = "";
        yield return new WaitForSeconds(19);
        textBox.GetComponent<TMPro.TextMeshProUGUI>().text = "......";
        yield return new WaitForSeconds(2);
        textBox.GetComponent<TMPro.TextMeshProUGUI>().text = "Ok, let's get started.";
        yield return new WaitForSeconds(2);
        textBox.GetComponent<TMPro.TextMeshProUGUI>().text = "";
    }
}
