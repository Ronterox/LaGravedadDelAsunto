using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Archievements : MonoBehaviour
{
    public GameObject note;
    public bool active = false;
    public GameObject title;
    public GameObject description;
    public GameObject image;

    public int count;
    public int trigger;
    public String textTitle;
    public String textDescription;

    void Update()
    {
        if (count == trigger)
        {
            StartCoroutine(TriggerArchivement());
        }
        
    }

   IEnumerator TriggerArchivement()
    {
        active = true;
        image.SetActive(true);
        title.GetComponent<TextMesh>().text = textTitle;
        description.GetComponent<TextMesh>().text = textDescription;
        note.SetActive(true);
        yield return new WaitForSeconds(5);
        active = false;
        image.SetActive(false);
        title.GetComponent<TextMesh>().text = "";
        description.GetComponent<TextMesh>().text = "";
        note.SetActive(false);


    }
}
