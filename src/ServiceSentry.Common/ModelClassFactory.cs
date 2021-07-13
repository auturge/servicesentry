using System;
using ServiceSentry.Extensibility.Logging;
using ServiceSentry.Common.Client;
using ServiceSentry.Common.Communication;
using ServiceSentry.Common.Email;
using ServiceSentry.Common.Server;
using ServiceSentry.Common.Services;

namespace ServiceSentry.Common
{
    public abstract class ModelClassFactory
    {
        public static ModelClassFactory GetInstance(Logger logger)
        {
            return new ModelClassFactoryImplementation(logger);
        }

        public abstract ServiceWrapper GetLocalServiceController(string serviceName);
        public abstract LocalServiceFinder GetLocalServiceFinder();
        public abstract ClientMediator GetClientMediator(ClientList clients, SubscriptionPacket packet);
        public abstract Responder GetResponder();
        public abstract Emailer GetEmailer(SMTPInfo smtpInfo);

        private sealed class ModelClassFactoryImplementation : ModelClassFactory
        {
            private readonly Logger _logger;

            internal ModelClassFactoryImplementation(Logger logger)
            {
                _logger = logger;
            }

            public override ServiceWrapper GetLocalServiceController(string serviceName)
            {
                return ServiceWrapper.GetInstance(serviceName);
            }

            public override LocalServiceFinder GetLocalServiceFinder()
            {
                return LocalServiceFinder.Default;
            }

            public override ClientMediator GetClientMediator(ClientList clients, SubscriptionPacket packet)
            {
                var output = ClientMediator.GetInstance(_logger, clients, packet, this);
                if (output == null)
                {
                    _logger.Error(Strings.Error_CouldNotGenerateClientMediator, packet.MachineName, packet.ServiceName);
                    throw new Exception(String.Format(Strings.Error_CouldNotGenerateClientMediator, packet.MachineName,
                                                      packet.ServiceName));
                }
                return output;
            }

            public override Responder GetResponder()
            {
                return Responder.GetInstance(_logger);
            }

            public override Emailer GetEmailer(SMTPInfo info)
            {
                return Emailer.GetInstance(info);
            }

        }
    }
}
