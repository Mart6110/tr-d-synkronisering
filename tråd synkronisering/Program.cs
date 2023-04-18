using System;
using System.Threading;

namespace tråd_synkronisering
{
    class Program
    {
        static object _lock = new object();
        static int _number = 0;

        public static void CountUp()
        {
            while(true)
            {
                //Lock object
                Monitor.Enter(_lock);
                try
                {
                    int threadId = Thread.CurrentThread.ManagedThreadId;
                    //Console.WriteLine($" Thread: {threadId} Entered into the critical section ");

                    _number += 2;
                    Console.WriteLine(_number);
                    Thread.Sleep(TimeSpan.FromSeconds(1));
                }
                finally
                {
                    //Releasing object
                    Monitor.Exit(_lock);
                    Console.WriteLine($"Thread : {Thread.CurrentThread.ManagedThreadId} Released");
                }
            }
        }

        public static void CountDown()
        {
            while (true)
            {
                //Lock object
                Monitor.Enter(_lock);
                try
                {
                    int threadId = Thread.CurrentThread.ManagedThreadId;
                    //Console.WriteLine($"Thread: {threadId} Entered into the critical section ");

                    _number -= 1;
                    Console.WriteLine(_number);
                    Thread.Sleep(TimeSpan.FromSeconds(2));

                }
                finally
                {
                    //Releasing object
                    Monitor.Exit(_lock);
                    //Console.WriteLine($"Thread : {Thread.CurrentThread.ManagedThreadId} Released");
                }
            }
        }


        static void Main(string[] args)
        {
            Thread countUp = new Thread(CountUp);
            Thread countDown = new Thread(CountDown);

            //Executing threads
            countUp.Start();
            countDown.Start();


            countUp.Join();
            countDown.Join();

            Console.ReadKey();

        }
    }
}
