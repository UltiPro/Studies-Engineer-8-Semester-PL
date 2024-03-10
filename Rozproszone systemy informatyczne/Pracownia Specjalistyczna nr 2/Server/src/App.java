import java.net.MalformedURLException;
import java.rmi.Naming;
import java.rmi.RemoteException;
import java.rmi.registry.LocateRegistry;

public class App {
    public static void main(String[] args) throws Exception {
        try {
            System.setProperty("java.security.policy", "security.policy");
            if (System.getSecurityManager() == null) {
                System.setSecurityManager(new SecurityManager());
            }
            // System.setProperty("java.rmi.server.codebase","file:/C:/Users/Jacek/workspace/RMIServer/bin/");
            System.setProperty("java.rmi.server.hostname", "localhost");
            System.setProperty("java.rmi.server.codebase",
                    "D:\\3. PROJEKTY\\Studies-Engineer-8-Semester-PL\\Rozproszone systemy informatyczne\\Pracownia Specjalistyczna nr 1\\Server\\bin");
            System.setProperty("java.rmi.server.codebase", "http://localhost/ulti/");
            System.out.println("Codebase: " + System.getProperty("java.rmi.server.codebase"));
            LocateRegistry.createRegistry(1099);
            MyServerImpl obj1 = new MyServerImpl();
            Naming.rebind("//localhost/ABC", obj1);
            System.out.println("Serwer oczekuje ...");
        } catch (RemoteException | MalformedURLException e) {
            e.printStackTrace();
        }
    }
}
