using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GetPoint : MonoBehaviour
{
    private float initialX;
    private float initialY;
    private float initialZ;

    public GameObject rayChicken;
    public float range;

    void Start() {
        initialX = transform.position.x;
        initialY = transform.position.y;
        initialZ = transform.position.z;
    }


    //criar um novo ponto para o animal ir dentro do raio
    public void NewPosition(){
        float x = Random.Range(initialX-range, initialX+range);
        float y = Random.Range(initialY-range, initialY+range);

        Vector3 newPos = new Vector3(x, y, 0);
        Debug.DrawRay (newPos, Vector3.up, Color.red, 1);
        transform.position = newPos;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos ()
    {
        Gizmos.DrawWireSphere (rayChicken.transform.position, range);
    }

#endif

}
