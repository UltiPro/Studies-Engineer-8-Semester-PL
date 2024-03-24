package webservices;
import java.util.List;
import javax.jws.WebMethod;
import javax.jws.WebService;
import javax.jws.soap.SOAPBinding;
import javax.jws.soap.SOAPBinding.Style;
import javax.jws.soap.SOAPBinding.Use;

@WebService(targetNamespace = "testowyjakisNamespace")
@SOAPBinding(style = Style.DOCUMENT, use = Use.LITERAL)
public interface HelloWorld {
    @WebMethod(action = "POST")
    String getHelloWorldAsString(String name);
    
    @WebMethod
    List<Product> getProducts();
}