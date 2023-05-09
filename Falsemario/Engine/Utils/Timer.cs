using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Falsemario.Engine.Utils
{
    public delegate void TimerTick(object invoker);

    public class Timer
    {
        public int Delay { get; set; } = 1000;

        public bool IsRunning { get; private set; } = false;

        public event TimerTick OnTick;

        public void Start()
        {
            IsRunning = true;
            Task.Run(async () =>
            {
                while (IsRunning)
                {
                    try
                    {
                        OnTick.Invoke(this);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }

                    await Task.Delay(Delay);
                }
            });
        }

        public void Stop()
        {
            IsRunning = false;
        }
    }
}
