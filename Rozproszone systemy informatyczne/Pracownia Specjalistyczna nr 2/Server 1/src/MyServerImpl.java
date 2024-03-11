import java.rmi.RemoteException;
import java.rmi.server.UnicastRemoteObject;
import java.util.*;

public class MyServerImpl extends UnicastRemoteObject implements MyServerInt {
    int i = 0;

    public ArrayList<Product> products = new ArrayList<Product>();
    public ArrayList<String> chat = new ArrayList<String>();

    protected MyServerImpl() throws RemoteException {
        super();
        products.add(new Product(0,"Chleb"));
        products.add(new Product(1,"Masło"));
        products.add(new Product(2,"Jajko"));
    }

    @Override
    public String getDescription(String text) throws RemoteException {
        i++;
        System.out.println("MyServerImpl.getDescription: " + text + " " + i);
        return "getDescription: " + text + " " + i;
    }
    
    public String GetProducts() {
        String s = "";
        for(Product product : products){
            s += product.id+" "+product.name+"\n";
        }
        return s;
    }

    public String GetProduct(String name) {
        for(Product product : products){
            if(product.name.equals(name)) return product.id+" "+product.name;
        }
        return null;
    }
}
