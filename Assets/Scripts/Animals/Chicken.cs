using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Chicken : MonoBehaviour
{   
    public bool collectedChicken;
    public GameObject chicken;
    
    [SerializeField] private GameObject hiro;
    [SerializeField] private CollectAnimals canCaptureAnimal;
    [SerializeField] private Player player; 
    [SerializeField] private Items iten;
    [SerializeField] private GetPoint point;
	
    [Header("Velocity")]
	public float speed;
    public float runVelocity;
    [SerializeField] private UnityEngine.AI.NavMeshAgent agent;

    private FMOD.Studio.EventInstance GalinhaSFX;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        GalinhaSFX = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Bichos/Galinha");
        GalinhaSFX.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(this.transform.transform.position));
        GalinhaSFX.start();
        GalinhaSFX.release();
    }

    // Update is called once per frame
    void Update()
    {
        //verifica se a galinha pode ser capturada
        canCaptureChicken();
        if(collectedChicken){
            agent.speed = player._speed - 0.5f;
            Vector3 pos = player.transform.position;
            pos = new Vector3(pos.x-2f, pos.y-2f, pos.z);
            agent.destination = pos;

            if(player.speak)
                chicken.SetActive(false);
	    }
    }

    private void canCaptureChicken(){
        //se o player estiver no raio de captura da galinha
        if(canCaptureAnimal.canCapture){

            //se tiver com o item certo captura a galinha
            if(hiro.transform.GetComponent<CollectedItems>().itemName == "corn"){
                agent.speed = 0f;
                if(player.playerInteract){
                    collectedChicken = true;
                    iten.used = true;
	                hiro.transform.GetComponent<CollectedItems>().totalAnimals--;
                    hiro.transform.GetComponent<CollectedItems>().checkUsedItem();
                    player.disableUntilDeliver();

                    if(SceneManager.GetActiveScene().name == "2. TutorialScene"){
                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                    }
                        
                }

            }else{
                //fazer com que o animal vá para uma direção oposta do player
                //FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Bichos/Galinha");
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
