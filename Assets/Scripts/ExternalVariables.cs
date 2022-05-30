using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ExternalVariables : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI txt;
    private int cantidadEnemigos;
    private bool win = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cantidadEnemigos= GameObject.FindGameObjectsWithTag("Enemy").Length;
        txt.text = "Enemigos restantes: " + cantidadEnemigos;
        if (cantidadEnemigos == 0)
        {
            Invoke("showWinInterface", 1f);
            Invoke("pause", 1.5f);
        }
    }

    void showWinInterface()
    {
        if (!win)
        {
            SceneManager.LoadScene("Win", LoadSceneMode.Additive);
            win = true;
        }
    }

    void pause()
    {
        Time.timeScale = 0f;
    }
}
