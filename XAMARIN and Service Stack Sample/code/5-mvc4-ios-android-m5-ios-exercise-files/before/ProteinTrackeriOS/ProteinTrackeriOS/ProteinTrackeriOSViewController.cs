using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using ProteinTrackerTest;
using ProteinTrackerMVC.Api;
using System.Collections.Generic;
using ServiceStack.ServiceClient.Web;
using System.Linq;
using RestSharp;

namespace ProteinTrackeriOS
{
	public partial class ProteinTrackeriOSViewController : UIViewController
	{
		private JsonServiceClient client;
		private RestClient altClient;
		private IList<User> users;

		public ProteinTrackeriOSViewController () : base ("ProteinTrackeriOSViewController", null)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			var tap = new UITapGestureRecognizer ();
			tap.AddTarget (() => {
				View.EndEditing (true);
			});
			View.AddGestureRecognizer (tap);

			client = new JsonServiceClient ("http://mario:56381/api");

			PopulateUsers ();

			addUserButton.TouchUpInside += (object sender, EventArgs e) => {
				client.Send (new AddUser { Name = nameText.Text, Goal = int.Parse (goalText.Text)});
				nameText.Text = string.Empty;
				goalText.Text = string.Empty;
				PopulateUsers();
			};

			addAmountButton.TouchUpInside += (object sender, EventArgs e) => {
				var userPickerModel = selectUserPicker.Model as UserPickerModel;
				var response = client.Send (new AddProtein {Amount = int.Parse(amountText.Text), UserId = userPickerModel.SelectedItem.Id});
				userPickerModel.SelectedItem.Total = response.NewTotal;
				totalLabel.Text = response.NewTotal.ToString();
			};
		}

		void PopulateUsers ()
		{
			altClient = new RestClient("http://mario:56381/api");
			var request = new RestRequest ("users/", Method.GET);
			var response = altClient.Execute<UsersResponse> (request);
			users = response.Data.Users;

			//users = client.Get (new Users ()).Users.ToList ();

			var model = new UserPickerModel (users);
			model.ItemSelected += (object sender, EventArgs e) => {
				goalLabel.Text = model.SelectedItem.Goal.ToString();
				totalLabel.Text = model.SelectedItem.Total.ToString();
			};

			selectUserPicker.Model = model;
		}

		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			// Return true for supported orientations
			return (toInterfaceOrientation != UIInterfaceOrientation.PortraitUpsideDown);
		}
	}

	public class UserPickerModel : GenericPickerModel<User> 
	{
		public UserPickerModel(IList<User> users) : base(users) {
		}
	}
}

