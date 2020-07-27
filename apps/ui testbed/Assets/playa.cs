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
        
        var control = GameObject.Find("Canvas").GetComponent<UITestbed>();

        if (control.playMyLovelyVideo == true)
        {
            vp.clip = control.testVideo;
            transform.localScale = new Vector3(2,2,1);
            transform.localEulerAngles = new Vector3(0,0,-90);
        }
        else
        {
            vp.clip = control.properVideo;
            transform.localScale = new Vector3(1,1,1);
            transform.localEulerAngles = new Vector3(0,0,0);
        }

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