using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POEPART36221
{
    public partial class Add_Tasks : Form
    {
        private List<TaskItem> tasks = new List<TaskItem>();
        private string pendingTaskTitle = null;
        private string pendingTaskDescription = null;
        private TextBox input;
        private TextBox txtChat;

        public Add_Tasks()
        {
            InitializeComponent();

        }

        private void Send_Click(object sender, EventArgs e)
        {
            Play_Game myTask = new Play_Game();
            myTask.Show();

           
        }
            

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            string userInput = input.Text.Trim();
            if (string.IsNullOrEmpty(userInput)) return;

            AppendChat("user", userInput);
            HandleUserInput(userInput);
            input.Clear();
        }

        private void HandleUserInput(string inputText)
        {
            if (string.IsNullOrEmpty(inputText)) return;

            if (inputText.StartsWith("Add task -", StringComparison.OrdinalIgnoreCase))
            {
                pendingTaskTitle = inputText.Substring(10).Trim();
                pendingTaskDescription = $"Cybersecurity Task: {pendingTaskTitle}";
                AppendChat("Bot", $"Task added with the description \"{pendingTaskDescription}\". Would you like a reminder?");
            }
            else if (pendingTaskTitle != null && inputText.IndexOf("yes", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                int days = ExtractDays(inputText);
                DateTime? reminderDate = days > 0 ? DateTime.Now.AddDays(days) : (DateTime?)null;

                tasks.Add(new TaskItem
                {
                    Title = pendingTaskTitle,
                    Description = pendingTaskDescription,
                    ReminderDate = reminderDate,
                });

                AppendChat("Bot", days > 0
                   ? $"Got it! I'll remind you in {days} days(s)."
                    : "Task added without a reminder.");

                pendingTaskTitle = null;
                pendingTaskDescription = null;
            }
            else if (inputText.StartsWith("Show tasks", StringComparison.OrdinalIgnoreCase))
            {
                string filter = inputText.Length > 10
                   ? inputText.Substring(10).Trim().ToLower()
                    : "all";

                var filtered = tasks.AsEnumerable();

                if (filter == "completed")
                    filtered = filtered.Where(t => t.Completed);
                else if (filter == "pending")
                    filtered = filtered.Where(t => !t.Completed);

                if (!filtered.Any())
                {
                    AppendChat("Bot", "No tasks available for that filter.");
                    return;
                }

                string response = "Here are your tasks:\r\n";
                int i = 1;
                foreach (var t in filtered)
                {
                    response += $"{i++}. {t.Title} - {t.Description}"
                                + (t.ReminderDate.HasValue ? $" (Reminder: {t.ReminderDate.Value:d})" : "")
                                + (t.Completed ? "[Completed]" : "")
                                + "\r\n";
                }

                AppendChat("Bot", response);
            }
            else if (inputText.StartsWith("Delete", StringComparison.OrdinalIgnoreCase))
            {
                var parts = inputText.Split(' ');
                if (parts.Length >= 2 && int.TryParse(parts.Last(), out int d) && d >= 1 && d <= tasks.Count)
                {
                    tasks.RemoveAt(d - 1);
                    AppendChat("Bot", $"Task {d} deleted.");
                }
                else
                {
                    AppendChat("Bot", "Invalid task number.");
                }
            }
            else if (inputText.StartsWith("Complete", StringComparison.OrdinalIgnoreCase))
            {
                var parts = inputText.Split(' ');
                if (parts.Length >= 2 && int.TryParse(parts.Last(), out int c) && c >= 1 && c <= tasks.Count)
                {
                    tasks[c - 1].Completed = true;
                    AppendChat("Bot", $"Task {c} marked as completed.");
                }
                else
                {
                    AppendChat("Bot", "Invalid task number.");
                }
            }
            else if (inputText.Equals("Help", StringComparison.OrdinalIgnoreCase))
            {
                AppendChat("Bot", "Here are the commands you can use:\r\n" +
                    "_ Add task - [task name]\r\n" +
                    "_ Yes [optional: in X days]\r\n" +
                    "_ Show tasks [all|completed|pending]\r\n" +
                    "_ Delete [task number]\r\n" +
                    "_ Complete [task number]");
            }
            else
            {
                AppendChat("Bot", "Sorry, I didn't understand that. Try 'Add task-', 'how tasks', 'Delete 1', or 'Complete 1',");
            }
        }

        private int ExtractDays(string inputText)
        {
            var words = inputText.Split(' ');
            for (int i = 0; i < words.Length - 1; i++)
            {
                if (int.TryParse(words[i], out int n) && words[i + 1].ToLower().Contains("day"))
                {
                    return n;
                }
            }
            return 0;
        }

        private void AppendChat(string sender, string message)
        {
            if (txtChat != null)
            {
                txtChat.AppendText($"{sender}: {message}\r\n");
            }
        }
    }

    public class TaskItem
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? ReminderDate { get; set; }
        public bool Completed { get; set; } = false;
    }
}