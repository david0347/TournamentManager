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

            //Dynamic list to add gamers
            List<Gamer> allEntries = new List<Gamer>();

            //Open connection and read from the database
            connection.Open();
            SqlCommand getAllGamers = new SqlCommand(getAllString, connection);

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

        //Function to display all check boxes on left hand side of the screen
        //In retrospect, I should have created a displayCheckBoxes class because I need this implementation twice
        private void displayAllCheckBoxes()
        {
            List<Gamer> allGamers = getAllGamers();
            //Starting position on the form
            int top = 130;
            int left = 30;
            
            //Add checkBoxes for each gamer
            for(int i = 0; i< allGamers.Count; i++)
            {
                checkBoxes.Add(addCheckBox(allGamers[i]));
            }
            
            //Display checkboxes
            for(int j = 0; j < checkBoxes.Count; j++)
            {
                checkBoxes[j].Location = new Point(left, top);
                this.Controls.Add(checkBoxes[j]);
                top += 25;
            }
        }

        //Function to create a basic checkbox
        private CheckBox addCheckBox(Gamer gamer)
        {
            CheckBox chkGamers = new CheckBox();
            chkGamers.Text = gamer.getGamerTag();
            chkGamers.AutoSize = true;

            return chkGamers;
        }

        //Function to check if there are 8 gamers selected, and if so put them into the bracket
        private void btnConfirmGamers_Click(object sender, EventArgs e)
        {
            List<String> allGamers = new List<String>();
            allGamers = gamersPlaying(checkBoxes);

            //If count == 8 make the confirmGamers button not clickable with the checkboxes
            //Then set each player in the bracket
            if(allGamers.Count == 8)
            {
                btnConfirmGamers.Enabled = false;
                
                for(int i = 0; i < checkBoxes.Count; i++)
                {
                    checkBoxes[i].Enabled = false;
                }

                //Set text and enable all the first round buttons
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
            //Else display error message
            else
            {
                MessageBox.Show("Select 8 Players to Play. Currently " + allGamers.Count.ToString() + " Players Selected");
            }
        }

        //Return a list of type string with gamer names to be put into checkBoxes
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

            return gamersInTournament;
        }

        //Function to select a winner, change colors of buttons and enable buttons
        //Also updates the win/loss columns in the database with other function calls
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

        //Function to add a win to the gamer's win column
        private void addWinToGamer(string gamerTag)
        {

            Int32 wins = 0;
            //Variables to set up the Sql operation to read and then add one to wins
            SqlConnection connection = new SqlConnection(ConnectionString);
            string getWinQuery = "SELECT Wins FROM PlayerDB WHERE GamerTag = @gamerTag";
            string updateWinQuery = "UPDATE PlayerDB set Wins = @wins WHERE GamerTag = @gamerTag";
            

            //Open connection, set wins equal to the wins of the gamer
            connection.Open();
            SqlCommand getWinsCommand = new SqlCommand(getWinQuery, connection);
            SqlCommand updateWinsCommand = new SqlCommand(updateWinQuery, connection);

            getWinsCommand.Parameters.AddWithValue("@gamerTag", gamerTag);
            updateWinsCommand.Parameters.AddWithValue("@gamerTag", gamerTag);
            wins = (Int32)getWinsCommand.ExecuteScalar();

            //Add 1 to gamer and update the database
            wins++;
            updateWinsCommand.Parameters.AddWithValue("@wins", wins);
            updateWinsCommand.ExecuteNonQuery();
            connection.Close();
        }

        //Function to add a loss to the gamer's loss column
        private void addLossToGamer(string gamerTag)
        {

            Int32 losses = 0;
            //Variables for losses
            SqlConnection connection = new SqlConnection(ConnectionString);
            

            //Open connection, read the losses, add 1 and update the database
            connection.Open();
            string getLossQuery = "SELECT Losses FROM PlayerDB WHERE GamerTag = @gamerTag";
            string updateLossQuery = "UPDATE PlayerDB set Losses = @losses WHERE GamerTag = @gamerTag";
            SqlCommand getLossCommand = new SqlCommand(getLossQuery, connection);
            SqlCommand updateLossCommand = new SqlCommand(updateLossQuery, connection);

            getLossCommand.Parameters.AddWithValue("@gamerTag", gamerTag);
            updateLossCommand.Parameters.AddWithValue("@gamerTag", gamerTag);
            losses = (Int32)getLossCommand.ExecuteScalar();
            losses++;

            updateLossCommand.Parameters.AddWithValue("@losses", losses);
            updateLossCommand.ExecuteNonQuery();
            connection.Close();
        }

        //Function to disable buttons to save a little bit of code
        private void disableButtons(Button btn1, Button btn2)
        {
            btn1.Enabled = false;
            btn2.Enabled = false;
        }

        //Function to get called on all round 1 buttons when clicked. Updates the database, moves people
        //to the next bracket, changes colors, disables, and enables buttons
        private void startingButtonClicked(Button winnerBtn, Button loserBtn, Button nextBtn)
        {
            winnerSelected(winnerBtn, loserBtn);
            nextBtn.Text = winnerBtn.Text;
            disableButtons(winnerBtn, loserBtn);
            nextBtn.Enabled = true;
        }

        //Round 2 version of the function above
        private void round2ButtonClicked(Button winnerBtn, Button loserBtn, Button nextBtn)
        {
            if (loserBtn.Text != string.Empty)
            {
                winnerSelected(winnerBtn, loserBtn);
                nextBtn.Text = winnerBtn.Text;
                disableButtons(winnerBtn, loserBtn);
                nextBtn.Enabled = true;
            }
            else
            {
                MessageBox.Show("Error: 2 gamers need to be in the bracket");
            }
        }

        //Round 3 version of the function above
        private void round3ButtonClicked(Button winnerBtn, Button loserBtn, Label winnerLbl)
        {
            if (loserBtn.Text != string.Empty)
            {
                winnerSelected(winnerBtn, loserBtn);
                winnerLbl.Text = winnerBtn.Text;
                disableButtons(winnerBtn, loserBtn);
            }
            else
            {
                MessageBox.Show("Error: 2 gamers need to be in the bracket");
            }
        }

        //All button on click functions that call the 3 above functions
        private void btnPlayer1_Click(object sender, EventArgs e)
        {
            startingButtonClicked(btnPlayer1, btnPlayer2, btn1Round2);
        }

        private void btnPlayer2_Click(object sender, EventArgs e)
        {
            startingButtonClicked(btnPlayer2, btnPlayer1, btn1Round2);
        }

        private void btnPlayer3_Click(object sender, EventArgs e)
        {
            startingButtonClicked(btnPlayer3, btnPlayer4, btn2Round2);
        }

        private void btnPlayer4_Click(object sender, EventArgs e)
        {
            startingButtonClicked(btnPlayer4, btnPlayer3, btn2Round2);
        }

        private void btnPlayer5_Click(object sender, EventArgs e)
        {
            startingButtonClicked(btnPlayer5, btnPlayer6, btn3Round2);
        }

        private void btnPlayer6_Click(object sender, EventArgs e)
        {
            startingButtonClicked(btnPlayer6, btnPlayer5, btn3Round2);
        }

        private void btnPlayer7_Click(object sender, EventArgs e)
        {
            startingButtonClicked(btnPlayer7, btnPlayer8, btn4Round2);
        }

        private void btnPlayer8_Click(object sender, EventArgs e)
        {
            startingButtonClicked(btnPlayer8, btnPlayer7, btn4Round2);
        }

        private void btn1Round2_Click(object sender, EventArgs e)
        {
            round2ButtonClicked(btn1Round2, btn2Round2, btn1Round3);
        }

        private void btn2Round2_Click(object sender, EventArgs e)
        {
            round2ButtonClicked(btn2Round2, btn1Round2, btn1Round3);
        }

        private void btn3Round2_Click(object sender, EventArgs e)
        {
            round2ButtonClicked(btn3Round2, btn4Round2, btn2Round3);
        }

        private void btn4Round2_Click(object sender, EventArgs e)
        {
            round2ButtonClicked(btn4Round2, btn3Round2, btn2Round3);
        }

        private void btn1Round3_Click(object sender, EventArgs e)
        {
            round3ButtonClicked(btn1Round3, btn2Round3, lblWinner);
        }

        private void btn2Round3_Click(object sender, EventArgs e)
        {
            round3ButtonClicked(btn2Round3, btn1Round3, lblWinner);
        }
    }
}
