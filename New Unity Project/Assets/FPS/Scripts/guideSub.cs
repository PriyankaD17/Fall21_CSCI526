using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class guideSub : MonoBehaviour
{
    public GameObject textBox;

    void Start(){
        StartCoroutine(theSequence());
    }

    IEnumerator theSequence(){
        yield return new WaitForSeconds(4);
        textBox.GetComponent<TMPro.TextMeshProUGUI>().text = "Welcome to “Aincrad”, the magic world.";
        yield return new WaitForSeconds(3);
        textBox.GetComponent<TMPro.TextMeshProUGUI>().text = "In this world, you have to beat all the bosses to get back to the real life.";
        yield return new WaitForSeconds(4);
        textBox.GetComponent<TMPro.TextMeshProUGUI>().text = "If you die in this world, you will die physically.";
        yield return new WaitForSeconds(2);
        textBox.GetComponent<TMPro.TextMeshProUGUI>().text = "";
        yield return new WaitForSeconds(4);
        textBox.GetComponent<TMPro.TextMeshProUGUI>().text = "Use the left button to control your movements and right buttons to shoot and jump.";
        yield return new WaitForSeconds(5);
        textBox.GetComponent<TMPro.TextMeshProUGUI>().text = "Once you collect new weapons you can use the right middle buttons to switch.";
        yield return new WaitForSeconds(5);
        textBox.GetComponent<TMPro.TextMeshProUGUI>().text = "At the start of the game, you have a gun and use it to attack enemies.";
        yield return new WaitForSeconds(5);
        textBox.GetComponent<TMPro.TextMeshProUGUI>().text = "Good luck and Hope to see you soon!";
        yield return new WaitForSeconds(2);
        textBox.GetComponent<TMPro.TextMeshProUGUI>().text = "";
    }
}