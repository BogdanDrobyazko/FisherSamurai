using Kilosoft.Tools;
using UnityEngine;

public class SetIndex : MonoBehaviour
{
    //создвем кнопку в инспекторе пр нажатии на которую переименуем все дочерние кнопки с иконками рыб с
    //SC_ClickTracker в  CollectionFishButton_(порядковый номер дочерненго обьекта)

    [EditorButton("Set Indexes")]
    public void Index()
    {
        for (int i = 0; i < 50; i++)
        {
            transform.GetChild(i).GetComponent<SC_ClickTracker>().buttonName = $"CollectionFishButton_{i}";
        }
    }
}