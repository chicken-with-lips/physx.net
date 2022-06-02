namespace ChickenWithLips.PhysX.Net;

using System;
using System.Collections.Generic;

public class PxInstanceCache
{
    #region Members

    private readonly Dictionary<IntPtr, PxBase> _cache = new();

    #endregion

    #region Methods

    public T Get<T>(IntPtr ptr)
        where T : class 
    {
        if (ptr == IntPtr.Zero || !_cache.ContainsKey(ptr)) {
            throw new Exception("Pointer not found in cache");
        }

        return _cache[ptr] as T;
    }

    public T GetOrCreate<T>(IntPtr ptr, CreateInstance createInstance)
        where T : class 
    {
        if (ptr == IntPtr.Zero) {
            return default;
        }

        if (!_cache.ContainsKey(ptr)) {
            _cache.Add(ptr, createInstance(ptr));
        }

        return _cache[ptr] as T;
    }

    public void ManuallyRegisterCache(IntPtr ptr, PxBase instance)
    {
        if (_cache.ContainsKey(ptr)) {
            throw new InvalidOperationException("Instance already registered");
        }

        _cache.Add(ptr, instance);
    }

    public void Remove(IntPtr cameraPtr)
    {
        if (_cache.ContainsKey(cameraPtr)) {
            _cache.Remove(cameraPtr);
        }
    }

    #endregion

    public delegate PxBase CreateInstance(IntPtr ptr);

    #region Singleton

    public static PxInstanceCache Instance { get; }

    static PxInstanceCache()
    {
        Instance = new PxInstanceCache();
    }

    #endregion
}
