﻿using System.Runtime.InteropServices;

namespace ChickenWithLips.PhysX.Native;

internal static class PxDefaultCpuDispatcher
{
    [DllImport("PhysX.Native", EntryPoint="physx_PxDefaultCpuDispatcher_New")]
    public static extern IntPtr Create(uint numThreads);
}
