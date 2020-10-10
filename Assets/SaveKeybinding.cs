using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SaveKeybinding : MonoBehaviour
{
    public KeyBindingButton horizontalWalkLeftKeyboard;
    public KeyBindingButton horizontalWalkLeftAltKeyboard;
    public KeyBindingButton horizontalWalkRightKeyboard;
    public KeyBindingButton horizontalWalkRightAltKeyboard;
    public KeyBindingButton verticalWalkUpKeyboard;
    public KeyBindingButton verticalWalkUpAltKeyboard;
    public KeyBindingButton verticalWalkDownKeyboard;
    public KeyBindingButton verticalWalkDownAltKeyboard;
    public KeyBindingButton pauseparkpadKeyboard;
    public KeyBindingButton worldInteractKeyboard;
    public KeyBindingButton submitKeyboard;
    public KeyBindingButton QTE1Keyboard;
    public KeyBindingButton QTE2Keyboard;
    public KeyBindingButton QTE3Keyboard;
    public KeyBindingButton QTE4Keyboard;
    public KeyBindingButton QTE5Keyboard;

    public KeyBindingButton pauseparkpadController;
    public KeyBindingButton worldInteractController;
    public KeyBindingButton submitController;
    public KeyBindingButton QTE1Controller;
    public KeyBindingButton QTE2Controller;
    public KeyBindingButton QTE3Controller;
    public KeyBindingButton QTE4Controller;
    public KeyBindingButton QTE5Controller;
    public PlayerData playerData;

    public List<KeyBindingButton> keyBindingList = new List<KeyBindingButton>();

    // Start is called before the first frame update
    void Start()
    {
        LoadInput();
    }

    public void SaveInput()
    {
        if(horizontalWalkLeftKeyboard.kCode != KeyCode.None)
        {
            playerData.walkHorizontalLeftKeyboard = horizontalWalkLeftKeyboard.kCode;
        }

        if (horizontalWalkLeftAltKeyboard.kCode != KeyCode.None)
        {
            playerData.walkHorizontalLeftKeyboardAlt = horizontalWalkLeftAltKeyboard.kCode;
        }

        if (horizontalWalkRightKeyboard.kCode != KeyCode.None)
        {
            playerData.walkHorizontalRightKeyboard = horizontalWalkRightKeyboard.kCode;
        }

        if (horizontalWalkRightAltKeyboard.kCode != KeyCode.None)
        {
            playerData.walkHorizontalRightKeyboardAlt = horizontalWalkRightAltKeyboard.kCode;
        }

        if (verticalWalkUpKeyboard.kCode != KeyCode.None)
        {
            playerData.walkVerticalUpKeyboard = verticalWalkUpKeyboard.kCode;
        }

        if (verticalWalkUpAltKeyboard.kCode != KeyCode.None)
        {
            playerData.walkVerticalUpKeyboardAlt = verticalWalkUpAltKeyboard.kCode;
        }

        if (verticalWalkDownKeyboard.kCode != KeyCode.None)
        {
            playerData.walkVerticalDownKeyboard = verticalWalkDownKeyboard.kCode;
        }

        if (verticalWalkDownAltKeyboard.kCode != KeyCode.None)
        {
            playerData.walkVerticalDownKeyboardAlt = verticalWalkDownAltKeyboard.kCode;
        }

        if (submitKeyboard.kCode != KeyCode.None)
        {
            playerData.submitKeyboard = submitKeyboard.kCode;
        }

        if (worldInteractKeyboard.kCode != KeyCode.None)
        {
            playerData.interactKeyboard = worldInteractKeyboard.kCode;
        }

        if (pauseparkpadKeyboard.kCode != KeyCode.None)
        {
            playerData.pauseKeyboard = pauseparkpadKeyboard.kCode;
        }

        if (QTE1Keyboard.kCode != KeyCode.None)
        {
            playerData.quicktimeKeyboard1 = QTE1Keyboard.kCode;
        }

        if (QTE2Keyboard.kCode != KeyCode.None)
        {
            playerData.quicktimeKeyboard2 = QTE2Keyboard.kCode;
        }

        if (QTE3Keyboard.kCode != KeyCode.None)
        {
            playerData.quicktimeKeyboard3 = QTE3Keyboard.kCode;
        }

        if (QTE4Keyboard.kCode != KeyCode.None)
        {
            playerData.quicktimeKeyboard4 = QTE4Keyboard.kCode;
        }

        if (QTE5Keyboard.kCode != KeyCode.None)
        {
            playerData.quicktimeKeyboard5 = QTE5Keyboard.kCode;
        }
    }

    public void LoadInput()
    {
        if(playerData.walkHorizontalLeftKeyboard != horizontalWalkLeftKeyboard.defaultValue)
        {
            horizontalWalkLeftKeyboard.kCode = playerData.walkHorizontalLeftKeyboard;
        }

        if(playerData.walkHorizontalRightKeyboard != horizontalWalkRightKeyboard.defaultValue)
        {
            horizontalWalkRightKeyboard.kCode = playerData.walkHorizontalRightKeyboard;
        }

        if(playerData.walkHorizontalRightKeyboardAlt != horizontalWalkRightAltKeyboard.defaultValue)
        {
            horizontalWalkRightAltKeyboard.kCode = playerData.walkHorizontalRightKeyboardAlt;
        }

        if(playerData.walkHorizontalRightKeyboardAlt != horizontalWalkRightAltKeyboard.defaultValue)
        {
            horizontalWalkRightAltKeyboard.kCode = playerData.walkHorizontalRightKeyboardAlt;
        }

        if(playerData.walkVerticalUpKeyboard != verticalWalkUpKeyboard.defaultValue)
        {
            verticalWalkUpKeyboard.kCode = playerData.walkVerticalUpKeyboard;
        }

        if(playerData.walkVerticalUpKeyboardAlt != verticalWalkUpAltKeyboard.defaultValue)
        {
            verticalWalkUpAltKeyboard.kCode = playerData.walkVerticalUpKeyboardAlt;
        }

        if(playerData.walkVerticalDownKeyboard != verticalWalkDownKeyboard.defaultValue)
        {
            verticalWalkDownKeyboard.kCode = playerData.walkVerticalDownKeyboard;
        }

        if(playerData.walkVerticalDownKeyboardAlt != verticalWalkDownAltKeyboard.defaultValue)
        {
            verticalWalkDownAltKeyboard.kCode = playerData.walkVerticalDownKeyboardAlt;
        }

        if(playerData.submitKeyboard != submitKeyboard.defaultValue)
        {
            submitKeyboard.kCode = playerData.submitKeyboard;
        }

        if(playerData.interactKeyboard != worldInteractKeyboard.defaultValue)
        {
            worldInteractKeyboard.kCode = playerData.interactKeyboard;
        }

        if(playerData.pauseKeyboard != pauseparkpadKeyboard.defaultValue)
        {
            pauseparkpadKeyboard.kCode = playerData.pauseKeyboard;
        }

        if(playerData.quicktimeKeyboard1 != QTE1Keyboard.defaultValue)
        {
            QTE1Keyboard.defaultValue = playerData.quicktimeKeyboard1;
        }

        if (playerData.quicktimeKeyboard2 != QTE2Keyboard.defaultValue)
        {
            QTE2Keyboard.defaultValue = playerData.quicktimeKeyboard2;
        }

        if (playerData.quicktimeKeyboard3 != QTE3Keyboard.defaultValue)
        {
            QTE3Keyboard.defaultValue = playerData.quicktimeKeyboard3;
        }

        if (playerData.quicktimeKeyboard4 != QTE4Keyboard.defaultValue)
        {
            QTE4Keyboard.defaultValue = playerData.quicktimeKeyboard4;
        }

        if (playerData.quicktimeKeyboard5 != QTE5Keyboard.defaultValue)
        {
            QTE5Keyboard.defaultValue = playerData.quicktimeKeyboard5;
        }
    }
}
