using System.ComponentModel;
using System.Text.Json;
using Timer = System.Windows.Forms.Timer;

namespace TheNicestToDoList
{
    public partial class FrmMain : Form
    {
        private static Random rnd = new Random();
        BindingList<TaskItem> tasks = new BindingList<TaskItem>();
        private ContextMenuStrip cmsTray;

        private List<string> messages = new List<string>
        {
            "Tick-tock! Your laziness is showing. Do your task!",
            "Hurry up! That task isn’t going to do itself, you know!",
            "Stop procrastinating and get to work! Time’s running out!",
            "Are you seriously waiting until the last second? Do it now!",
            "You’re running out of time, you slacker! Get moving!",
            "I’m not joking! Your task is almost due! Get to it!",
            "Don’t make me remind you again! You’ve got a task to finish!",
            "You have one job. Don’t blow it now!",
            "C’mon! Stop being so lazy, your task is almost due!",
            "Waking up to this? Yep, your task is almost overdue. Do something about it!",
            "Seriously? You still haven’t done that task? Get a move on!",
            "You're on thin ice! Get your act together or regret it!",
            "You’re running out of time! What are you waiting for?",
            "Hurry up! Your task is about to slap you in the face!",
            "Stop being lazy! Your task is about to explode into your face!",
            "The clock’s ticking, and you’re just sitting there. Do something!",
            "Task due in less than 24 hours. Can we hurry up already?",
            "Tick-tock! You’re wasting precious time, don’t blow it!",
            "If you don’t start now, I’m going to keep bugging you until you do!",
            "You better not ignore this. The task is coming for you!"
        };

        private List<string> angryMessages = new List<string>
        {
            "Why aren't you working yet? Get off your butt and do it!",
            "I can't believe you're still procrastinating! Just do it already!",
            "Seriously? You’re wasting time when you should be working!",
            "You’re making me angry by not finishing your tasks!",
            "Are you really just sitting there? You have work to do!",
            "You think this is a game? Get to work before I lose it!",
            "Enough is enough! You need to focus and finish that task!",
            "Stop being lazy, your task won't complete itself!",
            "I can't keep waiting for you to get moving! Hurry up!",
            "The clock’s ticking, and you're still doing nothing! Get moving!",
            "Every second you waste is making me angrier! Get to work!",
            "I’m done with excuses. You need to start working right now!",
            "This procrastination has gone on long enough! Get back to work!",
            "You’ve been warned! Get off your chair and get to work!",
            "If I have to remind you one more time, you’ll regret it!",
            "I’m losing my patience! You better finish that task!",
            "You’re wasting time and I’m losing my mind! Get to work!",
            "Don’t make me remind you again! You have tasks to do!",
            "How many times do I have to tell you? Get your act together!",
            "You're really pushing my patience here. Do the task!"
        };

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
        private Timer dueTaskCheckTimer = new Timer();

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

            dueTaskCheckTimer.Interval = 5000;
            dueTaskCheckTimer.Tick += DueTaskCheckTimerTick;
            dueTaskCheckTimer.Start();

            // Event handlers
            //this.Load += FrmMainLoad;
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

            AdjustTimerIntervals();
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

            AdjustTimerIntervals();
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

        private TaskItem? GetMostUrgentTask()
        {
            return tasks
                .Where(task => task.dueTime > DateTime.Now && task.dueTime <= DateTime.Now.AddHours(24))
                .OrderBy(task => task.dueTime)
                .FirstOrDefault();
        }

        private void AdjustTimerIntervals()
        {
            TaskItem? urgentTask = GetMostUrgentTask();
            if (urgentTask == null)
            {
                randomSoundTimer.Interval = 30000;
                notificationTimer.Interval = 120000;
                messageBoxTimer.Interval = 300000;
                return;
            }

            TimeSpan timeLeft = urgentTask.dueTime - DateTime.Now;
            double factor = Math.Max(0.1, timeLeft.TotalHours / 24.0);

            randomSoundTimer.Interval = (int)(30000 * factor);
            notificationTimer.Interval = (int)(120000 * factor);
            messageBoxTimer.Interval = (int)(300000 * factor);
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
                var randomMessage = messages[rnd.Next(messages.Count)];
                ShowNotification("Upcoming Task", randomMessage);
            }
        }

        private void MessageBoxTimerTick(object sender, EventArgs e)
        {
            var upcomingTasks = tasks.Where(task => task.dueTime >= DateTime.Now && task.dueTime <= DateTime.Now.AddHours(24)).ToList();
            if (upcomingTasks.Any())
            {
                var randomMessage = messages[rnd.Next(messages.Count)];
                MessageBox.Show(randomMessage, "Task Reminder", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void DueTaskCheckTimerTick(object sender, EventArgs e)
        {
            var overdueTasks = tasks.Where(task => task.dueTime < DateTime.Now).ToList();

            foreach (var overdueTask in overdueTasks)
            {
                tasks.Remove(overdueTask);
            }

            if (overdueTasks.Any())
            {
                for (int i = 0; i < 5; i++)
                {
                    MessageBox.Show(angryMessages[rnd.Next(angryMessages.Count)], "Task Overdue!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            AdjustTimerIntervals();
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
