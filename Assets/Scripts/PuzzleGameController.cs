using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PuzzleGameController : MonoBehaviour
{
    public GameObject popUpPanel; // Reference to the pop-up panel in the UI canvas
    public List<PuzzlePiecePair> puzzlePiecePairs; // List of specific target pieces for each puzzle piece

    private bool gameOver = false;

    [System.Serializable]
    public class PuzzlePiecePair
    {
        public ChildComponentScript snappoint1; // Reference to the puzzle piece
        public ChildComponentScript snappoint2; // Reference to the target piece it should snap to
    }

    private void Start()
    {
        popUpPanel.SetActive(false);
    }

    public bool ValidateMove()
    {
        // Iterate through all puzzle piece pairs and check if they are correctly snapped
        foreach (PuzzlePiecePair pair in puzzlePiecePairs)
        {
            if (pair.snappoint1.currentLink != pair.snappoint2)
            {
                print("NO WIN");
                return false; // If any piece is not correctly snapped, the game is not over
            }
        }
        popUpPanel.SetActive(true);
        gameOver = true; // All pieces are correctly snapped, game is over
        return true;
    }

    public bool getGameOver()
    {
        return gameOver;
    }
}
