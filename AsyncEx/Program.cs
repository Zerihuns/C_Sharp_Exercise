using System.Threading.Channels;

var ch = Channel.CreateUnbounded<string>();

var consumer = Task.Run(async () =>
{
    while (await ch.Reader.WaitToReadAsync())
        Console.WriteLine(await ch.Reader.ReadAsync());
});

var producer = Task.Run(async () =>
{
    var rnd = new Random();
    for (int i = 0; i < 5; i++)
    {
        await Task.Delay(TimeSpan.FromSeconds(rnd.Next(3)));
        await ch.Writer.WriteAsync($"Message {i}");
    }
    ch.Writer.Complete();
});

await Task.WhenAll(producer, consumer);

//Let's try With Go 
/*package main

import (
	"time"
)

func main()
{
    ch:= make(chan int)

    go Publisher(ch)
    Consumer(ch)
//    ConsumerWithRange(ch)
}

func Publisher(ch chan<- int)
{
iter:= 0

    for {
        ch < -iter

        iter++

        time.Sleep(2 * time.Second)

        if iter == 10 {
            close(ch)

            return

        }
    }
}

func Consumer(ch<-chan int)
{
    for {
        select {
		case data, ok:= < -ch:
			switch {
    case ok:
        println(data)

            case !ok:
        println("Channel Closed")

                return

            }
		}
	}
}

func ConsumerWithRange(ch<-chan int)
{
    for data := range ch
{
    println(data)

    }
println("Channel Closed")
}*/
