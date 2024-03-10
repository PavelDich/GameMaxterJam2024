using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogSystem : MonoBehaviour
{
    public string[] sentences;
    private int index;
    public float typingSpeed;
    public GameObject continueButton;
    public GameObject dialogueText;
    public GameObject player;
    public float triggerDistance = 3.0f; 

    private bool isInRange = false;

    void Start()
    {
        continueButton.SetActive(false);
    }

    void Update()
    {
       
        if (Vector3.Distance(player.transform.position, this.transform.position) < triggerDistance)
        {
            isInRange = true;
        }
        else
        {
            isInRange = false;
        }

        
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (!continueButton.activeInHierarchy)
            {
                StartCoroutine(Type());
            }
            else
            {
                NextSentence();
            }
        }
    }

    IEnumerator Type()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            dialogueText.GetComponent<UnityEngine.UI.Text>().text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        continueButton.SetActive(true);
    }

    public void NextSentence()
    {
        continueButton.SetActive(false);

        if (index < sentences.Length - 1)
        {
            index++;
            dialogueText.GetComponent<UnityEngine.UI.Text>().text = "";
            StartCoroutine(Type());
        }
        else
        {
            dialogueText.GetComponent<UnityEngine.UI.Text>().text = "";
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        continueButton.SetActive(false);
        index = 0;
        dialogueText.SetActive(false);
    }
    private void OnTriggerEnter(Collider collision)
    {
        continueButton.SetActive(true);
        dialogueText.SetActive(true);
    }
}