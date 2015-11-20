using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace DolphinBisectTool
{
    public partial class MainWindow : Form
    {
        Backend m_backend = new Backend();

        public MainWindow()
        {
            InitializeComponent();
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;

            download_label.Text = "Downloading build index";
            download_label.Visible = true;

            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += PostLoadTasks;
            bw.RunWorkerCompleted += PostLoadTasksCompleted;
            bw.RunWorkerAsync();
        }

        private void PostLoadTasks(object sender, DoWorkEventArgs e)
        {
            m_backend.DownloadBuildList();
            m_backend.ProcessBuildList();
        }

        private void PostLoadTasksCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            PopulateComboBoxes(m_backend.m_build_list);
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "All GC/Wii Files|*.elf; *.dol; *.gcm; *.iso;" +
                        "*.wbfs; *.ciso; *.gcz; *.wad| All Files (*.*)|*.*";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                file_path_textbox.Text = fd.FileName;
                boot_title.Checked = true;
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            string title;

            if (first_dev_build.SelectedIndex == second_dev_build.SelectedIndex ||
                (radio_stable.Checked && second_dev_build.SelectedIndex == 0))
            {
                MessageBox.Show("Selected builds cannot be the same",
                                "Error", MessageBoxButtons.OK);
                return;
            }

            if (first_dev_build.SelectedIndex >= second_dev_build.SelectedIndex)
            {
                MessageBox.Show("First build cannot be newer than second",
                                "Error", MessageBoxButtons.OK);
                return;
            }

            if (boot_title.Checked && file_path_textbox.Text.Equals(""))
            {
                MessageBox.Show("Boot title enabled with no game / title selected",
                                "Error", MessageBoxButtons.OK);
                return;
            }
            else
            {
                title = file_path_textbox.Text;
            }

            if (radio_stable.Checked && boot_title.Checked)
            {
                m_backend.SetSettings(-1, second_dev_build.SelectedIndex, file_path_textbox.Text,
                                      this);
            }
            else if (radio_stable.Checked)
            {
                m_backend.SetSettings(-1, second_dev_build.SelectedIndex, this);
            }
            else if (!radio_stable.Checked && boot_title.Checked)
            {
                m_backend.SetSettings(first_dev_build.SelectedIndex,
                                    second_dev_build.SelectedIndex, file_path_textbox.Text, this);
            }
            else
            {
                m_backend.SetSettings(first_dev_build.SelectedIndex,
                                    second_dev_build.SelectedIndex, this);
            }

            start_button.Enabled = false;
            var testWorker = new BackgroundWorker();
            testWorker.DoWork += (s, ea) => m_backend.Run();
            testWorker.RunWorkerCompleted += (s, ea) => start_button.Enabled = true;
            testWorker.RunWorkerAsync();
        }
        
        public void ChangeProgressBar(int v, string t)
        {
            // pass the call to the UI thread if necessary
            if (InvokeRequired)
            {
                Invoke(new Action<int, string>(ChangeProgressBar), v, t);
                return;
            }

            if (v != 100)
            {
                download_bar.Visible = true;
                download_label.Text = t;
                download_label.Visible = true;
                download_bar.Value = v;
            }
            else
            {
                download_label.Visible = false;
                download_bar.Visible = false;
            }
        }

        private void PopulateComboBoxes(List<int> l)
        {
            foreach (int i in l)
            {
                first_dev_build.Items.Add(Backend.s_major_version + "-" + i);
                second_dev_build.Items.Add(Backend.s_major_version + "-" + i);
            }

            first_dev_build.SelectedIndex = 0;
            second_dev_build.SelectedIndex = 0;
            EnableUI();
        }

        private void EnableUI()
        {
            radio_stable.Select();
            first_build_label.Enabled = true;
            second_build_label.Enabled = true;
            second_dev_build.Enabled = true;
            boot_title.Enabled = true;
            file_path_textbox.Enabled = true;
            browse_button.Enabled = true;
            start_button.Enabled = true;
            radio_development.Enabled = true;
            radio_stable.Enabled = true;
            download_label.Visible = false;
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
