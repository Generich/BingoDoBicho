using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public void startMusic()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Trilha/Trilha");
    }
}
