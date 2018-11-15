using System;
namespace Lab_2_CIS_247C
{
    public class PlayTwentyOne
    {
        CardDeck cd = new CardDeck();
        Menu menu = new Menu();
        Player player;
        int pot;
        int potRaise;
        int botRaisedPot;
        //==================================================================================
        public void play21()
        {
            player = menu.welcomeMenu(); //welcome to game, user
            pot = 10;
            while (true) //game Begin
            {
                //Solo or Multiplayer
                int gameplayMode = menu.gameplayMenu();
                Bot[] bots = createBots(gameplayMode);
                //To initialize bots or nullify

                if (player.viewMoney() >= pot)
                {
                    cd.shuffleTheDeck(); //shuffle the deck
                    dealCards(bots, player); //deals the cards
                    actionLoop(player, bots);
                    int userContinue = menu.endGameScreen(player, bots, pot);//
                    if (!(userContinue == 1))
                    {
                        break;
                    }
                }
            }//end gameplay loop
        }//==================================================================================
        private Bot[] createBots(int one)
        {
            if (one == 1)
            {
                Random rng = new Random();
                int rngNum = rng.Next(1, 5);
                Bot[] bots = new Bot[rngNum];
                for (int i = 0; i < bots.Length; i++)
                {
                    bots[i] = new Bot();
                    menu.playerJoinedTheGame(bots[i]);
                }
                return bots;
            }
            return null;
        }//==================================================================================
        private void dealCards(Bot[] bots, Player player)
        {
            player.setStartHandIndex(2); //how many cards they get at first
            player.setHand(cd.deal2Player(player, 0, player.getHandIndex()));
            //string[] aces = { "1D", "1C", "1S", null, null, null };
            //player.setHand(aces); HACKS 
            if (bots != null)
            {
                for (int i = 0; i < bots.Length; i++)
                {
                    bots[i].setStartHandIndex(2);
                    bots[i].setHand(cd.deal2Player(bots[i], 0, bots[i].getHandIndex()));
                }
            }

        }//==================================================================================
        private void actionLoop(Player player, Bot[] bots){
            int round = 1;
            while (true)//action loop
            {
                if (round < 4)
                {
                    menu.roundX(round);
                    playerAction(bots, botRaisedPot);
                    botRaisedPot = 0;
                    if(player.getPlayerHandValues()>21||player.getPlayerHandValues() == 21){
                        menu.endGameScreen(player, bots, pot);
                        break;
                    }
                    if (bots != null)
                    {
                        botsBehavior(bots, potRaise);
                        potRaise = 0; //resets pot raise
                    }//end bot AI
                    if(bots==null&&potRaise>0){
                        pot += 2*potRaise;
                        potRaise = 0;
                    }
                    round += 1;
                    Console.Clear();
                }
                else
                {
                    break;
                }
            }//action loop
        }//==================================================================================
        private void playerAction(Bot[] bots, int botRaisedPot)
        {
            if (botRaisedPot > player.viewMoney() || player.viewMoney() < 0)
            {
                menu.playerRanOutOfMoney(player);
                player.takeMoney(botRaisedPot);
            }
            else
            {
                if (botRaisedPot > 0)
                {
                    player.takeMoney(botRaisedPot);
                    menu.playerGivesToPot(player, botRaisedPot);
                    pot += botRaisedPot;
                }
                int userAction = menu.chooseAction(player, pot, player.getHand(), bots);
                switch (userAction)
                {
                    case 1:
                        player.setHand(cd.deal2Player(player, player.getHandIndex(), (player.getHandIndex() + 1)));
                        player.setHandIndex(1);
                        break;
                    case 2:
                        potRaise += menu.raiseMoneyPot(player);
                        break;
                    case 3:
                        menu.playerStand(player);
                        break;
                }//end Player actions
                player.updatePlayerHandValues(cd.countHand(player.getHand()));//updates After choice
                Console.WriteLine("Player Hand Value  : " + player.getPlayerHandValues());
            }
        }//==================================================================================
        private void botsBehavior(Bot[] bots, int potRaise)
        {
            Random rng = new Random();
            for (int i = 0; i < bots.Length; i++)
            {
                //Bot AI Behaviour
                if (bots[i] != null)
                {
                    int rngNum = rng.Next(1, 10);
                    int tempHandVal = cd.countHand(bots[i].getHand()); //counts hand
                    int index = bots[i].getHandIndex(); //index for the hand
                    if (potRaise > bots[i].viewMoney())
                    {
                        menu.playerRanOutOfMoney(bots[i]);
                        continue;
                    }
                    if (potRaise > 0)
                    {
                        bots[i].takeMoney(potRaise); //A. botsPay = 5
                        pot += (potRaise);
                        menu.playerGivesToPot(bots[i], potRaise);
                    }

                    if (tempHandVal < 21 && tempHandVal <= 11 && rngNum > 3)
                    {
                        bots[i].takeMoney(5);
                        potRaise += 5;
                        botRaisedPot += 5;
                        menu.playerRaisedMoney(bots[i], potRaise);
                    }
                    else if (tempHandVal < 21 && tempHandVal >= 12 && rngNum > 5)
                    {
                        bots[i].setHand(cd.deal2Player(bots[i], index, (index + 1)));
                        bots[i].setHandIndex(1);
                    }
                    else if (tempHandVal < 21 && tempHandVal >= 17 && rngNum >= 8)
                    {
                        bots[i].setHand(cd.deal2Player(bots[i], index, (index + 1)));
                        bots[i].setHandIndex(1);
                    }
                    bots[i].updatePlayerHandValues(cd.countHand(bots[i].getHand()));
                    if (tempHandVal > 21)//check for bust
                    {
                        menu.playerXlost(bots[i]);
                        bots[i] = null;
                        continue;
                    }
                    else
                    {
                        menu.playerStand(bots[i]);
                    }
                    //Console.WriteLine(bots[i].getUsername()+" Hand Value  : " + bots[i].getPlayerHandValues());
                }
            }
        }//==================================================================================
    }//==================================================================================
}//==================================================================================
