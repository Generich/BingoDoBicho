using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextMovement : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    public float textSpeed = 0.02f;

    public bool isShowing { get; private set; }

    private IEnumerator effectCoroutine;
    public GameObject dialogue;

    public void ShowTextLetterByLetter(string textoDoDialogo)
    {
        text.text = textoDoDialogo;
        effectCoroutine = EffectLetterByLetter();
        StartCoroutine(effectCoroutine);
        isShowing = true;
    }

    public void ShowAllText(){
        StopCoroutine(effectCoroutine);
        text.maxVisibleCharacters = text.text.Length;

        isShowing = false;
    }

    private IEnumerator EffectLetterByLetter(){   
        int caracteresTotais = text.text.Length;
        text.maxVisibleCharacters = 0;

        for (int i = 0; i <= caracteresTotais; i++)
        {
            text.maxVisibleCharacters = i;
            yield return new WaitForSeconds(textSpeed);
        }
        isShowing = false;
    }
}
