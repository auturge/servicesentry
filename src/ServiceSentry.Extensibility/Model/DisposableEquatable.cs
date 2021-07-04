using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace ServiceSentry.Extensibility
{
    /// <summary cref="object">
    ///     A simple base class that provides an implementation of the
    ///     <see cref="System.IEquatable&lt;Object&gt;" />, 
    ///     <see cref="T:System.IDisposable" /> and
    ///     <see cref="T:System.ComponentModel.INotifyPropertyChanged" /> patterns.
    /// </summary>
    [DataContract]
    public abstract class DisposableEquatable : Equatable, IDisposable
    {
        /// <summary>
        ///     Tracks whether <see cref="M:ServiceSentry.Extensibility.DisposableEquatable.Dispose" /> has been called or not.
        /// </summary>
        private bool _isDisposed;

        /// <summary>
        ///     Tracks whether <see cref="M:ServiceSentry.Extensibility.DisposableEquatable.Dispose" /> has been called or not.
        /// </summary>
        public virtual bool IsDisposed
        {
            get => _isDisposed;
            protected set
            {
                _isDisposed = value;
                OnPropertyChanged(nameof(IsDisposed));
            }
        }

        /// <summary>
        ///     Disposes the object.
        /// </summary>
        /// <remarks>
        ///     This method is not virtual by design. Derived classes
        ///     should override
        ///     <see
        ///         cref="M:ServiceSentry.Extensibility.DisposableEquatable.Dispose(System.Boolean)" />
        ///     .
        /// </remarks>
        [DebuggerStepThrough]
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     This destructor will run only if the
        ///     <see
        ///         cref="M:ServiceSentry.Extensibility.DisposableEquatable.Dispose" />
        ///     method does not get called. This gives this base class the
        ///     opportunity to finalize.
        ///     <para>
        ///         Important: Do not provide destructors in types derived from
        ///         this class.
        ///     </para>
        /// </summary>
        ~DisposableEquatable()
        {
            Dispose(false);
        }

        /// <summary>
        ///     <c>Dispose(bool disposing)</c> executes in two distinct scenarios.
        ///     If disposing equals <c>true</c>, the method has been called directly
        ///     or indirectly by a user's code. Managed and unmanaged resources
        ///     can be disposed.
        /// </summary>
        /// <param name="disposing">
        ///     If disposing equals <c>false</c>, the method
        ///     has been called by the runtime from inside the finalizer and you
        ///     should not reference other objects. Only unmanaged resources can
        ///     be disposed.
        /// </param>
        /// <remarks>
        ///     Check the <see cref="P:ServiceSentry.Extensibility.DisposableEquatable.IsDisposed" /> property to determine whether
        ///     the method has already been called.
        /// </remarks>
        protected virtual void Dispose(bool disposing)
        {
            if (_isDisposed) return;
            if (disposing)
                IsDisposed = true;
            else
                _isDisposed = true;
        }
    }
}