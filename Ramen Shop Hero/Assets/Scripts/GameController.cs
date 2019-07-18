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
    private Canvas ingredientCanvas;
    private Canvas cookingPreludeCanvas;
    private Canvas cookingCanvas;

    void Start()
    {
        orderCanvas = GameObject.Find("OrderCanvas").GetComponent<Canvas>();
        conversationCanvas = GameObject.Find("ConversationCanvas").GetComponent<Canvas>();

        DisableConversationCanvas();
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
}
