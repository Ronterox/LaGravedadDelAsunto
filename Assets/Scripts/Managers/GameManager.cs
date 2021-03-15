using System.Collections.Generic;
using Plugins.Tools;
using Questing_System;

namespace Managers
{
    public class GameManager : PersistentSingleton<GameManager>
    {
        public List<Campaign> campaigns;

        public void UpdateCampaigns() => campaigns.ForEach(x => x.UpdateCampaign());
    }
}
