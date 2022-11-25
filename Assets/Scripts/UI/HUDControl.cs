using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDControl : MonoBehaviour
{
    public GameObject X;
    public GameObject Item;

    public void markAnimal(int a)
    {
        X.transform.GetChild(a).gameObject.SetActive(true);
    }

    public void getItem(int a)
    {
        Item.transform.GetChild(a).gameObject.SetActive(true);
    }

    public void letItem(int a)
    {
        Item.transform.GetChild(a).gameObject.SetActive(false);
    }

}
