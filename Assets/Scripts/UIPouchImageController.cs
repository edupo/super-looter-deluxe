using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIPouchImageController : MonoBehaviour
{
    public Image image;
    public Texture2D texture;

    public void Start()
    {
        texture = image.sprite.texture;
        Color[] pixels = texture.GetPixels();
        for (int i = 0; i < pixels.Length; i++)
        {
            pixels[i].a = 0;
        }
        texture.SetPixels(0, 0, texture.width, texture.height, pixels);
        texture.Apply(false);
    }

    public void Picked(Object obj)
    {
        var item = obj as Item;

        var rect = item.data.sprite.textureRect;
        int sw = (int)rect.width;
        int sh = (int)rect.height;
        int sx = (int)rect.x;
        int sy = (int)rect.y;
        var sp = item.data.sprite.texture.GetPixels(sx, sy, sw, sh);

        int iw = texture.width;
        int ih = texture.height;
        int ix = Random.Range(0, iw - sw);
        int iy = Random.Range(0, ih - sh);
        var ip = texture.GetPixels(ix, iy, sw, sh);

        for (int i = 0; i < sp.Length; i++)
        {
            if (sp[i].a > .5f)
                sp[i] = sp[i] * item.data.color;
            else
                sp[i] = ip[i];
        }

        texture.SetPixels(ix, iy, sw, sh, sp);
        texture.Apply(false);
    }

    private void OnValidate()
    {
        if (!image)
            image = GetComponent<Image>();
    }
}
