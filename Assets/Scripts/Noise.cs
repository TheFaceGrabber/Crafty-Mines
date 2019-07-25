using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Noise
{
    public static float Perlin(float x, float y, int seed, float scale, int octaves, float persistance, float lacunarity)
    {
        System.Random prng = new System.Random(seed);
        if (scale <= 0)
        {
            scale = 0.0001f;
        }

        x += (float)prng.NextDouble();
        y += (float)prng.NextDouble();

        float maxNoiseHeight = float.MinValue;
        float minNoiseHeight = float.MaxValue;

        float halfWidth = Chunk.ChunkSize / 2f;
        float halfHeight = Chunk.ChunkSize / 2f;

        float amplitude = 1;
        float frequency = 1;
        float noiseHeight = 0;

        for (int i = 0; i < octaves; i++)
        {
            float sampleX = (x - halfWidth) / scale * frequency;
            float sampleY = (y - halfHeight) / scale * frequency;

            float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
            noiseHeight += perlinValue * amplitude;

            amplitude *= persistance;
            frequency *= lacunarity;
        }

        /*if (noiseHeight > maxNoiseHeight)
        {
            maxNoiseHeight = noiseHeight;
        }
        else if (noiseHeight < minNoiseHeight)
        {
            minNoiseHeight = noiseHeight;
        }*/

        //noiseHeight = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseHeight);

        return noiseHeight;
    }
}
