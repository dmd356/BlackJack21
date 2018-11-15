using System;
namespace Lab_2_CIS_247C
{
    public class Bot : Player
    {
        private string[] botNames = {
            "Optimus Prime", "Zuuulll", "Mario", "Artom", "Ariel", "Alen/Alan/Allan/Allen", "Rory",
            "The ONE", "Master Iron Chef", "Iron Maiden", "AlphaBetaCharlie",
            "Snowflake", "Private Joker", "Sierra-117", "Charlie Day", "GhostInTheMachine",
            "Jiminy Cricket", "Yabda", "HelpMeCall911"
        };

        public Bot() : base(0,"")
        {
            Random rng = new Random();
            int rngNum = rng.Next(0, botNames.Length);
            userName = botNames[rngNum];
            while(true){
                rngNum = rng.Next(10, 80);
                if(rngNum%5==0){
                    this.money = rngNum;
                    break;
                }
            }
        }
    }
}
