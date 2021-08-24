using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CharacterSelect : MonoBehaviour
{
    [Header("Characters List")]
    [SerializeField]
    Sprite[] PlayerImages;
    [SerializeField]
    GameObject PlayerImage;


    [Header("Buttons")]
    [SerializeField]
    GameObject previousButton;
    [SerializeField]
    GameObject nextButton;

    int index;
    [Header("Prefabs")]
    [SerializeField]
    GameObject[] PlayerPrefabs;
    [SerializeField]
    int prefabIndex;
    [Header("Sounds")]
    [SerializeField]
    AudioSource Source;
    [SerializeField]
    AudioClip buttonPress;
    private void Start()
    {
        index = 0;
        previousButton.SetActive(false);
    }
    public void PreviousCharacter()
    {
        Source.PlayOneShot(buttonPress);
        if (index >= 1)
        {
            PlayerImage.GetComponent<Image>().sprite = PlayerImages[--index];
            if (index == 0)
                previousButton.SetActive(false);
            if (index == PlayerImages.Length - 2)
                nextButton.SetActive(true);

        }
      
    }
    public void NextCharacter()
    {
        Source.PlayOneShot(buttonPress);
        if (index < PlayerImages.Length-1)
        {
           
            PlayerImage.GetComponent<Image>().sprite = PlayerImages[++index];
            if (index == PlayerImages.Length - 1)
                nextButton.SetActive(false);
            if (index == 1)
                previousButton.SetActive(true);

        }
      
    }
    public int getIndex()
    {
        return index;
    }
}
