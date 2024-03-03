import java.rmi.RemoteException;
import java.rmi.server.UnicastRemoteObject;

public class MyServerImpl extends UnicastRemoteObject implements MyServerInt {
    int i = 0;

    protected MyServerImpl() throws RemoteException {
        super();
    }

    @Override
    public String getDescription(String text) throws RemoteException {
        i++;
        System.out.println("MyServerImpl.getDescription: " + text + " " + i);
        return "getDescription: " + text + " " + i;
    }

    public double Addition(double a, double b) {
        return a + b;
    }

    public double Subtraction(double a, double b) {
        return a - b;
    }

    public double Multiplication(double a, double b) {
        return a * b;
    }

    public double Division(double a, double b) {
        if (b == 0)
            return 0;
        return a / b;
    }
}
