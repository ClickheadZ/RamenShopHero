using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    //This class should control the game state, display the proper canvas, and reference the Scoring script.
    //Needs reference to each canvas

    #region Canvas references
    private Canvas orderCanvas;
    private Canvas conversationCanvas;
    private Canvas cookingPreludeCanvas;
    private Canvas ingredientCanvas;
    private Canvas postRamenCanvas;
    private Canvas levelFinishedCanvas;
    #endregion

    private int ingredientNumber;

    void Start()
    {
        orderCanvas = GameObject.Find("OrderCanvas").GetComponent<Canvas>();
        conversationCanvas = GameObject.Find("ConversationCanvas").GetComponent<Canvas>();
        cookingPreludeCanvas = GameObject.Find("CookingPreludeCanvas").GetComponent<Canvas>();
        ingredientCanvas = GameObject.Find("IngredientCanvas").GetComponent<Canvas>();
        postRamenCanvas = GameObject.Find("PostRamenCanvas").GetComponent<Canvas>();
        levelFinishedCanvas = GameObject.Find("LevelFinishedCanvas").GetComponent<Canvas>();

        ingredientNumber = -1;

        DisableConversationCanvas();
        DisableCookingPreludeCanvas();
        DisableIngredientCanvas();
        DisablePostRamenCanvas();
    }

    #region Canvas manipulation

    public void EnableOrderCanvas()
    {
        orderCanvas.gameObject.SetActive(true);
    }

    public void DisableOrderCanvas()
    {
        orderCanvas.gameObject.SetActive(false);
    }

    public void EnableConversationCanvas()
    {
        conversationCanvas.gameObject.SetActive(true);
    }

    public void DisableConversationCanvas()
    {
        conversationCanvas.gameObject.SetActive(false);
    }

    public void EnableCookingPreludeCanvas()
    {
        cookingPreludeCanvas.gameObject.SetActive(true);
    }

    public void DisableCookingPreludeCanvas()
    {
        cookingPreludeCanvas.gameObject.SetActive(false);
    }

    public void EnableIngredientCanvas()
    {
        ingredientCanvas.gameObject.SetActive(true);
    }

    public void DisableIngredientCanvas()
    {
        ingredientCanvas.gameObject.SetActive(false);
    }

    public void EnablePostRamenCanvas()
    {
        postRamenCanvas.gameObject.SetActive(true);
    }

    public void DisablePostRamenCanvas()
    {
        postRamenCanvas.gameObject.SetActive(false);
    }

    public void EnableLevelFinishedCanvas()
    {
        levelFinishedCanvas.gameObject.SetActive(true);
    }

    public void DisableLevelFinishedCanvas()
    {
        levelFinishedCanvas.gameObject.SetActive(false);
    }
    #endregion

    public void IncreaseScoreBy10()
    {
        Scoring.Score += 10;
    }

    public void ShowScore()
    {
        Debug.Log(Scoring.Score);
    }

    public void HandleDialogue()
    {
        //this code handles the dialogue portion of the game
    }

    #region Cooking Functions

    /*For reference,
     * MEAT = 0 = JOYFUL
     * VEGGIE = 1 = PEACEFUL
     * FRUIT = 2 = GENTLE
     * SOUP = 3 = GOODNESS
     * SPICE = 4 = PATIENCE
     * SEAFOOD = 5 = FAITHFULNESS
     * DAIRY = 6 = SELFCONTROL
     * FISH = 7 = TEMPERANCE
     * NOODLE = 8 = MODESTY
     */

    public void ProcessIngredient(int ingredient)
    {
        ingredientNumber = ingredient;
    }

    //This function takes the player to the next screen (post ramen canvas), but makes sure they picked an ingredient beforehand.
    public void ServeRamen()
    {
        if(ingredientNumber < 0)
        {
            Debug.Log("No ingredient selected!");
        } else
        {
            DisableIngredientCanvas();
            EnablePostRamenCanvas();
        }
    }
    #endregion
}
