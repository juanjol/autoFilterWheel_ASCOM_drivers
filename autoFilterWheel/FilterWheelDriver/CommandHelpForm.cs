using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ASCOM.autoFilterWheel.FilterWheel
{
    /// <summary>
    /// Form to display command help and allow command selection
    /// </summary>
    public class CommandHelpForm : Form
    {
        private ListBox listBoxCommands;
        private TextBox textBoxSearch;
        private TextBox textBoxDetails;
        private Button btnSelect;
        private Button btnCancel;
        private Label labelSearch;
        private Label labelCommands;
        private Label labelDetails;

        public string SelectedCommand { get; private set; }

        public CommandHelpForm(string initialCommand = "")
        {
            InitializeComponent();
            LoadCommands();

            // Select initial command if provided
            if (!string.IsNullOrWhiteSpace(initialCommand))
            {
                textBoxSearch.Text = initialCommand;
                FilterCommands(initialCommand);
            }
        }

        private void InitializeComponent()
        {
            this.Text = "Command Help";
            this.Size = new Size(700, 500);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // Search label
            labelSearch = new Label
            {
                Text = "Search:",
                Location = new Point(10, 10),
                Size = new Size(50, 20)
            };
            this.Controls.Add(labelSearch);

            // Search textbox
            textBoxSearch = new TextBox
            {
                Location = new Point(70, 10),
                Size = new Size(200, 20)
            };
            textBoxSearch.TextChanged += TextBoxSearch_TextChanged;
            this.Controls.Add(textBoxSearch);

            // Commands label
            labelCommands = new Label
            {
                Text = "Available Commands:",
                Location = new Point(10, 40),
                Size = new Size(150, 20)
            };
            this.Controls.Add(labelCommands);

            // Commands listbox
            listBoxCommands = new ListBox
            {
                Location = new Point(10, 65),
                Size = new Size(250, 350),
                Font = new Font("Consolas", 9F)
            };
            listBoxCommands.SelectedIndexChanged += ListBoxCommands_SelectedIndexChanged;
            listBoxCommands.DoubleClick += ListBoxCommands_DoubleClick;
            this.Controls.Add(listBoxCommands);

            // Details label
            labelDetails = new Label
            {
                Text = "Command Details:",
                Location = new Point(270, 40),
                Size = new Size(150, 20)
            };
            this.Controls.Add(labelDetails);

            // Details textbox
            textBoxDetails = new TextBox
            {
                Location = new Point(270, 65),
                Size = new Size(410, 350),
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical,
                Font = new Font("Consolas", 9F),
                BackColor = Color.White
            };
            this.Controls.Add(textBoxDetails);

            // Select button
            btnSelect = new Button
            {
                Text = "Select",
                Location = new Point(520, 425),
                Size = new Size(75, 30),
                DialogResult = DialogResult.OK
            };
            btnSelect.Click += BtnSelect_Click;
            this.Controls.Add(btnSelect);
            this.AcceptButton = btnSelect;

            // Cancel button
            btnCancel = new Button
            {
                Text = "Cancel",
                Location = new Point(605, 425),
                Size = new Size(75, 30),
                DialogResult = DialogResult.Cancel
            };
            this.Controls.Add(btnCancel);
            this.CancelButton = btnCancel;
        }

        private void LoadCommands()
        {
            var commands = CommandHelp.SearchCommands("");
            listBoxCommands.Items.Clear();

            foreach (var cmd in commands)
            {
                listBoxCommands.Items.Add(cmd);
            }

            if (listBoxCommands.Items.Count > 0)
            {
                listBoxCommands.SelectedIndex = 0;
            }
        }

        private void FilterCommands(string searchText)
        {
            var commands = CommandHelp.SearchCommands(searchText);
            listBoxCommands.Items.Clear();

            foreach (var cmd in commands)
            {
                listBoxCommands.Items.Add(cmd);
            }

            if (listBoxCommands.Items.Count > 0)
            {
                listBoxCommands.SelectedIndex = 0;
            }
        }

        private void TextBoxSearch_TextChanged(object sender, EventArgs e)
        {
            FilterCommands(textBoxSearch.Text);
        }

        private void ListBoxCommands_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxCommands.SelectedItem is CommandHelp.CommandInfo cmdInfo)
            {
                textBoxDetails.Text = cmdInfo.GetDetailedHelp();
            }
        }

        private void ListBoxCommands_DoubleClick(object sender, EventArgs e)
        {
            BtnSelect_Click(sender, e);
        }

        private void BtnSelect_Click(object sender, EventArgs e)
        {
            if (listBoxCommands.SelectedItem is CommandHelp.CommandInfo cmdInfo)
            {
                SelectedCommand = cmdInfo.Command;
            }
        }
    }
}
