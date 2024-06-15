using Experimental.System.Messaging;

string queuePath = @".\private$\MyQueue";

// Sprawdź, czy kolejka istnieje, jeśli nie, utwórz ją
if (!MessageQueue.Exists(queuePath)) MessageQueue.Create(queuePath);

// Otwórz lub utwórz kolejkę
using (MessageQueue queue = new MessageQueue(queuePath))
{
    try
    {
        while (true)
        {
            // Utwórz nową wiadomość
            Message msg = new Message();
            msg.Body = Console.ReadLine();

            // Wyślij wiadomość do kolejki
            queue.Send(msg);

            Console.WriteLine("Wiadomość została wysłana do kolejki.");
        }
    }
    catch
    {
        return;
    }
}