using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectedItems : MonoBehaviour
{
    public bool collectedItem;   
    public string itemName = null;
    public int totalAnimals;
    public int totalAnimalsStart;
    public int animalCount = 0;

    [SerializeField] private GameObject [] items; 
    [SerializeField] private Player player;
    [SerializeField] private CollectAnimals [] canCaptureAnimal;
    [SerializeField] private Bicheiro bicheiro;
    private int itemPosition = -1;
    private int animalPosition = -1;
    public bool hiroCanCapure;

    [SerializeField] private GameObject Inventory;

    public void showCollectedItem(string name)
    {
        if(string.Equals(name, "topper"))
        {
            Inventory.transform.GetChild(0).gameObject.SetActive(true);
        }
        if(string.Equals(name, "phone"))
        {
            Inventory.transform.GetChild(1).gameObject.SetActive(true);
        }
        if(string.Equals(name, "boina"))
        {
            Inventory.transform.GetChild(2).gameObject.SetActive(true);
        }
        if(string.Equals(name, "flag"))
        {
            Inventory.transform.GetChild(3).gameObject.SetActive(true);
        }
    }

    public void hideItem(string name)
    {
        if (string.Equals(name, "topper"))
        {
            Inventory.transform.GetChild(0).gameObject.SetActive(false);
        }
        if (string.Equals(name, "phone"))
        {
            Inventory.transform.GetChild(1).gameObject.SetActive(false);
        }
        if (string.Equals(name, "boina"))
        {
            Inventory.transform.GetChild(2).gameObject.SetActive(false);
        }
        if (string.Equals(name, "flag"))
        {
            Inventory.transform.GetChild(3).gameObject.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        totalAnimals = canCaptureAnimal.Length;
        totalAnimalsStart = canCaptureAnimal.Length;
    }

    // Update is called once per frame
    void Update(){

        //verifica se algum animal esta no raio de captura e salva a sua posicao
        checkAnimalCanCapture();

        //verifica se o player pode coletar ou soltar um item
        CanCollect();
    }

    private void checkAnimalCanCapture(){
        //verifica se algum animal esta no raio de captura e salva a sua posicao
        animalCount = 0;
        for(int i = 0; i<totalAnimals; i++){
            if(canCaptureAnimal[i].transform.GetComponent<CollectAnimals>().canCapture){
                animalPosition = i;
            } else
            {
                animalCount++;
            }
        }
    }

    public void CanCollect(){

        if(player.playerInteract && !bicheiro.transform.GetComponent<Bicheiro>().canSpeak){
            //verifica se o player não coletou nenhum item
            if(!collectedItem){
                for(int i = 0; i<items.Length; i++){
                    //verifica qual item o player coletou e marca que tem um item coletado
                    if(items[i].transform.GetComponent<Items>().collected && !items[i].transform.GetComponent<Items>().used){
                        Debug.Log("Item " + items[i].name + " coletado");

                        //salva o nome do item que esta coletado
                        itemName = items[i].name;
                        showCollectedItem(itemName);
                        itemPosition = i;
                        collectedItem = true;

                        player.playerInteract = false;
                        break;
                    }
                }
            }

            //se o player tiver com um item coletado e apertar o botao para coletar outro item
            //(por enquanto) o antigo item coletado eh soltado e retorna para o ponto de origem
            //acao so eh executada se o player nao estiver perto de nenhum bicho
            else if(animalCount == totalAnimals && !items[itemPosition].transform.GetComponent<Items>().used){
                if(items[itemPosition].transform.GetComponent<Items>().collected && !hiroCanCapure)
                {
                    Debug.Log("Item " + items[itemPosition].name + " soltado");
                    hideItem(itemName);
                    collectedItem = false;
                    items[itemPosition].transform.GetComponent<Items>().collected = false;
                     items[itemPosition].SetActive(true);

                    //limpa o nome do item coletado
                    itemName = null;
                    itemPosition = -1;

                    player.playerInteract = false;
                }
            }
        }
        
    }

    public void checkUsedItem(){
        //desativa o item usado e limpa os campos de item coletado
        if(itemPosition >-1){
            if(items[itemPosition].transform.GetComponent<Items>().used){
                hideItem(itemName);
                collectedItem = false;
                itemName = null;
                itemPosition = -1;
            }
        }
    }
}
