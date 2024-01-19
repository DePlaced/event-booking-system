using BigDSignClientDesktop.Controller;
using BigDSignClientDesktop.GUI;
using BigDSignClientDesktop.Model;
using BigDSignClientDesktop.Service;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using AsyncAwaitBestPractices;

namespace BigDSignClientDesktop
{
    /// <summary>
    /// Represents the graphical user interface for managing signs related to a selected stadium.
    /// </summary>
    public partial class SignGUI : Form
    {
        readonly SignControl _signControl;
        readonly StadiumGUI _priorWindow;
        readonly int _stadiumId;

        /// <summary>
        /// Initializes a new instance of the SignGUI class with a selected stadium.
        /// </summary>
        /// <param name="idProvider">Dependency injection used to aqquire stadiumId</param>
        public SignGUI(int stadiumId, StadiumGUI priorWindow)
        {
            InitializeComponent();
            _signControl = new SignControl();
            _stadiumId = stadiumId;
            _priorWindow = priorWindow;
            RefreshSignList().SafeFireAndForget();
        }

        /// <summary>
        /// Creates a new sign using information from textboxes and saves it through the SignControl.
        /// </summary>
        private async void BtnCreate_Click(object sender, EventArgs e)
        {
            int insertedId = -1;
            string messageText;
            // Values from testboxes must be fetched
            string inLocation = textLocation.Text;
            string inSize = textSize.Text;
            string inResolution = textResolution.Text;
            int inStadiumId = _stadiumId;

            // Evaluate and act accordingly
            if (InputIsOk(inLocation, inSize, inResolution))
            {
                try
                {
                    // Call the ControlLayer to get the data saved
                    insertedId = await _signControl.SaveSign(inSize, inResolution, inLocation, inStadiumId);
                    messageText = $"Sign saved with ID: {insertedId}";
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
            labelProcessText.Text = messageText;

            // refresh the list to reflect the updated sign
            await RefreshSignList();
        }

        /// <summary>
        /// Refreshes the sign list related to the selected stadium.
        /// </summary>
        private async Task RefreshSignList()
        {
            //string searchText = SearchBox.Text;
            string processText;
            try
            {
                List<Sign> fetchedSigns = await _signControl.GetSignsByStadiumId(_stadiumId);
                // Check if the stadium contains any signs
                if (fetchedSigns.Count > 0)
                {
                    listBoxSigns.DataSource = fetchedSigns;
                    // Refresh the ListBox to update UI
                    listBoxSigns.Refresh();
                    processText = "Ok";
                }
                else
                {
                    processText = "No events related to selected sign";
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
            labelProcessTextSign.Text = processText;
        }

        /// <summary>
        /// Uses the RefreshSignList method, to update list of signs
        /// </summary>
        private async void ButtonGetSigns_Click(object sender, EventArgs e)
        {
            await RefreshSignList();
        }

        /// <summary>
        /// Updates the selected sign's information based on textbox inputs and saves changes through SignControl.
        /// </summary>
        private async void BtnUpdate_Click(object sender, EventArgs e)
        {
            // Check if a sign is selected
            if (listBoxSigns.SelectedIndex == -1)
            {
                labelProcessText.Text = "No sign selected";
                return;
            }
            // Retrieve the selected Sign object
            Sign selectedSign = (Sign)listBoxSigns.SelectedItem;

            var tempLocation = textLocation.Text;
            var tempSize = textSize.Text;
            var tempResolution = textResolution.Text;

            if (InputIsOk(tempLocation, tempSize, tempResolution))
            {

                // Update the properties of the selected sign
                selectedSign.Location = tempLocation;
                selectedSign.Size = tempSize;
                selectedSign.Resolution = tempResolution;

                try
                {
                    // Call the SignControl's UpdateSign method to update the sign
                    bool updated = await _signControl.UpdateSign(selectedSign);
                    // Update the label with the result
                    labelProcessText.Text = "Sign updated successfully.";
                }
                // Catch exceptions returned from the API service.
                catch (ServiceException exception)
                {
                    labelProcessText.Text = $"{(int)exception.StatusCode} {exception.Message}";
                }
                // Catch other exceptions.
                catch (Exception exception)
                {
                    Console.Error.Write(exception);
                    labelProcessText.Text = $"500 Internal server error.";
                }

                // refresh the list to reflect the updated sign
                await RefreshSignList();
            }
        }

		/// <summary>
		/// Deletes the selected sign based on its ID and refreshes the sign list.
		/// </summary>
		private async void BtnDelete_ClickAsync(object sender, EventArgs e)
		{
			try
			{
				var signId = int.Parse(textId.Text);
				var isDeleted = await _signControl.DeleteSign(signId);

				labelProcessText.Text = isDeleted ? $"Sign {signId} was successfully deleted" : $"Failed to delete sign with id {signId}";
			}
			catch (FormatException)
			{
				labelProcessText.Text = "Invalid format for Sign ID";
			}
			catch (Exception ex)
			{
				labelProcessText.Text = $"Error: {ex.Message}";
			}

			// refresh the list to reflect the updated sign
			await RefreshSignList();
		}


		/// <summary>
		/// Populates textboxes with information from the selected sign in the sign list.
		/// </summary>
		private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (listBoxSigns.SelectedIndex != -1)
            {
                Sign selectedSign = (Sign)listBoxSigns.SelectedItem;
                textId.Text = selectedSign.Id.ToString();
                textLocation.Text = selectedSign.Location;
                textSize.Text = selectedSign.Size;
                textResolution.Text = selectedSign.Resolution;
            }
            else
            {
                labelProcessText.Text = "Sign not found";
            }
        }

        /// <summary>
        /// Opens the EventGUI for the selected sign when double-clicked in the sign list.
        /// </summary>
        private void ListBoxSigns_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int clickedIndex = listBoxSigns.IndexFromPoint(e.Location);

            if (clickedIndex != ListBox.NoMatches)
            {
                // Check if the double-clicked item matches the selected item
                if (clickedIndex == listBoxSigns.SelectedIndex)
                {
                    Sign selectedSign = (Sign)listBoxSigns.SelectedItem;
                    EventGUI eventGUI = new EventGUI(selectedSign.Id, this);
                    eventGUI.Text = $"Location: {selectedSign.Location}";
                    eventGUI.Show();
                    Hide();
                }
            }
        }


        /// <summary>
        /// Clears all textboxes and selections in the sign GUI.
        /// </summary>
        private void BtnClear_Click(object sender, EventArgs e)
        {
            //Clears item selection in groupbox
            listBoxSigns.ClearSelected();

            // Clearing all the text fields
            textId.Text = "";
            textLocation.Text = "";
            textSize.Text = "";
            textResolution.Text = "";

            // Clearing process text
            labelProcessText.Text = "";
            
        }

        /// <summary>
        /// Validates input parameters for creating and updating a sign.
        /// </summary>
        /// <returns>Boolean indicating if the input is valid.</returns>
        private static bool InputIsOk(string inLocation, string inSize, string inResolution)
        {
            return !String.IsNullOrWhiteSpace(inLocation) && inLocation.Length > 1 &&
                   !String.IsNullOrWhiteSpace(inSize) && inSize.Length > 1 &&
                   !String.IsNullOrWhiteSpace(inResolution) && inResolution.Length > 2;
        }

        /// <summary>
        /// Event handler triggered when the form is closing.
        /// Handles the closing event of WindowB, preventing the default closing behavior
        /// and shows the prior window (WindowA) instead.
        /// </summary>
        private void SignGUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true; // Cancel the form closing event to allow for manual handling
            this.Hide(); // Hide WindowB
            _priorWindow.Show(); // Show the prior window, WindowA in this case
        }
    }
}