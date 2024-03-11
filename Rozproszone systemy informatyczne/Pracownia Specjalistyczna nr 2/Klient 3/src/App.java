import java.rmi.Naming;
import java.util.Scanner;

public class App {
    public static void main(String[] args) {
        System.setProperty("java.security.policy", "security.policy");
        System.setSecurityManager(new SecurityManager());
        try {
            MyServerInt myRemoteObject = (MyServerInt) Naming.lookup("//localhost/ABC");
            Scanner myInput = new Scanner(System.in);
            while (true) {
                System.out.print(myRemoteObject.Play(myInput.nextInt()));
            }
        } catch (Exception e) {
            e.printStackTrace();
        }
    }
}
