using Renault.Models;
using Renault.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Renault.Pages
{
    public partial class DealerPage : ContentPage
    {
        public Card Card { get; set; }

        public DealerPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            Card = ServiceManager.Manager.getCard();
            if (Card != null)
            {
              
                dealer.Text = Card.dealerName;
                tot.Text = (Card.endMileage != null && Card.endMileage.Trim() != "") ? Card.expireDate  + " of " + Card.endMileage + " km": Card.expireDate;
                address.Text = Card.address;
                postCode.Text = Card.postCode + " " + Card.placeName;
                phone.Text = Card.phone;
            }
        }

        private void ButtonClicked(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("tel:" + Card.phone));
        }
    }
}
