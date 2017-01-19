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
    public partial class PasPage : ContentPage
    {
        public Card Card { get; set; }
        public PasPage()
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
                tot.Text = (Card.endMileage != null && Card.endMileage.Trim() != "") ? Card.expireDate + " of " + Card.endMileage + " km" : Card.expireDate;
                phone.Text = Card.phone;
                name.Text = Card.fullName;
                license.Text = Card.license;
                number.Text = Card.cardNumber;
            }
        }
    }
}
