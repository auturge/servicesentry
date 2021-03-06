using System.Reflection;
using System.Runtime.Serialization;

namespace ServiceSentry.Testing
{
    // The assembly class doesn't define a deserializer, so we need to implement one
    // in order to mock an assembly.
    public abstract class TestAssembly : Assembly
    {
        public new abstract void GetObjectData(SerializationInfo info, StreamingContext context);
    }
}