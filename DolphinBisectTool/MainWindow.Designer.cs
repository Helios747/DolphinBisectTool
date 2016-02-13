namespace DolphinBisectTool
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.first_build_label = new System.Windows.Forms.Label();
            this.second_build_label = new System.Windows.Forms.Label();
            this.boot_title = new System.Windows.Forms.CheckBox();
            this.file_path_textbox = new System.Windows.Forms.TextBox();
            this.radio_group = new System.Windows.Forms.Panel();
            this.radio_development = new System.Windows.Forms.RadioButton();
            this.radio_stable = new System.Windows.Forms.RadioButton();
            this.download_bar = new System.Windows.Forms.ProgressBar();
            this.download_label = new System.Windows.Forms.Label();
            this.first_dev_build = new System.Windows.Forms.ComboBox();
            this.second_dev_build = new System.Windows.Forms.ComboBox();
            this.first_stable_label = new System.Windows.Forms.Label();
            this.browse_button = new System.Windows.Forms.Button();
            this.start_button = new System.Windows.Forms.Button();
            this.radio_group.SuspendLayout();
            this.SuspendLayout();
            // 
            // first_build_label
            // 
            this.first_build_label.AutoSize = true;
            this.first_build_label.Enabled = false;
            this.first_build_label.Location = new System.Drawing.Point(16, 11);
            this.first_build_label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.first_build_label.Name = "first_build_label";
            this.first_build_label.Size = new System.Drawing.Size(408, 17);
            this.first_build_label.TabIndex = 0;
            this.first_build_label.Text = "Select a stable or development build before the issue appeared";
            // 
            // second_build_label
            // 
            this.second_build_label.AutoSize = true;
            this.second_build_label.Enabled = false;
            this.second_build_label.Location = new System.Drawing.Point(16, 110);
            this.second_build_label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.second_build_label.Name = "second_build_label";
            this.second_build_label.Size = new System.Drawing.Size(348, 17);
            this.second_build_label.TabIndex = 1;
            this.second_build_label.Text = "Select the development build where you saw the issue";
            // 
            // boot_title
            // 
            this.boot_title.AutoSize = true;
            this.boot_title.Enabled = false;
            this.boot_title.Location = new System.Drawing.Point(24, 206);
            this.boot_title.Margin = new System.Windows.Forms.Padding(4);
            this.boot_title.Name = "boot_title";
            this.boot_title.Size = new System.Drawing.Size(85, 21);
            this.boot_title.TabIndex = 9;
            this.boot_title.Text = "Boot title";
            this.boot_title.UseVisualStyleBackColor = true;
            // 
            // file_path_textbox
            // 
            this.file_path_textbox.Enabled = false;
            this.file_path_textbox.Location = new System.Drawing.Point(185, 201);
            this.file_path_textbox.Margin = new System.Windows.Forms.Padding(4);
            this.file_path_textbox.Name = "file_path_textbox";
            this.file_path_textbox.Size = new System.Drawing.Size(253, 22);
            this.file_path_textbox.TabIndex = 10;
            // 
            // radio_group
            // 
            this.radio_group.Controls.Add(this.radio_development);
            this.radio_group.Controls.Add(this.radio_stable);
            this.radio_group.Location = new System.Drawing.Point(20, 32);
            this.radio_group.Margin = new System.Windows.Forms.Padding(4);
            this.radio_group.Name = "radio_group";
            this.radio_group.Size = new System.Drawing.Size(152, 74);
            this.radio_group.TabIndex = 15;
            // 
            // radio_development
            // 
            this.radio_development.AutoSize = true;
            this.radio_development.Enabled = false;
            this.radio_development.Location = new System.Drawing.Point(5, 34);
            this.radio_development.Margin = new System.Windows.Forms.Padding(4);
            this.radio_development.Name = "radio_development";
            this.radio_development.Size = new System.Drawing.Size(112, 21);
            this.radio_development.TabIndex = 1;
            this.radio_development.TabStop = true;
            this.radio_development.Text = "Development";
            this.radio_development.UseVisualStyleBackColor = true;
            this.radio_development.CheckedChanged += new System.EventHandler(this.rbFirstDevelopment_CheckedChanged);
            // 
            // radio_stable
            // 
            this.radio_stable.AutoSize = true;
            this.radio_stable.Enabled = false;
            this.radio_stable.Location = new System.Drawing.Point(5, 5);
            this.radio_stable.Margin = new System.Windows.Forms.Padding(4);
            this.radio_stable.Name = "radio_stable";
            this.radio_stable.Size = new System.Drawing.Size(69, 21);
            this.radio_stable.TabIndex = 0;
            this.radio_stable.TabStop = true;
            this.radio_stable.Text = "Stable";
            this.radio_stable.UseVisualStyleBackColor = true;
            this.radio_stable.CheckedChanged += new System.EventHandler(this.rbFirstStable_CheckedChanged);
            // 
            // download_bar
            // 
            this.download_bar.Location = new System.Drawing.Point(25, 270);
            this.download_bar.Margin = new System.Windows.Forms.Padding(4);
            this.download_bar.Name = "download_bar";
            this.download_bar.Size = new System.Drawing.Size(307, 28);
            this.download_bar.TabIndex = 17;
            this.download_bar.Visible = false;
            // 
            // download_label
            // 
            this.download_label.AutoSize = true;
            this.download_label.Location = new System.Drawing.Point(21, 246);
            this.download_label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.download_label.Name = "download_label";
            this.download_label.Size = new System.Drawing.Size(89, 17);
            this.download_label.TabIndex = 18;
            this.download_label.Text = "Downloading";
            this.download_label.Visible = false;
            // 
            // first_dev_build
            // 
            this.first_dev_build.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.first_dev_build.Enabled = false;
            this.first_dev_build.FormattingEnabled = true;
            this.first_dev_build.Location = new System.Drawing.Point(185, 48);
            this.first_dev_build.Margin = new System.Windows.Forms.Padding(4);
            this.first_dev_build.Name = "first_dev_build";
            this.first_dev_build.Size = new System.Drawing.Size(253, 24);
            this.first_dev_build.TabIndex = 25;
            this.first_dev_build.SelectedIndexChanged += new System.EventHandler(this.first_dev_build_SelectedIndexChanged);
            // 
            // second_dev_build
            // 
            this.second_dev_build.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.second_dev_build.Enabled = false;
            this.second_dev_build.FormattingEnabled = true;
            this.second_dev_build.Location = new System.Drawing.Point(185, 142);
            this.second_dev_build.Margin = new System.Windows.Forms.Padding(4);
            this.second_dev_build.Name = "second_dev_build";
            this.second_dev_build.Size = new System.Drawing.Size(253, 24);
            this.second_dev_build.TabIndex = 26;
            this.second_dev_build.SelectedIndexChanged += new System.EventHandler(this.second_dev_build_SelectedIndexChanged);
            // 
            // first_stable_label
            // 
            this.first_stable_label.AutoSize = true;
            this.first_stable_label.Enabled = false;
            this.first_stable_label.Location = new System.Drawing.Point(181, 48);
            this.first_stable_label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.first_stable_label.Name = "first_stable_label";
            this.first_stable_label.Size = new System.Drawing.Size(46, 17);
            this.first_stable_label.TabIndex = 27;
            this.first_stable_label.Text = "label3";
            this.first_stable_label.Visible = false;
            // 
            // browse_button
            // 
            this.browse_button.Enabled = false;
            this.browse_button.Location = new System.Drawing.Point(452, 198);
            this.browse_button.Margin = new System.Windows.Forms.Padding(4);
            this.browse_button.Name = "browse_button";
            this.browse_button.Size = new System.Drawing.Size(100, 28);
            this.browse_button.TabIndex = 29;
            this.browse_button.Text = "Browse";
            this.browse_button.UseVisualStyleBackColor = true;
            this.browse_button.Click += new System.EventHandler(this.browse_button_Click);
            // 
            // start_button
            // 
            this.start_button.Enabled = false;
            this.start_button.Location = new System.Drawing.Point(452, 270);
            this.start_button.Margin = new System.Windows.Forms.Padding(4);
            this.start_button.Name = "start_button";
            this.start_button.Size = new System.Drawing.Size(100, 28);
            this.start_button.TabIndex = 28;
            this.start_button.Text = "Start";
            this.start_button.UseVisualStyleBackColor = true;
            this.start_button.Click += new System.EventHandler(this.start_button_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(565, 313);
            this.Controls.Add(this.browse_button);
            this.Controls.Add(this.start_button);
            this.Controls.Add(this.first_stable_label);
            this.Controls.Add(this.second_dev_build);
            this.Controls.Add(this.first_dev_build);
            this.Controls.Add(this.download_label);
            this.Controls.Add(this.download_bar);
            this.Controls.Add(this.radio_group);
            this.Controls.Add(this.file_path_textbox);
            this.Controls.Add(this.boot_title);
            this.Controls.Add(this.second_build_label);
            this.Controls.Add(this.first_build_label);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "MainWindow";
            this.Text = "Dolphin Bisect Tool";
            this.radio_group.ResumeLayout(false);
            this.radio_group.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label first_build_label;
        private System.Windows.Forms.Label second_build_label;
        private System.Windows.Forms.CheckBox boot_title;
        private System.Windows.Forms.TextBox file_path_textbox;
        private System.Windows.Forms.Panel radio_group;
        private System.Windows.Forms.RadioButton radio_development;
        private System.Windows.Forms.RadioButton radio_stable;
        private System.Windows.Forms.ProgressBar download_bar;
        private System.Windows.Forms.Label download_label;
        private System.Windows.Forms.ComboBox first_dev_build;
        private System.Windows.Forms.ComboBox second_dev_build;
        private System.Windows.Forms.Label first_stable_label;
        private System.Windows.Forms.Button start_button;
        private System.Windows.Forms.Button browse_button;
    }
}

