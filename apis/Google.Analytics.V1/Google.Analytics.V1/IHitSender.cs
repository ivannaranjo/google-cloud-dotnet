using System.Collections.Generic;

namespace Google.Analytics.V1
{
    public interface IHitSender
    {
        void SendHitData(Dictionary<string, string> hitData);
    }
}
