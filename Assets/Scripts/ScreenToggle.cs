using UnityEngine;

public class ScreenToggle : MonoBehaviour
{
    public int windowWidth = 720;
    public int windowHeight = 1280;
    
    private bool isFullScreen = false;

    private PlayerInputHandler _playerInputHandler;

    private void Awake()
    {
        _playerInputHandler = GetComponent<PlayerInputHandler>();
    }

    void Start()
    {
        Screen.SetResolution(windowWidth, windowHeight, false);
    }

    private void OnEnable()
    {
        if (_playerInputHandler != null)
        {
            _playerInputHandler.OnInputChanged += UpdateInput;
        }
    }

    private void OnDisable()
    {
        if (_playerInputHandler != null)
        {
            _playerInputHandler.OnInputChanged -= UpdateInput;
        }
    }
    
    private void UpdateInput(PlayerInputData inputData)
    {
        if (inputData.IsFullscreenedOrWindowed)
        {
            DoFullscreenOrWindowed();
        }
    }
    
    void DoFullscreenOrWindowed()
    {
        isFullScreen = !isFullScreen;
        if (isFullScreen)
        {
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
            Screen.fullScreen = true;
        }
        else
        {
            Screen.fullScreen = false;
            Screen.SetResolution(windowWidth, windowHeight, false);
        }
    } 
}