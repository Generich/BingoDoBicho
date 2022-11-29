using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DialogueSystem : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TextMovement letterByLetter;
    [SerializeField] private TextMeshProUGUI nameCharacter;
    [SerializeField] private Player player;
    [SerializeField] private Timer timer;
    private Queue<string> dialogueQueue;   
    private int dialogueIndex;
    private bool activeDialogue;
    private bool tutorial;


    public List<Dialogue> allDialogues = new List<Dialogue>();
    public Dialogue currentDialogue;
    public bool randomDialogue;

    // Start is called before the first frame update
    void Start()
    {
        //comecar a primeira cena com o dialogo
        if(SceneManager.GetActiveScene().name == "2. TutorialScene"){
            Debug.Log("Entou na cena tutorial");
            tutorial = true;
            dialogueBox.SetActive(true);
            currentDialogue = allDialogues[0];
            StartDialogue(currentDialogue);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(dialogueBox.activeSelf && !activeDialogue && !tutorial )
            SelectDialogue();
    }

    //funcao que vai determinar depois os dialogos para as quests
    private void SelectDialogue(){
        if(allDialogues.Count == 1){
            currentDialogue = allDialogues[0];
            StartDialogue(currentDialogue);
        }

        if(allDialogues.Count > 1 && !randomDialogue){
            int pos = Random.Range(0, allDialogues.Count-1);
            Debug.Log("pos Dialogo " + pos);
            Debug.Log("allDialogues.Count " + allDialogues.Count);
            currentDialogue = allDialogues[pos];
            randomDialogue = true;
            StartDialogue(currentDialogue);
        }else if(randomDialogue){
            StartDialogue(currentDialogue);
        } 
    }

    private void StartDialogue(Dialogue dialogue){
        player.speak = true;
        Debug.Log("Iniciou Dialogo");
        activeDialogue = true;
        //Inicializa a fila de dialogos
        dialogueQueue = new Queue<string>();

        currentDialogue = dialogue;
        dialogueIndex = 0;

        if (timer != null)
            timer.Pause();
        NextDialogue();
    }

    public void NextDialogue(){
        
        if(letterByLetter.isShowing){
            letterByLetter.ShowAllText();
            return;
        }

        if(dialogueQueue.Count == 0){
            if(dialogueIndex < currentDialogue.Text.Length){

                //Coloca o nome do personagem na caixa de diálogo
                nameCharacter.text = currentDialogue.Text[dialogueIndex].character.Name;
                
                if(currentDialogue.Text[dialogueIndex].character.Name == "Liro")
                {
                    FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Personagens/Liro");
                }
                else
                {
                    FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Personagens/Bicheiro");
                }
                //Coloca todas as falas da expressão atual em uma fila
                foreach (string dialogueText in currentDialogue.Text[dialogueIndex].DialogueText){   
                    dialogueQueue.Enqueue(dialogueText);
                }

                dialogueIndex++;
            }else{   
                //Faz sumir a caixa de diálogo
                player.speak = false;
                dialogueBox.SetActive(false);
                activeDialogue = false;
                tutorial = false;
                if(!randomDialogue)
                    currentDialogue = null;
                if (timer != null)
                    timer.Pause();

                if(SceneManager.GetActiveScene().name == "3. DialogueScene")
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                return;
            }
        }

        letterByLetter.ShowTextLetterByLetter(dialogueQueue.Dequeue());
    }
}
