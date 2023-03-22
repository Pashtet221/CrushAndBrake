using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Test : MonoBehaviour
{
   // [Header("Shake")]
   // public float duration, strength, vibrato, randomness;
   public float x, y, z, speed;
   public int i;


    
    public void FixedUpdate ()
	{
		//End all animations first
		transform.DOComplete ();

		transform.DOShakePosition (speed, new Vector3 (x, y, z), i, 0).SetEase (Ease.Linear);
	}
}
