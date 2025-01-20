using System.ComponentModel;
using System.Text.Json;
using Timer = System.Windows.Forms.Timer;

namespace TheNicestToDoList
{
    public partial class FrmMain : Form
    {
        BindingList<TaskItem> tasks = new BindingList<TaskItem>();
        private ContextMenuStrip cmsTray;

        // Level related stuff
        private int currentLevel = 1;
        private int currentXP = 0;
        private int xpNeeded;
        private int excessXP = 0;
        private const int BASE_XP = 100;
        private const double GROWTH_RATE = 1.2;
        private const int MAX_PANEL_WIDTH = 498;

        private const string SAVE_FILE_PATH = "data.json";

        // Timers
        private Timer randomSoundTimer = new Timer();
        private Timer notificationTimer = new Timer();
        private Timer messageBoxTimer = new Timer();

        public FrmMain()
        {
            InitializeComponent();

            // Tray Menu
            cmsTray = new ContextMenuStrip();
            cmsTray.Items.Add("Open", null, OpenApplication);
            cmsTray.Items.Add("Exit", null, ExitApplication);
            ntfIcon.ContextMenuStrip = cmsTray;

            // Populate comboBox
            cbxPriority.DataSource = Enum.GetValues(typeof(Priority));

            LoadData();

            // Level Stuff
            xpNeeded = GetXPForNextLevel(currentLevel);
            RefreshLevelControls();

            lbxTasks.DataSource = tasks;

            // Set up timers
            randomSoundTimer.Interval = 30000;
            randomSoundTimer.Tick += RandomSoundTimerTick;
            randomSoundTimer.Start();

            notificationTimer.Interval = 120000;
            notificationTimer.Tick += NotificationTimerTick;
            notificationTimer.Start();

            messageBoxTimer.Interval = 300000;
            messageBoxTimer.Tick += MessageBoxTimerTick;
            messageBoxTimer.Start();

            // Event handlers
            this.Load += FrmMainLoad;
            this.FormClosing += FrmMainFormClosing;
            ntfIcon.DoubleClick += OpenApplication;
            btnAddTask.Click += BtnAddTaskClick;
            btnCompleteTask.Click += BtnCompleteTaskClick;
        }

        private void BtnAddTaskClick(object sender, EventArgs e)
        {
            TaskItem newTask = new TaskItem(txbTitle.Text, (Priority)cbxPriority.SelectedItem, dtpDueTime.Value);

            if (!tasks.Any(task =>
                task.title == newTask.title &&
                task.dueTime == newTask.dueTime)
            )
            {
                tasks.Add(new TaskItem(txbTitle.Text, (Priority)cbxPriority.SelectedItem, dtpDueTime.Value));
            }
            else
            {
                MessageBox.Show("Task with the same title and due time already exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCompleteTaskClick(object sender, EventArgs e)
        {
            var selectedTasks = lbxTasks.SelectedItems.Cast<TaskItem>().ToList();

            if (selectedTasks.Any())
            {
                foreach (var task in selectedTasks)
                {
                    int xpAwarded = task.priority switch
                    {
                        Priority.Low => 10,
                        Priority.Medium => 15,
                        Priority.High => 25,
                        _ => 0
                    };

                    excessXP += xpAwarded;

                    while (excessXP >= xpNeeded)
                    {
                        excessXP -= xpNeeded;
                        currentLevel++;
                        xpNeeded = GetXPForNextLevel(currentLevel);
                    }

                    tasks.Remove(task);
                }

                currentXP = excessXP;

                RefreshLevelControls();
                ShowNotification("Task Completed!", "Congratulations! You've earned XP for completing a task.");
            }
            else
            {
                MessageBox.Show("Please select a task to complete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void FrmMainFormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.WindowState = FormWindowState.Minimized;
                this.ShowInTaskbar = false;
                ntfIcon.Visible = true;
            }
            else
            {
                SaveData();
            }
        }

        private void OpenApplication(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
            ntfIcon.Visible = false;
        }

        private void FrmMainLoad(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
            ntfIcon.Visible = true;
        }

        private void ExitApplication(object sender, EventArgs e)
        {
            SaveData();

            ntfIcon.Visible = false;
            Application.Exit();
        }

        private void RefreshLevelControls()
        {
            lblLevel.Text = $"Level {currentLevel} | {currentXP}/{xpNeeded} XP";
            pnlLvlProgress.Width = (int)((double)currentXP / xpNeeded * MAX_PANEL_WIDTH);

            if (currentLevel >= 20)
            {
                randomSoundTimer.Stop();
            }
            if (currentLevel >= 16)
            {
                notificationTimer.Stop();
            }
            if (currentLevel >= 8)
            {
                messageBoxTimer.Stop();
            }
        }

        private int GetXPForNextLevel(int currentLevel)
        {
            return (int)(BASE_XP * Math.Pow(GROWTH_RATE, currentLevel - 1));
        }

        private void ShowNotification(string title, string message)
        {
            ntfIcon.BalloonTipTitle = title;
            ntfIcon.BalloonTipText = message;
            ntfIcon.BalloonTipIcon = ToolTipIcon.Info;
            ntfIcon.ShowBalloonTip(3000);
        }

        private void RandomSoundTimerTick(object sender, EventArgs e)
        {
            if (tasks.Where(task => task.dueTime >= DateTime.Now && task.dueTime <= DateTime.Now.AddHours(24)).Any())
            {
                PlaySound();
            }
        }

        private void PlaySound()
        {
            System.Media.SystemSounds.Beep.Play();
        }

        private void NotificationTimerTick(object sender, EventArgs e)
        {
            var upcomingTasks = tasks.Where(task => task.dueTime >= DateTime.Now && task.dueTime <= DateTime.Now.AddHours(24)).ToList();
            if (upcomingTasks.Any())
            {
                var task = upcomingTasks.OrderBy(t => t.dueTime).First(); // Get the soonest task
                ShowNotification("Upcoming Task", $"You have an upcoming task: {task.title} due in less than 24 hours.");
            }
        }

        private void MessageBoxTimerTick(object sender, EventArgs e)
        {
            var upcomingTasks = tasks.Where(task => task.dueTime >= DateTime.Now && task.dueTime <= DateTime.Now.AddHours(24)).ToList();
            if (upcomingTasks.Any())
            {
                var task = upcomingTasks.OrderBy(t => t.dueTime).First(); // Get the soonest task
                MessageBox.Show($"Reminder: You have a task due in less than 24 hours: {task.title}", "Task Reminder", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void SaveData()
        {
            var savedData = new SaveData
            {
                CurrentLevel = currentLevel,
                CurrentXP = currentXP,
                Tasks = tasks.ToList()
            };

            var json = JsonSerializer.Serialize(savedData);
            File.WriteAllText(SAVE_FILE_PATH, json);
        }

        private void LoadData()
        {
            if (File.Exists(SAVE_FILE_PATH))
            {
                try
                {
                    var json = File.ReadAllText(SAVE_FILE_PATH);
                    var savedData = JsonSerializer.Deserialize<SaveData>(json);

                    if (savedData != null)
                    {
                        currentLevel = savedData.CurrentLevel;
                        currentXP = savedData.CurrentXP;
                        tasks = new BindingList<TaskItem>(savedData.Tasks);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
