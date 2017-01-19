using Renault.Helpers;
using Renault.Interfaces;
using Renault.Models;
using Renault.Services.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Renault.Services
{
    public sealed class ServiceManager
    {
        private static ServiceManager _serviceManager;
        private ICardManager cardManager;
        private Card card;

        private ServiceManager()
        {
            cardManager = new CardManager();
        }
        
        public async Task<Card> get(string license)
        {
            string url = UrlHelper.generateCommand(Constants.CARD_INFO_URL, license);
            card = await cardManager.get(url);
            return card; 
        }
        public Card getCard()
        {
            return card;
        }
        public static ServiceManager Manager
        {
            get
            {
                return _serviceManager ?? (_serviceManager = new ServiceManager());
            }
            private set { }
        }


    }
}
