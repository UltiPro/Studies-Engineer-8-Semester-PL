package webservices;
import javax.jws.WebService;

@WebService(endpointInterface = "webservices.HelloWorld")
public class HelloWorldImpl implements HelloWorld {
    @Override
    public String getHelloWorldAsString(String name) {
        return "Witaj świecie JAX-WS: " + name;
    }
}