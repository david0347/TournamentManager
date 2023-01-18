using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TournamentManager
{
    /*
     * This class is used to easily deal with the all the gamers,
     * it includes simple setters and getters as well as an empty
     * constructor, and a populated constructor
     */
    internal class Gamer
    {
        //Variables
        string userName;
        string gamerTag;
        int wins = 0;
        int losses = 0;
        
        //Constructor with parameters
        public Gamer() {
            userName = string.Empty;
            gamerTag = string.Empty;
        }

        //Constructor with parameters
        public Gamer(string userName, string gamerTag)
        {
            this.userName = userName;
            this.gamerTag = gamerTag;
        }

        //Setters and Getters
        public void setUserName(string userName)
        {
            this.userName = userName;
        }

        public string getUserName()
        {
            return this.userName;
        }

        public void setGamerTag(string gamerTag)
        {
            this.gamerTag = gamerTag;
        }

        public string getGamerTag()
        {
            return this.gamerTag;
        }

        public void setWins(int wins)
        {
            this.wins = wins;
        }

        public int getWins()
        {
            return this.wins;
        }

        public void setLosses(int losses)
        {
            this.losses = losses;
        }

        public int getLosses()
        {
            return this.losses;
        }
    }
}
