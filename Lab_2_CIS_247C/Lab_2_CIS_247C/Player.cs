using System;
namespace Lab_2_CIS_247C
{
    public class Player
    {
        protected int money;
        protected int playerHandValues;
        protected string userName;
        protected string[] playerHand;
        protected int handIndex;
        //==================================================================================
        public Player(int money, string userName) :base()
        {
            this.money = money;
            this.userName = userName;
            handIndex = 0;
            resetHand();
        }//==================================================================================
        public void setHand(string[] newHand) { playerHand = newHand; }
        public void resetHand() { playerHand = new string[12]; }
        public void takeMoney(int take){money-=take;}
        public void giveMoney(int give) { money += give; }
        public void updatePlayerHandValues(int playerHandValues){this.playerHandValues = playerHandValues;}
        public void setStartHandIndex(int handIndex) { this.handIndex = handIndex; }
        public void setHandIndex(int handIndex) { this.handIndex += handIndex; }
        public string[] getHand(){return playerHand;}
        public string getUsername() { return userName; }
        public int viewMoney() { return money; }
        public int getPlayerHandValues(){return playerHandValues;}
        public int getHandIndex(){ return handIndex;}

    }
}