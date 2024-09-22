using Starkwood_RPS.MoveSets;
using Starkwood_RPS.Moves;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace Starkwood_RPS
{
    internal class Game
    {
        public string PlayerName { get; set; }
        public IMoveSet MoveSet { get; set; }

        public int TotalWins { get; set; } = 0;

        public int TotalDraws { get; set; } = 0;

        public int TotalLosses { get; set; } = 0;

        public int TotalTurns { get; set; } = 0;

        public List<IMove> TotalMovesPlayed { get; set; } = new List<IMove>();

        public StringBuilder GameRecord { get; set; } = new StringBuilder();

        public Game(string name){
            PlayerName = name;
            MoveSet = SelectMoveSet();
            var playAgain = true;
            GameRecord.AppendLine($"Player: {PlayerName} || Session Date: {DateTime.Now.Date.ToString().Split(" ")[0]}\n");
            while (playAgain)
            {
                RunGame();
                Console.WriteLine("Play again? y/n");
                var yes = Console.ReadLine() == "y";
                if (!yes) 
                {
                    playAgain = false;
                }
                else
                {
                    Console.WriteLine("Change game mode? y/n");
                    yes = Console.ReadLine() == "y";
                    if (yes)
                    {
                        MoveSet = SelectMoveSet();
                    }
                    Console.Clear();
                    Console.WriteLine("---New Game---");
                }
            }
        }

        private IMoveSet SelectMoveSet() {
            IMoveSet? chosenMoveSet = null;
            Console.WriteLine($"\nHello {PlayerName}, please select a game mode:\n");
            while (chosenMoveSet is null)
            {
                
                Console.WriteLine("[1.] Rock Paper Scissors");
                Console.WriteLine("[2.] Rock Paper Scissors Lizard Spock");
                try
                {
                    var choice = int.Parse(Console.ReadLine());
                    switch (choice)
                    {
                        default:
                            Console.WriteLine("--Invalid Choice--\n");
                            break;
                        case 1:
                            chosenMoveSet = new Classic();
                            break;
                        case 2:
                            chosenMoveSet = new RPSSL();
                            Console.WriteLine("Bazinga!");
                            break;
                    }
                }
                catch
                {
                    Console.WriteLine("--Invalid Choice--\n");
                }
                
            }

            if (MoveSet is not null)
            {
                GameRecord.AppendLine($"\nGame mode change: {MoveSet.Name} => {chosenMoveSet.Name}");
            } else
            {
                GameRecord.AppendLine($"\nInitial game mode: {chosenMoveSet.Name}");
            }

            return chosenMoveSet;
        }

        public IMove SelectMove(bool cpu = false) 
        {
            var moves = MoveSet.Moves.ToList();
            IMove? chosenMove = null;
            if (cpu)
            {
                var random = new Random();
                chosenMove = moves[random.Next(0, moves.Count - 1)];
                Console.WriteLine($"CPU has chosen {chosenMove.Name}");
            }
            else
            {
                Console.WriteLine("--------------------------------------------------");
                while (chosenMove is null)
                {
                    Console.WriteLine("Make your play:");
                    for (int i = 0; i < moves.Count; i++)
                    {
                        var move = moves[i];
                        Console.WriteLine($"{i + 1}. {move.Name}");
                    } 
                    try
                    {
                        var choice = int.Parse(Console.ReadLine());
                        if (moves[choice - 1] is IMove)
                        {
                            chosenMove = moves[choice - 1];
                            Console.WriteLine($"\nYou have chosen {chosenMove.Name}");
                            Thread.Sleep(200);
                        }
                    }
                    catch
                    {
                        
                        Console.WriteLine("--Invalid Choice--");
                        
                    }
                    
                    
                }
            }
            return chosenMove;
        }

        public void RunGame() 
        {
            var gameOver = false;
            
            var playerMoves = new List<IMove>();
            var cpuMoves = new List<IMove>();
            var turns = 0;
            while (!gameOver)
            {


               
               var playerMove = SelectMove();
               var cpuMove = SelectMove(true);
               playerMoves.Add(playerMove);
               cpuMoves.Add(cpuMove);
               turns++;
               var win = playerMove.Beats(cpuMove);
               if (!win)
               {
                    if (playerMove.Name == cpuMove.Name) //Game continues on a draw
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("You draw!");
                        Thread.Sleep(100);
                        Console.ForegroundColor = ConsoleColor.White;
                        TotalDraws++;
                    }
                    else //Player loses
                    {
                        var cpuWinLine = cpuMove.WinLines.FirstOrDefault(line => line.Contains($"{playerMove.Name}"));
                        Console.WriteLine(cpuWinLine ?? "an error has occured");
                        Thread.Sleep(200);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("---You Lose---");
                        Console.ForegroundColor = ConsoleColor.White;
                        Thread.Sleep(300);
                        TotalLosses++;
                        gameOver = true;
                        TotalTurns += turns;
                        TotalMovesPlayed.AddRange(playerMoves);
                        TotalMovesPlayed.AddRange(playerMoves);
                        Console.WriteLine(PostGameStats(win, turns, playerMoves, cpuMoves));
                    }
                }
                else //Player wins
                {
                    var playerWinLine = playerMove.WinLines.FirstOrDefault(line => line.Contains($"{cpuMove.Name}"));
                    Console.WriteLine(playerWinLine ?? "an error has occured");
                    Thread.Sleep(200);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("---You Win---");
                    Console.ForegroundColor = ConsoleColor.White;
                    Thread.Sleep(300);
                    TotalWins++;
                    gameOver = true;
                    TotalTurns += turns;
                    TotalMovesPlayed.AddRange(playerMoves);
                    TotalMovesPlayed.AddRange(playerMoves);
                    Console.WriteLine(PostGameStats(win, turns, playerMoves, cpuMoves));
                }
            }
            
        }

        public string PostGameStats(bool playerWon, 
            int turnsThisGame, 
            List<IMove> playerMovesThisGame,
            List<IMove> cpuMovesThisGame) 
        {
            List<IMove> allMovesThisGame = new List<IMove>();
            allMovesThisGame.AddRange(playerMovesThisGame);
            allMovesThisGame.AddRange(cpuMovesThisGame);

            var strBuilder = new StringBuilder();
            strBuilder.AppendLine("\n\n---Stats---");
            Thread.Sleep(100);
            // Winner
            strBuilder.AppendLine($"Winner: {(playerWon ? PlayerName : "CPU")}\n----------------------");
            // Turns
            strBuilder.AppendLine($"Turns: {turnsThisGame}\n----------------------");
            // Moves
            strBuilder.AppendLine($"Moves this game ({PlayerName} / CPU):");
            for (int i = 0; i < turnsThisGame; i++)
            {
                strBuilder.AppendLine($"[{i+1}.] {playerMovesThisGame[i].Name} / {cpuMovesThisGame[i].Name}");
            }
            // Player/CPU favourite move
            Thread.Sleep(100);
            strBuilder.AppendLine("\n----------------------");
            strBuilder.AppendLine($"{PlayerName}'s favourite move: {playerMovesThisGame.GroupBy(it => it.Name).OrderByDescending(group => group.Count()).FirstOrDefault()?.ToList()[0].Name}\n");
            strBuilder.AppendLine($"CPU's favourite move: {cpuMovesThisGame.GroupBy(it => it.Name).OrderByDescending(group => group.Count()).FirstOrDefault()?.ToList()[0].Name}\n");
            strBuilder.AppendLine($"Most popular move this game: {allMovesThisGame.GroupBy(it => it.Name).OrderByDescending(group => group.Count()).FirstOrDefault()?.ToList()[0].Name}\n");

            //Totals
            Thread.Sleep(100);
            strBuilder.AppendLine("----------------------\n");
            strBuilder.AppendLine($"Total Wins: {TotalWins}");
            strBuilder.AppendLine($"Total Draws: {TotalDraws}");
            strBuilder.AppendLine($"Total Losses: {TotalLosses}");
            strBuilder.AppendLine($"Total Turns: {TotalTurns}");
            strBuilder.AppendLine($"All time favourite move: {TotalMovesPlayed.GroupBy(it => it.Name).OrderByDescending(group => group.Count()).FirstOrDefault()?.ToList()[0].Name}\n\n");

            GameRecord.AppendLine($"---------GAME {TotalWins + TotalLosses}--------");
            GameRecord.AppendLine(strBuilder.ToString());

            return strBuilder.ToString();
        }
    }
}
