namespace DiningPhilosophers;
class Philosopher(int id, Mutex leftFork, Mutex rightFork)
{
    public void Run()
    {
        while (true)
        {
            Think();
            while (!TryEat()) 
            {
                Console.WriteLine($"Философ {id} не смог поесть и пытается снова");
                Thread.Sleep(500);
            }
        }
    }

    private void Think()
    {
        Console.WriteLine($"Философ {id} думает.");
        Thread.Sleep(new Random().Next(1000, 2000));
    }

    private bool TryEat()
    {
        Console.WriteLine($"Философ {id} пытается начать кушать");

        Mutex firstFork = leftFork;
        Mutex secondFork = rightFork;
        
        bool hasFirstFork = false;
        bool hasSecondFork = false;

        try
        {
            hasFirstFork = firstFork.WaitOne(1000); 
            if (hasFirstFork)
            {
                hasSecondFork = secondFork.WaitOne(1000); 
                if (hasSecondFork)
                {
                    Console.WriteLine($"Философ {id} кушает");
                    Thread.Sleep(new Random().Next(1000, 2000)); 
                    return true;
                }
            }
            return false;
        }
        finally
        {
            if (hasSecondFork)
            {
                secondFork.ReleaseMutex();
            }
            if (hasFirstFork)
            {
                firstFork.ReleaseMutex();
            }
        }
    }
}