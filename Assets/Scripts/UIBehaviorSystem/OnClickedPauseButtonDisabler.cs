using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class OnClickedPauseButtonDisabler : MonoBehaviour
{
    [SerializeField] private List<Button> buttons;

    public void ButtonDisabler(int index)
    {
        foreach (var button in buttons)
        {
            button.interactable = true;
        }

        buttons[index].interactable = false;
    }
}