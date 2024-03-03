import java.rmi.Remote;
import java.rmi.RemoteException;

public interface MyServerInt extends Remote {
    String getDescription(String text) throws RemoteException;

    double Addition(double a, double b) throws RemoteException;

    double Subtraction(double a, double b) throws RemoteException;

    double Multiplication(double a, double b) throws RemoteException;

    double Division(double a, double b) throws RemoteException;
}
