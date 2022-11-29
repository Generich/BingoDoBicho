using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit : MonoBehaviour
{
    public bool collectedRabbit;
    public GameObject rabbit;

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

    private FMOD.Studio.EventInstance CoelhoSFX;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        CoelhoSFX = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Bichos/Coelho");
        CoelhoSFX.start();
        CoelhoSFX.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(this.transform.transform.position));
        CoelhoSFX.release();
    }

    // Update is called once per frame
    void Update()
    {
        CoelhoSFX.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(this.transform.transform.position));
        canCaptureRabbit();
    }

    private void canCaptureRabbit()
    {
        if (canCaptureAnimal.canCapture)
        {
            if (hiro.transform.GetComponent<CollectedItems>().itemName == "topper")
            {
                agent.speed = 0f;

                if (player.playerInteract)
                {

                    collectedRabbit = true;
                    Debug.Log("Entou Aqui!!!!!!!!!!!!!");
                    iten.used = true;
                    hiro.transform.GetComponent<CollectedItems>().totalAnimals--;
                    hiro.transform.GetComponent<CollectedItems>().checkUsedItem();
                    player.disableUntilDeliver();

                    /*
                    *
                    * Executar aqui a animação do coelho entrando na cartola
                    *
                    */

                    dialogue.currentDialogue = null;
                    dialogue.allDialogues.Remove(hint);
                    dialogue.randomDialogue = false;
                    rabbit.SetActive(false);
                }
            }
            else
            {
                //Debug.Log("Item Errado");
                //fazer com que o animal vá para uma direção oposta do player
                //FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Bichos/Coelho");
                agent.speed = runVelocity;
                float x, y;
                Vector3 pos = player.transform.position;
                if (transform.position.x < pos.x)
                    x = pos.x * (-1.5f);
                else
                    x = pos.x * 1.5f;

                if (transform.position.y < pos.y)
                    y = pos.y * (-1.5f);
                else
                    y = pos.y * 1.5f;

                pos = new Vector3(x, y, pos.z);


                agent.SetDestination(pos);
            }
        }
        else
        {
            agent.speed = speed;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        point.NewPosition();
    }
}
