using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;
using System.Linq;

public enum Blocks
{
    Air = 0,
    Dirt = 1,
    Grass = 2,
    Stone = 3,
    Sand = 4
}

public static class BlockRegistrar
{
    static Dictionary<int, Block> Blocks = new Dictionary<int, Block>();

    static bool isInited;

    public static void Init()
    {
        if (isInited) return;

        foreach (Type t in Assembly.GetAssembly(typeof(Block)).GetTypes().Where(x => x.IsClass && x.IsSubclassOf(typeof(Block))))
        {
            var block = (Block)Activator.CreateInstance(t);
            Blocks.Add(block.BlockID, block);
        }

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
