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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        
        private void Button1_Click(object sender, EventArgs e)
        {
            Add_Tasks AddTasks = new Add_Tasks();//Create an instance of Add Tasks
            AddTasks.Show();//Show Add Tasks
         
        }

        

        private void openPlayGame_Click(object sender, EventArgs e)
        {
            Play_Game PlayGame = new Play_Game();//Create an instance of Play Game
            PlayGame.Show();//Show Play_Game 
        }

        private void openChatBot_Click(object sender, EventArgs e)
        {


            MessageBox.Show("This feature is not implemented yet.");
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}