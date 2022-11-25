using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public FMOD.Studio.EventInstance Music;

    public void startMusic()
    {
        Music = FMODUnity.RuntimeManager.CreateInstance("event:/Trilha/Trilha");
        Music.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(Camera.allCameras[0].transform.position));
        Music.start();
        Music.release();
        Music.setParameterByName("AnimalCount", 4);
    }
    public void SetAnimal(float a)
    {
        float x = 4 - a;
        Debug.Log("New animal count: " + x);
        Music.setParameterByName("AnimalCount", x);
    }
}
