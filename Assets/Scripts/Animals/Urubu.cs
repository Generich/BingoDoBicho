using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Urubu : MonoBehaviour
{
    public bool collectedUrubu;
    public GameObject urubu;
    
    [SerializeField] private GameObject hiro;
    [SerializeField] private CollectAnimals canCaptureAnimal;
    [SerializeField] private Player player; 
    [SerializeField] private Items iten;
    [SerializeField] private Items iten2;
    [SerializeField] private GetPoint point;
    [SerializeField] private GameObject point2;

    [Header("Hint")]
    public Dialogue hint;
    [SerializeField] private DialogueSystem dialogue;
	
    [Header("Velocity")]
	public float speed;
    public float runVelocity;
    [SerializeField] private UnityEngine.AI.NavMeshAgent agent;

    private FMOD.Studio.EventInstance UrubuSFX;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        UrubuSFX = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Bichos/Urubu");
        UrubuSFX.start();
        UrubuSFX.release();
        UrubuSFX.setVolume(0f);
    }

    // Update is called once per frame
    void Update()
    {
        //verifica se o urubu pode ser capturada
        canCaptureUrubu();
        if(collectedUrubu){
            agent.speed = player._speed - 0.5f;
            Vector3 pos = player.transform.position;
            pos = new Vector3(pos.x-2f, pos.y-2f, pos.z);
            agent.destination = pos;

            if(player.speak){
                urubu.SetActive(false);
            }
	    }
    }

    private void canCaptureUrubu(){
        //se o player estiver no raio de captura do urubu
        if(canCaptureAnimal.canCapture){

            //se tiver com o item certo captura o urubu
            if(hiro.transform.GetComponent<CollectedItems>().itemName == "phone"){
                agent.speed = 0f;
                if(player.playerInteract){
                    collectedUrubu = true;
                    iten.used = true;
	                hiro.transform.GetComponent<CollectedItems>().totalAnimals--;
                    hiro.transform.GetComponent<CollectedItems>().checkUsedItem();
                    player.disableUntilDeliver();
                    dialogue.currentDialogue = null;
                    dialogue.allDialogues.Remove(hint);
                    dialogue.randomDialogue = false;
                }

            }
            else{
                if(hiro.transform.GetComponent<CollectedItems>().itemName == "money"){
                    agent.SetDestination(point2.transform.position);
                    iten2.used = true;
                    hiro.transform.GetComponent<CollectedItems>().checkUsedItem();
                }else{
                    //fazer com que o animal vá para uma direção oposta do player

                    FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Bichos/UrubuBravo");
                    agent.speed = runVelocity;
                    float x,y;
                    Vector3 pos = player.transform.position;
                    if(transform.position.x < pos.x)
                        x = pos.x*(-1.5f);
                    else    
                        x = pos.x*1.5f;

                    if(transform.position.y < pos.y)	                   
                        y = pos.y*(-1.5f);
                    else    
                        y = pos.y*1.5f;	

                    pos = new Vector3(x, y, pos.z);
                    agent.SetDestination(pos);
                }
                
            }
        }else{
            agent.speed = speed;
        }
    }
    
    void OnCollisionEnter(Collision collision){
        point.NewPosition();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "screenColl")
            UrubuSFX.setVolume(5f);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "screenColl")
            UrubuSFX.setVolume(0f);
    }
}
