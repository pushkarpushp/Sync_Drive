using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LoadOnlineImageToCanvas : MonoBehaviour
{
    public string TextureURL = "";

    // Start is called before the first frame update
    void Start()
    {
        SetMusicBanner(TextureURL);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetMusicBanner(string banner)
    {
        StartCoroutine(DownloadImage(banner));
    }

    IEnumerator DownloadImage(string MediaUrl)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
        }
        else
        {
            Texture2D webTexture = DownloadHandlerTexture.GetContent(request);

            // Adjust texture settings for better quality
            webTexture.filterMode = FilterMode.Bilinear; // or FilterMode.Trilinear for even better quality
            webTexture.anisoLevel = 9; // Increase anisotropic level
            webTexture.wrapMode = TextureWrapMode.Clamp; // Ensure proper wrapping

            gameObject.GetComponent<RawImage>().texture = webTexture;
        }
    }

    Sprite SpriteFromTexture2D(Texture2D texture)
    {
        return Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
    }
}