import webservices2.*;

public class WebServiceClient {
    public static void main(String[] args) {
        HelloWorldImplService helloworld = new HelloWorldImplService();
        HelloWorld hello = helloworld.getHelloWorldImplPort();

        String zapytanie = "To ja - KLIENT";
        String response = hello.getHelloWorldAsString(zapytanie);
        System.out.println("Klient wysłał:" + zapytanie);
        System.out.println("Klient otrzymał:" + response);
    }
}
