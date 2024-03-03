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
            Boolean calualtor = true;
            Scanner myInput = new Scanner( System.in );
            while (calualtor) {
                System.out.println("First Number:");
                int fn = myInput.nextInt();
                System.out.println("Second Number:");
                int sn = myInput.nextInt();
                PrintMenu();
                switch (myInput.nextInt()) {
                    case 1:
                        System.out.println(myRemoteObject.Addition(fn, sn));
                        break;
                    case 2:
                        System.out.println(myRemoteObject.Subtraction(fn, sn));
                        break;
                    case 3:
                        System.out.println(myRemoteObject.Multiplication(fn, sn));
                        break;
                    case 4:
                        System.out.println(myRemoteObject.Division(fn, sn));
                        break;
                    case 5:
                        calualtor = false;
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
        System.out.println("1. Addition");
        System.out.println("2. Subtraction");
        System.out.println("3. Multiplication");
        System.out.println("4. Division");
        System.out.println("5. Exit");
    }
}
