using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThemeEnabler : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            if (!AudioManager.instance.IsPlaying("Theme"))
                AudioManager.instance.Play("Theme");
    }
}
