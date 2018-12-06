﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Dialog : MonoBehaviour {

    public TextMeshProUGUI textDisplay;
    public string[] sentences;
    private int index;
    public float typeSpeed = 0.02f;
    public GameObject cButton;
    public Animator anim;

    public GameObject bButton;
    public GameObject gButton;
    public GameObject skipButton;

    private void Start()
    {
        StartCoroutine(Type());
        cButton.SetActive(false);
        AudioManager.instance.Play("Intro");
    }
    private void Update()
    {
        if (textDisplay.text == sentences[index])
        {
            if (cButton != null)
                cButton.SetActive(true);
        }

    }
    IEnumerator Type()
    {
        foreach(char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typeSpeed);
        }
    }

    public void nextSentenceOutro()
    {
        if (cButton != null)
            cButton.SetActive(false);
        
        if (index < sentences.Length - 1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
            if (index < 14)
                anim.Play("Intro 1");
            if (index == 14)
            {
                bButton.SetActive(true);
                gButton.SetActive(true);
                Destroy(cButton);
                Destroy(skipButton);

            }
            if (index == 15)
            {

                bButton.SetActive(false);
                gButton.SetActive(false);
                SceneManager.LoadScene(0);
            }
        }
        else
        {
            AudioManager.instance.Stop("Intro");
        }
    }
    public void NextSentence()

    {
        cButton.SetActive(false);
        if (index < sentences.Length - 1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
            if (index == 2)
                anim.Play("Intro 1");
            if (index == 11)
                anim.Play("Curse");
        }
        else
        {
            AudioManager.instance.Stop("Intro");
            SceneManager.LoadScene(2);

        }
            
    }
    public void Skip()
    {
        AudioManager.instance.Stop("Intro");
        SceneManager.LoadScene(2);
    }
    public void Menu()
    {
        SceneManager.LoadScene(0);
    }

    IEnumerator LoadMenuAfterDelay()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(0);
    }
    public void Bad()
    {
        bButton.SetActive(false);
        gButton.SetActive(false);
        AudioManager.instance.Play("ButtonPress");
        anim.Play("Intro_Bad");
        index = 15;
        textDisplay.text = "";
        StartCoroutine(Type());
        StartCoroutine(LoadMenuAfterDelay());

    }

    public void Good()
    {
        bButton.SetActive(false);
        gButton.SetActive(false);
        AudioManager.instance.Play("ButtonPress");
        anim.Play("Intro_Good");
        index = 15;
        textDisplay.text = "";
        StartCoroutine(Type());
        StartCoroutine(LoadMenuAfterDelay());
    }

}
