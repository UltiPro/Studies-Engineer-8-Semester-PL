import java.net.MalformedURLException;
import java.net.URL;

import javax.xml.namespace.QName;
import javax.xml.ws.Service;

import webservices2.*;

public class Client2 {
        public static void main(String[] args) throws MalformedURLException {
            URL url = new URL("http://localhost:8080/ws/hello?wsdl");
            QName qName = new QName("http://webservices/", "HelloWorldImplService");

            Service service = Service.create(url, qName);
            HelloWorld hello = service.getPort(HelloWorld.class);

            String zapytanie = "To ja - KLIENT";
            String response = hello.getHelloWorldAsString(zapytanie);
            System.out.println("Klient wysłał:" + zapytanie);
            System.out.println("Klient otrzymał:" + response);
    }
}
