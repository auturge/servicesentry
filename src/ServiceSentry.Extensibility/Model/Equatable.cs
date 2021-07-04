using System;
using System.Runtime.Serialization;

namespace ServiceSentry.Extensibility
{
    /// <summary>
    ///     A simple base class that provides an implementation of the
    ///     <see cref="System.IEquatable&lt;Object&gt;" /> and 
    ///     <see cref="T:System.ComponentModel.INotifyPropertyChanged" /> patterns.
    /// </summary>
    [DataContract]
    public abstract class Equatable : NotifyPropertyChanged, IEquatable<object>
    {
        public abstract override bool Equals(object other);

        public abstract override int GetHashCode();
        
        public static bool operator ==(Equatable left, Equatable right)
        {
            if (ReferenceEquals(left, right)) return true;
            if (((object)left) == null || ((object)right) == null) return false;
            return left.Equals(right);
        }

        public static bool operator !=(Equatable left, Equatable right)
        {
            return !(left == right);
        }
    }
}
