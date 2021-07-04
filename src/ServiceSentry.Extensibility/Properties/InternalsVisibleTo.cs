#region References

using System.Runtime.CompilerServices;

#endregion

[assembly: InternalsVisibleTo("ServiceSentry.Testing")]
[assembly: InternalsVisibleTo("ServiceSentry.Model.UnitTests")]
[assembly: InternalsVisibleTo("ServiceSentry.Agent.UnitTests")]
[assembly: InternalsVisibleTo("ServiceSentry.Client.UnitTests")]
[assembly: InternalsVisibleTo("ServiceSentry.Common.UnitTests")]
[assembly: InternalsVisibleTo("ServiceSentry.Extensibility.UnitTests")]

//[assembly: InternalsVisibleTo("ServiceSentry.ServiceFramework.UnitTests")]
//[assembly: InternalsVisibleTo("ServiceSentry.AVS.UnitTests")]

// This assembly is the default dynamic assembly generated Castle DynamicProxy, 
// used by Moq. Paste in a single line. 
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]