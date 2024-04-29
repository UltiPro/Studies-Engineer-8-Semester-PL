from zeep import Client
from zeep.transports import Transport
import requests

session = requests.Session()
session.verify = "./cert.crt"

client = Client(
    wsdl="https://localhost:7107/Motor.wsdl", transport=Transport(session=session)
)

# print(client.service.Create({"Name" : "test", "RentPrice": 1024}))
# print(client.service.Remove(7))
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
print(client.service.Rent(3, 4)) 
print(client.service.Detail(3))
"""

print(client.service.GeneratePDF(1))