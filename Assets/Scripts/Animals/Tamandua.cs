using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tamandua : MonoBehaviour
{
    public bool collectedTamandua;
    public GameObject tamandua;
    public float forceWithIten;
    public float force;
    

    [SerializeField] private GameObject hiro;
    [SerializeField] private CollectAnimals canCaptureAnimal;
    [SerializeField] private Player player; 
    [SerializeField] private Items iten;	
    [SerializeField] private PointEffector2D effect;
    [SerializeField] private GameObject pointBicheiro;

    [Header("Hint")]
    public Dialogue hint;
    [SerializeField] private DialogueSystem dialogue;

    [Header("Time Area")]
    public int duration;
    public int remainingTime;

    private FMOD.Studio.EventInstance TamanduaSFX;

    // Start is called before the first frame update
    void Start()
    {
        TamanduaSFX = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Bichos/Tamandua");
        TamanduaSFX.start();
        TamanduaSFX.release();
        TamanduaSFX.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(this.transform.transform.position));
        TamanduaSFX.setParameterByName("Perto", 1); // Toca o som normal se o player n�o chegou perto ainda

    }

    

    // Update is called once per frame
    void Update()
    {
        canCaptureTamandua();
        if(collectedTamandua){
            this.transform.position = pointBicheiro.transform.position;
            if(player.speak){
                tamandua.SetActive(false);
            }
        }
    }

    private void canCaptureTamandua(){
        //se o player estiver no raio de captura do tamandua
        if(canCaptureAnimal.canCapture){
            Debug.Log("Area Tamandua");
            Debug.Log("Item: " + hiro.transform.GetComponent<CollectedItems>().itemName);
            //se tiver com o item certo captura o tamandua
            if(hiro.transform.GetComponent<CollectedItems>().itemName == "flag"){
                effect.forceMagnitude = forceWithIten;
                if(player.playerInteract){
                    collectedTamandua = true;
                    iten.used = true;
	                hiro.transform.GetComponent<CollectedItems>().totalAnimals--;
                    hiro.transform.GetComponent<CollectedItems>().checkUsedItem();
                    player.disableUntilDeliver();
                    dialogue.currentDialogue = null;
                    dialogue.allDialogues.Remove(hint);
                    dialogue.randomDialogue = false;
                }

            } else {
                //
                TamanduaSFX.setParameterByName("Perto", 0); //toca o som do haki do tamandua
                effect.forceMagnitude = force;
                // Debug.Log("Force Magnitude" + effect.forceMagnitude);
            }
        
        }
    }

    //fazer a contagem de tempo dentro da area do tamandua
    private void OnTriggerEnter2D(Collider2D other){
        if(hiro.transform.GetComponent<CollectedItems>().itemName != "flag" && other.gameObject.tag != "screenColl"){
            Begin(duration);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        TamanduaSFX.setParameterByName("Perto", 1); // Toca o som normal se o player n�o chegou perto ainda
    }

    // Update is called once per frame
    private void Begin(int second)
    {
        remainingTime = second;
        StartCoroutine(UpdateTimer());
    }

    private IEnumerator UpdateTimer()
    {
        
        while (remainingTime >= 0 && canCaptureAnimal.canCapture)
        {
                Debug.Log("Tempo restante: " + remainingTime);
                //uiText.text = $"{remainingTime / 60:00}:{remainingTime % 60:00}";
                remainingTime--;
                yield return new WaitForSeconds(1f);
            
            yield return null;
        }

        if(remainingTime < 0){
            Debug.Log("Player Morreu");
            SceneManager.LoadScene(5);
        }
    }
}
