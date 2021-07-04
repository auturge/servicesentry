using System.Reflection;

namespace ServiceSentry.Extensibility
{
    public abstract class AssemblyInspector
    {
        public static AssemblyInspector Default => new AwHelperImplementation();

        public abstract bool HasEntryAssembly { get; }

        private sealed class AwHelperImplementation : AssemblyInspector
        {
            /// <summary>
            ///     Determines whether the EntryAssembly is available.
            /// </summary>
            public override bool HasEntryAssembly
            {
                get
                {
                    try
                    {
                        var assembly = Assembly.GetEntryAssembly();
                        return (assembly != null);
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
        }
    }
}