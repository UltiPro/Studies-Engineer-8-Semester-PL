from zeep import Client

client = Client(wsdl='http://DESKTOP-8BUGJP2:8080/Pracownia_Specjalistyczna_nr_4/HelloWorldImplService?WSDL')

print(client.service.getProducts())