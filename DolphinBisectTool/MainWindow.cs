using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace DolphinBisectTool
{
    public partial class MainWindow : Form
    {

        // TODO - get rid of these two variables so I stop bouncing it through the code.
        List<int> build_list;
        static string s_major_version = "4.0";

        DownloadBuildList m_download_build_list = new DownloadBuildList();
        DownloadBuild m_download_build = new DownloadBuild();

        public MainWindow()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.FixedSingle;

            m_download_build_list.UpdateProgress += ChangeProgressBar;
            m_download_build.UpdateProgress += ChangeProgressBar;

            download_label.Text = "Downloading build index";
            download_label.Visible = true;

            m_download_build_list.Download();
        }

        private UserInput BisectUserDialog(int build, bool final_trigger)
        {

            DialogResult result;
            if (!final_trigger)
               result = MessageBox.Show("Tested build " + s_major_version + "-" + build_list[build] + ". Did the bug happen in this build?",
                                        "Bisect", MessageBoxButtons.YesNoCancel);
            else
                result = MessageBox.Show("Build " + s_major_version + "-" + build_list[build] + " may be the cause of your issue. " +
                                         "Do you want to open the URL for that build?", "Notice",
                                         MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
                return UserInput.Yes;
            else if (result == DialogResult.No)
                return UserInput.No;
            else
                return UserInput.Cancel;

        }

        private void browse_button_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "All GC/Wii Files|*.elf; *.dol; *.gcm; *.iso;" +
                        "*.wbfs; *.ciso; *.gcz; *.wad| All Files (*.*)|*.*";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                file_path_textbox.Text = dialog.FileName;
                boot_title.Checked = true;
            }
        }

        // Not the biggest fan of this implementation
        private void start_button_Click(object sender, EventArgs e)
        {
            start_button.Enabled = false;
            int first_build;

            if (radio_stable.Checked)
                first_build = 0;
            else
                first_build = first_dev_build.SelectedIndex;

            Backend backend = new Backend(first_build, second_dev_build.SelectedIndex, build_list, s_major_version);
            backend.BisectEvent += BisectUserDialog;

            if (boot_title.Checked)
                backend.Bisect(file_path_textbox.Text);
            else
                backend.Bisect();   

        }

        public void ChangeProgressBar(int value, string text, ProgressBarStyle style)
        {
            if (value != 100)
            {
                download_bar.Visible = true;
                download_label.Text = text;
                download_label.Visible = true;
                download_bar.Style = style;
            }
            else if (value == 100 && text.Equals("Parsing build list"))
            {
                download_label.Text = text;
                download_bar.Style = style;
                ProcessBuildList process_build = new ProcessBuildList();
                build_list = process_build.Run(s_major_version);
                PopulateComboBoxes(build_list);
            }
            else
            {
                download_label.Visible = false;
                download_bar.Visible = false;
            }
        }

        private void PopulateComboBoxes(List<int> list)
        {
            foreach (int i in list)
            {
                first_dev_build.Items.Add(s_major_version + "-" + i);
                second_dev_build.Items.Add(s_major_version + "-" + i);
            }

            first_dev_build.SelectedIndex = 0;
            second_dev_build.SelectedIndex = 0;
            EnableUI();
        }

        private void EnableUI()
        {
            Enabled = true;
            download_label.Visible = false;
            radio_development.Enabled = true;
            radio_stable.Select();
        }

        private void rbFirstStable_CheckedChanged(object sender, EventArgs e)
        {
            if (radio_stable.Checked)
            {
                first_stable_label.Text = "Version 4.0.2 will be tested";
                first_stable_label.Visible = true;
                first_stable_label.Enabled = true;
                first_dev_build.Enabled = false;
                first_dev_build.Visible = false;
            }
        }

        private void rbFirstDevelopment_CheckedChanged(object sender, EventArgs e)
        {
            if (radio_development.Checked)
            {
                first_stable_label.Visible = false;
                first_dev_build.Enabled = true;
                first_dev_build.Visible = true;
            }
        }
    }
}
