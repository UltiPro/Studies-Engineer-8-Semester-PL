import java.rmi.Remote;
import java.rmi.RemoteException;

public interface MyServerInt extends Remote {
    String getDescription(String text) throws RemoteException;

    String GetProducts() throws RemoteException;

    String GetProduct(String name) throws RemoteException;

    void Send(String text) throws RemoteException;

    String getChat() throws RemoteException;
}
