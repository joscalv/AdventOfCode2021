namespace AdventOfCode2021
{
    public interface IDay<out TProblem1, out TProblem2>
    {
        TProblem1 ExecutePart1();
        TProblem2 ExecutePart2();
    }
}
