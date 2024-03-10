import java.rmi.RemoteException;
import java.rmi.server.UnicastRemoteObject;

public class ChatClientImpl extends UnicastRemoteObject implements ChatI {
    public ChatClientImpl() throws RemoteException {
        super();
    }

    @Override
    public void messageFromServer(String message) throws RemoteException {
        System.out.println("Server: " + message);
    }
}