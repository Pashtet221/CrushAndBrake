using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class TileGeneration : MonoBehaviour
{
    public SpriteRenderer render;
    public Sprite[] sp;

    float xPos = -2.5f;
    float yPos = -2.5f;



    public int count = 20;

   
   
    // Update is called once per frame
    void Update()
    {
       
    }



    private void Start()
    {
        /*
         GameObject parent = new GameObject();
         for (int i = 0; i < sp.Length; i++)
           {

               SpriteRenderer sps = Instantiate(render, new Vector2(xPos, yPos), Quaternion.identity,parent.transform) as SpriteRenderer;
               sps.sprite = sp[i];
               xPos += 5f;


           }
        */
        GameObject parent = new GameObject();
        for (int i = 0; i < sp.Length; i++)
        {

            SpriteRenderer sps = Instantiate(render, new Vector2(xPos, yPos), Quaternion.identity, parent.transform) as SpriteRenderer;
            sps.sprite = sp[i];
            xPos += 5f;
            if(i%count-1==0&&i!=0)
            {
                yPos -= 5f;
                xPos = -2.5f;
            }


        }




    }
}
