import java.rmi.Remote;
import java.rmi.RemoteException;

public interface MyServerInt extends Remote {
    String Play(int position) throws RemoteException;
}
