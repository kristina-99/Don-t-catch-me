using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ItemManager : MonoBehaviour
{
    public List<Image> itemTicks;
    public void DisableItemImages()
    {
        foreach (var image in itemTicks)
        {
            image.enabled = false;
        }
    }
}