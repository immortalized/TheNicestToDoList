namespace TheNicestToDoList
{
    partial class FrmMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            ntfIcon = new NotifyIcon(components);
            lbxTasks = new ListBox();
            pnlLvlProgress = new Panel();
            lblLevel = new Label();
            lblTitle = new Label();
            txbTitle = new TextBox();
            lblPriority = new Label();
            cbxPriority = new ComboBox();
            dtpDueTime = new DateTimePicker();
            lblDueTime = new Label();
            btnAddTask = new Button();
            pnlLvlPlaceholder = new Panel();
            btnCompleteTask = new Button();
            lblNotifications = new Label();
            label1 = new Label();
            label2 = new Label();
            pnlLvlPlaceholder.SuspendLayout();
            SuspendLayout();
            // 
            // ntfIcon
            // 
            ntfIcon.Icon = (Icon)resources.GetObject("ntfIcon.Icon");
            ntfIcon.Text = "The Nicest To-Do List";
            ntfIcon.Visible = true;
            // 
            // lbxTasks
            // 
            lbxTasks.FormattingEnabled = true;
            lbxTasks.ItemHeight = 15;
            lbxTasks.Location = new Point(12, 200);
            lbxTasks.Name = "lbxTasks";
            lbxTasks.SelectionMode = SelectionMode.MultiExtended;
            lbxTasks.Size = new Size(248, 289);
            lbxTasks.TabIndex = 0;
            // 
            // pnlLvlProgress
            // 
            pnlLvlProgress.BackColor = Color.LightSkyBlue;
            pnlLvlProgress.Location = new Point(0, 0);
            pnlLvlProgress.Name = "pnlLvlProgress";
            pnlLvlProgress.Size = new Size(498, 23);
            pnlLvlProgress.TabIndex = 1;
            // 
            // lblLevel
            // 
            lblLevel.AutoSize = true;
            lblLevel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 238);
            lblLevel.Location = new Point(7, 492);
            lblLevel.Name = "lblLevel";
            lblLevel.Size = new Size(126, 21);
            lblLevel.TabIndex = 2;
            lblLevel.Text = "Level 1 | 0/20 XP";
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 238);
            lblTitle.Location = new Point(12, 9);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(39, 21);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Title";
            // 
            // txbTitle
            // 
            txbTitle.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 238);
            txbTitle.Location = new Point(12, 33);
            txbTitle.Name = "txbTitle";
            txbTitle.Size = new Size(248, 29);
            txbTitle.TabIndex = 3;
            // 
            // lblPriority
            // 
            lblPriority.AutoSize = true;
            lblPriority.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 238);
            lblPriority.Location = new Point(12, 75);
            lblPriority.Name = "lblPriority";
            lblPriority.Size = new Size(61, 21);
            lblPriority.TabIndex = 4;
            lblPriority.Text = "Priority";
            // 
            // cbxPriority
            // 
            cbxPriority.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxPriority.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 238);
            cbxPriority.FormattingEnabled = true;
            cbxPriority.Location = new Point(12, 99);
            cbxPriority.Name = "cbxPriority";
            cbxPriority.Size = new Size(248, 29);
            cbxPriority.TabIndex = 5;
            // 
            // dtpDueTime
            // 
            dtpDueTime.CustomFormat = "yyyy/MM/dd HH:mm";
            dtpDueTime.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 238);
            dtpDueTime.Format = DateTimePickerFormat.Custom;
            dtpDueTime.ImeMode = ImeMode.NoControl;
            dtpDueTime.Location = new Point(12, 165);
            dtpDueTime.Name = "dtpDueTime";
            dtpDueTime.Size = new Size(248, 27);
            dtpDueTime.TabIndex = 6;
            // 
            // lblDueTime
            // 
            lblDueTime.AutoSize = true;
            lblDueTime.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 238);
            lblDueTime.Location = new Point(12, 141);
            lblDueTime.Name = "lblDueTime";
            lblDueTime.Size = new Size(76, 21);
            lblDueTime.TabIndex = 7;
            lblDueTime.Text = "Due Time";
            // 
            // btnAddTask
            // 
            btnAddTask.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 238);
            btnAddTask.Location = new Point(321, 33);
            btnAddTask.Name = "btnAddTask";
            btnAddTask.Size = new Size(149, 29);
            btnAddTask.TabIndex = 8;
            btnAddTask.Text = "Add Task";
            btnAddTask.UseVisualStyleBackColor = true;
            // 
            // pnlLvlPlaceholder
            // 
            pnlLvlPlaceholder.BackColor = Color.Gray;
            pnlLvlPlaceholder.Controls.Add(pnlLvlProgress);
            pnlLvlPlaceholder.Location = new Point(12, 516);
            pnlLvlPlaceholder.Name = "pnlLvlPlaceholder";
            pnlLvlPlaceholder.Size = new Size(498, 23);
            pnlLvlPlaceholder.TabIndex = 2;
            // 
            // btnCompleteTask
            // 
            btnCompleteTask.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 238);
            btnCompleteTask.Location = new Point(321, 163);
            btnCompleteTask.Name = "btnCompleteTask";
            btnCompleteTask.Size = new Size(149, 29);
            btnCompleteTask.TabIndex = 9;
            btnCompleteTask.Text = "Complete Task(s)";
            btnCompleteTask.UseVisualStyleBackColor = true;
            // 
            // lblNotifications
            // 
            lblNotifications.AutoSize = true;
            lblNotifications.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 238);
            lblNotifications.Location = new Point(266, 253);
            lblNotifications.Name = "lblNotifications";
            lblNotifications.Size = new Size(241, 17);
            lblNotifications.TabIndex = 10;
            lblNotifications.Text = "Reach level 8 to disable message boxes";
            lblNotifications.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 238);
            label1.Location = new Point(266, 336);
            label1.Name = "label1";
            label1.Size = new Size(230, 17);
            label1.TabIndex = 11;
            label1.Text = "Reach level 16 to disable notifications.";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 238);
            label2.Location = new Point(266, 419);
            label2.Name = "label2";
            label2.Size = new Size(202, 17);
            label2.TabIndex = 12;
            label2.Text = "Reach level 20 to disable sounds.";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // FrmMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(522, 551);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(lblNotifications);
            Controls.Add(btnCompleteTask);
            Controls.Add(pnlLvlPlaceholder);
            Controls.Add(btnAddTask);
            Controls.Add(lblDueTime);
            Controls.Add(dtpDueTime);
            Controls.Add(cbxPriority);
            Controls.Add(lblPriority);
            Controls.Add(txbTitle);
            Controls.Add(lblTitle);
            Controls.Add(lblLevel);
            Controls.Add(lbxTasks);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FrmMain";
            Text = "The Nicest To-Do List";
            pnlLvlPlaceholder.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private NotifyIcon ntfIcon;
        private ListBox lbxTasks;
        private Panel pnlLvlProgress;
        private Label lblLevel;
        private Label lblTitle;
        private TextBox txbTitle;
        private Label lblPriority;
        private ComboBox cbxPriority;
        private DateTimePicker dtpDueTime;
        private Label lblDueTime;
        private Button btnAddTask;
        private Panel pnlLvlPlaceholder;
        private Button btnCompleteTask;
        private Label lblNotifications;
        private Label label1;
        private Label label2;
    }
}
