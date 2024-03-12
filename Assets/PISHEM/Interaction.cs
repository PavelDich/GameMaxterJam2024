using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public GameObject indicator;
    public GameObject panel1;
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 2))
        {
            if (hit.collider.tag == "Item")
            {

                indicator.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    hit.collider.GetComponent<Item>().Interaction();

                }

            }
            else if (hit.collider.tag == "Pismo")
            {
                panel1.SetActive(true);
                //GetComponent<Item>().ClosePismo();
            }
        }
        else
        {
            indicator.SetActive(false);
            panel1.SetActive(false);
        }

    }
}