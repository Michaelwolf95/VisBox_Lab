using UnityEngine;
using System.Collections;

public static class MiddleVRInput
{
    public static bool GetButton(uint buttonIndex)
    {
        vrButtons wandButtons = MiddleVR.VRDeviceMgr.GetWandButtons();
        uint buttonNb = wandButtons.GetButtonsNb();
        if (buttonIndex > buttonNb) return false;
        switch(buttonIndex)
        {
            case 0:
                return wandButtons.IsPressed(MiddleVR.VRDeviceMgr.GetWandButton0());
            case 1:
                return wandButtons.IsPressed(MiddleVR.VRDeviceMgr.GetWandButton1());
            case 2:
                return wandButtons.IsPressed(MiddleVR.VRDeviceMgr.GetWandButton2());
            case 3:
                return wandButtons.IsPressed(MiddleVR.VRDeviceMgr.GetWandButton3());
            case 4:
                return wandButtons.IsPressed(MiddleVR.VRDeviceMgr.GetWandButton4());
            case 5:
                return wandButtons.IsPressed(MiddleVR.VRDeviceMgr.GetWandButton5());
        }
        return false;
    }

    public static bool GetButtonDown(uint buttonIndex)
    {
        vrButtons wandButtons = MiddleVR.VRDeviceMgr.GetWandButtons();
        uint buttonNb = wandButtons.GetButtonsNb();
        if (buttonIndex > buttonNb) return false;
        switch (buttonIndex)
        {
            case 0:
                return wandButtons._IsToggledPressed(MiddleVR.VRDeviceMgr.GetWandButton0());
            case 1:
                return wandButtons._IsToggledPressed(MiddleVR.VRDeviceMgr.GetWandButton1());
            case 2:
                return wandButtons._IsToggledPressed(MiddleVR.VRDeviceMgr.GetWandButton2());
            case 3:
                return wandButtons._IsToggledPressed(MiddleVR.VRDeviceMgr.GetWandButton3());
            case 4:
                return wandButtons._IsToggledPressed(MiddleVR.VRDeviceMgr.GetWandButton4());
            case 5:
                return wandButtons._IsToggledPressed(MiddleVR.VRDeviceMgr.GetWandButton5());
        }
        return false;
    }
    public static bool GetButtonUp(uint buttonIndex)
    {
        vrButtons wandButtons = MiddleVR.VRDeviceMgr.GetWandButtons();
        uint buttonNb = wandButtons.GetButtonsNb();
        if (buttonIndex > buttonNb) return false;
        switch (buttonIndex)
        {
            case 0:
                return wandButtons._IsToggledReleased(MiddleVR.VRDeviceMgr.GetWandButton0());
            case 1:
                return wandButtons._IsToggledReleased(MiddleVR.VRDeviceMgr.GetWandButton1());
            case 2:
                return wandButtons._IsToggledReleased(MiddleVR.VRDeviceMgr.GetWandButton2());
            case 3:
                return wandButtons._IsToggledReleased(MiddleVR.VRDeviceMgr.GetWandButton3());
            case 4:
                return wandButtons._IsToggledReleased(MiddleVR.VRDeviceMgr.GetWandButton4());
            case 5:
                return wandButtons._IsToggledReleased(MiddleVR.VRDeviceMgr.GetWandButton5());
        }
        return false;
    }

    public static bool GetTriggerDown()
    {
        return MiddleVR.VRDeviceMgr.GetWandButtons().IsPressed(MiddleVR.VRDeviceMgr.GetWandButton0());
    }
    public static bool GetTrigger()
    {
        return MiddleVR.VRDeviceMgr.GetWandButtons()._IsToggledPressed(MiddleVR.VRDeviceMgr.GetWandButton0());
    }
    public static bool GetTriggerUp()
    {
        return MiddleVR.VRDeviceMgr.GetWandButtons()._IsToggledReleased(MiddleVR.VRDeviceMgr.GetWandButton0());
    }

    /*
     vrButtons wandButtons = m_DeviceMgr.GetWandButtons();

        if (wandButtons != null)
        {
            uint buttonNb = wandButtons.GetButtonsNb();
            if (buttonNb > 0)
            {
                WandButton0 = wandButtons.IsPressed(m_DeviceMgr.GetWandButton0());
            }
            if (buttonNb > 1)
            {
                WandButton1 = wandButtons.IsPressed(m_DeviceMgr.GetWandButton1());
            }
            if (buttonNb > 2)
            {
                WandButton2 = wandButtons.IsPressed(m_DeviceMgr.GetWandButton2());
            }
            if (buttonNb > 3)
            {
                WandButton3 = wandButtons.IsPressed(m_DeviceMgr.GetWandButton3());
            }
            if (buttonNb > 4)
            {
                WandButton4 = wandButtons.IsPressed(m_DeviceMgr.GetWandButton4());
            }
            if (buttonNb > 5)
            {
                WandButton5 = wandButtons.IsPressed(m_DeviceMgr.GetWandButton5());
            }
        }

        WandAxisHorizontal = m_DeviceMgr.GetWandHorizontalAxisValue();
        WandAxisVertical = m_DeviceMgr.GetWandVerticalAxisValue();
    */


}
