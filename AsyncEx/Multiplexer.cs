using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace AsyncEx
{
    internal class Multiplexer
    {

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

        static ChannelReader<T> Merge<T>(
              ChannelReader<T> first, ChannelReader<T> second)
        {
            var output = Channel.CreateUnbounded<T>();

            Task.Run(async () =>
            {
                await foreach (var item in first.ReadAllAsync())
                    await output.Writer.WriteAsync(item);
            });
            Task.Run(async () =>
            {
                await foreach (var item in second.ReadAllAsync())
                    await output.Writer.WriteAsync(item);
            });

            return output;
        }

        public async void Execute()
        {
            var ch = Merge(CreateMessenger("Joe", 3), CreateMessenger("Ann", 5));

            await foreach (var item in ch.ReadAllAsync())
                Console.WriteLine(item);

        }
        static ChannelReader<T> Merge<T>(params ChannelReader<T>[] inputs)
        {
            var output = Channel.CreateUnbounded<T>();

            Task.Run(async () =>
            {
                async Task Redirect(ChannelReader<T> input)
                {
                    await foreach (var item in input.ReadAllAsync())
                        await output.Writer.WriteAsync(item);
                }

                await Task.WhenAll(inputs.Select(i => Redirect(i)).ToArray());
                output.Writer.Complete();
            });

            return output;
        }
    }
}
