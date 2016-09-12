using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Google.Analytics.V1
{
    public interface IHitSender
    {
        void SendHitData(Dictionary<string, string> hitData);
    }
}
