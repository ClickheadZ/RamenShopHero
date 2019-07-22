﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    //This class should control the game state, display the proper canvas, and reference the Scoring script.
    //Needs reference to each canvas

    #region Canvas references
    private Canvas orderCanvas;
    [SerializeField] Canvas conversationCanvas;
    private Canvas cookingPreludeCanvas;
    private Canvas ingredientCanvas;
    private Canvas postRamenCanvas;
    private Canvas levelFinishedCanvas;
    #endregion

    private CharacterData characterData;

    #region Dialogue Variables
    private int dialogueCounter;
    private int characterCounter;
    private int maxCharacterCount;
    private Text dialogueBox;
    private bool noddingPaused;
    private Sprite flavourSprite;
    #endregion

    private int ingredientNumber;
    private int score;

    void Start()
    {
        orderCanvas = GameObject.Find("OrderCanvas").GetComponent<Canvas>();
        //conversationCanvas = GameObject.Find("ConversationCanvas").GetComponent<Canvas>();
        cookingPreludeCanvas = GameObject.Find("CookingPreludeCanvas").GetComponent<Canvas>();
        ingredientCanvas = GameObject.Find("IngredientCanvas").GetComponent<Canvas>();
        postRamenCanvas = GameObject.Find("PostRamenCanvas").GetComponent<Canvas>();
        levelFinishedCanvas = GameObject.Find("LevelFinishedCanvas").GetComponent<Canvas>();
        characterData = GetComponent<CharacterData>();

        dialogueBox = GameObject.FindGameObjectWithTag("Textbox").GetComponent<Text>();
        noddingPaused = false;
        characterCounter = 0;
        maxCharacterCount = 1000;
        flavourSprite = characterData.FlavourSprite;

        ingredientNumber = -1;

        DisableConversationCanvas();
        DisableCookingPreludeCanvas();
        DisableIngredientCanvas();
        DisablePostRamenCanvas();
        DisableLevelFinishedCanvas();
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

    //Test function
    public void IncreaseScoreBy10()
    {
        Scoring.Score += 10;
    }

    //Test function
    public void ShowScore()
    {
        Debug.Log(Scoring.Score);
    }

    //Prints the first dialogue paragraph that the character has.
    public void HandleDialogue()
    {
        dialogueBox.text = characterData.IntroDialogueParagraphs[0];
        dialogueCounter = 1;
    }

    //Prints the next dialogue paragraph. If the dialogueCounter matches the paraph we want a picture for, waits a few seconds before showing the sprite
    public void NodDialogue()
    {
        if(noddingPaused == false)
        {
            if (dialogueCounter < characterData.IntroDialogueParagraphs.Length)
            {
                dialogueBox.text = dialogueBox.text + "\n" + characterData.IntroDialogueParagraphs[dialogueCounter];

                if (dialogueCounter == characterData.ParagraphToPicture)
                {
                    StartCoroutine(WaitingBeforeClick());

                    IEnumerator WaitingBeforeClick()
                    {
                        noddingPaused = true;
                        yield return new WaitForSecondsRealtime(4);
                        noddingPaused = false;
                    }

                    StartCoroutine(WaitingForPicture());

                    IEnumerator WaitingForPicture()
                    {
                        //SHOW flavourSprite HERE
                        yield return new WaitForSecondsRealtime(3);
                        //HIDE flavourSprite HERE
                    }
                }

                dialogueCounter++;
            } else
            {
                DisableConversationCanvas();
                EnableOrderCanvas();
            }
        }
    }

    public void BetterNodDialogue()
    {
        
        if (noddingPaused == false)
        {
            if (dialogueCounter < characterData.IntroDialogueParagraphs.Length)
            {
                if (characterCounter + characterData.IntroDialogueParagraphs[dialogueCounter].Length < maxCharacterCount)
                {
                    dialogueBox.text = dialogueBox.text + "\n" + characterData.IntroDialogueParagraphs[dialogueCounter];
                }
                else
                {
                    dialogueBox.text = characterData.IntroDialogueParagraphs[dialogueCounter];
                }

                if (dialogueCounter == characterData.ParagraphToPicture)
                {
                    StartCoroutine(WaitingBeforeClick());

                    IEnumerator WaitingBeforeClick()
                    {
                        noddingPaused = true;
                        yield return new WaitForSecondsRealtime(4);
                        noddingPaused = false;
                    }

                    StartCoroutine(WaitingForPicture());

                    IEnumerator WaitingForPicture()
                    {
                        //SHOW flavourSprite HERE
                        yield return new WaitForSecondsRealtime(3);
                        //HIDE flavourSprite HERE
                    }
                }

                characterCounter += characterData.IntroDialogueParagraphs[dialogueCounter].Length;
                dialogueCounter++;
            }
            else
            {
                DisableConversationCanvas();
                EnableOrderCanvas();
            }
        }
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

    //This function calculates the number of stars that the player gets for this level, and updates the total score as well.
    public void ScoreCalculator()
    {
        score = Mathf.Abs( ingredientNumber - characterData.Emotion );

        //This gives us a number between 0 and 8, which is a spread of 9, but we want it to be between 1 and 5, for a spread of 5.

        score += 1;
        score /= (9 / 5);

        //Now we have numbers between 0.5555 and 5
        //If we add to it 1-(5/9) and then round to nearest int, all our values will be between 1 and 5

        score += (1 - (5 / 9));
        score = Mathf.RoundToInt(score);

        Scoring.Score += score;

    }
    #endregion
}
