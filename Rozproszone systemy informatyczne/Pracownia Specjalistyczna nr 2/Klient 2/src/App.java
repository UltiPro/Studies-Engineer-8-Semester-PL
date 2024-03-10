import java.rmi.Naming;
import java.util.Scanner;

public class App {
    public static void main(String[] args) throws Exception {
        try {
            System.setProperty("java.security.policy", "security.policy");
            ChatServerInterface server = (ChatServerInterface) Naming.lookup("rmi://localhost/ChatServer");
            ChatClientInterface client = new ChatClientImpl(server);
            server.registerClient(client);
            Scanner scanner = new Scanner(System.in);
            while (true) {
                String message = scanner.nextLine();
                server.sendMessageToServer(message);
            }
        } catch (Exception e) {
            e.printStackTrace();
        }
    }
}
