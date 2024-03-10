import java.rmi.RemoteException;
import java.rmi.server.UnicastRemoteObject;
import java.util.ArrayList;
import java.util.List;
import java.util.Scanner;

public class App extends UnicastRemoteObject implements ChatServerInterface {
    private List<ChatClientInterface> clients;

    public App() throws RemoteException {
        super();
        clients = new ArrayList<>();
    }

    @Override
    public void sendMessageToServer(String message) throws RemoteException {
        System.out.println("Client: " + message);
    }

    @Override
    public void sendMessageToClient(String message) throws RemoteException {
        for (ChatClientInterface client : clients) {
            client.receiveMessage(message);
        }
    }

    public void registerClient(ChatClientInterface client) throws RemoteException {
        System.out.println(client);
        clients.add(client);
    }

    public static void main(String[] args) {
        try {
            System.setProperty( "java.rmi.server.hostname", "localhost" );
            System.setProperty("java.security.policy", "security.policy");
            if (System.getSecurityManager() == null) {
                    System.setSecurityManager(new SecurityManager());
                }
            App server = new App();
            java.rmi.registry.LocateRegistry.createRegistry(1099);
            String url = "//localhost/ChatServer";
            System.out.println(url);
            java.rmi.Naming.rebind(url, server);
            System.out.println("Server started...");
            Scanner scanner = new Scanner(System.in);
            while (true) {
                String message = scanner.nextLine();
                server.sendMessageToClient(message);
            }
        } catch (Exception e) {
            e.printStackTrace();
        }
    }
}
