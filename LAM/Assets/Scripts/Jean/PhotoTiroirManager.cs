using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoTiroirManager : MonoBehaviour
{
    public GameObject photoBtn;
    public GameObject zoomedPhotoBtn;
    public GameObject darkBackground;
    public GameObject backButton;

    public void ZoomOnPhoto()
    {
        zoomedPhotoBtn.SetActive(true);
        darkBackground.SetActive(true);
        photoBtn.SetActive(false);
        backButton.SetActive(false);
    }

    public void DezoomPhoto()
    {
        zoomedPhotoBtn.SetActive(false);
        darkBackground.SetActive(false);
        photoBtn.SetActive(true);
        backButton.SetActive(true);
    }
}
