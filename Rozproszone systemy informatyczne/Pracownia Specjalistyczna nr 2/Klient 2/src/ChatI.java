import java.rmi.Remote;
import java.rmi.RemoteException;

public interface ChatI extends Remote {
    public void messageFromServer(String message) throws RemoteException;
}
