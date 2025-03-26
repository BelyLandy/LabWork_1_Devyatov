using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{

    private Vector2 _movementInput;
    private bool _isFullscreenedOrWindowed;
    
    public event Action<PlayerInputData> OnInputChanged;

    private void OnMoveCells(InputValue inputValue)
    {
        _movementInput = inputValue.Get<Vector2>();
        
        Debug.Log("Movement Input: " + _movementInput);
        
        TriggerInputEvent();
    }

    private void OnFullscreenOrWindowed()
    {
        _isFullscreenedOrWindowed = true;
        TriggerInputEvent();
        _isFullscreenedOrWindowed = false;
        
        Debug.Log("FullscreenOrWindowed Activated!");
    }
    
    private void OnQuitApplication()
    {
        #if UNITY_EDITOR
            // Если игра запущена в редакторе, остановить выполнение игры
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            // Если игра запущена в билде, закрыть приложение
            Application.Quit();
        #endif
        
        _isFullscreenedOrWindowed = true;
        TriggerInputEvent();
        _isFullscreenedOrWindowed = false;
        
        Debug.Log("QuitApplication Activated!");
    }
    
    private void TriggerInputEvent() => OnInputChanged?.Invoke(new PlayerInputData(
        _movementInput,
        _isFullscreenedOrWindowed
        ));
}
