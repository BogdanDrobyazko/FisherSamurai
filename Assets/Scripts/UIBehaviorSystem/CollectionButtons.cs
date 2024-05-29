using System.Collections.Generic;
using Kilosoft.Tools;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class CollectionButtons : MonoBehaviour
{
    [SerializeField] private int _childsCount;

    // массив спрайтов рыб
    [SerializeField] private Sprite _notAvailableFishSprite;
    [FormerlySerializedAs("fishSprites")] [SerializeField] private List<Sprite> _fishSprites;
    [FormerlySerializedAs("fishSpritesShadowed")] [SerializeField] private List<Sprite> _fishSpritesShadowed;
    [FormerlySerializedAs("framesSprites")] [SerializeField] private List<Sprite> _framesSprites;
    [FormerlySerializedAs("fishesData")] [SerializeField] private List<FishData> _fishesData;
    [Range(0f, 1f)] [SerializeField] private float _interectableButtonAlfa;
    [Range(0f, 1f)] [SerializeField] private float _noninterectableButtonAlfa;
    [Range(0f, 150f)] [SerializeField] private float _sizeOfFishImage;


    public void SetImage(int buttonIndex, bool isAvailable, bool isСaught)
    {
        if (buttonIndex < _childsCount || buttonIndex >= 0)
        {
            Image fishImage = transform.GetChild(buttonIndex).transform.GetChild(0).GetComponent<Image>();
            Button fishButton = transform.GetChild(buttonIndex).GetComponent<Button>();
            if (isAvailable)
            {
                if (isСaught)
                {
                    fishImage.sprite = _fishSprites[buttonIndex]; 
                    fishImage.color = new Color(1, 1, 1, 1);
                    fishButton.interactable = true;
                }
                else
                {
                    fishImage.sprite = _fishSpritesShadowed[buttonIndex];
                    fishImage.color = new Color(1, 1, 1, 0.2f);
                    fishButton.interactable = false;
                }
            }
            else
            {
                fishImage.sprite = _notAvailableFishSprite;
                fishImage.color = new Color(1, 1, 1, 0.2f);
                fishButton.interactable = false;
            }
        }
    }

    // создаем кнопку в инспекторе при вызове которой положим в дочерние обьекты соответсвенную картинку из массива

    [EditorButton("Set Images From Sprites to Childs")]
    public void SetImages()
    {
        for (int i = 0; i < _childsCount; i++)
        {
            transform.GetChild(i).GetComponent<Image>().sprite =
                _framesSprites[0];
            transform.GetChild(i).transform.GetChild(0).GetComponent<Image>().sprite = _fishSprites[i];
            transform.GetChild(i).transform.GetChild(0).GetComponent<Image>().rectTransform.sizeDelta =
                new Vector2(70, 70);
        }
    }


    [EditorButton("Change Names")]
    public void ChangeNames()
    {
        for (int i = 0; i < _childsCount; i++)
        {
            var remove = transform.GetChild(i).name;
            transform.GetChild(i).name = "CollectionFishButton" + remove;

            //new string {"CollectionFish"}
        }
    }

    [EditorButton("Set Images Size")]
    public void SetImagesSize()
    {
        for (int i = 0; i < _childsCount; i++)
        {
            transform.GetChild(i).transform.GetChild(0).GetComponent<Image>().rectTransform.sizeDelta =
                new Vector2(_sizeOfFishImage, _sizeOfFishImage);
        }
    }

    [EditorButton("Set Alfa For Buttons")]
    public void SetButtonsAlfa()
    {
        for (int i = 0; i < _childsCount; i++)
        {
            Button button = transform.GetChild(i).GetComponent<Button>();
            ColorBlock colorBlock = button.colors;
            colorBlock.normalColor = new Color(colorBlock.normalColor.r, colorBlock.normalColor.g,
                colorBlock.normalColor.b, _noninterectableButtonAlfa);


            transform.GetChild(i).transform.GetChild(0).GetComponent<Image>().sprite = _fishSprites[i];
            transform.GetChild(i).transform.GetChild(0).GetComponent<Image>().rectTransform.sizeDelta =
                new Vector2(_sizeOfFishImage, _sizeOfFishImage);
        }
    }
}