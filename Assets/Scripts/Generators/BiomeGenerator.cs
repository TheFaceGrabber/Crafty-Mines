using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(menuName = "Generator/Biome", fileName = "Biome Generator")]
public class BiomeGenerator : Generator
{
    [Header("Temperature")]
    public float TemperatureNoiseScale = 10;
    public int TemperatureOctaves = 5;
    public float TemperaturePersistance = 2;
    public float TemperatureLacunarity = 0.5f;
    [Header("Rainfall")]
    public float RainfallNoiseScale = 10;
    public int RainfallOctaves = 5;
    public float RainfallPersistance = 2;
    public float RainfallLacunarity = 0.5f;
    [Header("Terrain")]
    public float NoiseScale = 10;
    public int Octaves = 5;
    public float Persistance = 2;
    public float Lacunarity = 0.5f;
    public float HeightScale = 2;
    public float ChanceOfTree = 20;
    public int TreeCount = 20;
    public int MinimumHeight = 12;
    public Structure Tree;

    float heightMul = 1;

    public override void OnGenerate(Chunk chunk)
    {
        float rainfall = 0; //Noise.Perlin(chunk.NoisePos.x, chunk.NoisePos.y, Seed, RainfallNoiseScale,
            //RainfallOctaves, RainfallPersistance, RainfallLacunarity) + 1;
       // rainfall /= 2;
        float temperature = 0; //Noise.Perlin(chunk.NoisePos.x, chunk.NoisePos.y, Seed, TemperatureNoiseScale,
            //TemperatureOctaves, TemperaturePersistance, TemperatureLacunarity) + 1;
       // temperature /= 2;
        for (int x = 0; x < Chunk.ChunkSize; x++)
        {
            for (int z = 0; z < Chunk.ChunkSize; z++)
            {
                var pos = chunk.NoisePos;
                pos += new Vector2(x, z);
                float blockRainfall = Noise.Perlin(pos.x, pos.y, Seed, RainfallNoiseScale,
                RainfallOctaves, RainfallPersistance, RainfallLacunarity) + 1;
                blockRainfall /= 2;
                float blockTemp = Noise.Perlin(pos.x, pos.y, Seed, TemperatureNoiseScale,
                TemperatureOctaves, TemperaturePersistance, TemperatureLacunarity) + 1;
                blockTemp /= 2;
                if (x == 0 && z == 0)
                {
                    rainfall = blockRainfall;
                    temperature = blockTemp;
                }
                heightMul = Mathf.Pow(Mathf.Abs(1 - blockRainfall), 2);//2 + .5f;
                heightMul = Mathf.Abs(1 - heightMul);
                //Debug.Log(heightMul);
                int height = (int)GetNoise(pos);
                chunk.Heightmap[x, z] = height;
                for (int y = 0; y < height; y++)
                {
                    if (y > height)
                    {
                        chunk.ApplyBlock(x, y, z, Blocks.Air);
                    }
                    else if (y == height - 1)
                    {
                        if (height - 1 > Chunk.SeaLevel && blockRainfall > 0.25)
                        {
                            if(blockRainfall > .35)
                                chunk.ApplyBlock(x, y, z, Blocks.Grass);
                            else
                                chunk.ApplyBlock(x, y, z, Blocks.DryGrass);
                        }
                        else if (!chunk.HasBlockAt(x, y + 1, z))
                            chunk.ApplyBlock(x, y, z, Blocks.Sand);
                    }
                    else if (y <= height - 5)
                    {
                        chunk.ApplyBlock(x, y, z, Blocks.Stone);
                    }
                    else
                    {
                        if (y > Chunk.SeaLevel && blockRainfall > 0.25)
                            chunk.ApplyBlock(x, y, z, Blocks.Dirt);
                        else
                            chunk.ApplyBlock(x, y, z, Blocks.Sand);
                    }
                }
            }
        }

        float treeCount = TreeCount * rainfall;
        if (rainfall <= 0.35)
            treeCount = 0;
        List<Vector2> treePoses = new List<Vector2>();
        int treeSize = 1;

        for (int i = 0; i < treeCount; i++)
        {
            int xPos = Random.Range(2, Chunk.ChunkSize - 2);
            int yPos = Random.Range(2, Chunk.ChunkSize - 2);

            if (treePoses.Any(v => Mathf.Abs(v.x - xPos) <= treeSize && Mathf.Abs(v.y - yPos) <= treeSize))
                continue;

            treePoses.Add(new Vector2(xPos, yPos));
            if (CheckChance(ChanceOfTree))
            {
                AddTree(xPos, chunk.Heightmap[xPos, yPos], yPos, chunk);
            }
        }
    }

    void AddTree(int x, int y, int z, Chunk chunk)
    {
        if (y - 1 > Chunk.SeaLevel + 1)
        {
            int height = Random.Range(2, 5);
            CreateColumn(x, y, z, height, chunk);
            TrySpawnStructure(Tree, x, y + height, z, chunk);
        }
    }

    public void CreateColumn(int x, int y, int z, int height, Chunk chunk)
    {
        for (int i = 0; i < height; i++)
        {
            chunk.ApplyBlock(x, y + i, z, (int)Blocks.Log);
        }
    }

    public void TrySpawnStructure(Structure str, int xPos, int yPos, int zPos, Chunk chunk)
    {
        int centerX = Mathf.CeilToInt(str.sizeX / 2);
        int centerY = Mathf.CeilToInt(str.sizeY / 2);
        for (int i = 0; i < str.Layers.Count; i++)
        {
            var layer = str.Layers[i];
            for (int x = 0; x < str.sizeX; x++)
            {
                for (int z = 0; z < str.sizeY; z++)
                {
                    int xActual = (x - centerX) + xPos;
                    int yActual = yPos + i;
                    int zActual = (z - centerY) + zPos;

                    if (layer.Grid[x + z * str.sizeX] == ' ')
                        continue;

                    var block = str.Key.SingleOrDefault(v => v.Key == layer.Grid[x + z * str.sizeX]);
                    float r = Random.value;
                    if (block.ChanceToSpawn <= r || block.ChanceToSpawn == 1)
                    {
                        int blockVal = block.Value + 1;

                        chunk.ApplyBlock(xActual, yActual, zActual, blockVal);
                    }
                }
            }
        }
    }

    public override float GetNoise(Vector2 pos)
    {
        float n = Noise.Perlin(pos.x, pos.y, Seed, NoiseScale, Octaves, Persistance, Lacunarity) * 
            heightMul;
        float r = Mathf.RoundToInt(n * HeightScale) + 18;

        return r;//Mathf.Clamp(r, MinimumHeight, Chunk.ChunkHeight);
    }
}