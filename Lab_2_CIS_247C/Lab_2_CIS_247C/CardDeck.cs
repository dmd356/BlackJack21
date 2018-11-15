using System;
namespace Lab_2_CIS_247C
{
    public class CardDeck
    {//==================================================================================
        private Random rng;
        private string[] deck = {
            "1D", "2D", "3D", "4D", "5D", "6D", "7D","8D","9D","10D","11D","12D","13D",
            "1C", "2C", "3C", "4C", "5C", "6C", "7C","8C","9C","10C","11C","12C","13C",
            "1H", "2H", "3H", "4H", "5H", "6H", "7H","8H","9H","10H","11H","12H","13H",
            "1S", "2S", "3S", "4S", "5S", "6S", "7S","8S","9S","10S","11S","12S","13S"
        };
        private string[] shuffledDeck;
        //==================================================================================
        public int countHand(string[] playerHand)
        {
            int countAces = 0;
            int[] valuesArr = new int[playerHand.Length];
            for (int i = 0; i < playerHand.Length; i++){
                if(playerHand[i]!=null){
                    if(int.Parse(playerHand[i].Substring(0, (playerHand[i].Length - 1)))==1){
                        valuesArr[i] = 1;
                        countAces++;
                    }else{
                        valuesArr[i] = int.Parse(playerHand[i].Substring(0, (playerHand[i].Length - 1)));
                    }
                }
            }//This loop checks for the aces, else, just add the value to 
            int count = 0;
            foreach (int x in valuesArr){
                if(x!=0){
                    if(x>10){
                        count += 10 % x;
                    }else if(x==1){
                        count += 11;
                    }
                    else{
                        count += x;
                    }
                }
            }
            while(count>21){
                if (count > 21 && countAces > 0)
                {
                    count-=10;
                    countAces -=1;
                    continue;
                }
                break;
            }
            return count;
        }//==================================================================================
        public void shuffleTheDeck()
        {
            shuffledDeck = new string[52];
            rng = new Random();
            for (int i = 0; i < deck.Length; i++)
            {
                int rngNum = 0;
                if(i!=0){
                    while (true)
                    {
                        rngNum = rng.Next(0, deck.Length);
                        int count = 0;
                        for (int j = 0; j < i; j++)
                        {
                            if (deck[rngNum] != shuffledDeck[j])
                            {
                                count++;
                            }
                        }
                        if (count==(i))
                        {
                            Console.WriteLine(shuffledDeck[i]);
                            break;
                        }
                    }
                }
                if(i==0){
                    rngNum = rng.Next(0, deck.Length);
                    Console.WriteLine(shuffledDeck[i]);
                }
                shuffledDeck[i] = deck[rngNum];
            }
        }//==================================================================================
        public string[] deal2Player(Player player, int startIndex,int endIndex){
            rng = new Random();
            string[] newHand = player.getHand();
            for (int i = startIndex; i < endIndex; i++)
            {
                int rngNum = 0;
                while(true){
                    rngNum = rng.Next(0, 51);
                    if(shuffledDeck[rngNum]!=null){
                        newHand[i] = shuffledDeck[rngNum];
                        removeCardFromShuffledDeck(rngNum);
                        break;
                    }
                }
            }
            return newHand;
        }//==================================================================================
        private void removeCardFromShuffledDeck(int index){
            shuffledDeck[index] = null;
        }//==================================================================================
        public string[] returnShuffledDeck(){
            return shuffledDeck;
        }//==================================================================================
    }
}
