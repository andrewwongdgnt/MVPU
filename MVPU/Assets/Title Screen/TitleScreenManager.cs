﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreenManager : MonoBehaviour
{
    private const float Y_POSITION = -200f;

    public GameObject temp;

    public Image splash;
    public Canvas canvas;

    [Tooltip("At least 2  images required")]
    public Image[] images;

    private List<ImageBucket> clonedImages = new List<ImageBucket>();

    private class ImageBucket
    {
        public Image Image { get; set; }
        public bool HasSpawnedAnother { get; set; }

        internal ImageBucket(Image image)
        {
            Image = image;
        }
    }

    private int imageIndexToSpawn;
    // Use this for initialization
    void Start()
    {
        temp.SetActive(false);

        //Shuffle
        for (int i = 0; i < images.Length; i++)
        {
            Image tmp = images[i];
            int r = UnityEngine.Random.Range(i, images.Length);
            images[i] = images[r];
            images[r] = tmp;
        }

        var canvasRect = canvas.transform as RectTransform;

        Spawn(images[0], -(canvasRect.rect.width / 2 + images[0].rectTransform.rect.width / 2) / 3);
        Spawn(images[1], (canvasRect.rect.width / 2 + images[1].rectTransform.rect.width / 2) / 3);


    }

    void Update()
    {
        List<ImageBucket> clonedImagesToRemove = new List<ImageBucket>();
        List<ImageBucket> clonedImagesToSpawn = new List<ImageBucket>();
        var canvasRect = canvas.transform as RectTransform;

        clonedImages.ForEach(imageBucket =>
        {
            Image image = imageBucket.Image;
            bool hasSpawnedAnother = imageBucket.HasSpawnedAnother;
            float newPos = image.rectTransform.localPosition.x - 1.5f;
            image.rectTransform.localPosition = new Vector3(newPos, Y_POSITION, 0);


            float leftEnd = -(canvasRect.rect.width / 2 + image.rectTransform.rect.width / 2);

            if (newPos < leftEnd)
            {
                clonedImagesToRemove.Add(imageBucket);
                Destroy(image.gameObject);
            }

            float spawnPoint = leftEnd / 3;

            if (newPos < spawnPoint && !hasSpawnedAnother)
            {
                imageBucket.HasSpawnedAnother = true;
                clonedImagesToSpawn.Add(imageBucket);
            }
        });

        clonedImagesToRemove.ForEach(imageBucket =>
        {
            clonedImages.Remove(imageBucket);
        });

        clonedImagesToSpawn.ForEach(imageBucket =>
        {
            Image image = imageBucket.Image;
            float rightEnd = canvasRect.rect.width / 2 + image.rectTransform.rect.width / 2;
            Spawn(images[imageIndexToSpawn], rightEnd);
        });


    }

    void Spawn(Image source, float relativePosition)
    {

        Image clone = Instantiate(source, new Vector3(0, 0, 0), Quaternion.identity, canvas.transform);
        clone.transform.localPosition = new Vector3(relativePosition, Y_POSITION, 0);
        clonedImages.Add(new ImageBucket(clone));
        imageIndexToSpawn++;
        if (imageIndexToSpawn >= images.Length)
        {
            imageIndexToSpawn = 0;
        }

    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("Main");

    }
}
