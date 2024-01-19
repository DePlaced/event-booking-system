using BigDSignClientDesktop.Controller;
using BigDSignClientDesktop.Model;
using BigDSignClientDesktop.GUI;
using System;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using BigDSignClientDesktop.Service;
using AsyncAwaitBestPractices;

namespace BigDSignClientDesktop.GUI
{
    /// <summary>
    /// Represents the graphical user interface for managing stadiums.
    /// </summary>
    public partial class StadiumGUI : Form
    {
        private readonly StadiumControl _stadiumControl;
        private readonly int _adminId = 1;

        /// <summary>
        /// Initializes a new instance of the StadiumGUI class.
        /// </summary>
        public StadiumGUI()
        {
            InitializeComponent();
            _stadiumControl = new StadiumControl();
            RefreshStadiumList().SafeFireAndForget();
        }

        /// <summary>
        /// Creates a new stadium and saves it through the StadiumControl.
        /// </summary>
        private async void BtnCreateS_Click(object sender, EventArgs e)
        {
            var stadiumName = textStadiumName.Text;
            var street = textStreet.Text;
            var city = textCity.Text;
            var adminId = _adminId;
            if (InputIsOk(stadiumName, street, city) && int.TryParse(textZipcode.Text, out int resultZipcode))
            {
                try
                {
                    var newStadiumId = await _stadiumControl.SaveStadium(stadiumName, street, city, resultZipcode, adminId);
                    labelPTS.Text = $"Stadium created successfully with ID: {newStadiumId}";
                }
                // Catch exceptions returned from the API service.
                catch (ServiceException exception)
                {
                    labelPTS.Text = $"{(int)exception.StatusCode} {exception.Message}";
                }
                // Catch other exceptions.
                catch (Exception exception)
                {
                    Console.Error.Write(exception);
                    labelPTS.Text = $"500 Internal server error.";
                }
            }
            else
            {
                labelPTS.Text = ("Input validation failed.");
            }
            await RefreshStadiumList();
        }

        /// <summary>
        /// Refreshes the list of stadiums displayed in the GUI.
        /// </summary>
        private async Task RefreshStadiumList()
        {
            try
            {
                var stadiums = await _stadiumControl.GetAllStadiums();
                listBoxStadiums.DataSource = stadiums;
                listBoxStadiums.DisplayMember = "StadiumName";
            }
            // Catch exceptions returned from the API service.
            catch (ServiceException exception)
            {
                MessageBox.Show($"{(int)exception.StatusCode} {exception.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // Catch other exceptions.
            catch (Exception exception)
            {
                Console.Error.Write(exception);
                MessageBox.Show($"500 Internal server error.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void BtnGetS_Click(object sender, EventArgs e)
        {
            await RefreshStadiumList();
        }

        /// <summary>
        /// Updates the information of an existing stadium through the StadiumControl.
        /// </summary>
        private async void BtnUpdateS_Click(object sender, EventArgs e)
        {
            var stadiumName = textStadiumName.Text;
            var street = textStreet.Text;
            var city = textCity.Text;
            var adminId = _adminId;

            if (InputIsOk(stadiumName, street, city) && int.TryParse(textZipcode.Text, out int resultZipcode) && int.TryParse(textId.Text, out int resultStadiumId))
            {
                Model.Stadium stadiumToUpdate = new Model.Stadium(resultStadiumId, stadiumName, street, city, resultZipcode, adminId);
                try
                {
                    var updated = await _stadiumControl.UpdateStadium(stadiumToUpdate);
                    labelPTS.Text = updated ? "Stadium updated successfully." : "Failed to update stadium.";
                }
                // Catch exceptions returned from the API service.
                catch (ServiceException exception)
                {
                    labelPTS.Text = $"{(int)exception.StatusCode} {exception.Message}";
                }
                // Catch other exceptions.
                catch (Exception exception)
                {
                    Console.Error.Write(exception);
                    labelPTS.Text = $"500 Internal server error.";
                }
            }
            else
            {
                labelPTS.Text = ("Input validation failed.");
            }
            await RefreshStadiumList();
        }

        /// <summary>
        /// Deletes a stadium through the StadiumControl.
        /// </summary>
        private async void BtnDeleteS_Click(object sender, EventArgs e)
        {
            try
            {
                var stadiumId = int.Parse(textId.Text);
                var isDeleted = await _stadiumControl.DeleteStadium(stadiumId);
                labelPTS.Text = (isDeleted ? "Stadium deleted successfully." : "Failed to delete stadium.");
            }
            // Catch exceptions returned from the API service.
            catch (ServiceException exception)
            {
                labelPTS.Text = $"{(int)exception.StatusCode} {exception.Message}";
            }
            // Catch other exceptions.
            catch (Exception exception)
            {
                Console.Error.Write(exception);
                labelPTS.Text = $"500 Internal server error.";
            }
            await RefreshStadiumList();
        }

        /// <summary>
        /// Clears all textboxes and selections in the stadium GUI.
        /// </summary>
        private void BtnClearS_Click(object sender, EventArgs e)
        {
            //Clears textBoxes
            textId.Text = "";
            textStadiumName.Text = "";
            textStreet.Text = "";
            textCity.Text = "";
            textZipcode.Text = "";

            //Clear selection
            listBoxStadiums.ClearSelected();

            // clear procces text
            labelPTS.Text = "";
        }

        /// <summary>
        /// Populates textboxes with information from the selected stadium in the stadium list.
        /// </summary>
        private void ListBoxStadiums_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxStadiums.SelectedIndex != -1)
            {
                var selectedStadium = (Model.Stadium)listBoxStadiums.SelectedItem;
                textId.Text = selectedStadium.Id.ToString();
                textStadiumName.Text = selectedStadium.StadiumName;
                textStreet.Text = selectedStadium.Street;
                textCity.Text = selectedStadium.City;
                textZipcode.Text = selectedStadium.Zipcode.ToString();
            }
            else
            {
                labelPTS.Text = "Stadium not found.";
            }
        }

        /// <summary>
        /// Opens the SignGUI for the selected stadium when double-clicked in the stadium list.
        /// </summary>
        private void ListBoxStadiums_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int clickedIndex = listBoxStadiums.IndexFromPoint(e.Location);

            if (clickedIndex != ListBox.NoMatches)
            {
                // Check if the double-clicked item matches the selected item
                if (clickedIndex == listBoxStadiums.SelectedIndex)
                {
                    Stadium selectedStadium = (Stadium)listBoxStadiums.SelectedItem;
                    SignGUI signGUI = new SignGUI(selectedStadium.Id, this);
                    signGUI.Text = selectedStadium.StadiumName;
                    signGUI.Show();
                    this.Hide();
                }
            }
        }

        /// <summary>
        /// Validates input parameters for creating and updating a stadium.
        /// </summary>
        /// <returns>Boolean indicating if the input is valid.</returns>
        private static bool InputIsOk(string stadiumName, string street, string city)
        {
            bool isValidInput = false;
            if (!String.IsNullOrWhiteSpace(stadiumName) && !String.IsNullOrWhiteSpace(street) && !String.IsNullOrWhiteSpace(city))
            {
                if (stadiumName.Length > 0 && street.Length > 0 && city.Length > 0)
                {
                    isValidInput = true;
                }
            }
            return isValidInput;
        }
    }
}
