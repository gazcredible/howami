using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class playa : MonoBehaviour
{
    public RenderTexture rt;

    public int width = 720;
    public int height = 1280;
    public int skipFrames = 0;
    
    void  Start()
    {
        rt = new RenderTexture(width, height, 16, RenderTextureFormat.ARGB32);
        rt.Create();

        transform.Find("Video Player").GetComponent<VideoPlayer>().targetTexture = rt;
        transform.Find("video_screen").GetComponent<RawImage>().texture = rt;
        
        transform.Find("Video Player").GetComponent<VideoPlayer>().frame = skipFrames;
    }

    // Update is called once per frame
    void Update()
    {        
    }
}