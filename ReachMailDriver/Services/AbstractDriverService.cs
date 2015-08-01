using ReachMailDriver.Services.Gateway;
using ReachMailDriver.Util;

namespace ReachMailDriver.Services
{
    public abstract class AbstractDriverService
    {
        protected IReachMailApiGateway apiGateway;

        public AbstractDriverService(IReachMailApiGateway apiGateway)
        {
            Validate.Begin().IsNotNull(apiGateway, "apiGateway");
            this.apiGateway = apiGateway;
        }
    }
}
