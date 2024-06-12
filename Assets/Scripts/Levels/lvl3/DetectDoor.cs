using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DetectDoor : MonoBehaviour
{
    [SerializeField] private Texture2D[] images;
    [SerializeField] private RawImage displayImage;

    [SerializeField] private AudioSource destroyWallSound;
    
    private Collider currColider;

    private char currImg;

    private void Start()
    {
        float vision = GameManager.visionValue * (1 + GameManager.visionPerc / 25);
        
        Vector3 currScale = this.transform.localScale;
        currScale.z = vision;
        this.transform.localScale = currScale;

        Vector3 currPosition = this.transform.localPosition;
        currPosition.z = (vision - 3) * 0.5f + 1.75f;
        this.transform.localPosition = currPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (CollidedIsADoor(other.name))
        {
            DisplayRandomBtn();
            currColider = other;
        }
    }

    private void Update()
    {
        if (displayImage.IsActive() && Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), currImg.ToString().ToUpper())))
        {
            destroyWallSound.Play();
            currColider.gameObject.SetActive(false);
            displayImage.gameObject.SetActive(false);
            currColider = null;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (CollidedIsADoor(other.name))
        {
            displayImage.gameObject.SetActive(false);
            currColider = null;
        }
    }

    private bool CollidedIsADoor(string collName) => collName.ToLower().Contains("cube");

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
