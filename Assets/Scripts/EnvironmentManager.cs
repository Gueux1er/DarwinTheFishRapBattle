using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    public Material[] wallMaterials;

    [System.Serializable]
    public struct TextureLevel
    {
        public List<Texture> texturesList;
    }
    public List<TextureLevel> wallTextureLevels;

    void Start()
    {
        for (int i = 0; i < wallMaterials.Length; i++)
        {
            wallMaterials[i].mainTexture = wallTextureLevels[LevelManager.Instance.level].texturesList[i];
        }   
    }
}
