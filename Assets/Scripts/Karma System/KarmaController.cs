using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KarmaController : MonoBehaviour
{
   public  int karma = 50;
   public  Slider karmabar;

    void Start()
    {
        karmabar.value = karma;
    }

    //Only for test if works after implemented.
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            ChangeKarma(-1);
        }
        if (Input.GetKeyDown(KeyCode.Z))
            ChangeKarma(+1);
        {

        }
    }

    public void ChangeKarma(int karmanro)
    {
        karma += karmanro;
        karmabar.value = karma;
    }

   
}
