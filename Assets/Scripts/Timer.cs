using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Timer : MonoBehaviour
{
    [SerializeField] private Text uiText;

    public int duration;
    private int remainingTime;
    private bool pause = false;

    // Start is called before the first frame update
    private void Start()
    {
        Begin(duration);
    }

    // Update is called once per frame
    private void Begin(int second)
    {
        remainingTime = second;
        StartCoroutine(UpdateTimer());
    }

    // Tem que adicionar um botão no player pra poder pausar
    public void Pause()
    {
        pause = !pause;
    }

    private IEnumerator UpdateTimer()
    {
        while (remainingTime >= 0)
        {
            if (!pause)
            {
                uiText.text = $"{remainingTime / 60:00}:{remainingTime % 60:00}";
                remainingTime--;
                yield return new WaitForSeconds(1f);
            }
            yield return null;
        }

        OnEnd();
    }

    public void OnEnd()
    {
        Debug.Log("Cabou o sonho!");
        // Acabar o jogo com final ruim
        SceneManager.LoadScene(sceneName: "6. Defeat");
    }
}
