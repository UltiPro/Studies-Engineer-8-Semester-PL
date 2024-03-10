import java.rmi.RemoteException;
import java.rmi.server.UnicastRemoteObject;

public class ChatClientImpl extends UnicastRemoteObject implements ChatI {
    private MyServerInt server;

    public ChatClientImpl(MyServerInt server) throws RemoteException {
        super();
        this.server = server;
    }

    @Override
    public void messageFromServer(String message) throws RemoteException {
        System.out.println(message);
    }
}