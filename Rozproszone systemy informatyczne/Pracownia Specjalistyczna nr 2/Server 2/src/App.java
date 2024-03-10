import java.net.MalformedURLException;
import java.rmi.Naming;
import java.rmi.RemoteException;
import java.rmi.registry.LocateRegistry;
import java.rmi.server.UnicastRemoteObject;
import java.util.Scanner;
import java.util.Vector;

public class App extends UnicastRemoteObject implements MyServerInt{
    private Vector<Chatter> chatters;

    public App() throws RemoteException{
        super();
        chatters = new Vector<Chatter>(10, 1);
    }

    public static void main(String[] args) throws Exception {
		String host = "localhost";

        System.setProperty("java.security.policy", "security.policy");
        if (System.getSecurityManager() == null) {
            System.setSecurityManager(new SecurityManager());
        }
        LocateRegistry.createRegistry(1099);
        System.setProperty("java.rmi.server.hostname", host);
        System.setProperty("java.rmi.server.codebase", "http://"+host+"/Chat/");
        try {
            MyServerInt myServerInt = new App();
			Naming.rebind("//"+host+"/Chat", myServerInt);
			System.out.println("Group Chat RMI Server is running...");
			Scanner scanner = new Scanner(System.in);
        	while (true) {
            	String message = scanner.nextLine();
            	myServerInt.sendMessageToClient(message);
        	}
        } catch (RemoteException | MalformedURLException e) {
            e.printStackTrace();
        }
    }

	@Override
    public void sendMessageToServer(String message) throws RemoteException {
        System.out.println("Client: " + message);
    }

    @Override
    public void sendMessageToClient(String message) throws RemoteException {
        for (Chatter chatter : chatters) {
            chatter.client.messageFromServer(message);
        }
    }	

	@Override
	public void registerListener(String name, ChatI client) throws RemoteException {	
		try{
			chatters.addElement(new Chatter(name, client));	
		}
		catch(Exception e){
			e.printStackTrace();
		}
	}
}
