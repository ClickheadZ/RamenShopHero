﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    //This class should control the game state, display the proper canvas, and reference the Scoring script.
    //Needs reference to each canvas

    #region Canvas references
    [SerializeField] Canvas orderCanvas;
    [SerializeField] Canvas conversationCanvas;
    [SerializeField] Canvas workingMenuCanvas;    
    [SerializeField] Canvas cookingPreludeCanvas;
    [SerializeField] Canvas ingredientCanvas;
    [SerializeField] Canvas postRamenCanvas;
    [SerializeField] Canvas levelFinishedCanvas;
    [SerializeField] Canvas gameMenuCanvas; 
       
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

    private CurrentScreen currentScreen;
    private int ingredientNumber;
    private int score;

    private enum CurrentScreen { Order, Dialogue, CookingPrelude, Ingredient, PostRamen, LevelFinished };

    void Start()
    {
        orderCanvas = GameObject.Find("OrderCanvas").GetComponent<Canvas>();
        conversationCanvas = GameObject.Find("ConversationCanvas").GetComponent<Canvas>();
        workingMenuCanvas = GameObject.Find("WorkingMenuCanvas").GetComponent<Canvas>();
        cookingPreludeCanvas = GameObject.Find("CookingPreludeCanvas").GetComponent<Canvas>();
        ingredientCanvas = GameObject.Find("IngredientCanvas").GetComponent<Canvas>();
        postRamenCanvas = GameObject.Find("PostRamenCanvas").GetComponent<Canvas>();
        levelFinishedCanvas = GameObject.Find("LevelFinishedCanvas").GetComponent<Canvas>();
        gameMenuCanvas = GameObject.Find("GameMenuCanvas").GetComponent<Canvas>();


        characterData = GetComponent<CharacterData>();
        dialogueBox = GameObject.FindGameObjectWithTag("Textbox").GetComponent<Text>();
        noddingPaused = false;
        characterCounter = 0;
        maxCharacterCount = 550;
        flavourSprite = characterData.FlavourSprite;

        ingredientNumber = -1;
    
        DisableConversationCanvas();
        DisableCookingPreludeCanvas();
        DisableIngredientCanvas();
        DisablePostRamenCanvas();
        DisableLevelFinishedCanvas();
    }

    void Update()
    {
        switch(currentScreen)
        {
            case CurrentScreen.Dialogue:
                {
                    if(Input.GetKeyDown(KeyCode.Return) == true)
                    {
                        SingleNodDialogue();
                    }
                }
                break;

            default:
                {
                    
                }
                break;
        }
    }

    #region Canvas manipulation

    public void EnableWorkingMenuCanvas()
    {
        workingMenuCanvas.gameObject.SetActive(true);
    }

    public void DisableWorkingMenuCanvas()
    {
        if (workingMenuCanvas == null)
        {
            workingMenuCanvas = GameObject.Find("WorkingMenuCanvas").GetComponent<Canvas>();
            DisableWorkingMenuCanvas();
        }
        else
        {
            workingMenuCanvas.gameObject.SetActive(false);
        }

    }
    public void EnableGameMenuCanvas()
    {
        gameMenuCanvas.gameObject.SetActive(true);
    }

    public void DisableGameMenuCanvas()
    {
        if (gameMenuCanvas == null)
        {
            gameMenuCanvas = GameObject.Find("GameMenuCanvas").GetComponent<Canvas>();
            DisableGameMenuCanvas();
        }
        else
        {
            gameMenuCanvas.gameObject.SetActive(false);
        }

    }    

    public void EnableOrderCanvas()
    {
        orderCanvas.gameObject.SetActive(true);
    }

    public void DisableOrderCanvas()
    {
        if (orderCanvas != null)
        {
            orderCanvas.gameObject.SetActive(false);
        }
        else
        {
            print("Order canvas null");
        }

    }

    public void EnableConversationCanvas()
    {
        conversationCanvas.gameObject.SetActive(true);
        currentScreen = CurrentScreen.Dialogue;
    }

    public void DisableConversationCanvas()
    {
        conversationCanvas.gameObject.SetActive(false);
    }

    public void EnableCookingPreludeCanvas()
    {
        cookingPreludeCanvas.gameObject.SetActive(true);
        currentScreen = CurrentScreen.CookingPrelude;
    }

    public void DisableCookingPreludeCanvas()
    {
        cookingPreludeCanvas.gameObject.SetActive(false);
    }

    public void EnableIngredientCanvas()
    {
        ingredientCanvas.gameObject.SetActive(true);
        currentScreen = CurrentScreen.Ingredient;
    }

    public void DisableIngredientCanvas()
    {
        ingredientCanvas.gameObject.SetActive(false);
    }

    public void EnablePostRamenCanvas()
    {
        postRamenCanvas.gameObject.SetActive(true);
        currentScreen = CurrentScreen.PostRamen;
    }

    public void DisablePostRamenCanvas()
    {
        postRamenCanvas.gameObject.SetActive(false);
    }

    public void EnableLevelFinishedCanvas()
    {
        levelFinishedCanvas.gameObject.SetActive(true);
        currentScreen = CurrentScreen.LevelFinished;
    }

    public void DisableLevelFinishedCanvas()
    {
        levelFinishedCanvas.gameObject.SetActive(false);
    }
    #endregion

    //Test function
    public void ShowScore()
    {
        Debug.Log(Scoring.Score);
    }

    #region Dialogue functions
    //Prints the first dialogue paragraph that the character has.
    public void HandleDialogue()
    {
        if (dialogueBox == null)
        {
            print("dialogueBox null");
            dialogueBox = GameObject.FindGameObjectWithTag("Textbox").GetComponent<Text>();
        }
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
                EnableWorkingMenuCanvas();
                EnableGameMenuCanvas();
            }
        }
    }

    public void SingleNodDialogue()
    {
        if (noddingPaused == false)
        {
            if (dialogueCounter < characterData.IntroDialogueParagraphs.Length)
            {
                dialogueBox.text = characterData.IntroDialogueParagraphs[dialogueCounter];

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
            }
            else
            {
                DisableConversationCanvas();
                EnableOrderCanvas();
                EnableWorkingMenuCanvas();
                EnableGameMenuCanvas();                
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
                    characterCounter = 0;
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
                EnableWorkingMenuCanvas();
                EnableGameMenuCanvas();                 
            }
        }
    }
    #endregion

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
