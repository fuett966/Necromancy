using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class CiircularMenu : MonoBehaviour
{
    public delegate void MethodContainer();
    public event MethodContainer onButtonPressed;



    public List<MenuButton> buttons = new List<MenuButton>();
    private Vector2 mousePosition;
    private Vector2 fromVector2M = new Vector2(0.5f, 1.0f);
    private Vector2 centreCircle = new Vector2(0.5f, 0.5f);
    private Vector2 toVector2M;

    public int menuItems;
    public int currentMenuItem;
    private int oldMenuItem;

    public Canvas menuCanvas;

    private bool _currentButtonIsPressed;
    public bool _CurrentButtonIsPressed => _currentButtonIsPressed;

    // Start is called before the first frame update
    private void Awake()
    {
        menuCanvas = GetComponentInChildren<Canvas>();
    }
    void Start()
    {
        menuCanvas.gameObject.SetActive(false);
        menuItems = buttons.Count;
        foreach (MenuButton button in buttons)
        {
            button.sceneImage.color = button.normalColor;
        }
        currentMenuItem = 0;
        oldMenuItem = 0;
    }

    // Update is called once per frame
    void Update()
    {
        GetCurrentMenuItem();
        if(Input.GetButtonDown("Fire1"))
        {
            ButtonAction();
        }
    }
    public void GetCurrentMenuItem()
    {
        mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        toVector2M = new Vector2(mousePosition.x / Screen.width, mousePosition.y / Screen.height);
        float angle = (Mathf.Atan2(fromVector2M.y - centreCircle.y, fromVector2M.x - centreCircle.x) - Mathf.Atan2(toVector2M.y - centreCircle.y, toVector2M.x - centreCircle.x))* Mathf.Rad2Deg;
        if(angle< 0)
        {
            angle += 360;
        }
        currentMenuItem = (int)(angle / (360 / menuItems));
        if(currentMenuItem != oldMenuItem)
        {
            buttons[oldMenuItem].sceneImage.color = buttons[oldMenuItem].normalColor;
            buttons[oldMenuItem]._isPressed = false;
            oldMenuItem = currentMenuItem;
            buttons[oldMenuItem].sceneImage.color = buttons[currentMenuItem].highLightedColor;
        }
    }
    public void ButtonAction()
    {
        if(!menuCanvas.gameObject.activeSelf)
        {
            _currentButtonIsPressed = false;
            return;
        }
        _currentButtonIsPressed = true;

        buttons[currentMenuItem].sceneImage.color = buttons[currentMenuItem].pressedColor;
        buttons[currentMenuItem]._isPressed = true;
        onButtonPressed();

        switch (currentMenuItem)
        {
            case 0:
                print("Button1");

                break;
            case 1:
                print("Button2");
                break;
            case 2:
                print("Button3");
                break;
            case 3:
                print("Button4");
                break;
            default:
                break;
        }

    }
}
[System.Serializable]
public class MenuButton
{
    public string name;
    public Image sceneImage;
    public Color normalColor = Color.white;
    public Color highLightedColor = Color.gray;
    public Color pressedColor = Color.gray;
    public bool _isPressed = false;
}
