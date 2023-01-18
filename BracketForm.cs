using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;
using System.Windows.Forms;

namespace TournamentManager
{
    public partial class BracketForm : Form
    {

        string ConnectionString = "Data Source = DAVID\\SQLEXPRESS; AttachDbFilename = C:\\test\\DavidsDB.mdf; Integrated Security = True; Connect Timeout = 30; User Instance = True";
        List<CheckBox> checkBoxes = new List<CheckBox>();


        public BracketForm()
        {
            InitializeComponent();
            
            displayAllCheckBoxes();
        }

        //Get all gamers
        private List<Gamer> getAllGamers()
        {
            //Query to get all the players from the database
            string getAllString = "SELECT * FROM PlayerDB";

            SqlConnection connection = new SqlConnection(ConnectionString);

            SqlCommand getAllGamers = new SqlCommand(getAllString, connection);
            //Dynamic list to add gamers
            List<Gamer> allEntries = new List<Gamer>();

            //Open connection and read from the database
            connection.Open();
            //the sqlReader is needed to go through all the data
            using (SqlDataReader reader = getAllGamers.ExecuteReader())
            {
                //While reader is still reading data base add a new gamer to the gamer list
                while (reader.Read())
                {
                    allEntries.Add(new Gamer(reader["Playername"].ToString(), reader["GamerTag"].ToString()));
                }
            }
            //Return list
            return allEntries;
        }

        private void displayAllCheckBoxes()
        {
            List<Gamer> allGamers = getAllGamers();
            int top = 130;
            int left = 30;

            for(int i = 0; i< allGamers.Count; i++)
            {
                checkBoxes.Add(addCheckBox(allGamers[i]));
            }
            
            for(int j = 0; j < checkBoxes.Count; j++)
            {
                checkBoxes[j].Location = new Point(left, top);
                this.Controls.Add(checkBoxes[j]);
                top += 25;
            }
        }

        private CheckBox addCheckBox(Gamer gamer)
        {
            CheckBox chkGamers = new CheckBox();
            chkGamers.Text = gamer.getGamerTag();
            chkGamers.AutoSize = true;

            return chkGamers;
        }


        private void btnConfirmGamers_Click(object sender, EventArgs e)
        {
            List<String> allGamers = new List<String>();
            allGamers = gamersPlaying(checkBoxes);

            if(allGamers.Count == 8)
            {
                btnConfirmGamers.Enabled = false;
                
                for(int i = 0; i < checkBoxes.Count; i++)
                {
                    checkBoxes[i].Enabled = false;
                }

                btnPlayer1.Text = allGamers[0];
                btnPlayer2.Text = allGamers[1];
                btnPlayer3.Text = allGamers[2];
                btnPlayer4.Text = allGamers[3];
                btnPlayer5.Text = allGamers[4];
                btnPlayer6.Text = allGamers[5];
                btnPlayer7.Text = allGamers[6];
                btnPlayer8.Text = allGamers[7];

                btnPlayer1.Enabled = true;
                btnPlayer2.Enabled = true;
                btnPlayer3.Enabled = true;
                btnPlayer4.Enabled = true;
                btnPlayer5.Enabled = true;
                btnPlayer6.Enabled = true;
                btnPlayer7.Enabled = true;
                btnPlayer8.Enabled = true;
            }
            else
            {
                MessageBox.Show("Select 8 Players to Play. Currently " + allGamers.Count.ToString() + " Players Selected");
            }
        }

        private List<String> gamersPlaying(List<CheckBox> checkBoxes)
        {
            List<String> gamersInTournament= new List<String>();

            for(int i = 0; i < checkBoxes.Count; i++)
            {
                if (checkBoxes[i].Checked == true)
                {
                    gamersInTournament.Add(checkBoxes[i].Text);
                }
            }

            return gamersInTournament ;
        }

        private string winnerSelected(Button winner, Button loser)
        {
            winner.BackColor = Color.Green;
            loser.BackColor = Color.Red;

            winner.Enabled = false;
            loser.Enabled = false;

            //Function to add 1 to winner gamer
            addWinToGamer(winner.Text);
            //Function to add 1 to loser gamer
            addLossToGamer(loser.Text);

            return winner.Text;
        }

        private void addWinToGamer(string gamerTag)
        {

            Int32 wins = 0;
            //Variables to set up the Sql operation to read and then add one to wins
            SqlConnection connection = new SqlConnection(ConnectionString);
            string getWinQuery = "SELECT Wins FROM PlayerDB WHERE GamerTag = @gamerTag";
            string updateWinQuery = "UPDATE PlayerDB set Wins = @wins WHERE GamerTag = @gamerTag";
            SqlCommand getWinsCommand = new SqlCommand(getWinQuery, connection);
            SqlCommand updateWinsCommand = new SqlCommand(updateWinQuery, connection);

            getWinsCommand.Parameters.AddWithValue("@gamerTag", gamerTag);
            updateWinsCommand.Parameters.AddWithValue("@gamerTag", gamerTag);

            //Open connection, set wins equal to the wins of the gamer
            connection.Open();
            wins = (Int32)getWinsCommand.ExecuteScalar();

            //Add 1 to gamer and update the database
            wins++;
            updateWinsCommand.Parameters.AddWithValue("@wins", wins);
            updateWinsCommand.ExecuteNonQuery();
            connection.Close();
        }

        private void addLossToGamer(string gamerTag)
        {

            Int32 losses = 0;
            //Variables for losses
            SqlConnection connection = new SqlConnection(ConnectionString);
            string getLossQuery = "SELECT Losses FROM PlayerDB WHERE GamerTag = @gamerTag";
            string updateLossQuery = "UPDATE PlayerDB set Losses = @losses WHERE GamerTag = @gamerTag";
            SqlCommand getLossCommand = new SqlCommand(getLossQuery, connection);
            SqlCommand updateLossCommand = new SqlCommand(updateLossQuery, connection);

            getLossCommand.Parameters.AddWithValue("@gamerTag", gamerTag);
            updateLossCommand.Parameters.AddWithValue("@gamerTag", gamerTag);

            //Open connection, read the losses, add 1 and update the database
            connection.Open();
            losses = (Int32)getLossCommand.ExecuteScalar();
            losses++;

            updateLossCommand.Parameters.AddWithValue("@losses", losses);
            updateLossCommand.ExecuteNonQuery();
            connection.Close();
        }

        private void disableButtons(Button btn1, Button btn2)
        {
            btn1.Enabled = false;
            btn2.Enabled = false;
        }

        private void btnPlayer1_Click(object sender, EventArgs e)
        {
            winnerSelected(btnPlayer1, btnPlayer2);
            btn1Round2.Text = btnPlayer1.Text;
            disableButtons(btnPlayer1, btnPlayer2);
            btn1Round2.Enabled = true;
        }

        private void btnPlayer2_Click(object sender, EventArgs e)
        {
            winnerSelected(btnPlayer2, btnPlayer1);
            btn1Round2.Text = btnPlayer2.Text;
            disableButtons(btnPlayer1, btnPlayer2);
            btn1Round2.Enabled = true;

        }

        private void btnPlayer3_Click(object sender, EventArgs e)
        {
            winnerSelected(btnPlayer3, btnPlayer4);
            btn2Round2.Text = btnPlayer3.Text;
            disableButtons(btnPlayer3, btnPlayer4);
            btn2Round2.Enabled = true;

        }

        private void btnPlayer4_Click(object sender, EventArgs e)
        {
            winnerSelected(btnPlayer4, btnPlayer3);
            btn2Round2.Text = btnPlayer4.Text;
            disableButtons(btnPlayer4, btnPlayer3);
            btn2Round2.Enabled = true;

        }

        private void btnPlayer5_Click(object sender, EventArgs e)
        {
            winnerSelected(btnPlayer5, btnPlayer6);
            btn3Round2.Text = btnPlayer5.Text;
            disableButtons(btnPlayer5, btnPlayer6);
            btn3Round2.Enabled = true;
        
        }

        private void btnPlayer6_Click(object sender, EventArgs e)
        {
            winnerSelected(btnPlayer6, btnPlayer5);
            btn3Round2.Text = btnPlayer6.Text;
            disableButtons(btnPlayer6, btnPlayer5);
            btn3Round2.Enabled = true;
        }

        private void btnPlayer7_Click(object sender, EventArgs e)
        {
            winnerSelected(btnPlayer7, btnPlayer8);
            btn4Round2.Text = btnPlayer7.Text;
            disableButtons(btnPlayer7, btnPlayer8);
            btn4Round2.Enabled = true;
        }

        private void btnPlayer8_Click(object sender, EventArgs e)
        {
            winnerSelected(btnPlayer8, btnPlayer7);
            btn4Round2.Text=btnPlayer8.Text;
            disableButtons(btnPlayer8, btnPlayer7);
            btn4Round2.Enabled = true;
        }

        private void btn1Round2_Click(object sender, EventArgs e)
        {
            if(btn2Round2.Text != string.Empty)
            {
                winnerSelected(btn1Round2, btn2Round2);
                btn1Round3.Text = btn1Round2.Text;
                disableButtons(btn1Round2, btn2Round2);
                btn1Round3.Enabled = true;
            }
            else
            {
                MessageBox.Show("Error: 2 gamers need to be in the bracket");
            }
        }

        private void btn2Round2_Click(object sender, EventArgs e)
        {
            if(btn1Round2.Text != string.Empty)
            {
                winnerSelected(btn2Round2, btn1Round2);
                btn1Round3.Text = btn2Round2.Text;
                disableButtons(btn2Round2, btn1Round2);
                btn1Round3.Enabled = true;
            }
            else
            {
                MessageBox.Show("Error: 2 gamers need to be in the bracket");
            }
        }

        private void btn3Round2_Click(object sender, EventArgs e)
        {
            if(btn4Round2.Text != string.Empty)
            {
                winnerSelected(btn3Round2, btn4Round2);
                btn2Round3.Text = btn3Round2.Text;
                disableButtons(btn3Round2, btn4Round2);
                btn2Round3.Enabled = true;
            }
            else
            {
                MessageBox.Show("Error: 2 gamers need to be in the bracket");
            }
        }

        private void btn4Round2_Click(object sender, EventArgs e)
        {
            if(btn3Round2.Text!= string.Empty)
            {
                winnerSelected(btn4Round2, btn3Round2);
                btn2Round3.Text = btn4Round2.Text;
                disableButtons(btn4Round2, btn3Round2);
                btn2Round3.Enabled=true;
            }
            else
            {
                MessageBox.Show("Error: 2 gamers need to be in the bracket");
            }
        }

        private void btn1Round3_Click(object sender, EventArgs e)
        {
            if(btn2Round3.Text != string.Empty)
            {
                winnerSelected(btn1Round3, btn2Round3);
                lblWinner.Text = btn1Round3.Text;
                disableButtons(btn1Round3, btn2Round3);
            }
            else 
            {
                MessageBox.Show("Error: 2 gamers need to be in the bracket");
            }
        }

        private void btn2Round3_Click(object sender, EventArgs e)
        {
            if(btn1Round3.Text != string.Empty)
            {
                winnerSelected(btn2Round3, btn1Round3);
                lblWinner.Text = btn2Round3.Text;
                disableButtons(btn2Round3, btn1Round3);
            }
            else
            {
                MessageBox.Show("Error: 2 gamers need to be in the bracket");
            }
        }
    }
}
