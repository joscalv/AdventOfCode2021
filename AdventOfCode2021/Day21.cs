using System.Collections;
using System.Runtime.CompilerServices;

namespace AdventOfCode2021
{
    public class Day21 : IDay<long, long>
    {
        private static readonly int[] DiceCombinations;
        private readonly int _positionPlayer1;
        private readonly int _positionPlayer2;

        public Day21()
        {
            var lines = File
                .ReadAllText(Path.Combine("Inputs", "input21.txt"))
                .Split('\n');

            _positionPlayer1 = int.Parse(lines[0].Split("position: ", StringSplitOptions.RemoveEmptyEntries)[1]);
            _positionPlayer2 = int.Parse(lines[1].Split("position: ", StringSplitOptions.RemoveEmptyEntries)[1]);
        }


        static Day21()
        {
            DiceCombinations = new int[10];
            for (int dice1 = 1; dice1 <= 3; dice1++)
            {
                for (int dice2 = 1; dice2 <= 3; dice2++)
                {
                    for (int dice3 = 1; dice3 <= 3; dice3++)
                    {
                        DiceCombinations[dice1 + dice2 + dice3]++;
                    }
                }
            }
        }

        public long ExecutePart1()
        {
            return PlayGame(_positionPlayer1, _positionPlayer2);
        }

        public long ExecutePart2()
        {
            return PlayDiracGame(_positionPlayer1, _positionPlayer2);
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
                var diceSum = 3 * dice + 3;

                if (turn == 0)
                {
                    player1Position = Move(player1Position, diceSum);
                    scorePlayer1 += player1Position + 1;
                }
                else
                {
                    player2Position = Move(player2Position, diceSum);
                    scorePlayer2 += player2Position + 1;
                }

                turn = (turn + 1) % 2;
                dice += 3;
            }

            return scorePlayer1 < scorePlayer2 ? scorePlayer1 * (dice - 1) : scorePlayer2 * (dice - 1);
        }


        public record DiracGameStatus(int P1Position, int P1Score, int P2Position, int P2Score, int turn);

        public static long PlayDiracGame(int pos1, int pos2)
        {
            var memo = new Dictionary<DiracGameStatus, (long, long)>();
            var result = PlayDiracGame(memo,
                new DiracGameStatus(pos1 - 1, 0, pos2 - 1, 0, 0));
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

            for (int i = 3; i < 10; i++)
            {
                if (gameStatus.turn == 0)
                {
                    var newPos1 = Move(gameStatus.P1Position, i);
                    var newScore1 = gameStatus.P1Score + newPos1 + 1;
                    if (newScore1 >= 21)
                    {
                        wins1 += 1 * DiceCombinations[i];
                    }
                    else
                    {
                        var tmp = PlayDiracGame(memo, new DiracGameStatus(newPos1, newScore1, gameStatus.P2Position, gameStatus.P2Score, 1 - gameStatus.turn));
                        wins1 += (tmp.Item1 * DiceCombinations[i]);
                        wins2 += (tmp.Item2 * DiceCombinations[i]);
                    }
                }
                else if (gameStatus.turn == 1)
                {
                    var newPos2 = Move(gameStatus.P2Position, i);
                    var newScore2 = gameStatus.P2Score + newPos2 + 1;
                    if (newScore2 >= 21)
                    {
                        wins2 = wins2 + 1 * DiceCombinations[i];
                    }
                    else
                    {
                        var tmp = PlayDiracGame(memo, gameStatus with
                        {
                            P2Position = newPos2,
                            P2Score = newScore2,
                            turn = 1 - gameStatus.turn
                        });
                        wins1 += (tmp.Item1 * DiceCombinations[i]);
                        wins2 += (tmp.Item2 * DiceCombinations[i]);
                    }
                }

            }

            memo.Add(gameStatus, (wins1, wins2));
            return (wins1, wins2);
        }
        
        private static int Move(int position, int movement) => (position + movement) % 10;
    }
}