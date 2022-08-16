namespace ChickenWithLips.PhysX;

using System;

public interface PxBase
{
}

public abstract class PxBase<T> : PxBase, IDisposable
    where T : class
{
    #region Members

    public IntPtr NativePtr { get; protected set; }

    #endregion

    #region Methods

    protected PxBase(IntPtr ptr, bool automaticallyRegisterInCache = false)
    {
        NativePtr = ptr;

        if (automaticallyRegisterInCache) {
            ManuallyRegisterCache(ptr, this);
        }
    }

    ~PxBase()
    {
        Dispose(false);
    }

    protected static T GetOrCreateCache(IntPtr ptr, PxInstanceCache.CreateInstance createInstance)
    {
        return PxInstanceCache.Instance.GetOrCreate<T>(ptr, createInstance);
    }

    protected static void ManuallyRegisterCache(IntPtr ptr, PxBase instance)
    {
        PxInstanceCache.Instance.ManuallyRegisterCache(ptr, instance);
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

        PxInstanceCache.Instance.Remove(NativePtr);

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
