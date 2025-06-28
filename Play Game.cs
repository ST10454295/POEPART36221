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
    public partial class Play_Game : Form
    {
        private int currentQuestion = 0;
        private int score = 0;

        private List<Question> questions;

        public Play_Game()
        {
            InitializeComponent();
            LoadQuestions();
            DisplayQuestion();
        }
        private void Next_Click(object sender, EventArgs e)
        {
            Play_Game playgame = new Play_Game();
            playgame.Show();
        }

        private void Quit_Click(object sender, EventArgs e)
        {
            Play_Game playgame = new Play_Game();
            playgame.Show();
        }

        private void LoadQuestions()
        {
            questions = new List<Question>
            {
                new Question("What is the primary goal of a firewall?",
                new string [] {"A) To block unauthorized access\r\n","B) To allow all traffic\r\n","C) To encrypt data\r\n","D) To scan for viruses"},1),

                new Question("What is phishing?",
                new string [] {"A) A type of malware\r\n","B) A type of social engineering attack\r\n","C) A type of network attack\r\n","D) A type of password attack "}, 2),

                new Question("What is a virus?",
                new string [] {"A) A type of malware that replicates itself\r\n","B) A type of malware that steals data\r\n","C) A type of malware that encrypts data\r\n","D) A type of malware that deletes data"}, 1),

                new Question("What is encryption?",
                new string [] {"A) The process of converting plaintext to ciphertext\r\n","B) The process of converting ciphertext to plaintext\r\n","C) The process of deleting data\r\n","D) The process of encrypting data twice"}, 1),

                new Question("What is a password manager?",
                new string [] {"A) A tool that generates complex passwords\r\n","B) A tool that stores passwords securely\r\n","C) A tool that cracks passwords\r\n","D) A tool that deletes passwords"},4 ),

                new Question("What is a denial-of-service (DoS) attack?",
                new string [] {"A) A type of attack that floods a system with traffic\r\n","B) A type of attack that steals data\r\n","C) A type of attack that encrypts data\r\n","D) A type of attack that deletes data"}, 3),

                new Question("What is a Trojan horse?",
                new string [] {"A) A type of malware that disguises itself as a legitimate program\r\n","B) A type of malware that replicates itself\r\n","C) A type of malware that steals data\r\n","D) A type of malware that encrypts data"}, 3),

                new Question("What is two-factor authentication (2FA)?",
                new string [] {"A) A security process that requires one form of verification\r\n","B) A security process that requires two forms of verification\r\n","C) A security process that requires three forms of verification\r\n","D) A security process that requires no verification"}, 1),

                new Question("What is a patch?",
                new string [] {"A) A software update that fixes a security vulnerability\r\n","B) A software update that adds new features\r\n","C) A software update that deletes data\r\n","D) A software update that encrypts data"}, 2),

                new Question("What is a threat actor?",
                new string [] {"A) An individual or group that poses a threat to security\r\n","B) An individual or group that helps with security\r\n","C) An individual or group that is neutral to security\r\n","D) An individual or group that is unknown to security"}, 4),

            };

        }

        private void DisplayQuestion()
        {
            if (currentQuestion >= questions.Count)
            {
                MessageBox.Show($"Quiz Completed!\nYour Score: {score}/{questions.Count}", "Result");
                button1.Enabled = false;
                return;
            }

            var q = questions[currentQuestion];
            label1.Text = q.Text;

            radioButton1.Text = q.Options[0];
            radioButton2.Text = q.Options[1];
            radioButton3.Text = q.Options[2];
            radioButton4.Text = q.Options[3];

            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            radioButton4.Checked = false;

        }
        
      

        private void button1_Click_1(object sender, EventArgs e)
        {
            int selected = -1;
            if (radioButton1.Checked) selected = 0;
            else if (radioButton2.Checked) selected = 1;
            else if (radioButton3.Checked) selected = 2;
            else if (radioButton4.Checked) selected = 3;

            if (selected == -1)
            {
                MessageBox.Show("please select an answer");
                return;
            }

            if (selected == questions[currentQuestion].CorrectIndex)
            {
                score++;
            }

            currentQuestion++;
            DisplayQuestion();


        }

        public class Question
        {
            public string Text;
            public string[] Options;
            public int CorrectIndex;

            public Question(string text, string[] options, int correctIndex)
            {
                Text = text;
                Options = options;
                CorrectIndex = correctIndex;
            }
        }
    }

}
