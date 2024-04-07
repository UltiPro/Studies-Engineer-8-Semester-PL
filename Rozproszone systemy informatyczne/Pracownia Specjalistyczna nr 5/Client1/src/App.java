import java.net.MalformedURLException;
import java.net.URL;
import java.util.Collections;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import javax.xml.namespace.QName;
import javax.xml.ws.BindingProvider;
import javax.xml.ws.Service;
import javax.xml.ws.handler.MessageContext;

import webservices.Errorek;
import webservices.HelloWorld;

public class App {
    private static final String WS_URL = "http://DESKTOP-8BUGJP2:8080/Pracownia_Specjalistyczna_nr_4/HelloWorldImplService?wsdl";

    public static void main(String[] args) throws MalformedURLException, Errorek {
        URL url = new URL(WS_URL);
        QName qname = new QName("http://webservices/", "HelloWorldImplService");

        Service service = Service.create(url, qname);
        HelloWorld hello = service.getPort(HelloWorld.class);

        Map<String, Object> req_ctx = ((BindingProvider)hello).getRequestContext();
        req_ctx.put(BindingProvider.ENDPOINT_ADDRESS_PROPERTY, WS_URL);

        Map<String, List<String>> headers = new HashMap<>();
        headers.put("Username", Collections.singletonList("admin"));
        headers.put("Password", Collections.singletonList("admin"));
        req_ctx.put(MessageContext.HTTP_REQUEST_HEADERS, headers);
        
        System.out.println(hello.getHelloWorldAsString());
        try{
            System.out.println(hello.getErrorOrNot(true));
        }
        catch(Errorek e){
            System.out.println(e.getMessage());
        };
        
    }
}
