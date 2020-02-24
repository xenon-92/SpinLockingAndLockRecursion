using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace LockRecusrion
{
    class Program
    {
        static SpinLock sl = new SpinLock();
        static void Main(string[] args)
        {
            LockRecusrion(5);
            Console.ReadKey();
        }
        static void LockRecusrion(int x)
        {
            bool lockTaken = false;
            try
            {
                sl.Enter(ref lockTaken);
            }
            catch (LockRecursionException e)
            {
                Console.WriteLine("exception " + e);
            }
            finally
            {
                if (lockTaken)
                {
                    Console.WriteLine($"Took a lock, x= {x}");
                    LockRecusrion(x-1);
                    sl.Exit();
                }
                else
                {
                    Console.WriteLine($"failed to take a lock for x={x}");
                }
            }
        }
    }
}
