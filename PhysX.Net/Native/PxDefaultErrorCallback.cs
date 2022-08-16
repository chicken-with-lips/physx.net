﻿using System.Runtime.InteropServices;

namespace ChickenWithLips.PhysX.Native;

internal static class PxDefaultErrorCallback
{
    [DllImport("PhysX.Native", EntryPoint="physx_PxDefaultErrorCallback_New")]
    public static extern IntPtr Create();
}
