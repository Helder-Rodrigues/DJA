using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DetectDoor : MonoBehaviour
{
    [SerializeField] private Texture2D[] images;

    [SerializeField] private RawImage displayImage;

    private char currImg;

    private void OnTriggerEnter(Collider other)
    {
        if (CollidedIsADoor(other.name))
            DisplayRandomBtn();
    }

    private void OnTriggerExit(Collider other)
    {
        if (CollidedIsADoor(other.name))
            displayImage.gameObject.SetActive(false);
    }

    private bool CollidedIsADoor(string collName) => collName.ToLower().Contains("door");

    private void DisplayRandomBtn()
    {
        if (images.Length == 0)
        {
            Debug.LogWarning("No images assigned.");
            return;
        }

        int newImgIndex = Random.Range(0, images.Length);

        displayImage.texture = images[newImgIndex];
        displayImage.gameObject.SetActive(true);

        currImg = images[newImgIndex].name[0];
    }
}
