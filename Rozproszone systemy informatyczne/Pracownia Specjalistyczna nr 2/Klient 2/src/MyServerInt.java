import java.rmi.Remote;
import java.rmi.RemoteException;

public interface MyServerInt extends Remote {
	public void sendMessageToServer(String message) throws RemoteException;

	public void sendMessageToClient(String message) throws RemoteException;

	public void registerListener(String name, ChatI client) throws RemoteException;
}
