using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capivara : MonoBehaviour
{
    public bool collectedCapivara;
    public GameObject capivara;
    
    [SerializeField] private GameObject hiro;
    [SerializeField] private CollectAnimals canCaptureAnimal;
    [SerializeField] private Player player; 
    [SerializeField] private Items iten;
    [SerializeField] private GetPoint point;

    [Header("Hint")]
    public Dialogue hint;
    [SerializeField] private DialogueSystem dialogue;

    [Header("Velocity")]
    public float speed;
    public float runVelocity;
    [SerializeField] private UnityEngine.AI.NavMeshAgent agent;

    private FMOD.Studio.EventInstance CapivaraSFX;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        CapivaraSFX = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Bichos/Capivara");
        CapivaraSFX.start();
        CapivaraSFX.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(this.transform.transform.position));
        CapivaraSFX.release();
    }

    // Update is called once per frame
    void Update()
    {
        CapivaraSFX.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(this.transform.transform.position));
        //verifica se a capivara pode ser capturada
        canCaptureCapivara();
        if(collectedCapivara){
            agent.speed = player._speed - 0.5f;
            Vector3 pos = player.transform.position;
            pos = new Vector3(pos.x-2f, pos.y-2f, pos.z);
            agent.destination = pos;

            if(player.speak){
                capivara.SetActive(false);
            }
        }
    }

    private void canCaptureCapivara(){
        //se o player estiver no raio de captura da capivara
        if(canCaptureAnimal.canCapture){

            //se tiver com o item certo captura a capivara
            if(hiro.transform.GetComponent<CollectedItems>().itemName == "boina"){

                agent.speed = 0f;
                if(player.playerInteract){
                    collectedCapivara = true;
                    iten.used = true;
                    hiro.transform.GetComponent<CollectedItems>().totalAnimals--;
                    hiro.transform.GetComponent<CollectedItems>().checkUsedItem();
                    FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Bichos/Capivarinhas");
                    player.disableUntilDeliver();
                    dialogue.currentDialogue = null;
                    dialogue.allDialogues.Remove(hint);
                    dialogue.randomDialogue = false;
                }
            }
            else{
                //fazer com que o animal vá para uma direção oposta do player
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
        }else{
            agent.speed = speed;
        }
    }

    void OnCollisionEnter(Collision collision){
        point.NewPosition();
    }

}
