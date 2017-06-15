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
            this.download_bar = new System.Windows.Forms.ProgressBar();
            this.download_label = new System.Windows.Forms.Label();
            this.first_dev_build = new System.Windows.Forms.ComboBox();
            this.second_dev_build = new System.Windows.Forms.ComboBox();
            this.browse_button = new System.Windows.Forms.Button();
            this.start_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // first_build_label
            // 
            this.first_build_label.AutoSize = true;
            this.first_build_label.Location = new System.Drawing.Point(12, 9);
            this.first_build_label.Name = "first_build_label";
            this.first_build_label.Size = new System.Drawing.Size(202, 13);
            this.first_build_label.TabIndex = 0;
            this.first_build_label.Text = "Select a development build that is broken";
            // 
            // second_build_label
            // 
            this.second_build_label.AutoSize = true;
            this.second_build_label.Location = new System.Drawing.Point(12, 89);
            this.second_build_label.Name = "second_build_label";
            this.second_build_label.Size = new System.Drawing.Size(203, 13);
            this.second_build_label.TabIndex = 1;
            this.second_build_label.Text = "Select the development build that worked\r\n";
            // 
            // boot_title
            // 
            this.boot_title.AutoSize = true;
            this.boot_title.Location = new System.Drawing.Point(18, 167);
            this.boot_title.Name = "boot_title";
            this.boot_title.Size = new System.Drawing.Size(67, 17);
            this.boot_title.TabIndex = 9;
            this.boot_title.Text = "Boot title";
            this.boot_title.UseVisualStyleBackColor = true;
            // 
            // file_path_textbox
            // 
            this.file_path_textbox.Location = new System.Drawing.Point(139, 163);
            this.file_path_textbox.Name = "file_path_textbox";
            this.file_path_textbox.Size = new System.Drawing.Size(191, 20);
            this.file_path_textbox.TabIndex = 10;
            // 
            // download_bar
            // 
            this.download_bar.Location = new System.Drawing.Point(19, 219);
            this.download_bar.Name = "download_bar";
            this.download_bar.Size = new System.Drawing.Size(230, 23);
            this.download_bar.TabIndex = 17;
            this.download_bar.Visible = false;
            // 
            // download_label
            // 
            this.download_label.AutoSize = true;
            this.download_label.Location = new System.Drawing.Point(16, 200);
            this.download_label.Name = "download_label";
            this.download_label.Size = new System.Drawing.Size(69, 13);
            this.download_label.TabIndex = 18;
            this.download_label.Text = "Downloading";
            this.download_label.Visible = false;
            // 
            // first_dev_build
            // 
            this.first_dev_build.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.first_dev_build.FormattingEnabled = true;
            this.first_dev_build.Location = new System.Drawing.Point(15, 39);
            this.first_dev_build.Name = "first_dev_build";
            this.first_dev_build.Size = new System.Drawing.Size(191, 21);
            this.first_dev_build.TabIndex = 25;
            // 
            // second_dev_build
            // 
            this.second_dev_build.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.second_dev_build.FormattingEnabled = true;
            this.second_dev_build.Location = new System.Drawing.Point(15, 116);
            this.second_dev_build.Name = "second_dev_build";
            this.second_dev_build.Size = new System.Drawing.Size(191, 21);
            this.second_dev_build.TabIndex = 26;
            // 
            // browse_button
            // 
            this.browse_button.Location = new System.Drawing.Point(339, 161);
            this.browse_button.Name = "browse_button";
            this.browse_button.Size = new System.Drawing.Size(75, 23);
            this.browse_button.TabIndex = 29;
            this.browse_button.Text = "Browse";
            this.browse_button.UseVisualStyleBackColor = true;
            this.browse_button.Click += new System.EventHandler(this.browse_button_Click);
            // 
            // start_button
            // 
            this.start_button.Location = new System.Drawing.Point(339, 219);
            this.start_button.Name = "start_button";
            this.start_button.Size = new System.Drawing.Size(75, 23);
            this.start_button.TabIndex = 28;
            this.start_button.Text = "Start";
            this.start_button.UseVisualStyleBackColor = true;
            this.start_button.Click += new System.EventHandler(this.start_button_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(424, 254);
            this.Controls.Add(this.browse_button);
            this.Controls.Add(this.start_button);
            this.Controls.Add(this.second_dev_build);
            this.Controls.Add(this.first_dev_build);
            this.Controls.Add(this.download_label);
            this.Controls.Add(this.download_bar);
            this.Controls.Add(this.file_path_textbox);
            this.Controls.Add(this.boot_title);
            this.Controls.Add(this.second_build_label);
            this.Controls.Add(this.first_build_label);
            this.Enabled = false;
            this.MaximizeBox = false;
            this.Name = "MainWindow";
            this.Text = "Dolphin Bisect Tool";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label first_build_label;
        private System.Windows.Forms.Label second_build_label;
        private System.Windows.Forms.CheckBox boot_title;
        private System.Windows.Forms.TextBox file_path_textbox;
        private System.Windows.Forms.ProgressBar download_bar;
        private System.Windows.Forms.Label download_label;
        private System.Windows.Forms.ComboBox first_dev_build;
        private System.Windows.Forms.ComboBox second_dev_build;
        private System.Windows.Forms.Button start_button;
        private System.Windows.Forms.Button browse_button;
    }
}

