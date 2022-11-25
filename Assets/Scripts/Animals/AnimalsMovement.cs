using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AnimalsMovement : MonoBehaviour
{
    [SerializeField] private GetPoint GetPoint;
    [SerializeField] private CollectAnimals animal;
    private NavMeshAgent agent;
    private bool facinRight = true;
    private float lastPositionX;

    
    void Start(){
        agent = GetComponent<NavMeshAgent>();
        //Variaveis setadas como False para Não utilizar os eixos Y Baseado em 3 dimensões
        agent.updateRotation = false;
        agent.updateUpAxis = false;  
    }
    
    void Update(){
        if(transform.position.x > lastPositionX && !facinRight)
            Flip();
        
        if(transform.position.x < lastPositionX && facinRight)
            Flip();
  

        if (!agent.hasPath){
            //faz com que o animal va até um ponto
            if(!animal.canCapture){
                agent.SetDestination(GetPoint.transform.position);
                GetPoint.NewPosition();
            }
        }
        lastPositionX = transform.position.x;
    }

    void Flip(){
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        
        facinRight = !facinRight;
        transform.localScale = theScale;
    }
}
