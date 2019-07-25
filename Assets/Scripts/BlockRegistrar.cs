using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;
using System.Linq;

public static class Blocks
{
    public const int Air = 0,
        Dirt = 1,
        Grass = 2,
        Stone = 3,
        Sand = 4,
        Log = 5,
        Leaves = 6,
        Water = 7,
        DryGrass = 8;
}

public static class BlockRegistrar
{
    public static Dictionary<int, Block> Blocks = new Dictionary<int, Block>();

    static bool isInited;

    public static int MaxSubmeshes { get; set; }

    public static void Init()
    {
        if (isInited) return;

        foreach (Type t in Assembly.GetAssembly(typeof(Block)).GetTypes().Where(x => x.IsClass && x.IsSubclassOf(typeof(Block))))
        {
            var block = (Block)Activator.CreateInstance(t);
            if(block.Submesh > MaxSubmeshes)
            {
                MaxSubmeshes = block.Submesh;
            }
            Blocks.Add(block.BlockID, block);
        }

        Blocks = Blocks.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);

        isInited = true;
    }

    public static Block GetBlock(int i)
    {

        if(Blocks.ContainsKey(i))
        {
            return Blocks[i];
        }

        return null;
    }
}
