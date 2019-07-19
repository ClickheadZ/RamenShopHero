using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    //This class should control the game state, display the proper canvas, and reference the Scoring script.
    //Needs reference to each canvas

    private Canvas orderCanvas;
    private Canvas conversationCanvas;
    private Canvas cookingPreludeCanvas;
    private Canvas ingredientCanvas;

    void Start()
    {
        orderCanvas = GameObject.Find("OrderCanvas").GetComponent<Canvas>();
        conversationCanvas = GameObject.Find("ConversationCanvas").GetComponent<Canvas>();
        cookingPreludeCanvas = GameObject.Find("CookingPreludeCanvas").GetComponent<Canvas>();
        ingredientCanvas = GameObject.Find("IngredientCanvas").GetComponent<Canvas>();

        DisableConversationCanvas();
        DisableCookingPreludeCanvas();
        DisableIngredientCanvas();
    }

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

    public void IncreaseScoreBy10()
    {
        Scoring.Score += 10;
    }

    public void ShowScore()
    {
        Debug.Log(Scoring.Score);
    }
}
