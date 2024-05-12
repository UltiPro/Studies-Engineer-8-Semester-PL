from zeep import Client
from zeep.transports import Transport
import requests

session = requests.Session()
session.verify = "./cert.crt"

client = Client(
    wsdl="https://localhost:7107/Motor.wsdl", transport=Transport(session=session)
)

try:
    # print(client.service.Create({"Name" : "test", "RentPrice": 1024}))
    # print(client.service.Remove(5))
    # print(client.service.Update({"Id": 5,"Name": "Jakiś motorek", "RentPrice": 2000}))
    # print(client.service.Detail(2))
    # print(client.service.GetAll())

    """
    print(client.service.Reserve(3)) 
    print(client.service.Detail(3))
    """

    """
    print(client.service.CancelReserve(3)) 
    print(client.service.Detail(3))
    """

    """
    print(client.service.Rent(1, 4)) 
    print(client.service.Detail(3))
    """
 
    """
    print(client.service.GetSelected("da"))
    """
    
    '''
    response = client.service.GeneratePDF(1)

    file_path = './file.pdf'
    with open(file_path, 'wb') as file:
        file.write(response)

    print(f'Plik zapisano jako: {file_path}')
    '''

except Exception as e:
    print(e)


    # INFO DLA MICHAŁKA jak jest error to client zwraca jakiś komunikat 
    # jeśli się wykonało dobrze to zwraca None możesz sobie powyższymi przetestować
    # wyjatki to detail, getall i generatePDF wiadomo one akurat muszą coś zwrócić es

    # a i aby twój kod był w try expect by przechwytywać te błędy i je printować a nie mieć w konsoli milion błędów
    # w razie co to dzwoń/pisz
    # i ten tego pamiętaj mieć dobry certyfikat

    # komenda do jego generowania w razie w
    # dotnet dev-certs https -ep ./cert.crt -np --trust --format Pem

    # uprzedzając pytania tak uważam cię za upośledzonego
