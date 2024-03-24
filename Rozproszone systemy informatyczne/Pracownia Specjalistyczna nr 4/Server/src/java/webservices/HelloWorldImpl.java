package webservices;
import javax.jws.WebService;
import java.util.List;
import java.util.ArrayList;

@WebService(endpointInterface = "webservices.HelloWorld")
public class HelloWorldImpl implements HelloWorld {
    @Override
    public String getHelloWorldAsString(String name) {
        return "Witaj świecie JAX-WS: " + name;
    }
    
    @Override
    public List<Product> getProducts() {
        List<Product> listaProduktow = new ArrayList<>();
        listaProduktow.add(new Product("P1", "Opis 1", 100));
        listaProduktow.add(new Product("P2", "Opis 2", 222));
        listaProduktow.add(new Product("P3", "Opis 3", 331));
        return listaProduktow;
    }
}