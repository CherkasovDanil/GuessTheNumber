using System;
using Game.UI;
using Game.UI.UIFramework.Interfaces;
using UnityEngine;
using Zenject;

public class BodyAnimation : MonoBehaviour
{
    [SerializeField] private GameObject[] bodyParts;
    [SerializeField] private ParticleSystem particleSystem;

    private UIGameWindowController _uiGameWindowController;
    private UIDeathWindow _uiDeathWindow;
    private UIWinWindow _uiWinWindow;
    
    private int _counter;


    [Inject]
    private void Inject(
        UIGameWindowController uiGameWindowController,
        IUIService uiService)
    {
        _uiGameWindowController = uiGameWindowController;
        _uiGameWindowController.OnAddBodyPart.AddListener(AddBodyPart);

        _uiDeathWindow = uiService.Get<UIDeathWindow>();
        _uiWinWindow = uiService.Get<UIWinWindow>();
        
        _uiGameWindowController.OnResetAnimation.AddListener(ResetAnimation);
        _uiDeathWindow.OnResetAnimation.AddListener(ResetAnimation);
        _uiWinWindow.OnResetAnimation.AddListener(ResetAnimation);
    }

    private void AddBodyPart()
    {
        particleSystem.gameObject.SetActive(true);
        if (_counter == 0)
        {
            bodyParts[_counter].gameObject.SetActive(true);
        }
        else
        {
            bodyParts[_counter].gameObject.transform.localScale = Vector3.one ;
        }
        particleSystem.Play();
       _counter++;
    }

    private void ResetAnimation()
    {
        _counter = 0;
        for (int i = 0; i < bodyParts.Length; i++)
        {
            if (i == 0)
            {
                bodyParts[i].gameObject.SetActive(false);
            }
            else
            {
                bodyParts[_counter].gameObject.transform.localScale = Vector3.zero ;
            }
        }
    }
}
