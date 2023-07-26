using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicVaseController : MonoBehaviour
{
    public GameObject winAnimation;
    public GameObject wrongAnimation;
    public List<VasePlacementPair> vasePlacementPair; 

    private bool gameOver = false;

    [System.Serializable]
    public class VasePlacementPair
    {
        public MusicVaseScript vase; 
        public MusicPlacementScript placement; 
    }

    public void Start()
    {
        winAnimation.SetActive(false);
        wrongAnimation.SetActive(false);
    }

    public void ValidateMove()
    {
        if(!gameOver)
        {
            foreach (VasePlacementPair pair in vasePlacementPair)
            {
                if (pair.vase.currentPlacement.gameObject != pair.placement.gameObject)
                {
                    wrongAnimation.SetActive(false);
                    wrongAnimation.SetActive(true);
                    return;
                }
            }
            winAnimation.SetActive(true);
            gameOver = true;
        }
    }
}
