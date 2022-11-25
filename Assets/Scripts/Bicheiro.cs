using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bicheiro : MonoBehaviour
{
    public bool canSpeak;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private DialogueSystem dialogue;

    // importando animais para verifica��o da HUD
    [SerializeField] private Rabbit Coelho;
    [SerializeField] private Tamandua Tamand;
    [SerializeField] private Urubu Urub;
    [SerializeField] private Capivara Capi;
    [SerializeField] private GameObject X;

    private void markAnimal()
    {
        if (Urub.collectedUrubu)
        {
            X.transform.GetChild(0).gameObject.SetActive(true);
        }
        if (Coelho.collectedRabbit)
        {
            X.transform.GetChild(1).gameObject.SetActive(true);
        }
        if (Tamand.collectedTamandua)
        {
            X.transform.GetChild(2).gameObject.SetActive(true);
        }
        if(Capi.collectedCapivara)
        {
            X.transform.GetChild(3).gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.GetComponent<Player>().playerInteract && canSpeak)
            if (player.transform.GetComponent<Player>().checkBingoCard())
                EndGame();
            else
            {
                CanSpeak();
                markAnimal();
            }
            
    }

    //permite a interacao com o bicheiro
    private void OnTriggerEnter2D(Collider2D other){
        canSpeak = true;
        player.transform.GetComponent<Player>().enableInteract();
    }

    private void OnTriggerExit2D(Collider2D other) {
        canSpeak = false;
    }

    private void CanSpeak(){
        if(dialogue.allDialogues.Count>0)
            dialogueBox.SetActive(true);
    }

    private void EndGame(){
        Debug.Log("Cabou, coletou todos os animais");
        SceneManager.LoadScene(sceneName: "5. Victory");
    }
}
