using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour , IDamagable
{
    public float Health = 150f;
    public GameObject DeathScreen;
    public GameObject WinScreen;
    public GameObject Boss;
    public bool isGameOver = false;

    public void TakeDamage(float damageAmount)
    {
        Health -= damageAmount;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Health<=0)
        {
            isGameOver = true;
            DeathScreen.SetActive(true);
            StartCoroutine(Timeout(10f));
            SceneManager.LoadScene(0);

        }
        if(isGameOver)
        {
            
            WinScreen.SetActive(true);
            StartCoroutine(Timeout(10f));
            SceneManager.LoadScene(0);
        }
        if(Boss.GetComponent<CharacterManager>().TempHealth <= 0)
        {
            WinScreen.SetActive(true);
            StartCoroutine(Timeout(10f));
            SceneManager.LoadScene(0);
        }
    }
    IEnumerator Timeout(float time)
    {
        yield return new WaitForSeconds(time);
    }
}
