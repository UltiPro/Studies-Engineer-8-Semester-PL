package webservices;

import java.awt.Image;
import javax.jws.WebMethod;
import javax.jws.WebService;
import javax.jws.soap.SOAPBinding;
import javax.jws.soap.SOAPBinding.Style;

@WebService
@SOAPBinding(style = Style.RPC)
public interface HelloWorld {
    @WebMethod
    String getHelloWorldAsString(String text);
    
    @WebMethod
    public Image getImage(String name);
    
    @WebMethod 
    public String uploadImage(Image data);
}