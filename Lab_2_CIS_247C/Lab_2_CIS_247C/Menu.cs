using System;
namespace Lab_2_CIS_247C
{
    public class Menu
    {//==================================================================================
        public Player welcomeMenu(){
            Console.WriteLine("-------------------------------");
            Console.WriteLine("-------------------------------");
            Console.WriteLine("Welcome to the BlackJak Application");
            Console.WriteLine("Please enter a name");
            String userName = Console.ReadLine();
            Player player = new Player(100, userName);
            Console.WriteLine("Hello " + userName + ". $100 has been added to your hand.");
            Console.WriteLine("-------------------------------");
            return player;
        }//==================================================================================
        public int gameplayMenu(){
            Console.WriteLine("-------------------------------");
            Console.WriteLine("1). to Play against bots");
            Console.WriteLine("2). to Play solo!");
            return getIntInput(1, 2);
        }//==================================================================================
        public int chooseAction(Player player, int pot, string[] playerHand, Bot[] bots){
            Console.WriteLine("-------------------------------");
            Console.WriteLine("The current pot : $"+pot);
            Console.WriteLine("-------------------------------");
            Console.WriteLine("Your Money : $" + player.viewMoney());
            Console.WriteLine("-------------------------------");
            interpretHand(playerHand);
            Console.WriteLine("Here are you're Choices since you haven't won or lost. Yet.");
            Console.WriteLine("1). Hit");
            Console.WriteLine("2). Raise pot");
            if(bots!=null){
                Console.WriteLine("3). Stand");
                return getIntInput(1, 3);
            }
            return getIntInput(1, 2);
        }//==================================================================================
        public int raiseMoneyPot(Player player){
            Console.WriteLine("-------------------------------");
            while(true){
                Console.WriteLine("How much would you like to raise by?");
                Console.WriteLine("Your Money : " + player.viewMoney());
                int raise = getIntInput(0, player.viewMoney());
                if (raise%5==0&&raise<=player.viewMoney()){
                    player.takeMoney(raise);
                    Console.WriteLine(player.getUsername()+" raised by $"+raise+"!");
                    Console.WriteLine("-------------------------------");
                    return raise;
                }
                Console.WriteLine("Remember, only increments of $5");
                Console.WriteLine("Your Money : " + player.viewMoney());
                Console.WriteLine("-------------------------------");
            }
        }//==================================================================================
        public void playerJoinedTheGame(Player player)
        {
            Console.WriteLine("-------------------------------");
            Console.WriteLine(player.getUsername() + " has joined this match!");
            Console.WriteLine("-------------------------------");
        }//==================================================================================
        public void roundX(int round){
            Console.WriteLine("-------------------------------");
            Console.WriteLine("Round " + round + ".");
            Console.WriteLine("-------------------------------");
        }//==================================================================================
        public void playerXlost(Player player){
            Console.WriteLine("-------------------------------");
            Console.WriteLine(player.getUsername()+" broke 21! Score: "+player.getPlayerHandValues());
            Console.WriteLine("-------------------------------");
        }//==================================================================================
        public void playerStand(Player player)
        {
            Console.WriteLine("-------------------------------");
            Console.WriteLine(player.getUsername() + " preferred to stand.");
            Console.WriteLine("-------------------------------");
        }//==================================================================================
        public void playerRaisedMoney(Player player, int raise){
            Console.WriteLine("-------------------------------");
            Console.WriteLine(player.getUsername() + " raised the pot by "+raise);
            Console.WriteLine("-------------------------------");
        }//==================================================================================
        public void playerGivesToPot(Player player, int pot)
        {
            Console.WriteLine("-------------------------------");
            Console.WriteLine(player.getUsername() + " had to add $"+pot+" to the pot.");
            Console.WriteLine("-------------------------------");

        }//==================================================================================
        public void playerRanOutOfMoney(Player player){
            Console.WriteLine("-------------------------------");
            Console.WriteLine(player.getUsername() + " ran out of money and cannot give anymore to pot!");
            Console.WriteLine("-------------------------------");
        }//==================================================================================        
        public int endGameScreen(Player player, Bot[] bots, int pot){//11/11/18 there is a problem here, thats why crash
            int countTwentyOne = player.getPlayerHandValues();
            int winningBotScore = 0;
            string winningBotName = "";
            if(bots!=null){
                for (int i = 0; i < bots.Length; i++)
                {
                    if (bots[i] != null)
                    {
                        if (i > 0)
                        {
                            for (int j = 0; j < i; j++)
                            {
                                if (bots[j] != null)
                                {
                                    if (bots[i].getPlayerHandValues() >= bots[j].getPlayerHandValues() && bots[i].getPlayerHandValues() > winningBotScore && bots[i].getPlayerHandValues()<=21)
                                    {
                                        winningBotScore = bots[i].getPlayerHandValues();
                                        winningBotName = bots[i].getUsername();
                                    }
                                }
                            }
                        }//If we are not in the first spot
                        else
                        {
                            winningBotScore = bots[i].getPlayerHandValues();
                            winningBotName = bots[i].getUsername();
                        }
                    }
                }//end for
            }
            winningBotName = (winningBotName.Length == 0) ? "The House" : winningBotName;
            if (countTwentyOne == 21)
            {
                Console.WriteLine("CONGRATULATIONS!!!! You got BlackJak 21!");
                player.giveMoney(pot);
            }else if(countTwentyOne>winningBotScore&&countTwentyOne<21){
                Console.WriteLine("CONGRATULATIONS!!!! You beat all opponents!");
                player.giveMoney(pot);
            }
            else if(countTwentyOne>21){
                Console.WriteLine("You Busted!!! Your score : "+countTwentyOne);
                Console.WriteLine(winningBotName + " beat you with a score of " + winningBotScore + "!!");
            }else if(winningBotScore>countTwentyOne){
                Console.WriteLine("You Lose!!!");
                Console.WriteLine(winningBotName + " beat you with a score of " + winningBotScore + "!!");
            }else if(winningBotScore==countTwentyOne){
                Console.WriteLine("You and "+winningBotName+" both tied!");
                Console.WriteLine("The House is declared winner!");
            }
            Console.WriteLine("Press enter to Continue...");
            Console.ReadLine();

            Console.WriteLine("Would you like to continue?");
            Console.WriteLine("1). Heck Yes");
            Console.WriteLine("2). Heck No");
            return getIntInput(1, 2);
        }//==================================================================================
        private void interpretHand(String[] playerHand){
            Console.WriteLine("---------Current Hand----------");
                for (int i = 0; i < playerHand.Length;i++){
                    if (playerHand[i] != null)
                    {
                        interpretCard(playerHand[i]);
                        if(playerHand[i+1]!=null){
                            Console.Write(" and ");
                        }
                        else if(playerHand[i + 1] == null){
                            Console.Write(".");
                            Console.WriteLine();

                            Console.WriteLine("-------------------------------");

                        }
                    } 
                } 
        }//==================================================================================
        private int getIntInput(int min, int maxNum)
        {
            while (true)
            {
                Console.WriteLine();
                String answer = Console.ReadLine();
                int num;
                if (int.TryParse(answer, out num) && int.Parse(answer) <= maxNum && int.Parse(answer) >= min)
                {
                    return num;
                }
                else
                {
                    Console.WriteLine("Please return a valid number!");
                    Console.WriteLine("Cannot be over " + maxNum + " or under " + min + ".");
                    Console.WriteLine();
                }
            }
        }//==================================================================================
        private void interpretCard(string card)
        {
            int value = int.Parse(card.Substring(0, (card.Length - 1)));
            string valueName = value.ToString();
            switch (value)
            {
                case 1:
                    valueName = "Ace";
                    break;
                case 11:
                    valueName = "Jack";
                    break;
                case 12:
                    valueName = "Queen";
                    break;
                case 13:
                    valueName = "King";
                    break;
            }
            if (card.Substring(card.Length - 1) == "D")
            {
                valueName += " of Diamonds";
            }
            if (card.Substring(card.Length - 1) == "C")
            {
                valueName += " of Clubs";
            }
            if (card.Substring(card.Length - 1) == "S")
            {
                valueName += " of Spades";
            }
            if (card.Substring(card.Length - 1) == "H")
            {
                valueName += " of Hearts";
            }
            Console.Write(valueName);
        }//==================================================================================
    }//==================================================================================
}//==================================================================================
