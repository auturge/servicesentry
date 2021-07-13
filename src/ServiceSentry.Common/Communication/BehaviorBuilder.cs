using System;
using System.ServiceModel.Description;
using ServiceSentry.Common.Enumerations;

namespace ServiceSentry.Common.Communication
{
    internal abstract class BehaviorBuilder
    {
        /// <summary>
        ///     Gets a new instance of the <see cref="BehaviorBuilder" /> class.
        /// </summary>
        internal static BehaviorBuilder GetInstance(ServiceHostType serviceHostType)
        {
            if (serviceHostType == ServiceHostType.NetTcp)
                return new NetTcpBehaviorImplementation();

            if (serviceHostType == ServiceHostType.Http)
                return new WebHttpBehaviorImplementation();

            throw new InvalidOperationException("ServiceHostType must be either net.tcp or http.");
        }

        /// <summary>
        ///     Gets a new <see cref="IEndpointBehavior" />.
        /// </summary>
        internal abstract IEndpointBehavior GetBehavior();

        private sealed class NetTcpBehaviorImplementation : BehaviorBuilder
        {
            internal override IEndpointBehavior GetBehavior()
            {
                return null;
            }
        }

        private sealed class WebHttpBehaviorImplementation : BehaviorBuilder
        {
            internal override IEndpointBehavior GetBehavior()
            {
                return new WebHttpBehavior();
            }
        }
    }
}