import java.rmi.RemoteException;
import java.rmi.server.UnicastRemoteObject;

public class ChatClientImpl extends UnicastRemoteObject implements ChatClientInterface {
    private ChatServerInterface server;

    public ChatClientImpl(ChatServerInterface server) throws RemoteException {
        super();
        this.server = server;
    }

    @Override
    public void receiveMessage(String message) throws RemoteException {
        System.out.println("Server: " + message);
    }
}
