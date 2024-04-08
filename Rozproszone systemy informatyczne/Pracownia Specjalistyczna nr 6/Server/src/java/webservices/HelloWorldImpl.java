package webservices;

import java.awt.Image;
import java.io.File;
import java.io.IOException;

import javax.imageio.ImageIO;
import javax.jws.WebService;
import javax.xml.ws.WebServiceException;
import javax.xml.ws.soap.MTOM;

@MTOM
@WebService
public class HelloWorldImpl implements HelloWorld {
    
    @Override
    public String getHelloWorldAsString(String text) {
        return String.format("Hello %s", text);
    }
    
    @Override
    public java.awt.Image getImage(String name) {
        try {
            File file = new File(System.getProperty("user.home", ""), "/workspace/" + name);
            System.out.println("Reading file: " + file.getAbsolutePath());
            return ImageIO.read(file);
        } catch (IOException e) {
            e.printStackTrace();
            return new java.awt.image.BufferedImage(1, 1, java.awt.image.BufferedImage.TYPE_INT_RGB);
        }
    }
    
    @Override
    public String uploadImage(Image data) {
        if(data!=null){
            //store somewhere
            return "Upload Successful";
        }

        throw new WebServiceException("Upload Failed!");
    }
}