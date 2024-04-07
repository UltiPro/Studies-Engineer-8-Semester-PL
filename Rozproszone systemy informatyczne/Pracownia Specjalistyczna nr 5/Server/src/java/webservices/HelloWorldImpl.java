package webservices;
import javax.jws.WebService;
import java.util.List;
import java.util.ArrayList;
import javax.annotation.Resource;
import javax.xml.ws.WebServiceContext;
import javax.xml.ws.handler.MessageContext;
import java.util.Map;

@WebService(endpointInterface = "webservices.HelloWorld")
public class HelloWorldImpl implements HelloWorld {
    @Resource
    WebServiceContext wsc;
    
    @Override
    public String getHelloWorldAsString() {
        MessageContext mc = wsc.getMessageContext();
        
        Map http_headers = (Map) mc.get(MessageContext.HTTP_REQUEST_HEADERS);
        List userList = (List) http_headers.get("Username");
        List passList = (List) http_headers.get("Password");
        
        String username = "";
        String password = "";
        
        if(userList != null) username = userList.get(0).toString();
        if(passList != null) password = passList.get(0).toString();
        
        if(username.equals("admin") && password.equals("admin")) {
            return "Witaj świecie JAX-WS: " + username;
        }
        else {
            return "Nieznany użytkownik.";
        }
    }
    
    @Override
    public String getErrorOrNot(boolean error) throws Errorek
    {
        if(!error) return "Good mordko działa";
        else throw new Errorek("To ma być błąd i jest super");
    }
}