using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace AsyncEx
{
    internal class Demultiplexer
    {
        static IList<ChannelReader<T>> Split<T>(ChannelReader<string> ch, int n)
        {
            var outputs = new Channel<T>[n];

            for (int i = 0; i < n; i++)
                outputs[i] = Channel.CreateUnbounded<T>();

            Task.Run(async () =>
            {
                var index = 0;
                await foreach (var item in ch.ReadAllAsync())
                {
           //         await outputs[index].Writer.WriteAsync(item);
                    index = (index + 1) % n;
                }

                foreach (var ch in outputs)
                    ch.Writer.Complete();
            });

            return outputs.Select(ch => ch.Reader).ToArray();
        }


        public async void Execute()
        {
            var joe = CreateMessenger("Joe", 10);
            var readers = Split<int>(joe, 3);
            var tasks = new List<Task>();

            for (int i = 0; i < readers.Count; i++)
            {
                var reader = readers[i];
                var index = i;
                tasks.Add(Task.Run(async () =>
                {
                    await foreach (var item in reader.ReadAllAsync())
                        Console.WriteLine($"Reader {index}: {item}");
                }));
            }

            await Task.WhenAll(tasks);
        }
        static ChannelReader<string> CreateMessenger(string msg, int count)
        {
            var ch = Channel.CreateUnbounded<string>();
            var rnd = new Random();

            Task.Run(async () =>
            {
                for (int i = 0; i < count; i++)
                {
                    await ch.Writer.WriteAsync($"{msg} {i}");
                    await Task.Delay(TimeSpan.FromSeconds(rnd.Next(3)));
                }
                ch.Writer.Complete();
            });

            return ch.Reader;
        }
    }

}
