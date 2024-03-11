import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.rmi.Naming;
import java.util.Scanner;

public class App {
    public static void main(String[] args) {
        System.setProperty("java.security.policy", "security.policy");
        System.setSecurityManager(new SecurityManager());
        try {
            MyServerInt myRemoteObject = (MyServerInt) Naming.lookup("//localhost/ABC");
            String text = "Hallo :-)";
            String result = myRemoteObject.getDescription(text);
            System.out.println("Wysłano do servera: " + text);
            System.out.println("Otrzymana z serwera odpowiedź: " + result);
            Boolean program = true;
            Scanner myInput = new Scanner(System.in);
            BufferedReader reader = new BufferedReader(new InputStreamReader(System.in));
            while (program) {
                PrintMenu();
                switch (myInput.nextInt()) {
                    case 1:
                        System.out.println(myRemoteObject.GetProducts());
                        break;
                    case 2:
                        System.out.println(myRemoteObject.GetProduct(reader.readLine()));
                        break;
                    case 5:
                        program = false;
                        break;
                    default:
                        break;
                }
            }
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    private static void PrintMenu() {
        System.out.println("1. Products");
        System.out.println("2. Find Product");
        System.out.println("5. Exit");
    }
}
