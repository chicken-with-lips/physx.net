﻿using System.Runtime.InteropServices;

namespace ChickenWithLips.PhysX.Native;

internal static class PxFoundation
{
    [DllImport("PhysX.Native", EntryPoint = "physx_PxFoundation_New")]
    public static extern IntPtr Create(uint version);

    [DllImport("PhysX.Native", EntryPoint = "physx_PxFoundation_CreatePhysics")]
    public static extern IntPtr CreatePhysics(IntPtr foundation, uint version, ref PxTolerancesScale scale, bool trackOutstandingAllocations, IntPtr pvd);
}
