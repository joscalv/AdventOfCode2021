using System.Collections;
using System.Runtime.CompilerServices;

namespace AdventOfCode2021
{
    public class Day21 : IDay<long, long>
    {
        private readonly int[] _values;

        public Day21()
        {
            //_values = File
            //    .ReadAllText(Path.Combine("Inputs", "input21.txt"))
            //    .Split('\n')
            //    .Select(int.Parse)
            //    .ToArray();
        }

        public long ExecutePart1()
        {
            return PlayGame(8, 1);
        }

        public static long PlayGame(int startPlayer1, int startPlayer2)
        {
            long scorePlayer1 = 0;
            int player1Position = startPlayer1 - 1;
            long scorePlayer2 = 0;
            int player2Position = startPlayer2 - 1;
            int turn = 0;

            int dice = 1;
            while (scorePlayer1 < 1000 && scorePlayer2 < 1000)
            {
                var diceSum = dice + dice + 1 + dice + 2;

                if (turn == 0)
                {
                    player1Position = Move(player1Position , diceSum) ;
                    scorePlayer1 += player1Position + 1;
                }
                else
                {
                    player2Position = Move(player2Position , diceSum);
                    scorePlayer2 += player2Position + 1;
                }

                turn = (turn + 1) % 2;
                dice += 3;
            }

            return scorePlayer1 < scorePlayer2 ? scorePlayer1 * (dice - 1) : scorePlayer2 * (dice - 1);
        }

        public long ExecutePart2()
        {
            return 0;
        }


        public record DiracGameStatus(int P1Position, int P1Score, int P2Position, int P2Score, int turn);



        public static long PlayDiracGame(int pos1, int pos2)
        {
            var memo = new Dictionary<DiracGameStatus, (long, long)>();
            var result = PlayDiracGame(memo,
                new DiracGameStatus(pos1, 0, pos2, 0, 0));
            return Math.Max(result.Item1, result.Item2);
        }


        public static (long, long) PlayDiracGame(Dictionary<DiracGameStatus, (long, long)> memo, DiracGameStatus gameStatus)
        {


            if (memo.TryGetValue(gameStatus, out var result))
            {
                return result;
            }

            long wins1 = 0;
            long wins2 = 0;
            if (gameStatus.turn == 0)
            {
                for (byte i = 1; i <= 3; i++)
                {

                    var newPos1 = Move(gameStatus.P1Position, i);
                    var newScore1 = gameStatus.P1Score + newPos1 + 1;
                    if (newScore1 >= 21)
                    {
                        wins1++;
                    }
                    else
                    {
                        //call
                        var tmp = PlayDiracGame(memo, gameStatus with
                        {
                            P1Position = newPos1,
                            P1Score = newScore1,
                            turn = (gameStatus.turn + 1) % 2
                        });
                        wins1 += tmp.Item1;
                        wins2 += tmp.Item2;
                    }
                }
            }
            else
            {
                for (byte i = 1; i <= 3; i++)
                {

                    var newPos2 = Move(gameStatus.P2Position, i);
                    var newScore2 = gameStatus.P2Score + newPos2 + 1;
                    if (newScore2 >= 21)
                    {
                        wins2++;
                    }
                    else
                    {
                        var tmp = PlayDiracGame(memo, gameStatus with
                        {
                            P2Position = newPos2,
                            P2Score = newScore2,
                            turn = (gameStatus.turn + 1) % 2
                        });
                        wins1 += tmp.Item1;
                        wins2 += tmp.Item2;
                    }
                }

            }
            memo.Add(gameStatus, (wins1, wins2));
            return (wins2, wins2);
        }

        private static int Move(int position, int movement) => (position + movement) % 10;
    }
}