using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class ASCIIRenderer : MonoBehaviour
{
    [SerializeField] private ComputeShader _ASCIIShader;

    [SerializeField] private Camera _camera;
    [SerializeField] private RenderTexture _outputTexture;

    [SerializeField] private Font _font;

    [SerializeField] private Material _outputMaterial;

    [SerializeField] private float _contrast;

    private Vector2Int _resolution;


    private Texture2DArray _glyphTextureArray;
    private ComputeBuffer _glyphCoveragesBuffer;

    public void OnFontsGenerated()
    {
        InitializeGlyphTextureArray();
        _resolution = new Vector2Int(Screen.width, Screen.height);

        _outputTexture = new RenderTexture(_resolution.x, _resolution.y, 0, RenderTextureFormat.ARGB32);
        _outputTexture.enableRandomWrite = true;
        _outputTexture.Create();

        InitializeGlyphCoveragesBuffer();
        _ASCIIShader.SetTexture(0, "glyphTextures", _glyphTextureArray);
        _ASCIIShader.SetInt("textureWidth", _resolution.x);
        _ASCIIShader.SetInt("textureHeight", _resolution.y);
        _ASCIIShader.SetInt("numGlyphs", _font._glyphs.Length);
        _ASCIIShader.SetFloat("contrast", _contrast);
    }


    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        EnsureOutputTexture(src);

        _ASCIIShader.SetTexture(0, "cameraTexture", src);
        _ASCIIShader.SetTexture(0, "outputTexture", _outputTexture);
        _ASCIIShader.SetFloat("contrast", _contrast);

        int threadGroupsX = Mathf.CeilToInt(_resolution.x / 16.0f);
        int threadGroupsY = Mathf.CeilToInt(_resolution.y / 16.0f);
        _ASCIIShader.Dispatch(0, threadGroupsX, threadGroupsY, 1);

        if (_outputMaterial != null)
            Graphics.Blit(_outputTexture, dest, _outputMaterial);
        else
            Graphics.Blit(_outputTexture, dest);
    }

    private void InitializeGlyphTextureArray()
    {
        int glyphCount = _font._glyphs.Length;
        int glyphSize = 16;

        _glyphTextureArray = new Texture2DArray(glyphSize,glyphSize, glyphCount, TextureFormat.ARGB32, false);

        for (int i = 0; i < glyphCount; i++)
        {
            Texture2D glyphTexture = new Texture2D(glyphSize, glyphSize, TextureFormat.ARGB32, false);
            glyphTexture.SetPixels(_font._glyphs[i].texture);
            glyphTexture.Apply();

            Graphics.CopyTexture(glyphTexture, 0, 0, _glyphTextureArray, i, 0);
        }
    }

    private void InitializeGlyphCoveragesBuffer()
    {
        int glyphCount = _font._glyphs.Length;
        float[] glyphCoverages = new float[glyphCount];

        for (int i = 0; i < glyphCount; i++)
        {
            glyphCoverages[i] = _font._glyphs[i].coverage;
        }

        _glyphCoveragesBuffer = new ComputeBuffer(glyphCoverages.Length, sizeof(float));
        _glyphCoveragesBuffer.SetData(glyphCoverages);

        _ASCIIShader.SetBuffer(0, "glyphCoverages", _glyphCoveragesBuffer);
    }

    private void EnsureOutputTexture(RenderTexture source)
    {
        if (_outputTexture != null && _outputTexture.width == source.width && _outputTexture.height == source.height)
            return;

        if (_outputTexture != null)
            _outputTexture.Release();

        _outputTexture = new RenderTexture(source.width, source.height, 0, RenderTextureFormat.ARGB32);
        _outputTexture.enableRandomWrite = true;
        _outputTexture.Create();

        _resolution = new Vector2Int(source.width, source.height);
        _ASCIIShader.SetInt("textureWidth", _resolution.x);
        _ASCIIShader.SetInt("textureHeight", _resolution.y);
    }



    private void OnDestroy()
    {
        if (_glyphTextureArray != null)
            Destroy(_glyphTextureArray);
        if (_glyphCoveragesBuffer != null)
            _glyphCoveragesBuffer.Release();
        if (_outputTexture != null)
            _outputTexture.Release();
    }
}
