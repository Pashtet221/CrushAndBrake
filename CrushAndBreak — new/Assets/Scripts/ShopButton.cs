using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour
{
    [SerializeField] Image onShop;
    [SerializeField] Image offShop;
    private bool opened = false;


    public void OnButtonPress()
    {
        if(!opened)
        {
            opened = true;
            CharacterShopUI.instance.OpenShop();
        }
        else
        {
            opened = false;
            CharacterShopUI.instance.CloseShop();
        }
        UpdateButtonIcon();
    }

    private void UpdateButtonIcon()
    {
        if(!opened)
        {
            onShop.enabled = true;
            offShop.enabled = false;
        }
        else
        {
            onShop.enabled = false;
            offShop.enabled = true;
        }
    }
}
