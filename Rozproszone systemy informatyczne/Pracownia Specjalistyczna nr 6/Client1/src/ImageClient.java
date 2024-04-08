import java.awt.Image;
import java.io.File;
import java.net.URL;
import javax.imageio.ImageIO;
import javax.swing.ImageIcon;
import javax.swing.JFrame;
import javax.swing.JLabel;
import javax.xml.namespace.QName;
import javax.xml.ws.BindingProvider;
import javax.xml.ws.Service;
import javax.xml.ws.soap.MTOMFeature;
import javax.xml.ws.soap.SOAPBinding;

import webservices.HelloWorld;

public class ImageClient{
	
	public static void main(String[] args) throws Exception {
	    URL url = new URL("http://desktop-8bugjp2:8080/Pracownia_Specjalistyczna_nr_4/HelloWorldImplService?wsdl");
        QName qname = new QName("http://webservices/", "HelloWorldImplService");

        Service service = Service.create(url, qname);
        HelloWorld imageServer = service.getPort(HelloWorld.class);
 
        /************  test download  ***************/
        Image image = imageServer.getImage("rss.png");
        
        //display it in frame
        JFrame frame = new JFrame();
        frame.setSize(300, 300);
        JLabel label = new JLabel(new ImageIcon(image));
        frame.add(label);
        frame.setVisible(true);

        System.out.println("imageServer.downloadImage() : Download Successful!");

    }

}