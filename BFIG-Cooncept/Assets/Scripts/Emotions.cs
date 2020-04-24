using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Emotions : MonoBehaviour
{

    private int currentEmmotion;

    [SerializeField] Sprite[] emotionList;
    [SerializeField] Image curr;

    public enum Emotes
    {
        Happy = 0, Confused = 1, Angry = 2, Sad = 3, Interested = 4
    }


    public void NextEmotion()
    {
        currentEmmotion++;

        if (currentEmmotion > 4)
        {
            currentEmmotion = 0;
        }

        curr.sprite = emotionList[currentEmmotion];
       

    }

    public void PrevEmotion()
    {
        currentEmmotion--;

        if (currentEmmotion < 0)
        {
            currentEmmotion = 4;
        }

        curr.sprite = emotionList[currentEmmotion];

    }

    public void ChooseEmotion()
    {

        NarrativeManager.Instance.emotion = currentEmmotion;

    }
}
