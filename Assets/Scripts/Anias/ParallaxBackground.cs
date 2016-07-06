#define DEBUG 
// #undef DEBUG
// #if statement in C# is Boolean and only tests whether the symbol has been defined or not. 
// The scope of a symbol created with #define is the file.
/// <seealso cref="https://msdn.microsoft.com/en-us/library/wkxst87d.aspx"/>
/// <see cref="http://docs.unity3d.com/Manual/PlatformDependentCompilation.html"/>

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/// <summary>
///  Usage:
///  1. Make empty gameobject + add ParallaxBackground 
///  2. add the layers as children to this game object + add ParallaxLayer to layers. 
///     The layers will move with parallaxFactor (1 speed of the camera; 0.5 half..)
///  3. Add script to ortographic camera +ParallaxCamera 
///     ParallaxCamera calls delegate onCameraTranslate in Update function.
///     /*
///     it does not need to know about ParallaxBackground at all just executes delegate
///     */
///     ParallaxBackground adds function Move(float delta) this delegate //parallaxCamera.onCameraTranslate += Move;
///     Move for each ParallaxLayer calls ParallaxLayer.Move(delta) where delta is the distance that the ortho camera moved 
///  You can always call ParallaxBackground.Move(float delta) from code without using delegates
/// </summary>
/// <see cref="http://answers.unity3d.com/questions/551808/parallax-scrolling-using-orthographic-camera.html"/>
/// <seealso cref="https://msdn.microsoft.com/en-us/library/aa288459(v=vs.71).aspx"/>

[ExecuteInEditMode]
public class ParallaxBackground : MonoBehaviour
{
    public ParallaxCamera parallaxCamera;

    List<ParallaxLayer> parallaxLayers = new List<ParallaxLayer>();

    void Start()
    {
        if (parallaxCamera == null)
            //if camera not passed assume main camera has the ParallaxCamera component
            parallaxCamera = Camera.main.GetComponent<ParallaxCamera>();
        if (parallaxCamera != null)
            //add function Move to parallaxCamera.onCameraTranslate delegate
            parallaxCamera.onCameraTranslate += Move;
        SetLayers();
    }
    /// <summary>
    /// 1.Seeks for ParallaxLayer component in children
    /// 1. adds those kids to the parallaxLayers list and name them. 
    /// </summary>
    void SetLayers()
    {
        parallaxLayers.Clear();
        for (int i = 0; i < transform.childCount; i++)
        {
            ParallaxLayer layer = transform.GetChild(i).GetComponent<ParallaxLayer>();

            if (layer != null)
            {
                #if UNITY_EDITOR
                layer.name = "Layer-" + i;//sets the name of the affected object - it is visible in the inspector
                #endif
                parallaxLayers.Add(layer);
            }
        }
    }
    /// <summary>
    ///     calls move() for each layer
    /// </summary>
    /// <param name="delta"></param>
    void Move(float delta)
    {
        foreach (ParallaxLayer layer in parallaxLayers)
        {
            layer.Move(delta);
        }
    }
}