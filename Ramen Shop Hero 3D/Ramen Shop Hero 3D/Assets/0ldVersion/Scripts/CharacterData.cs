using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterData : MonoBehaviour
{
    public int Emotion; //An int representing an emotion, from 0 to 8 where 0 is Sadness.
    public Sprite FlavourSprite; //The sprite for the picture we want to show at a specific point in the dialogue
    public int ParagraphToPicture; //The paragraph number after which we want a picture to be displayed
    public string[] IntroDialogueParagraphs; //Each string in this array will be displayed as a separate paragraph. This one is for the pre-ramen dialogue
    public string[] SpecialDialogueParagraphs; //This one is for the post-ramen dialogue.
}
