using BigDSignClientDesktop.Controller;
using BigDSignClientDesktop.Model;
using BigDSignClientDesktop.Service;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using AsyncAwaitBestPractices;
using System.Formats.Asn1;

namespace BigDSignClientDesktop.GUI
{

	/// <summary>
	/// Represents the graphical user interface for managing events related to a sign id.
	/// </summary>
	public partial class EventGUI : Form
	{
		private readonly EventControl _eventControl;
		private readonly int _signId;
		private readonly SignGUI _priorWindow;


		/// <summary>
		/// Initializes a new instance of the EventGUI class with a sign id.
		/// </summary>
		public EventGUI(int signId, SignGUI priorWindow)
		{
			InitializeComponent();
			_eventControl = new EventControl();
			_signId = signId;
			_priorWindow = priorWindow;
			RefreshEventList().SafeFireAndForget();
		}

		/// <summary>
		/// Creates a new event using information from textboxes and saves it through the EventControl.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private async void BtnCreateEvent_Click(object sender, EventArgs e)
		{
			int insertedId;
			string messageText;
			// Values from testboxes must be fetched
			string inName = textNameEvent.Text;
			string inDescription = textDescriptionEvent.Text;
			string inAvailabilityStatus = textStatusEvent.Text;
			int inSignId = _signId;

			// Evaluate and act accordingly
			if (InputIsOk(inName, inDescription, inAvailabilityStatus, inSignId)
				&& DateTime.TryParse(textDateEvent.Text, out var resultDate)
				&& decimal.TryParse(textPriceEvent.Text, out var resultPrice))
			{
				try
				{
					// Call the ControlLayer to get the data saved
					insertedId = await _eventControl.SaveEvent(inName, resultDate, inDescription, resultPrice, inAvailabilityStatus, inSignId);
					messageText = $"Event saved with ID: {insertedId}";
				}
				// Catch exceptions returned from the API service.
				catch (ServiceException exception)
				{
					messageText = $"{(int)exception.StatusCode} {exception.Message}";
				}
				// Catch other exceptions.
				catch (Exception exception)
				{
					Console.Error.Write(exception);
					messageText = $"500 Internal server error.";
				}
			}
			else
			{
				messageText = "Please input valid informations";
			}
			// Finally put out a message saying if the saving went well
			labelProcessTextEventStatus.Text = messageText;

			// refresh the list to reflect the updated event
			await RefreshEventList();
		}

		/// <summary>
		/// Fetches and refreshes the event list related to the selected event.
		/// </summary>
		private async Task RefreshEventList()
		{
			List<Event>? fetchedEvents;
			int signId = _signId;
			try
			{
				fetchedEvents = await _eventControl.GetEventsBySignId(signId);
				// Check if fetchedEvents is not null before binding to listBoxEvents
				if (fetchedEvents.Count > 0)
				{
					listBoxEvent.DataSource = fetchedEvents;
					labelProcessTextEvent.Text = "Ok";
				}
				else
				{
					labelProcessTextEvent.Text = "No events related to selected sign";
				}
			}
			// Catch exceptions returned from the API service.
			catch (ServiceException exception)
			{
				labelProcessTextEvent.Text = $"{(int)exception.StatusCode} {exception.Message}";
			}
			// Catch other exceptions.
			catch (Exception exception)
			{
				Console.Error.Write(exception);
				labelProcessTextEvent.Text = $"500 Internal server error.";
			}
			listBoxEvent.Refresh();
		}

		/// <summary>
		/// Updates the selected event's information based on textbox inputs and saves changes through EventControl.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private async void BtnUpdateEvent_Click(object sender, EventArgs e)
		{
			string messageText;
			// Check if a event is selected
			if (listBoxEvent.SelectedIndex == -1)
			{
				labelProcessTextEventStatus.Text = "No event selected";
				return;
			}

			// Retrieve the selected event object
			Event selectedEvent = (Event)listBoxEvent.SelectedItem;

			// Update the properties of the selected event
			string inName = textNameEvent.Text;
			string inDescription = textDescriptionEvent.Text;
			string inAvailabilityStatus = textStatusEvent.Text;
			int inSignId = _signId;

			// Evaluate and act accordingly
			if (InputIsOk(inName, inDescription, inAvailabilityStatus, inSignId)
				&& DateTime.TryParse(textDateEvent.Text, out var resultDate)
				&& decimal.TryParse(textPriceEvent.Text, out var resultPrice))
			{
				// Call the ControlLayer to get the data saved
				selectedEvent.EventName = inName;
				selectedEvent.EventDescription = inDescription;
				selectedEvent.AvailabilityStatus = inAvailabilityStatus;
				selectedEvent.SignId = inSignId;
				selectedEvent.EventDate = resultDate;
				selectedEvent.Price = resultPrice;

				try
				{
					// Call the EventControl's UpdateEvent method to update the event
					bool updateSuccessful = await _eventControl.UpdateEvent(selectedEvent);
					// Update the label with the result
					messageText = updateSuccessful ? "Event updated successfully." : "Failed to update event.";
				}
				// Catch exceptions returned from the API service.
				catch (ServiceException exception)
				{
					messageText = $"{(int)exception.StatusCode} {exception.Message}";
				}
				// Catch other exceptions.
				catch (Exception exception)
				{
					Console.Error.Write(exception);
					messageText = $"500 Internal server error.";
				}
			}
			else
			{
				messageText = "Please input valid informations";
			}

			labelProcessTextEventStatus.Text = messageText;
			await RefreshEventList();
		}

		/// <summary>
		/// Deletes the selected event based on its ID and refreshes the event list.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private async void BtnDeleteEvent_Click(object sender, EventArgs e)
		{
			bool eventFound;
			string messageText;
			// Check if a event is selected
			if (listBoxEvent.SelectedIndex == -1)
			{
				labelProcessTextEventStatus.Text = "No event selected";
				return;
			}

			// Retrieve the selected Event object
			Event selectedEvent = (Event)listBoxEvent.SelectedItem;
			try
			{
				eventFound = await _eventControl.DeleteEvent(selectedEvent.Id);
				if (eventFound)
				{
					messageText = $"Event {selectedEvent.Id} was succesfully deleted";
				}
				else
				{
					messageText = $"ERROR: Event {selectedEvent.Id} was not deleted";
				}
			}
			// Catch exceptions returned from the API service.
			catch (ServiceException exception)
			{
				messageText = $"{(int)exception.StatusCode} {exception.Message}";
			}
			// Catch other exceptions.
			catch (Exception exception)
			{
				Console.Error.Write(exception);
				messageText = $"500 Internal server error.";
			}
			labelProcessTextEventStatus.Text = messageText;
			await RefreshEventList();
		}

		/// <summary>
		/// Populates textboxes with information from the selected event in the event list.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ListBoxEvent_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (listBoxEvent.SelectedIndex != -1)
			{
				Event selectedEvent = (Event)listBoxEvent.SelectedItem;

				if (selectedEvent.AvailabilityStatus.Equals("Sold out"))
				{
					textIdEvent.Text = selectedEvent.Id.ToString();
					textNameEvent.Text = selectedEvent.EventName.ToString();
					textDateEvent.Text = selectedEvent.EventDate.ToString();// formatting date to MM/dd
					textStatusEvent.Text = selectedEvent.AvailabilityStatus.ToString();
					textPriceEvent.Text = selectedEvent.Price.ToString();
					textDescriptionEvent.Text = selectedEvent.EventDescription.ToString();
					textNameEvent.Enabled = false;
					textDateEvent.Enabled = false;
					textStatusEvent.Enabled = false;
					textStatusEvent.Enabled = false;
					textPriceEvent.Enabled = false;
					textDescriptionEvent.Enabled = false;
				}
				else
				{
					textIdEvent.Text = selectedEvent.Id.ToString();
					textNameEvent.Text = selectedEvent.EventName.ToString();
					textDateEvent.Text = selectedEvent.EventDate.ToString();// formatting date to MM/dd
					textStatusEvent.Text = selectedEvent.AvailabilityStatus.ToString();
					textPriceEvent.Text = selectedEvent.Price.ToString();
					textDescriptionEvent.Text = selectedEvent.EventDescription.ToString();
					textNameEvent.Enabled = true;
					textDateEvent.Enabled = true;
					textStatusEvent.Enabled = true;
					textPriceEvent.Enabled = true;
					textDescriptionEvent.Enabled = true;
				}
			}
			else
			{
				labelProcessTextEvent.Text = "Event not found";
			}

		}

		/// <summary>
		/// Clears all textboxes and selections in the event GUI.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnClearEvent_Click(object sender, EventArgs e)
		{
			//Clears item selection in groupbox
			listBoxEvent.ClearSelected();

			//Clears text boxes
			textIdEvent.Text = "";
			textNameEvent.Text = "";
			textDateEvent.Text = "";
			textStatusEvent.Text = "";
			textPriceEvent.Text = "";
			textDescriptionEvent.Text = "";
			textNameEvent.Enabled = true;
			textDateEvent.Enabled = true;
			textStatusEvent.Enabled = true;
			textPriceEvent.Enabled = true;
			textDescriptionEvent.Enabled = true;

			// Clearing process text
			labelProcessTextEvent.Text = "...";
			labelProcessTextEventStatus.Text = "...";
		}

		/// <summary>
		/// Validates input parameters for creating/updating an event.
		/// </summary>
		/// <param name="name"></param>
		/// <param name="description"></param>
		/// <param name="availabilityStatus"></param>
		/// <param name="signId"></param>
		/// <returns> Boolean indicating if the input is valid.</returns>
		private static bool InputIsOk(string name, string description, string availabilityStatus, int signId)
		{
			bool isValidInput = false;
			if (!String.IsNullOrWhiteSpace(name) && !String.IsNullOrWhiteSpace(description) && !String.IsNullOrWhiteSpace(availabilityStatus))
			{
				if (name.Length > 0 && description.Length > 0 && availabilityStatus.Length > 0 && signId > 0)
				{
					isValidInput = true;
				}
			}
			return isValidInput;
		}

		/// <summary>
		/// Event handler triggered when the form is closing.
		/// Handles the closing event of WindowB, preventing the default closing behavior
		/// and shows the prior window (WindowA) instead.
		/// </summary>
		private void EventGUI_FormClosing(object sender, FormClosingEventArgs e)
		{
			e.Cancel = true; // Cancel the form closing event to allow for manual handling
			Hide(); // Hide WindowB
			_priorWindow.Show(); // Show the prior window, WindowA in this case
		}


		/// <summary>
		/// Event handler triggered when date is changed in eventCalendar
		/// Shows a list of events based on the new date
		/// </summary>
		private async void EventCalendar_DateSelected(object sender, DateRangeEventArgs e)
		{
			DateTime selectedDate = eventCalendar.SelectionStart;
			string processText;
			List<Event>? fetchedEvents;

			try
			{
				// Fetch events for the selected date
				fetchedEvents = await _eventControl.GetAllEventsWithDate(selectedDate, _signId);
				listBoxEvent.DataSource = fetchedEvents;
				listBoxEvent.DisplayMember = "EventName";
				// Check if fetchedEvents has any events
				if (fetchedEvents.Any())
				{
					processText = "Events for selected date";
				}
				else
				{
					processText = "No events related to selected sign or date";
				}
			}
			// Catch exceptions returned from the API service.
			catch (ServiceException exception)
			{
				processText = $"{(int)exception.StatusCode} {exception.Message}";
			}
			// Catch other exceptions.
			catch (Exception exception)
			{
				Console.Error.Write(exception);
				processText = $"500 Internal server error.";
			}
			listBoxEvent.Refresh();
			labelProcessTextEvent.Text = processText;
		}

		private void BtnRefresh_Click(object sender, EventArgs e)
		{
			//Refreshes event list
			 RefreshEventList().SafeFireAndForget();
		}
	}
}
