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

    public VideoPlayer vp;
    
    void  Start()
    {
        rt = new RenderTexture(width, height, 16, RenderTextureFormat.ARGB32);
        rt.Create();

        vp = transform.Find("Video Player").GetComponent<VideoPlayer>(); 

        vp.targetTexture = rt;
        transform.Find("video_screen").GetComponent<RawImage>().texture = rt;
        
        vp.frame = skipFrames;
    }

    // Update is called once per frame
    void Update()
    {     
        //Debug.LogWarning(vp.frame.ToString() +'/'+ vp.frameCount.ToString() + " "+vp.isPlaying.ToString());
    }

    public bool isVideoDone
    {
        get
        {
            return (vp.frame > 10) && (vp.isPlaying == false);
        }
    }
}