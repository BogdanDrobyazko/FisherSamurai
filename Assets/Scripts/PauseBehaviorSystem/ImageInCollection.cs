using System.Collections.Generic;
using Kilosoft.Tools;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ImageInCollection : MonoBehaviour
{
    [SerializeField] private int _childsCount;

    // массив спрайтов рыб
    [SerializeField] private List<Sprite> fishSprites;
    [SerializeField] private List<Sprite> framesSprites;
    [SerializeField] private List<FishData> fishesData;

    [FormerlySerializedAs("_interectableAlfaForButton")] [Range(0f, 1f)] [SerializeField]
    private float _interectableButtonAlfa;

    [FormerlySerializedAs("_noninterectableAlfaForButton")] [Range(0f, 1f)] [SerializeField]
    private float _noninterectableButtonAlfa;

    [Range(0f, 150f)] [SerializeField] private float _sizeOfFishImage;

    private Dictionary<string, int> _rarenesDictionary;


    // создаем кнопку в инспекторе при вызове которой положим в дочерние обьекты соответсвенную картинку из массива
    [EditorButton("Set Images From Sprites to Childs")]
    public void SetImages()
    {
        _rarenesDictionary = new Dictionary<string, int>()
        {
            ["D"] = 0,
            ["C"] = 1,
            ["B"] = 2,
            ["A"] = 3,
            ["S"] = 4
        };
        
        for (int i = 0; i < _childsCount; i++)
        {
            
            transform.GetChild(i).GetComponent<Image>().sprite =
                framesSprites[0];
            transform.GetChild(i).transform.GetChild(0).GetComponent<Image>().sprite = fishSprites[i];
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


            transform.GetChild(i).transform.GetChild(0).GetComponent<Image>().sprite = fishSprites[i];
            transform.GetChild(i).transform.GetChild(0).GetComponent<Image>().rectTransform.sizeDelta =
                new Vector2(_sizeOfFishImage, _sizeOfFishImage);
        }
    }
}