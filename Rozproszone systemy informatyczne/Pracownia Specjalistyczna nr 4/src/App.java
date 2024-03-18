import javax.xml.ws.Endpoint;

import webservices.HelloWorldImpl;

public class App {
    public static void main(String[] args) throws Exception {
        Endpoint.publish("http://localhost:9999/ws/hello", new HelloWorldImpl());
        System.out.println("Oczekiwanie na klienta...");
    }
}
