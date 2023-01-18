using Microsoft.VisualBasic.ApplicationServices;
using static System.Net.Mime.MediaTypeNames;
using System;
using System.Data.SqlClient;
using System.Net.NetworkInformation;
using System.Reflection.Metadata.Ecma335;

namespace TournamentManager
{
    public partial class Form1 : Form
    {

        //Variable to connect to DB
        string ConnectionString = "Data Source = DAVID\\SQLEXPRESS; AttachDbFilename = C:\\test\\DavidsDB.mdf; Integrated Security = True; Connect Timeout = 30; User Instance = True";

        //Global variables
        int counter = 0;
        int top = 130;
        int left = 400;
        List<CheckBox> checkBoxes = new List<CheckBox>();
        List<Gamer> gamers = new List<Gamer>();


        //Form that loads initially
        public Form1()
        {
            InitializeComponent();
            
            //Gets a list of all the gamers from the database
            gamers = getAllGamers();

            //Loops through all the gamers and displays a checkbox
            for (int i = 0; i < gamers.Count; i++)
            {
              
                //Add a check box to the list, counter increments so when user adds to the list later
                //it can be tracked, set the new checkbox position below the last
                checkBoxes.Add(addCheckBox(gamers[i]));
                counter++;
                checkBoxes[i].Location = new System.Drawing.Point(left, top);

                this.Controls.Add(checkBoxes[i]);
                //Increment so next time a new check box is added it will be visable
                top += 25;
                
            }

        }

        private void btnAddGamer_Click(object sender, EventArgs e)
        {

            //Create a new gamer with info inside of text boxes
            Gamer newGamer = new Gamer(txtUsername.Text, txtGamerTag.Text);

            //Check to see if data is correct and if the user does not already exist
            if (checkUserName(newGamer))
            {

                //This variable connects us to the database using the global var connectionString
                SqlConnection connection = new SqlConnection(ConnectionString);

                //Open connection
                connection.Open();

                //The query I want to use to save users to the database
                string query = "INSERT INTO PlayerDB (Playername, GamerTag, Wins, Losses) VALUES ('" + txtUsername.Text + "', '" + txtGamerTag.Text + "',0,0)";

                //Command variable to exexcute query
                SqlCommand command = new SqlCommand(query, connection);
                //Execute query and close the connection
                command.ExecuteNonQuery();
                connection.Close();

                //Add a checkbox to the list with the new gamer
                gamers.Add(newGamer);
                checkBoxes.Add(addCheckBox(newGamer));

                //Display the new gamer on the form
                checkBoxes[counter].Location = new System.Drawing.Point(left, top);
                this.Controls.Add(checkBoxes[counter]);
                top += 25;
                counter++;
            }

            //Clear the fields
            txtUsername.Text = String.Empty;
            txtGamerTag.Text = String.Empty;
        }

        private void btnTournamentScreen_Click(object sender, EventArgs e)
        {
            //New bracket instance
            BracketForm bracketForm = new BracketForm();

            //Hide current bracket, show bracketForm
            this.Hide();
            bracketForm.Show();
        }
        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            //Query to delete everyone from the database
            string query = "DELETE FROM PlayerDB";

            //Sql connection and command
            SqlConnection connection = new SqlConnection(ConnectionString);
            SqlCommand deleteAllGamers = new SqlCommand(query, connection);

            //Loop through and delete the check boxes from the screen
            for(int i = 0; i < checkBoxes.Count; i++)
            {
                checkBoxes[i].Checked = false;
                this.Controls.Remove(checkBoxes[i]);
            }
            
            //Actual code to delete the whole database
            connection.Open();
            deleteAllGamers.ExecuteNonQuery();
            connection.Close();
        }

        //So far this function deletes the checkbox from the screen, but did not get the sql function working yet
        //Getting an error when I run it.
        private void btnDeleteChecked_Click(object sender, EventArgs e)
        {
            //Loop through all the checkboxes
            for(int i = 0; i < checkBoxes.Count; i++)
            {
                //If the check box is checked then delete the checkbox, set the check to false,
                //and eventually delete it from the database
                if (checkBoxes[i].Checked)
                {
                    //Remove the check boxes from the screen and check to false
                    this.Controls.Remove(checkBoxes[i]);
                    checkBoxes[i].Checked = false;

                    //Query to delete players where they have the same id as the gamer tag
                    string query = "DELETE FROM PlayerDB WHERE GamerTag = @GamerTag";

                    //Sql commands to run the SQL script
                    SqlConnection connection = new SqlConnection(ConnectionString);
                    SqlCommand deleteGamer = new SqlCommand(query, connection);
                    deleteGamer.Parameters.AddWithValue("@GamerTag", gamers[i].getGamerTag());

                    connection.Open();
                    deleteGamer.ExecuteNonQuery();
                    connection.Close();
                }
            }

        }

        //Function to check if the username meets standards
        private bool checkUserName(Gamer gamer)
        {
            string userName = gamer.getUserName();
            string gamerTag = gamer.getGamerTag();
            //Note with gamerCount: It needs to be Int32 to match the datatype of the SQL database
            Int32 gamerCount = 0;

            //SQL query to count the number of gamers to make sure there are no duplicates
            string query = "SELECT COUNT(*) FROM PlayerDB WHERE GamerTag = @gamerTag";
            SqlConnection connection = new SqlConnection(ConnectionString);
            SqlCommand countGamer = new SqlCommand(query, connection);
            countGamer.Parameters.AddWithValue("@gamerTag", gamer.getGamerTag());

            connection.Open();
            //Needs to have (Int32) to work syntactically
            gamerCount = (Int32) countGamer.ExecuteScalar();
            connection.Close();

            //Check for empty Strings and return false
            if(userName == string.Empty || gamerTag == string.Empty){
                MessageBox.Show("Username or GamerTag is empty");
                return false;
            }
            //Check if the gamer already exists and return false
            else
            {
                if(gamerCount > 0)
                {
                    MessageBox.Show("Username is taken");

                    return false;
                }
                //If it passes both checks, return true
                return true;
            }
        }

        //Function to return a list of all gamers in the database
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
            using(SqlDataReader reader = getAllGamers.ExecuteReader())
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

        //Function to create a checkbox and return it for the gamers
        private CheckBox addCheckBox(Gamer gamer)
        {
            CheckBox chkGamers = new CheckBox();
            chkGamers.Text = "[Username: " + gamer.getUserName() + "]" + "  [Gamer Tag: " + gamer.getGamerTag() + "]";
            chkGamers.AutoSize = true;
            chkGamers.Tag = counter;

            return chkGamers;
        } 
    }
}