using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectAnimals : MonoBehaviour
{
    public bool canCapture;
    [SerializeField] CollectedItems iten;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //so permite o player tentar capturar um bicho se ele estiver perto suficiente
    private void OnTriggerEnter2D(Collider2D other){
        canCapture = true;
        iten.hiroCanCapure = true;
    }

    private void OnTriggerExit2D(Collider2D other) {
        canCapture = false;
        iten.hiroCanCapure = false;
    }

}
