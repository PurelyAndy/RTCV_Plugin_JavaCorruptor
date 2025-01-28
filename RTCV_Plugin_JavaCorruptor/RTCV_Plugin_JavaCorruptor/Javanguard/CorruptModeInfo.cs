using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Java_Corruptor.Javanguard;

public static class CorruptModeInfo
{
    public static bool Live;
    public static Dictionary<string, byte[]> ClassData = new();
}