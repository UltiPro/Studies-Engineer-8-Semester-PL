using Experimental.System.Messaging;

string queuePath = @".\private$\MyQueue";

// Sprawdź, czy kolejka istnieje
if (MessageQueue.Exists(queuePath))
{
    using (MessageQueue queue = new MessageQueue(queuePath))
    {
        try
        {
            while (true)
            {
                // Odbierz wiadomość (czeka na wiadomość)
                Message msg = queue.Receive();
                msg.Formatter = new XmlMessageFormatter(new String[] { "System.String" });

                // Wyświetl treść wiadomości
                Console.WriteLine("Odebrano wiadomość: " + msg.Body.ToString());
            }
        }
        catch
        {
            return;
        }
    }
}
else
{
    Console.WriteLine("Kolejka nie istnieje.");
}