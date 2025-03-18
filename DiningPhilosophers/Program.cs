using System;
using System.Threading;
using DiningPhilosophers;

public static class Program
{
    static void Main(string[] args)
    {
        int numPhilosophers = 5;
        Philosopher[] philosophers = new Philosopher[numPhilosophers];
        Mutex[] forks = new Mutex[numPhilosophers];

        for (int i = 0; i < numPhilosophers; i++)
        {
            forks[i] = new Mutex();
        }

        for (int i = 0; i < numPhilosophers; i++)
        {
            philosophers[i] = new Philosopher(i, forks[i], forks[(i + 1) % numPhilosophers]);
            new Thread(philosophers[i].Run).Start();
        }
    }
}
