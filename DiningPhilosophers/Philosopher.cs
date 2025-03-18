namespace DiningPhilosophers;

public class Philosopher(int id, Mutex leftFork, Mutex rightFork)
{
    public void Run()
    {
        while (true)
        {
            Think();
            Eat();
        }
    }

    private void Think()
    {
        Console.WriteLine($"Философ {id} думает.");
        Thread.Sleep(new Random().Next(1000, 2000));
    }

    private void Eat()
    {
        Console.WriteLine($"Философ {id} пытается начать кушать");
        //ему нужны обе сразу
        bool hasLeftFork = false;
        bool hasRightFork = false;

        try
        {
            hasLeftFork = leftFork.WaitOne(1000); //берет в левую
            if (hasLeftFork)
            {
                hasRightFork = rightFork.WaitOne(1000); //берет в правую
                if (hasRightFork)
                {
                    Console.WriteLine($"Философ {id} кушает");
                    Thread.Sleep(new Random().Next(1000, 2000)); //кушает столько
                }
            }
        }
        finally
        {
            if (hasRightFork)
            {
                rightFork.ReleaseMutex();
            }
            if (hasLeftFork)
            {
                leftFork.ReleaseMutex();
            }
        }

        Console.WriteLine($"Философ {id} закончил кушать");
    }
}