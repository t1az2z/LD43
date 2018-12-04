using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalMission : MonoBehaviour {

    public CircleCollider2D circleCol;
    public GameObject spawner;
    bool end = false;
    int opened;
    public ParticleSystem altar;
    private bool seted = false;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!end)
            {
                circleCol.enabled = false;
                spawner.SetActive(true);
            }
            else
            {
                GameManager.Instance.LoadScene(3);
            }
        }
    }

    private void Update()
    {
        if (spawner.GetComponent<Spawner>().open)
        {
            if (!seted)
            {
                seted = true;
                circleCol.enabled = true;
                end = true;
                altar.Play();
                AudioManager.instance.Stop("Theme");
                AudioManager.instance.Play("EndEffect");
                print("End");
            }

        }
    }

}
