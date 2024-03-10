import java.rmi.Remote;
import java.rmi.RemoteException;

public interface MyServerInt extends Remote {
    String getDescription(String text) throws RemoteException;

    String GetProducts() throws RemoteException;

    String GetProduct(String name) throws RemoteException;

    String Send(String text) throws RemoteException;
}
