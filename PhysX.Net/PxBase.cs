namespace ChickenWithLips.PhysX.Net;

using System;

public abstract class PxBase<T> : IDisposable
    where T : class
{
    #region Members

    public IntPtr NativePtr { get; }

    private static PxInstanceCache<T> _cache = new();

    #endregion

    #region Methods

    protected PxBase(IntPtr ptr, bool automaticallyRegisterInCache = false)
    {
        NativePtr = ptr;

        if (automaticallyRegisterInCache) {
            ManuallyRegisterCache(ptr, this as T);
        }
    }

    ~PxBase()
    {
        Dispose(false);
    }

    protected static T GetOrCreateCache(IntPtr ptr, PxInstanceCache<T>.CreateInstance createInstance)
    {
        return _cache.GetOrCreate(ptr, createInstance);
    }

    protected static void ManuallyRegisterCache(IntPtr ptr, T instance)
    {
        _cache.ManuallyRegisterCache(ptr, instance);
    }

    #endregion

    #region IDisposable

    #region Properties

    public bool IsDisposed { get; private set; }

    #endregion

    public virtual void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        throw new Exception("NOT IMPL");
        if (IsDisposed) {
            return;
        }

        _cache.Remove(NativePtr);

        IsDisposed = true;
    }

    protected void ThrowExceptionIfDisposed()
    {
        if (IsDisposed) {
            throw new ObjectDisposedException(typeof(T).FullName);
        }
    }

    #endregion
}
