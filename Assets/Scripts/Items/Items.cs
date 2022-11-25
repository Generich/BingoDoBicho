using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    public bool collected;
    public GameObject item;
    public bool used;
    
    [SerializeField] private GameObject hiro;
    [SerializeField] private bool canCollect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //coleta o item se o player não tiver coletado nenhum outro item
        //e se ele apertou o botao de coleta 
        //e estiver dentro do raio de coleta
        if(canCollect && hiro.transform.GetComponent<Player>().playerInteract && !used){
            if(!hiro.transform.GetComponent<CollectedItems>().collectedItem){
                collected = true;
                item.SetActive(false);
            }
        }
    }

    //so permite que o objeto seja coletado se o plyer estiver dentro do raio permitido paea coleta
    private void OnTriggerEnter2D(Collider2D collision){
        canCollect = true;
    }

    private void OnTriggerExit2D(Collider2D other) {
        canCollect = false;
    }
}
